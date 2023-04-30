using System.Runtime.CompilerServices;
using ARKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
namespace IosOrientationLockXF.iOS
{
    public abstract class AppDelegateEx : FormsApplicationDelegate
    {
        protected AppDelegateEx() =>
            AppDelegateEx.Current = this;

        public static AppDelegateEx Current { get; private set; }

        public virtual UIInterfaceOrientationMask CurrentLockedOrientation { get; set; }

        //according to the Apple docs, Application and ViewController have to agree on the supported orientation, this forces it
        //https://developer.apple.com/documentation/uikit/uiapplicationdelegate/1623107-application?language=objc
        [Foundation.Export("application:supportedInterfaceOrientationsForWindow:")]
         public virtual UIInterfaceOrientationMask GetSupportedInterfaceOrientationsForWindow(UIApplication application, UIWindow forWindow)
             => this.CurrentLockedOrientation;
    }
}
