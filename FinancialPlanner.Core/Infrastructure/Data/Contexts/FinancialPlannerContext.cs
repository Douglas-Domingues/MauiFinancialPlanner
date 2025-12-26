
using Microsoft.EntityFrameworkCore;
using FinancialPlanner.Core.Domain.Entities;

namespace FinancialPlanner.Core.Infrastructure.Data.Contexts;

public sealed class FinancialPlannerContext : DbContext
{
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Tag> Tags { get; set; }

    public FinancialPlannerContext(DbContextOptions<FinancialPlannerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureEntityBase(modelBuilder);
        ConfigureBankEntity(modelBuilder);
        ConfigureAccountEntity(modelBuilder);
        ConfigureTransactionEntity(modelBuilder);
        ConfigureTagEntity(modelBuilder);
    }

    private void ConfigureEntityBase(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
            .Where(t => typeof(EntityBase).IsAssignableFrom(t.ClrType)))
        {
            // Id como auto-increment
            modelBuilder.Entity(entityType.ClrType)
                .HasKey(nameof(EntityBase.Id));

            // CreatedAt com valor padrão no banco  
            modelBuilder.Entity(entityType.ClrType)
                .Property(nameof(EntityBase.CreatedAt))
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Status como string no banco  
            modelBuilder.Entity(entityType.ClrType)
                .Property(nameof(EntityBase.Status))
                .HasConversion<string>()
                .HasMaxLength(20);
        }
    }

    private void ConfigureBankEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bank>(entity =>
        {
            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(e => e.FebrabanCode)
                  .IsRequired();

            entity.HasIndex(e => e.FebrabanCode)
                  .IsUnique();

            entity.HasMany(e => e.Accounts)
                  .WithOne(a => a.Bank)
                  .HasForeignKey(a => a.BankId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureAccountEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(75);

            entity.Property(entity => entity.Type)
                  .IsRequired()
                  .HasConversion<string>()
                  .HasMaxLength(20);

            entity.Property(e => e.AccountNumber)
                  .IsRequired(false)
                  .HasMaxLength(25);

            entity.HasIndex(e => new { e.BankId, e.AccountNumber })
                  .IsUnique()
                  .HasFilter("[AccountNumber] IS NOT NULL");

            entity.HasMany(e => e.Transactions)
                  .WithOne(t => t.Account)
                  .HasForeignKey(t => t.AccountId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureTransactionEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.Description)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.Property(e => e.Amount)
                  .IsRequired()
                  .HasPrecision(18, 2);

            entity.Property(e => e.Type)
                  .IsRequired()
                  .HasConversion<string>()
                  .HasMaxLength(25);

            entity.Property(e => e.TransactionStatus)
                  .IsRequired()
                  .HasConversion<string>()
                  .HasMaxLength(25);

            entity.Property(e => e.Comment)
                  .IsRequired(false)
                  .HasMaxLength(300);

            entity.Property(e => e.TransactionDate)
                  .IsRequired();

            entity.Property(e => e.DueDate)
                  .IsRequired(false);

            entity.Property(e => e.PaymentDate)
                  .IsRequired(false);

            entity.Property(e => e.StartsAt)
                  .IsRequired(false);

            entity.Property(e => e.EndsAt)
                  .IsRequired(false);

            entity.HasIndex(e => e.AccountId);
            entity.HasIndex(e => e.TransactionDate);

            entity.HasMany(e => e.Tags)
                  .WithMany(t => t.Transactions)
                  .UsingEntity(j => j.ToTable("TransactionTags"));
        });
    }

    private void ConfigureTagEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(e => e.HexColor)
                  .IsRequired()
                  .HasMaxLength(7);

            entity.HasIndex(e => e.Name)
                  .IsUnique();
        });
    }
}
