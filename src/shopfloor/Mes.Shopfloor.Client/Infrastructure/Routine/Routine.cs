using Mes.Shopfloor.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mes.Shopfloor.Client.Infrastructure.Routine;

internal sealed partial class Routine(
    IRoutineContext _routineContext,
    ILogger<Routine> _logger,
    IOptions<RoutineOptions> _options,
    IServiceProvider _serviceProvider) : IRoutine
{
    private List<IRoutineJob>? _jobs;

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var jobs = GetJobs();
        var iterationInterval = TimeSpan.FromMilliseconds(_options.Value.IntervalMs);

        while (!cancellationToken.IsCancellationRequested)
        {
            _routineContext.CurrentIterationStartedAt = DateTime.UtcNow;

            foreach (var job in jobs)
            {
                using var jobTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                if (job.Timeout.HasValue)
                    jobTokenSource.CancelAfter(job.Timeout.Value);

                try
                {
                    await job.ExecuteAsync(_routineContext, jobTokenSource.Token);
                    job.Synchronize(_routineContext);
                }
                catch (OperationCanceledException) when (jobTokenSource.IsCancellationRequested)
                {
                    _logger.LogWarning("Job '{jobName}' timed out after {timeout}.", job.GetType().Name, job.Timeout);
                }
                catch (RequiredRoutineDataMissingException ex)
                {
                    LogJobJobnameIsMissingRequiredRoutineData(job.GetType().Name, ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Job '{jobName}' threw an exception.", job.GetType().Name);
                }
            }

            _routineContext.LastIterationCompletedAt = DateTime.UtcNow;

            var iterationDuration = _routineContext.LastIterationCompletedAt - _routineContext.CurrentIterationStartedAt;
            var remainingDuration = iterationInterval - iterationDuration;
            var delayedMs = remainingDuration.Milliseconds <= 0 ? 0 : remainingDuration.Milliseconds;

            await Task.Delay(delayedMs, cancellationToken);

            LogRoutineIterationTookIterationDurationWithRemainingDurationLeft(iterationDuration, remainingDuration);
        }
    }

    private List<IRoutineJob> GetJobs()
    {
        return _jobs ??= _serviceProvider.GetServices<IRoutineJob>().OrderBy(j => j.Order).ToList();
    }

    [LoggerMessage(LogLevel.Critical, "Job '{jobName}' is missing required routine data.")]
    partial void LogJobJobnameIsMissingRequiredRoutineData(string jobName, RequiredRoutineDataMissingException requiredRoutineDataMissingException);

    [LoggerMessage(LogLevel.Information, "Routine iteration took {IterationDuration} with {RemainingDuration} left.")]
    partial void LogRoutineIterationTookIterationDurationWithRemainingDurationLeft(TimeSpan iterationDuration, TimeSpan remainingDuration);
}