namespace WpfApp10
{
    public class Chandelier : LightingFixture
    {
        private int currentMode; // = 0 by default
        private const int maxMode = 3; // the number of modes

        public Chandelier() : this(false) { }


        public Chandelier(bool isConnectedToNetwork)
            : base(isConnectedToNetwork, 6) { }

        public override void TurnOn()
        {
            if (!IsBroken)
            {
                Fail(); // Simulate a failure
                isOn = true;
                currentMode = (currentMode % maxMode) + 1; // Increment the mode
            }
        }

        public override bool IsOn // so that if broken mid modes IsOn is false
        {
            get { return !IsBroken && isOn; }
        }



        public override void TurnOff()
        {
            if (currentMode == 1) // If the mode is already at the minimum, turn off the chandelier
            {
                isOn = false;
                currentMode = 0;
            }
            else // Otherwise, decrement the mode
            {
                currentMode--;
            }
        }

        public int CurrentMode
        {
            get { return currentMode; }
        }

        public static int MaxMode
        {
            get { return maxMode; }
        }
    }
}
