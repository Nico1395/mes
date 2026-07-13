namespace Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Durational;

public interface IEnded
{
    DateTime? EndedAt { get; set; }
}