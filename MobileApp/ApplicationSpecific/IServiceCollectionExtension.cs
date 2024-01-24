using Microsoft.Extensions.Configuration;
using Presentation.View;
using Presentation.ViewModel;

namespace Presentation.ApplicationSpecific
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<ChatsPageViewModel>();
            services.AddSingleton<StatusPageViewModel>();
            services.AddSingleton<CallsPageViewModel>();
            services.AddSingleton<MainPageViewModel>();
            services.AddSingleton<LoginPageViewModel>();
            services.AddSingleton<ResetPasswordContentPageViewModel>();
            services.AddTransient<ChatPageViewModel>();
            services.AddSingleton<UserSelectionPageViewModel>();
            services.AddSingleton<ProfilePageViewModel>();
            services.AddSingleton<CameraHandlerPageViewModel>();
            services.AddSingleton<RegisterContentPageViewModel>();
            services.AddSingleton<SettingsPageViewModel>();

            services.AddSingleton<PageInstanceFactory>();
            return services;
        }
        public static string GetDatabasePath()
        {
            string dbPath = null;
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                dbPath = Global.DatabasePath;
                
            }
            if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                dbPath = Global.DatabasePath;
            }
            return dbPath;
        }
        public static MauiAppBuilder AddIConfiguration(this MauiAppBuilder builder)
        {

            using var stream = AssemblyReference.Assembly.GetManifestResourceStream("Presentation.appsettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            var section = config.GetSection("ConnectionStrings");
            string connStr = $"Filename={GetDatabasePath()}";
            section["JellyfishSqlLiteDatabase"] = connStr;
            builder.Configuration.AddConfiguration(config);
            return builder;
        }
        

        public static IServiceCollection AddPages(this IServiceCollection services)
        {

            return services;
        }
    }

}
