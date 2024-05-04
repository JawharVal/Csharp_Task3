using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WpfApp10;

public class LanternViewModel : ViewModelBase
{
    private Lantern lantern;

    public LanternViewModel()
    {
        
        lantern = new Lantern(isConnectedToNetwork: false); // Assuming lantern is always connected
    }

    public string Type => "Lantern";

    public ICommand TurnOnCommand => new RelayCommand(TurnOn);
    public ICommand TurnOffCommand => new RelayCommand(TurnOff);

    public bool IsBroken => lantern.IsBroken;

    public bool IsOn
    {
        get { return lantern.IsOn; }
        set
        {
            if (lantern.IsOn != value)
            {
                if (value)
                {
                    lantern.TurnOn();
                }
                else
                {
                    lantern.TurnOff();
                }

                OnPropertyChanged(nameof(IsOn));
            }
        }
    }


    private void TurnOn()
    {
        {
            lantern.TurnOn();
            OnPropertyChanged(nameof(IsOn));
            OnPropertyChanged(nameof(IsBroken));
            OnPropertyChanged(nameof(LampColor)); // Notify UI about the change in LampColor
        }
    }

    private void TurnOff()
    {
        {
            lantern.TurnOff();
            OnPropertyChanged(nameof(IsOn));
            OnPropertyChanged(nameof(IsBroken));
            OnPropertyChanged(nameof(LampColor));
        }
    }

    public void UpdateLantern(Lantern newLantern)
    {
        lantern = newLantern;
        OnPropertyChanged(nameof(IsOn));
        OnPropertyChanged(nameof(IsBroken));
        OnPropertyChanged(nameof(LampColor));
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

}