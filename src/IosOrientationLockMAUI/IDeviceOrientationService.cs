using System.Runtime.Versioning;
namespace IosOrientationLockMAUI
{
    public interface IDeviceOrientationService
    {
        DisplayOrientation CurrentOrientation { get; }

        void LockPortrait();
        
        void LockLandscape();

        [SupportedOSPlatform("ios16.0")]
        void UnlockOrientation();
    }
    
    
}
