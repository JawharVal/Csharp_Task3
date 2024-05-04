using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WpfApp10;

public class TableLampViewModel : ViewModelBase
{
    private TableLamp tableLamp;


    public TableLampViewModel()
    {
   
        tableLamp = new TableLamp(isConnectedToNetwork: false);
    }

    public string Type => "table";
    public ICommand TurnOnCommand => new RelayCommand(TurnOn);
    public ICommand TurnOffCommand => new RelayCommand(TurnOff);
    public ICommand ToggleNetworkConnectionCommand => new RelayCommand(ToggleNetworkConnection);


    public bool IsOn
    {
        get { return tableLamp.IsOn; }
        set
        {
            if (tableLamp.IsOn != value)
            {
                if (value)
                {
                    tableLamp.TurnOn();
                }
                else
                {
                    tableLamp.TurnOff();
                }

                OnPropertyChanged(nameof(IsOn));
            }
        }
    }
    public bool IsBroken => tableLamp.IsBroken;


    public bool IsConnectedToNetwork
    {
        get { return tableLamp.IsConnectedToNetwork; }
        set
        {
            if (tableLamp.IsConnectedToNetwork != value)
            {
                tableLamp.ToggleNetworkConnection(value);
                OnPropertyChanged(nameof(IsConnectedToNetwork));
            }
        }
    }

    private void TurnOn()
    {
        {
        tableLamp.TurnOn();
        OnPropertyChanged(nameof(IsOn));
        OnPropertyChanged(nameof(IsBroken));
        OnPropertyChanged(nameof(LampColor)); // Notify UI about the change in LampColor
        }
    }

    private void TurnOff()
    {
        {
        tableLamp.TurnOff();
        OnPropertyChanged(nameof(IsOn));
        OnPropertyChanged(nameof(IsBroken)); 
        OnPropertyChanged(nameof(LampColor));
        }
    }


    private void ToggleNetworkConnection()
    {
        bool newState = !tableLamp.IsConnectedToNetwork;
        tableLamp.ToggleNetworkConnection(newState);
        OnPropertyChanged(nameof(IsConnectedToNetwork)); // Notify the UI of the change
        OnPropertyChanged(nameof(LampColorss)); // Notify UI about the change in LampColor
    }

    public void UpdatetableLamp(TableLamp newtableLamp)
    {
        tableLamp = newtableLamp;
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