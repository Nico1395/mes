namespace Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Timestamped;

public interface ICreated
{
    DateTime CreatedAt { get; set; }
}