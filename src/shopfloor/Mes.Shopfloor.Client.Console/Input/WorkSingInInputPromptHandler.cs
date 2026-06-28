using Mes.Shopfloor.Client.Infrastructure.Input;
using Mes.Shopfloor.Client.ProductionManagement;

namespace Mes.Shopfloor.Client.Console.Input;

internal sealed class WorkSingInInputPromptHandler : IInputPromptHandler<WorkerSignInInputPrompt, string>
{
    public string? Prompt(WorkerSignInInputPrompt prompt)
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