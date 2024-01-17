using MobileApp.Handler.Data;
using MobileApp.ViewModel;
using MobileApp.Handler.Device.Vibrate;
using MobileApp.Handler.Device.Media.Camera;
using MobileApp.Handler.Device.Media.Contact;
using MobileApp.Handler.Device.Media.Communication;
using MobileApp.Handler.Device.Notification;
using MobileApp.Handler.Device.Filesystem;
using MobileApp.Handler.Device.Sensor;
using MobileApp.Handler.Device.ClipBoard;
using MobileApp.Handler.Device.Network;
using MobileApp.Handler.AppConfig;
using MobileApp.Handler.Data.InternalDataInterceptor.Invoker;
using MobileApp.Handler.Device.Media;
using CommunityToolkit.Maui;
using MobileApp.Handler.Data.InternalDataInterceptor;
using Shared.Infrastructure.Backend.Api;
using Shared.Infrastructure.Backend.SignalR;
using Shared.Infrastructure.Backend;
using MobileApp.Handler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

using System.Reflection;





#if ANDROID
using MobileApp.Handler.Data.InternalDataInterceptor.Invoker.Notification.Android;
#elif IOS
using MobileApp.Handler.Data.InternalDataInterceptor.Invoker.Notification.iOS;
#endif

namespace MobileApp.ApplicationSpecific
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

            using var stream = AssemblyReference.Assembly.GetManifestResourceStream("MobileApp.appsettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            var section = config.GetSection("ConnectionStrings");
            string connStr = $"Filename={GetDatabasePath()}";
            section["JellyfishSqlLiteDatabase"] = connStr;
            builder.Configuration.AddConfiguration(config);
            return builder;
        }
        public static IServiceCollection AddDeviceHandlers(this IServiceCollection services,ApplicationConfigHandler applicationHandler)
        {
            services.AddSingleton<ILocalStorageService, LocalStorageService>();
            services.AddSingleton<ICustomAuthentificationStateProvider, CustomAuthentificationStateProvider>();
            services.AddSingleton<ApplicationResourcesHandler>();
            services.AddSingleton<FileHandler>(new FileHandler(() => { }, () => { }));
            services.AddSingleton<VibrateHandler>(new VibrateHandler(() => { }, () => { }));
            services.AddSingleton<CameraHandler>(new CameraHandler(() => { }, () => { }));
            services.AddSingleton<DeviceContactHandler>();
            //services.AddSingleton<AbstractAudioPlayerHandler>();
            //services.AddSingleton<AbstractAudioRecorderHandler>();
            services.AddSingleton<DeviceCommunicationHandler>(new DeviceCommunicationHandler(() => { }, () => { }));
            services.AddSingleton<NotificationHandler>(new NotificationHandler(() => { }, () => { }));
            services.AddSingleton<GpsHandler>(new GpsHandler(() => { }, () => { }));
            services.AddSingleton<ClipBoardHandler>(new ClipBoardHandler());
            services.AddSingleton<NetworkingHandler>(new NetworkingHandler(() => { }, () => { }));

            services.AddSingleton<JellyfishBackendApi>();

            services.AddSingleton<JellyfishWebApiRestClientInvoker>();
            services.AddSingleton<ViewModelInvoker>();
            services.AddSingleton<SqlLiteDatabaseHandlerInvoker>();
            services.AddSingleton<NotificationInvoker>();
            services.AddSingleton<MobileApp.Handler.Data.InternalDataInterceptor.InternalDataInterceptorApplication>();

            bool containsRelevantService = services.ToList().Find(x => x.ServiceType == typeof(InternalDataInterceptorApplication)) != null;
            if (!containsRelevantService)
            {
                throw new ArgumentNullException(nameof(InternalDataInterceptorApplication) + " not found in DI or " + nameof(AddDeviceHandlers) + " is called before initialization of " + nameof(InternalDataInterceptorApplication) + "");
            }
            services.AddSingleton<InitDataInterceptorApplicationModel>();
            /*var signalrClient = new SignalRClient();
            services.AddSingleton<>();*/




            return services;
        }

        public static IServiceCollection AddPages(this IServiceCollection services)
        {

            return services;
        }
    }

}
