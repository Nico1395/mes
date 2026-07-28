namespace Mes.Shared.Contracts.SharedKernel.Abstractions.Durational;

public interface IEnded
{
    DateTime? EndedAt { get; set; }
}