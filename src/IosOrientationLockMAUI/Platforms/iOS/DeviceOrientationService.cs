using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;
using Foundation;
using Microsoft.Extensions.Logging;
using UIKit;
namespace IosOrientationLockMAUI.Platforms.iOS
{
    public class DeviceOrientationService : IDeviceOrientationService
    {
        private readonly AppDelegateEx _applicationDelegate;
        private readonly ILogger<IDeviceOrientationService> _logger;

        public DeviceOrientationService(ILogger<IDeviceOrientationService> logger)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(16, 0))
            {
                if (MauiUIApplicationDelegate.Current is AppDelegateEx orientationServiceDelegate)
                    _applicationDelegate = orientationServiceDelegate;
                else
                    throw new NotImplementedException($"AppDelegate must be derived from {nameof(AppDelegateEx)} to use this implementation!");
            }

            _logger = logger;
        }

        public DisplayOrientation CurrentOrientation => DeviceDisplay.Current.MainDisplayInfo.Orientation;

        
        [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
        private void SetOrientation(UIInterfaceOrientationMask uiInterfaceOrientationMask)
        {
            var rootWindowScene = (UIApplication.SharedApplication.ConnectedScenes.ToArray()?.FirstOrDefault()) as UIWindowScene;
            
            if (rootWindowScene == null)
                return;
            
#pragma warning disable CA1422
            var rootViewController = UIApplication.SharedApplication.KeyWindow?.RootViewController;
#pragma warning restore CA1422

            if (rootViewController == null)
                return;
            
            rootWindowScene.RequestGeometryUpdate(new UIWindowSceneGeometryPreferencesIOS(uiInterfaceOrientationMask),
            error =>
            {
                _logger.LogError("Error while attempting to lock orientation: {Error}", error.LocalizedDescription);
            });
            
            rootViewController.SetNeedsUpdateOfSupportedInterfaceOrientations();
            rootViewController.NavigationController?.SetNeedsUpdateOfSupportedInterfaceOrientations();
        }
        
        public void LockPortrait()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(16, 0))
            {
                _applicationDelegate.CurrentLockedOrientation = UIInterfaceOrientationMask.Portrait;

                SetOrientation(UIInterfaceOrientationMask.Portrait);
            }
            else
            {
                UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
            }
        }

        public void LockLandscape()
        {
            
            if (UIDevice.CurrentDevice.CheckSystemVersion(16, 0))
            {
                _applicationDelegate.CurrentLockedOrientation = UIInterfaceOrientationMask.LandscapeRight;

                SetOrientation(UIInterfaceOrientationMask.LandscapeRight);
            }
            else
            {
                UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeRight), new NSString("orientation"));
            }
        }

        public void UnlockOrientation()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(16, 0))
            {
                _applicationDelegate.CurrentLockedOrientation = UIInterfaceOrientationMask.AllButUpsideDown;
                SetOrientation(UIInterfaceOrientationMask.AllButUpsideDown);
            }
        }
    }
}
