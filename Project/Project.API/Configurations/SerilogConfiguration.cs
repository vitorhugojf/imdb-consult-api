using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Project.API.Configurations
{
    /// <summary>
    /// Configuração do Serilog.
    /// </summary>
    public static class SerilogConfiguration
    {
        public static void RegisterSerilog(this IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Seq("http://localhost:5341", compact: true)
                .WriteTo.Console() //restrictedToMinimumLevel:
                .WriteTo.File(new CompactJsonFormatter(), "Logs/log-.json", rollingInterval: RollingInterval.Hour)
                .CreateLogger();
        }
    }
}
