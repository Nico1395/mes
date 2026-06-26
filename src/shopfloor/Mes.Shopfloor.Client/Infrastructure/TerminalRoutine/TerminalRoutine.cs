using Mes.Shopfloor.Client.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mes.Shopfloor.Client.Infrastructure.TerminalRoutine;

internal sealed partial class TerminalRoutine(
    ITerminalRoutineContext terminalRoutineContext,
    ILogger<TerminalRoutine> _logger,
    IOptions<RoutineOptions> _options,
    IServiceProvider _serviceProvider) : ITerminalRoutine
{
    private List<ITerminalRoutineJob>? _jobs;

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var jobs = GetJobs();
        var iterationInterval = TimeSpan.FromMilliseconds(_options.Value.IntervalMs);

        while (!cancellationToken.IsCancellationRequested)
        {
            terminalRoutineContext.CurrentIterationStartedAt = DateTime.UtcNow;

            foreach (var job in jobs)
            {
                using var jobTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                if (job.Timeout.HasValue)
                    jobTokenSource.CancelAfter(job.Timeout.Value);

                try
                {
                    await job.ExecuteAsync(terminalRoutineContext, jobTokenSource.Token);
                    job.Synchronize(terminalRoutineContext);
                }
                catch (OperationCanceledException) when (jobTokenSource.IsCancellationRequested)
                {
                    _logger.LogWarning("Job '{jobName}' timed out after {timeout}.", job.GetType().Name, job.Timeout);
                }
                catch (RequiredTerminalRoutineDataMissingException ex)
                {
                    LogJobJobnameIsMissingRequiredRoutineData(job.GetType().Name, ex);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Job '{jobName}' threw an exception.", job.GetType().Name);
                }
            }

            terminalRoutineContext.LastIterationCompletedAt = DateTime.UtcNow;

            var iterationDuration = terminalRoutineContext.LastIterationCompletedAt - terminalRoutineContext.CurrentIterationStartedAt;
            var remainingDuration = iterationInterval - iterationDuration;
            var delayedMs = remainingDuration.Milliseconds <= 0 ? 0 : remainingDuration.Milliseconds;

            await Task.Delay(delayedMs, cancellationToken);

            LogRoutineIterationTookIterationDurationWithRemainingDurationLeft(iterationDuration, remainingDuration);
        }
    }

    private List<ITerminalRoutineJob> GetJobs()
    {
        return _jobs ??= _serviceProvider.GetServices<ITerminalRoutineJob>().OrderBy(j => j.Order).ToList();
    }

    [LoggerMessage(LogLevel.Critical, "Job '{jobName}' is missing required routine data.")]
    partial void LogJobJobnameIsMissingRequiredRoutineData(string jobName, RequiredTerminalRoutineDataMissingException requiredTerminalRoutineDataMissingException);

    [LoggerMessage(LogLevel.Information, "TerminalRoutine iteration took {IterationDuration} with {RemainingDuration} left.")]
    partial void LogRoutineIterationTookIterationDurationWithRemainingDurationLeft(TimeSpan iterationDuration, TimeSpan remainingDuration);
}