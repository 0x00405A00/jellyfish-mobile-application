using Application;
using Camera.MAUI;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Infrastructure;
using Infrastructure.Extension;
using Infrastructure.Handler.AppConfig;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Presentation.ApplicationSpecific;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Presentation
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();


            var appConfigHandler = ApplicationConfigHandlerSingleton.Get();
            appConfigHandler.ApplicationConfig = appConfigHandler.Load();

            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkitMediaElement()//Integrated Media Player UI Element for Videorecoding Playback
                .UseMauiCameraView()//Camera MAUI Lib Init
                .UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldSuppressExceptionsInConverters(false);
                    options.SetShouldSuppressExceptionsInBehaviors(false);
                    options.SetShouldSuppressExceptionsInAnimations(false);
                })
                .UseMauiCommunityToolkitMarkup()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .AddApplicationEvents(appConfigHandler);

#if DEBUG
		builder.Logging.AddDebug();
#endif
            //Pages können aktuell nicht in die DI injected werden, da es zu Problemen mit AppResouces auf dem Resources/* und ResourceDictionaries kommen kann, dass
            //Resource zur Laufzeit nicht gefunden werden.
            //Das Problem ist das die Pages in die DI gepackt werden ohne das die Resources überhaupt verfügbar sind. Diese sind erst nach dem builder.Build() verfügbar.


#if __ANDROID__//Fixxed bei einer CollectionView in der man Images auswählen kann folgenden Fehler: Canvas: trying to use a recycled bitmap
        ImageHandler.Mapper.PrependToMapping(nameof(Microsoft.Maui.IImage.Source), (handler, view) => PrependToMappingImageSource(handler, view));
#endif
            builder.AddIConfiguration();
            builder.Services.AddSingleton(appConfigHandler);
            var fs = Directory.GetFiles(FileSystem.AppDataDirectory);
            builder.AddIConfiguration();
            builder.Services.AddInfrastructure();
            builder.Services.AddPresentation(appConfigHandler);
            return builder.Build();
        }
#if __ANDROID__//Fixxed bei einer CollectionView in der man Images auswählen kann folgenden Fehler: Canvas: trying to use a recycled bitmap
    public static void PrependToMappingImageSource(IImageHandler handler, Microsoft.Maui.IImage image)
    {
        handler.PlatformView?.Clear();
    }
#endif


    }
}