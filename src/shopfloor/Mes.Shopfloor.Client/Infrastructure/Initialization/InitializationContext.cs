namespace Mes.Shopfloor.Client.Infrastructure.Initialization;

public sealed class InitializationContext
{
    private readonly List<InitializationIssue> _issues = [];

    public IReadOnlyList<InitializationIssue> Issues => _issues;

    public void ReportIssue(InitializationIssueSeverity severity, string message)
    {
        _issues.Add(new InitializationIssue(severity, message));
    }
}