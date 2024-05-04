using System;
using WpfApp10;

namespace WpfApp10
{
    public class FloorLamp : LightingFixture
    {

        public FloorLamp() : this(false) { }

        public FloorLamp(bool isConnectedToNetwork)
            : base(isConnectedToNetwork, 6)
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