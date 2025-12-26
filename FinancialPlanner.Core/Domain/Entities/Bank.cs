namespace FinancialPlanner.Core.Domain.Entities;

public sealed class Bank : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public int FebrabanCode { get; set; }

    public ICollection<Account> Accounts { get; set; } = [];
}
