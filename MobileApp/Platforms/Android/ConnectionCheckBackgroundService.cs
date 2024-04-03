using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Infrastructure.Authentification;
using Presentation.Services;
using Shared.Infrastructure.Backend.Api;
using Shared.Infrastructure.Backend.SignalR;

namespace Presentation.Platforms.Android
{
    [Service(Enabled = true,Exported = false, Name = "com.net.roos_it.jellyfish.connectioncheckbackgroundservice")]
    public class ConnectionCheckBackgroundService : Service
    {
        public static readonly TimeSpan CheckInterval = new TimeSpan(0,0,10);
        private const int SERVICE_NOTIFICATION_ID = 1001;
        private const string CHANNEL_ID = "ConnectionCheckBackgroundService";
        private static bool _connectionNotificationIsShowing = false;
        private static bool _noConnectionNotificationIsShowing = false;
        private IJellyfishBackendApi jellyfishBackendApi;
        private IAuthentificationService authentificationService;
        private JellyfishSignalRClient jellyfishSignalRClient;

        public ConnectionCheckBackgroundService()
        {
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        /// <summary>
        /// Will be called when service while initializing
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();
            jellyfishBackendApi = ServiceHelper.GetService<IJellyfishBackendApi>();
            /*authentificationService = ServiceHelper.GetService<IAuthentificationService>();
            jellyfishSignalRClient = ServiceHelper.GetService<JellyfishSignalRClient>();*/
        }

        /// <summary>
        /// When service gets destroyed by android
        /// </summary>
        public override void OnDestroy()
        {
            base.OnDestroy();
            ClearNoConnectionNotification();
            StopForeground(true);
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            CreateNotificationChannel();
            ShowConnectionCheckStartNotification();
            Task.Run(()=> CheckConnection());
            return StartCommandResult.Sticky;
        }
        private async void CheckConnection()
        {
            do
            {
                using PeriodicTimer periodicTimer = new PeriodicTimer(CheckInterval);
                
                await periodicTimer.WaitForNextTickAsync();
                //var currentAuthentification = await authentificationService.GetAuthentification(CancellationToken.None);

                bool test = await jellyfishBackendApi.ConnectionTest(CancellationToken.None);
                if (!test)
                {
                    
                    ShowNoConnectionNotification();
                }
                else
                {
                    /*if(currentAuthentification.IsAuthentificated && jellyfishSignalRClient.HubConnection.State != Microsoft.AspNetCore.SignalR.Client.HubConnectionState.Connected)
                    {

                    }*/
                    ShowConnectionCheckNotification();
                }
            }
            while (true);
        }
        private void ShowConnectionCheckStartNotification()
        {
            Notification notification = new Notification.Builder(this, CHANNEL_ID)
                .SetContentTitle("Internetverbindung")
                .SetContentText("Prüfe die Verbindung zu Jellyfish Servern")
                .SetSmallIcon(Resource.Drawable.exo_controls_next)
                .SetSound(null)
                .Build();

            StartForeground(SERVICE_NOTIFICATION_ID, notification);
        }
        private void ShowConnectionCheckNotification()
        {
            if(_connectionNotificationIsShowing)
                return;
            
            Notification notification = new Notification.Builder(this, CHANNEL_ID)
                .SetContentTitle("Internetverbindung")
                .SetContentText("Verbindung zu Jellyfish Servern erfolgreich")
                .SetSmallIcon(Resource.Drawable.exo_controls_next)
                .SetSound(null)
                .Build();

            StartForeground(SERVICE_NOTIFICATION_ID, notification);
            _connectionNotificationIsShowing = true;
            _noConnectionNotificationIsShowing = false;
        }
        private void ShowNoConnectionNotification()
        {
            if (_noConnectionNotificationIsShowing)
                return;

            Notification notification = new Notification.Builder(this, CHANNEL_ID)
                .SetContentTitle("Keine Internetverbindung")
                .SetContentText("Es besteht keine Verbindung zu Jellyfish.")
                .SetSmallIcon(Resource.Drawable.exo_controls_next)
                .SetSound(null)
                .Build();

            StartForeground(SERVICE_NOTIFICATION_ID, notification);
            _noConnectionNotificationIsShowing = true;
            _connectionNotificationIsShowing = false;
        }
        private void ClearNoConnectionNotification()
        {
            if (!_connectionNotificationIsShowing && !_noConnectionNotificationIsShowing)
                return;
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);

            notificationManager.Cancel(SERVICE_NOTIFICATION_ID);
            _connectionNotificationIsShowing = false;
            _noConnectionNotificationIsShowing = false;
        }
        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(CHANNEL_ID, CHANNEL_ID, NotificationImportance.Default);
                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }
        }
    }
}
