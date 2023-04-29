using IosOrientationLockMAUI.Platforms.iOS;
using Microsoft.Extensions.Logging;

namespace IosOrientationLockMAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

#if IOS
        builder.Services.AddTransient<IDeviceOrientationService, DeviceOrientationService>();
#endif
        
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MainPage>();
        
        return builder.Build();
    }
}
