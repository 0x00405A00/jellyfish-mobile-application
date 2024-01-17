using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extension.WebAppBuilderExtensions
{
    public static class MigrationExtensions
    {
        public static IServiceCollection AppendMigrations(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            using ApplicationDbContext applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            if (applicationDbContext.Database.EnsureCreated())
            {
                applicationDbContext.Database.Migrate();
            }
            return services;
        }
    }
}
