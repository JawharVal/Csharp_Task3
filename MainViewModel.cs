using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Generic;
using Microsoft.Win32;

namespace WpfApp10
{
    public class MainViewModel : ViewModelBase
    {
        //@"C:\Users\legion\Desktop\3.3\WpfApp10\WpfApp10\bin\Debug\net6.0-windows\WpfApp10.dll"

        // Dictionary to store instances of lighting fixtures by their type.
        private Dictionary<Type, ILightingFixture> instances = new Dictionary<Type, ILightingFixture>();

        // ViewModels for different types of lighting fixtures.
        private LanternViewModel lanternViewModel;

        private TableLampViewModel tableLampViewModel;

        private FloorLampViewModel floorLampViewModel;

        private ChandelierViewModel chandelierViewModel;

        // Collections to hold types and methods for dynamic interaction.
        public ObservableCollection<ViewModelBase> LightingFixtures { get; }
        public ObservableCollection<Type> Types { get; } = new ObservableCollection<Type>();
        public ObservableCollection<MethodInfo> Methods { get; } = new ObservableCollection<MethodInfo>();

        // Properties to hold the selected method and parameters for invocation.
        public MethodInfo SelectedMethod { get; set; }
        public string MethodParameters { get; set; }

        // Commands bound to UI actions.
        public ICommand LoadAssemblyCommand => new RelayCommand(LoadAssembly);
        public ICommand InvokeMethodCommand => new RelayCommand(InvokeMethod);

        // Log for operation results.
        public string Log { get; set; }
     
        public MainViewModel()
        {
           
            lanternViewModel = new LanternViewModel();

            tableLampViewModel = new TableLampViewModel();

            floorLampViewModel = new FloorLampViewModel();

            chandelierViewModel = new ChandelierViewModel();

            LightingFixtures = new ObservableCollection<ViewModelBase>
        {
            lanternViewModel,
            tableLampViewModel,
            floorLampViewModel,
            chandelierViewModel,
        };

        }

        // Loads a DLL and reflects on its types to find implementors of ILightingFixture.
        public void LoadAssembly()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "DLL Files (*.dll)|*.dll|All files (*.*)|*.*",
                Title = "Select a DLL file"
            };

