using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Extension;
using MobiInfrastructureleApp;
using Infrastructure.Abstractions;
using Shared.Infrastructure.Backend.Api;
using Infrastructure.Api;
using Microsoft.Extensions.Configuration;
using Shared.Infrastructure.Backend.SignalR;
using System;
using Shared.Infrastructure.Backend;
using Infrastructure.Storage;
using Infrastructure.Authentification;

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

            services.AddSingleton<ILocalStorageService, LocalStorageService>();
            services.AddSingleton<IWebApiRestClient, WebApiRestClient>();
            services.AddSingleton<IJellyfishBackendApi, JellyfishBackendApi>();
            services.AddSingleton<IJellyfishBackendApiDecorator,JellyfishBackendApiDecorator>();//Consumes JellyfishBackendApi, abstraction layer


            var sp = services.BuildServiceProvider();
            var configuration = sp.GetService<IConfiguration>();
            var infrastructureSignalRSection = configuration.GetSection("Infrastructure:SignalR");
            string baseUrl = infrastructureSignalRSection["SignalRHubBaseUrl"];
            int port = infrastructureSignalRSection.GetValue<int>("SignalRHubBaseUrlPort");
            string hub = infrastructureSignalRSection["SignalRHubEndpoint"];
            string transportProtocol = infrastructureSignalRSection["SignalRHubClientTransportProtocol"];
            services.AddSingleton<SignalRConnectionParams>(sp => new SignalRConnectionParams(baseUrl, port, hub, transportProtocol));
            services.AddSingleton<JellyfishSignalRClient>();
            services.AddSingleton<IAuthentificationService, AuthentificationService>();
            return services;

        }
    }
}
