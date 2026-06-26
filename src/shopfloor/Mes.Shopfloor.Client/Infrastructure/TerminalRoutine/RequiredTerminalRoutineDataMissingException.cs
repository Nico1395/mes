namespace Mes.Shopfloor.Client.Infrastructure.TerminalRoutine;

public sealed class RequiredTerminalRoutineDataMissingException(DataKey key) : Exception($"Required routine data for key '{key}' is missing.")
{
}