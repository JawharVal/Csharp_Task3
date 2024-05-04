using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WpfApp10;

public class FloorLampViewModel : ViewModelBase
{
    private FloorLamp floorLamp;

    public FloorLampViewModel()
    {
      
        floorLamp = new FloorLamp(isConnectedToNetwork: false);
    }

    public string Type => "Floor";

    public ICommand TurnOnCommand => new RelayCommand(TurnOn);
    public ICommand TurnOffCommand => new RelayCommand(TurnOff);

    public ICommand ToggleNetworkConnectionCommand => new RelayCommand(ToggleNetworkConnection);


    public bool IsBroken => floorLamp.IsBroken;

    public bool IsOn
    {
        get { return floorLamp.IsOn; }
        set
        {
            if (floorLamp.IsOn != value)
            {
                if (value)
                {
                    floorLamp.TurnOn();
                }
                else
                {
                    floorLamp.TurnOff();
                }

                OnPropertyChanged(nameof(IsOn));
            }
        }
    }

    public bool IsConnectedToNetwork
    {
        get { return floorLamp.IsConnectedToNetwork; }
        set
        {
            if (floorLamp.IsConnectedToNetwork != value)
            {
                floorLamp.ToggleNetworkConnection(value);
                OnPropertyChanged(nameof(IsConnectedToNetwork));
            }
        }
    }


    private void TurnOn()
    {
        {
            floorLamp.TurnOn();
        OnPropertyChanged(nameof(IsOn));
        OnPropertyChanged(nameof(IsBroken));
        OnPropertyChanged(nameof(LampColor)); // Notify UI about the change in LampColor
        }
    }

    private void TurnOff()
    {
        {
            floorLamp.TurnOff();
        OnPropertyChanged(nameof(IsOn));
        OnPropertyChanged(nameof(IsBroken));
        OnPropertyChanged(nameof(LampColor));
        }
    }

        private void ToggleNetworkConnection()
    {
        bool newState = !floorLamp.IsConnectedToNetwork;
        floorLamp.ToggleNetworkConnection(newState);
        OnPropertyChanged(nameof(IsConnectedToNetwork)); // Notify the UI of the change
        OnPropertyChanged(nameof(LampColorss)); // Notify UI about the change in LampColor
    }
    public void UpdatefloorLamp(FloorLamp newfloorLamp)
    {
        floorLamp = newfloorLamp;
        OnPropertyChanged(nameof(IsOn));
        OnPropertyChanged(nameof(IsBroken));
        OnPropertyChanged(nameof(IsConnectedToNetwork));
        OnPropertyChanged(nameof(LampColor));
        OnPropertyChanged(nameof(LampColorss));
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

    public Brush LampColorss
    {
        get
        {
            if (IsConnectedToNetwork)
                return Brushes.Blue; // Gray when the lamp is broken
            else
                return Brushes.Black; // Green when the lamp is on
        }
    }
}
