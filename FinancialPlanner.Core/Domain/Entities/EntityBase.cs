using FinancialPlanner.Core.Domain.Enums;

namespace FinancialPlanner.Core.Domain.Entities;

public abstract class EntityBase
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public EntityStatus Status { get; set; }
}
