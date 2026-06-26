namespace Mes.Shopfloor.Client.Infrastructure.Initialization;

public sealed record InitializationIssue(InitializationIssueSeverity Severity, string Message);