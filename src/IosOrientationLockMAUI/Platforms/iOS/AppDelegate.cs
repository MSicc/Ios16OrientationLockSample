using Foundation;
using IosOrientationLockMAUI.Platforms.iOS;
using UIKit;

namespace IosOrientationLockMAUI;

[Register("AppDelegate")]
public class AppDelegate : AppDelegateEx
{
    protected override MauiApp CreateMauiApp() => 
        MauiProgram.CreateMauiApp();
    
}
