
using FinancialPlanner.Core.Domain.Enums;

namespace FinancialPlanner.Core.Domain.Entities;

public sealed class Transaction : EntityBase
{
    public string Description { get; set; } = string.Empty;
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime? StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
    public TransactionType Type { get; set; }
    public TransactionStatus TransactionStatus { get; set; }
    public Account Account { get; set; } = null!;
    public string? Comment { get; set; }
    public ICollection<Tag> Tags { get; set; } = [];
}
