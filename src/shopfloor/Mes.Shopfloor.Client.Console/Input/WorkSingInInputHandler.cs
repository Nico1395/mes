using Mes.Shopfloor.Client.ProductionManagement;
using Mes.Shopfloor.Client.SharedKernel.Infrastructure.Input;

namespace Mes.Shopfloor.Client.Console.Input;

internal sealed class WorkSingInInputHandler : IInputHandler<WorkerSignInInputRequest, string>
{
    public string? RequestInput(WorkerSignInInputRequest request)
    {
        try
        {
            while (true)
            {
                System.Console.Write("\nSign in to the terminal by typing your employee number: ");

                var employeeNumber = System.Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(employeeNumber))
                    return employeeNumber;

                System.Console.WriteLine("Invalid input, try again.");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            throw;
        }
    }
}