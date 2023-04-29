using Foundation;
using Microsoft.Extensions.Logging;
using UIKit;

namespace IosOrientationLockMAUI.Platforms.iOS
{
    
    public abstract class AppDelegateEx : MauiUIApplicationDelegate
    {
        public virtual UIInterfaceOrientationMask CurrentLockedOrientation { get; set; }

        //according to the Apple docs, this gets called on every orientation change, so this one is the second essential step!
        //https://developer.apple.com/documentation/uikit/uiapplicationdelegate/1623107-application
        [Foundation.Export("application:supportedInterfaceOrientationsForWindow:")]
        public virtual UIInterfaceOrientationMask GetSupportedInterfaceOrientationsForWindow(UIApplication application, UIWindow forWindow)
            => this.CurrentLockedOrientation;
    }
}
