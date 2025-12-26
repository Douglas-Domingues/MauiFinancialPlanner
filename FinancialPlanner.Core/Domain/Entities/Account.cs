
using FinancialPlanner.Core.Domain.Enums;

namespace FinancialPlanner.Core.Domain.Entities;

public sealed class Account : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public AccountType Type { get; set; }
    public string? AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public int BankId { get; set; }
    public Bank Bank { get; set; } = null!;
    public ICollection<Transaction> Transactions { get; set; } = [];
}
