using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace IosOrientationLockXF
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IDeviceOrientationService _deviceOrientationService;
        private string _orientationLockState = "Unlocked";

        public MainViewModel(IDeviceOrientationService deviceOrientationService)
        {
            _deviceOrientationService = deviceOrientationService;

            this.LockPortraitCommand = new Command(() => LockOrientation(DisplayOrientation.Portrait));
            this.LockLandscapeCommand = new Command(() => LockOrientation(DisplayOrientation.Landscape));
            this.UnlockOrientationCommand = new Command(() => LockOrientation(null));
        }

        private void LockOrientation(DisplayOrientation? orientation)
        {
            if (_deviceOrientationService == null)
                return;
            
            switch (orientation)
            {
                case DisplayOrientation.Portrait:
                    this.OrientationLockState = "Locked Portrait";
                    _deviceOrientationService.LockPortrait();
                    break;
                case DisplayOrientation.Landscape:
                    _deviceOrientationService.LockLandscape();
                    this.OrientationLockState = "Locked Landscape";
                    break;
                case null:
                case DisplayOrientation.Unknown:
                default:
                    _deviceOrientationService.UnlockOrientation();
                    this.OrientationLockState = "Unlocked";
                    break;
            }
        }



        public ICommand LockPortraitCommand { get;  }
        public ICommand LockLandscapeCommand { get;  }
        public ICommand UnlockOrientationCommand { get;  }

        public string OrientationLockState  
        {
            get => _orientationLockState;
            set => SetField(ref _orientationLockState, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
