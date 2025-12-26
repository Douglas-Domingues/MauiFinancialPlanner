using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialPlanner.Core.Domain.Entities;

public sealed class Tag : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string HexColor { get; set; } = string.Empty;

    public ICollection<Transaction> Transactions { get; set; } = [];
}
