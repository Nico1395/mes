namespace Mes.Shopfloor.Client.Infrastructure.Routine;

public static class RoutineJobOrderExtensions
{
    public static int ToInt(this RoutineJobOrder order)
    {
        return (int)order;
    }
}