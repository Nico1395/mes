namespace Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Timestamped;

public interface IUpdated
{
    DateTime UpdatedAt { get; set; }
}