            // Open file dialog to choose DLL.
            if (openFileDialog.ShowDialog() == true)
            {
                DllPath = openFileDialog.FileName;  // This will update the TextBox due to binding

                try
                {
                    // Load assembly from file and get types.
                    var assembly = Assembly.LoadFrom(DllPath);
                    var types = assembly.GetTypes();
                    var lightingFixtureTypes = types.Where(t => t.GetInterfaces().Contains(typeof(ILightingFixture)) && !t.IsAbstract);
                    // not absctract because abstract classes cannot be instantienled directly.
                    Types.Clear();
                    instances.Clear();

                    // Instantiate each type implementing ILightingFixture and store references.
                    foreach (var type in lightingFixtureTypes)
                    {
                        Types.Add(type);
                        instances[type] = Activator.CreateInstance(type) as ILightingFixture; // is a method used to dynamically create an instance of the type type. allows for the creation of objects when the type is only known at runtime.
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading the assembly: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Loading cancelled by user.", "Loading Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Property for DLL path, notifies UI of changes.
        private string _dllPath;
        public string DllPath
        {
            get { return _dllPath; }
            set
            {
                if (_dllPath != value)
                {
                    _dllPath = value;
                    OnPropertyChanged(nameof(DllPath));
                }
            }
        }

        // Property for selected type, updates available methods when set.
        private Type selectedType;
        public Type SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                OnPropertyChanged(nameof(SelectedType));

                Methods.Clear();

                if (selectedType != null)
                {
                    foreach (var method in selectedType.GetMethods())
                    {
                        // Check if the type is Lantern or Chandelier and method name is not TurnOn or TurnOff
                        if ((selectedType == typeof(Lantern) || selectedType == typeof(Chandelier)) && method.Name != "TurnOn" && method.Name != "TurnOff")
                        {
                            continue; // Skip this method
                        }
                        // Check if the type is TableLamp or FloorLamp and method name is not TurnOn, TurnOff, or ToggleNetworkConnection
                        else if ((selectedType == typeof(TableLamp) || selectedType == typeof(FloorLamp)) && method.Name != "TurnOn" && method.Name != "TurnOff" && method.Name != "ToggleNetworkConnection")
                        {
                            continue; // Skip this method
                        }

                        //  add it to the list
                        Methods.Add(method);
                    }

                }
            }

        }


        // Invokes a selected method on a selected type.
        private async void InvokeMethod()
        {
            // Run the method on a separate thread
            await Task.Run(() => 
            {
                if (SelectedMethod != null)
                {
                    // Get the instance from the dictionary
                    var instance = instances[SelectedType];

                    if (SelectedType == typeof(Lantern))
                    {
                        // Cast the instance to 'Lantern' type to access Lantern-specific properties or methods.
                        var lanternInstance = (Lantern)instance;

                        // Search for the first 'LanternViewModel' in the collection of lighting fixture view models.
                        var lanternViewModel = LightingFixtures.OfType<LanternViewModel>().FirstOrDefault();

                        // Check if a corresponding ViewModel for the Lantern instance was found.
                        if (lanternViewModel != null)
                        {
                            // Update the ViewModel with the current state of the Lantern instance.
                            lanternViewModel.UpdateLantern(lanternInstance);
                        }
                    }

                    else if (SelectedType == typeof(TableLamp))
                    {
                        var tableLampInstance = (TableLamp)instance;
                        var tableLampViewModel = LightingFixtures.OfType<TableLampViewModel>().FirstOrDefault();
                        if (tableLampViewModel != null)
                        {
                            tableLampViewModel.UpdatetableLamp(tableLampInstance);
                        }
                    }

                    else if (SelectedType == typeof(FloorLamp))
                    {
                        var floorLampInstance = (FloorLamp)instance;
                        var floorLampViewModel = LightingFixtures.OfType<FloorLampViewModel>().FirstOrDefault();
                        if (floorLampViewModel != null)
                        {
                            floorLampViewModel.UpdatefloorLamp(floorLampInstance);
                        }
                    }

                    else if (SelectedType == typeof(Chandelier))
                    {
                        var chandelierInstance = (Chandelier)instance;
                        var chandelierViewModel = LightingFixtures.OfType<ChandelierViewModel>().FirstOrDefault();
                        if (chandelierViewModel != null)
                        {
                            chandelierViewModel.UpdateChandelier(chandelierInstance);
                        }
                    }



                    try
                    {
                        // Check if the method requires parameters
                        if (SelectedMethod.GetParameters().Length > 0)
                        {
                            // Check if MethodParameters is not null
                            if (!string.IsNullOrEmpty(MethodParameters))
                            {
                                // Parse the parameters
                                var parameters = MethodParameters.Split(',').Select(p => p.Trim()).ToArray();

                                // Convert the parameters to their appropriate types
                                var convertedParameters = new object[parameters.Length];
                                var methodParameters = SelectedMethod.GetParameters();
                                for (int i = 0; i < parameters.Length; i++)
                                {
                                    var type = methodParameters[i].ParameterType;
                                    convertedParameters[i] = Convert.ChangeType(parameters[i], type);
                                }

                                // Invoke the method with parameters
                                SelectedMethod.Invoke(instance, convertedParameters);
                            }
                            else
                            {
                                // Invoke the method without parameters
                                SelectedMethod.Invoke(instance, new object[0]);
                            }
                        }
                        else
                        {
                            // Invoke the method without parameters
                            SelectedMethod.Invoke(instance, null);
                        }

                        // Append the log message to the Log property
                        Log += $"Invoked {SelectedMethod.Name} on {SelectedType.Name}\n";
                        OnPropertyChanged(nameof(Log));
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that occur during reflection or method invocation
                        Log += $"An error occurred: {ex.Message}\n";
                        OnPropertyChanged(nameof(Log));
                    }
                }
            }
           );
        }



    }
}