namespace Mes.Shopfloor.Client.Infrastructure.Routine;

public sealed class RequiredRoutineDataMissingException(RoutineDataKey key) : Exception($"Required routine data for key '{key}' is missing.")
{
}