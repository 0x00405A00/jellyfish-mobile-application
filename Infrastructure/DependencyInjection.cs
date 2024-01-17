using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Extension;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AppendMigrations();
            var builder = services.BuildServiceProvider();
            var s = builder.GetRequiredService<ApplicationDbContext>();
            var users = s.Users.Where(x => x.DeletedTime == null).ToList();
            return services;

        }
    }
}
