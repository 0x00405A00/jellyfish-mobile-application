using Infrastructure.Extension;
using Infrastructure.Handler.AppConfig;
using Presentation.ApplicationSpecific;
using Presentation.Service;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, ApplicationConfigHandler applicationConfigHandler)
        {
            services.AddSingleton<NavigationService>();
            services.AddDeviceHandlers(applicationConfigHandler);
            services.AddViewModels();
            services.AddPages();
            return services;

        }
    }
}
