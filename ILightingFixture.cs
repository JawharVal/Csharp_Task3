using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp10
{
    public interface ILightingFixture
    {
        void TurnOn();
        void TurnOff();
        void ToggleNetworkConnection(bool state);
        bool IsOn { get; }
        bool IsBroken { get; }
        bool IsConnectedToNetwork { get; }
    }

}
