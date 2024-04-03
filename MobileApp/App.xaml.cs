using Infrastructure.Authentification;
using Infrastructure.Handler.AppConfig;
using Infrastructure.Handler.Data.InternalDataInterceptor;
using Infrastructure.Handler.Device.Media;
using Presentation.Services;
using Presentation.View;
using Presentation.ViewModel;

namespace Presentation
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private CancellationTokenSource _webApiActionCancelationToken = new CancellationTokenSource();
        public App(
            IAuthentificationService authentificationService,
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
            Load(authentificationService,applicationConfigHandler, loginPageViewModel, mainPageViewModel, navigationService, applicationResourcesHandler);
        }
        public static Dictionary<string, ResourceDictionary> ResourceDictionary;

        public async Task Load(
            IAuthentificationService authentificationService,
            ApplicationConfigHandler applicationConfigHandler,
            LoginPageViewModel loginPageViewModel,
            MainPageViewModel mainPageViewModel,
            NavigationService navigationService,
            ApplicationResourcesHandler applicationResourcesHandler)
        {
            var authObj = await authentificationService.GetAuthentification(CancellationToken.None);
            bool loggedin = authObj.IsAuthentificated;

            if (loggedin)
            {
                //await navigationService.PushAsync(new MainPage(mainPageViewModel));
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