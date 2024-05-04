using System;
using WpfApp10;

namespace WpfApp10
{
    public class TableLamp : LightingFixture
    {

        public TableLamp() : this(false) { }

        public TableLamp(bool isConnectedToNetwork)
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