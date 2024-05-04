using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp10
{
    public class ChandelierViewModel : ViewModelBase
    {
        private Chandelier chandelier;

        public ChandelierViewModel()
        {
           
            chandelier = new Chandelier(isConnectedToNetwork: true); // Assuming chandelier is always connected
        }
        public string Type => "chandelier";

        public bool CanIncreaseMode => chandelier.CurrentMode < Chandelier.MaxMode;

        public ICommand TurnOnCommand => new RelayCommand(TurnOn, CanTurnOn);

        private bool CanTurnOn() // the condition for turnON
        {
            return !chandelier.IsBroken && chandelier.CurrentMode < Chandelier.MaxMode;
        }


        private bool CanTurnOff() // the condition for turnOff
        {
            return chandelier.IsOn;
        }
        public ICommand TurnOffCommand => new RelayCommand(TurnOff, CanTurnOff);

        public bool IsOn
        {
            get { return chandelier.IsOn; }
            set
            {
                if (chandelier.IsOn != value)
                {
                    if (value)
                    {
                        chandelier.TurnOn();
                    }
                    else
                    {
                        chandelier.TurnOff();
                    }

                    OnPropertyChanged(nameof(IsOn));
                }
            }
        }

        public bool IsBroken => chandelier.IsBroken;
        public int CurrentMode => chandelier.CurrentMode; 

        private void TurnOn()
        {
            {
                chandelier.TurnOn();
            OnPropertyChanged(nameof(IsOn));
            OnPropertyChanged(nameof(IsBroken));
            OnPropertyChanged(nameof(CurrentMode));
            OnPropertyChanged(nameof(LampColor)); // Notify UI about the change in LampColor
            OnPropertyChanged(nameof(LampColor1)); 
            OnPropertyChanged(nameof(LampColor2));
            }
        }

        private void TurnOff()
        {
                {
                    chandelier.TurnOff();
            OnPropertyChanged(nameof(IsOn));
            OnPropertyChanged(nameof(IsBroken));
            OnPropertyChanged(nameof(CurrentMode));
            OnPropertyChanged(nameof(LampColor)); 
            OnPropertyChanged(nameof(LampColor1)); 
            OnPropertyChanged(nameof(LampColor2));
                }
        }

        public void UpdateChandelier(Chandelier newChandeliern)
        {
            chandelier = newChandeliern;
            OnPropertyChanged(nameof(IsOn));
            OnPropertyChanged(nameof(IsBroken));
            OnPropertyChanged(nameof(CurrentMode));
            OnPropertyChanged(nameof(LampColor));
            OnPropertyChanged(nameof(LampColor1));
            OnPropertyChanged(nameof(LampColor2));
        }

        public Brush LampColor
        {
            get
            {
                if (IsBroken)
                    return Brushes.Gray; // Gray when the lamp is broken
                else if (!IsOn)
                    return Brushes.Red; // Red when the lamp is off
                else
                    return Brushes.Green; // Green when the lamp is on
            }
        }
        public Brush LampColor1
        {
            get
            {
                if (IsBroken)
                    return Brushes.Gray; // Gray when the lamp is broken
                else if (CurrentMode == 1 || CurrentMode == 3)
                    return Brushes.Yellow; // Yellow in mode 1 and 3
                else
                    return Brushes.Transparent; // Transparent in other modes
            }
        }

        public Brush LampColor2
        {
            get
            {
                if (IsBroken)
                    return Brushes.Gray; // Gray when the lamp is broken
                else if (CurrentMode == 2 || CurrentMode == 3)
                    return Brushes.Magenta; // Magenta in mode 2 and 3
                else
                    return Brushes.Transparent; // Transparent in other modes
            }
        }


    }
}
