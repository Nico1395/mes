namespace Mes.Shopfloor.Client.Infrastructure.TerminalInitialization;

public sealed class TerminalInitializationContext
{
    private readonly List<TerminalInitializationIssue> _issues = [];

    public IReadOnlyList<TerminalInitializationIssue> Issues => _issues;

    public void ReportIssue(TerminalInitializationIssueSeverity severity, string message)
    {
        _issues.Add(new TerminalInitializationIssue(severity, message));
    }
}