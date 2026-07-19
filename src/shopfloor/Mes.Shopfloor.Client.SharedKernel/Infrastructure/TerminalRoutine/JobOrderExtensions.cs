namespace Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalRoutine;

public static class JobOrderExtensions
{
    public static int ToInt(this JobOrder order)
    {
        return (int)order;
    }
}