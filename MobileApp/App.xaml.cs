using Infrastructure.Handler.AppConfig;
using Infrastructure.Handler.Data.InternalDataInterceptor;
using Infrastructure.Handler.Device.Media;
using Presentation.Service;
using Presentation.View;
using Presentation.ViewModel;
using Shared.Infrastructure.Backend.SignalR;

namespace Presentation
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private CancellationTokenSource _webApiActionCancelationToken = new CancellationTokenSource();
        public App(
            InitDataInterceptorApplicationModel initDataInterceptorApplicationModel,
            ApplicationConfigHandler applicationConfigHandler,
            LoginPageViewModel loginPageViewModel,
            MainPageViewModel mainPageViewModel,
            NavigationService navigationService,
            ApplicationResourcesHandler applicationResourcesHandler)
        {
            InitializeComponent();
            Page viewPage = new NavigationPage(new LoginPage(loginPageViewModel));
            MainPage = viewPage;
            Load(applicationConfigHandler, loginPageViewModel, mainPageViewModel, navigationService, applicationResourcesHandler);

        }
        public static Dictionary<string, ResourceDictionary> ResourceDictionary;
        public async Task Load(
            ApplicationConfigHandler applicationConfigHandler,
            LoginPageViewModel loginPageViewModel,
            MainPageViewModel mainPageViewModel,
            NavigationService navigationService,
            ApplicationResourcesHandler applicationResourcesHandler)
        {
            bool loggedin = true;

            if (loggedin)
            {
                await navigationService.PushAsync(new MainPage(mainPageViewModel));
            }

            ResourceDictionary = new Dictionary<string, ResourceDictionary>();
            foreach (var dictionary in Microsoft.Maui.Controls.Application.Current.Resources.MergedDictionaries)
            {
                if (dictionary.GetType().FullName.ToLower().Contains("skia"))
                    continue;
                string key = dictionary.Source.OriginalString.Split(';').First().Split('/').Last().Split('.').First();
                ResourceDictionary.Add(key, dictionary);
            }
            applicationResourcesHandler.ResourceDictionary = ResourceDictionary;
        }
    }
}