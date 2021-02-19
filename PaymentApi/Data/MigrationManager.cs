using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace PaymentApi.Data
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var db = services.GetRequiredService<PaymentsDbContext>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    //var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                    //logger.Error(ex, "An error occurred while migrating the database.");
                }
            }

            return webHost;
        }
    }
}
