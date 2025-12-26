using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinancialPlanner.Core.Infrastructure.Data.Contexts
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FinancialPlannerContext>
    {
        public FinancialPlannerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinancialPlannerContext>();

            var connectionString = "Server=localhost;Port=3311;Database=FinancialPlanner;User=root;Password=R00tPa$$;";

            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(11, 8, 5))
            );

            return new FinancialPlannerContext(optionsBuilder.Options);
        }
    }
}
