using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extension
{
    public static class MigrationExtensions
    {
        public static IServiceCollection AppendMigrations(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            using ApplicationDbContext applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            if (applicationDbContext.Database.EnsureCreated())
            {
                var openMigrations = applicationDbContext.Database.GetPendingMigrations();
                if(openMigrations.Any())
                {
                    applicationDbContext.Database.Migrate();
                }
            }
            return services;
        }
    }
}
