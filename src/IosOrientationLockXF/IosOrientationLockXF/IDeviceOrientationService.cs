using Xamarin.Essentials;
namespace IosOrientationLockXF
{
    public interface IDeviceOrientationService
    {
        DisplayOrientation CurrentOrientation { get; }

        void LockPortrait();
        
        void LockLandscape();

        void UnlockOrientation();
    }
}
