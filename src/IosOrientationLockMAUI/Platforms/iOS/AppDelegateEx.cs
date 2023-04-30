using Foundation;
using Microsoft.Extensions.Logging;
using UIKit;

namespace IosOrientationLockMAUI.Platforms.iOS
{
    
    public abstract class AppDelegateEx : MauiUIApplicationDelegate
    {
        public virtual UIInterfaceOrientationMask CurrentLockedOrientation { get; set; }

        //according to the Apple docs, Application and ViewController have to agree on the supported orientation, this forces it
        //https://developer.apple.com/documentation/uikit/uiapplicationdelegate/1623107-application?language=objc
        [Foundation.Export("application:supportedInterfaceOrientationsForWindow:")]
        public virtual UIInterfaceOrientationMask GetSupportedInterfaceOrientationsForWindow(UIApplication application, UIWindow forWindow)
            => this.CurrentLockedOrientation;
    }
}
