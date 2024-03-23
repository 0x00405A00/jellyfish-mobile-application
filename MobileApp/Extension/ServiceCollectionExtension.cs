using Infrastructure.Handler;
using Infrastructure.Handler.AppConfig;
using Infrastructure.Handler.Data.InternalDataInterceptor;
using Infrastructure.Handler.Data.InternalDataInterceptor.Invoker;
using Infrastructure.Handler.Device.ClipBoard;
using Infrastructure.Handler.Device.Filesystem;
using Infrastructure.Handler.Device.Media;
using Infrastructure.Handler.Device.Media.Camera;
using Infrastructure.Handler.Device.Media.Communication;
using Infrastructure.Handler.Device.Media.Contact;
using Infrastructure.Handler.Device.Network;
using Infrastructure.Handler.Device.Notification;
using Infrastructure.Handler.Device.Sensor;
using Infrastructure.Handler.Device.Vibrate;
using Shared.Infrastructure.Backend;
using Shared.Infrastructure.Backend.Api;
using Microsoft.Maui.LifecycleEvents;
using Shared.Infrastructure.Backend.Interceptor.Abstraction;
using Shared.Infrastructure.Backend.Interceptor;


/*using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;*/

#if ANDROID
using Infrastructure.Handler.Data.InternalDataInterceptor.Invoker.Notification.Android;
#else
using Infrastructure.Handler.Data.InternalDataInterceptor.Invoker.Notification.iOS;
#endif

namespace Infrastructure.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDeviceHandlers(this IServiceCollection services, ApplicationConfigHandler applicationHandler)
        {
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


            services.AddSingleton<JellyfishWebApiRestClientInvoker>();
            services.AddSingleton<ViewModelInvoker>();
            services.AddSingleton<SqlLiteDatabaseHandlerInvoker>();
            services.AddSingleton<NotificationInvoker>();
            services.AddSingleton<IInternalDataInterceptorApplicationDispatcher, InternalDataInterceptorApplicationDispatcher>();

            bool containsRelevantService = services.ToList().Find(x => x.ServiceType == typeof(IInternalDataInterceptorApplicationDispatcher)) != null;
            if (!containsRelevantService)
            {
                throw new ArgumentNullException(nameof(IInternalDataInterceptorApplicationDispatcher) + " not found in DI or " + nameof(AddDeviceHandlers) + " is called before initialization of " + nameof(IInternalDataInterceptorApplicationDispatcher) + "");
            }
            services.AddSingleton<InitDataInterceptorApplicationModel>();
            /*var signalrClient = new SignalRClient();
            services.AddSingleton<>();*/




            return services;
        }
        public static MauiAppBuilder AddApplicationEvents(this MauiAppBuilder appBuilder, ApplicationConfigHandler applicationHandler)
        {
            appBuilder.ConfigureLifecycleEvents(events =>
            {

#if ANDROID
                events.AddAndroid(android => 
                    android.OnDestroy(e => applicationHandler.Safe())
                    );//ApplicationExit Event like (wenn Handy Ressourcen selbst freigibt oder der User die App terminated)
#elif IOS

                    events.AddiOS(ios => 
                        ios.WillTerminate(del => applicationHandler.Safe())
                        );//ApplicationExit Event like (wenn Handy Ressourcen selbst freigibt oder der User die App terminated)
#endif
            });

            /*appBuilder.UseLocalNotifications(config =>
            {
                config.AddCategory(new NotificationCategory(NotificationCategoryType.Status)
                {
                    ActionList = new HashSet<NotificationAction>(new List<NotificationAction>()
                        {
                            new NotificationAction(100)
                            {
                                Title = "Launch",
                                Android =
                                {
                                    LaunchAppWhenTapped = true,
                                    IconName =
                                    {
                                        ResourceName = "i2"
                                    }
                                }
                            },
                            new NotificationAction(101)
                            {
                                Title = "Close",
                                Android =
                                {

                                    LaunchAppWhenTapped = false,
                                    IconName =
                                    {
                                        ResourceName = "i3"
                                    }
                                }
                            }
                        })
                })
                .AddAndroid(android =>
                {
                    android.AddChannel(new NotificationChannelRequest
                    {
                        Sound = "good_things_happen"
                    });
                })
                .AddiOS(iOS =>
                {
#if IOS
                    iOS.UseCustomDelegate = true;
                    iOS.SetCustomUserNotificationCenterDelegate(new CustomUserNotificationCenterDelegate());
#endif
                });
            }); */
            return appBuilder;
        }
    }
}
