using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FinancialPlanner.Core.Infrastructure.Data.Contexts;
using System.Reflection;

namespace FinancialPlanner.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            var assembly = Assembly.GetExecutingAssembly();
            using var stream =
                assembly.GetManifestResourceStream("FinancialPlanner.App.appsettings.json");

            if (stream == null)
            {
                throw new InvalidOperationException("Config file not found.");
            }

            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonStream(stream); // Corrija para usar o método de extensão
            var config = configBuilder.Build();

            builder.Services.AddDbContext<FinancialPlannerContext>(options =>
                options.UseMySql(
                    config.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(11, 8, 5))
                )
            );

            return builder.Build();
        }
    }
}
