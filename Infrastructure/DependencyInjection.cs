using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Extension;
using MobiInfrastructureleApp;
using Infrastructure.Abstractions;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AppendMigrations();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            /*var builder = services.BuildServiceProvider();
            var s = builder.GetRequiredService<ApplicationDbContext>();
            var users = s.Users.Where(x => x.DeletedTime == null).ToList();*/

            services.Scan(selector => selector
                .FromAssemblies(AssemblyReference.Assembly)
                .AddClasses(false)
                .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
                .AsImplementedInterfaces()//ClassName => IClassName
                .WithScopedLifetime());
            return services;

        }
    }
}
