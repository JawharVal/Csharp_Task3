/* for in case needing network connection :
using System;
using WpfApp10;

namespace WpfApp10
{
    public class Lantern : LightingFixture
    {

        public Lantern() : this(false) { } 

        public Lantern(bool isConnectedToNetwork)
            : base(isConnectedToNetwork, 1)
        {
        }

        public override void TurnOn()
        {
            if (!IsBroken)
            {
                base.TurnOn();
            }
        }

        public override void TurnOff()
        {
            base.TurnOff();
        }
    }
}


w zid hetha fel xaml : <TextBlock Text="{Binding IsConnectedToNetwork, StringFormat=Network Connection: {0}}" />
*/

using System;
using WpfApp10;

namespace WpfApp10
{
    public class Lantern : LightingFixture
    {
        public Lantern() : this(false) { }

        public Lantern(bool isConnectedToNetwork)
            : base(isConnectedToNetwork, 1)
        {
        }

        public override void TurnOn()
        {
            if (!IsBroken)
            {
                Fail(); // Simulate a failure
                isOn = true;
            }
        }
    
        public override void TurnOff()
        {
            isOn = false;
        }
    }
}
