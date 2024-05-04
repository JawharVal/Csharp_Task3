using System;

namespace WpfApp10
{
    public abstract class LightingFixture : ILightingFixture//abstract : base classe have no implementation for the methods but instead used by child methods by their own implementations
    {
        protected bool isOn;
        protected bool isConnectedToNetwork;
        protected bool isBroken;
        protected int bulbsCount;


        public event EventHandler Broken;

        protected LightingFixture(bool isConnectedToNetwork, int bulbsCount) // all lamps are linked to the network and have bulbsCount
        {
            this.isConnectedToNetwork = isConnectedToNetwork;
            this.bulbsCount = bulbsCount;
        }

        // virtual so that it can be overridden
        public virtual void TurnOn()
        {
            if (isConnectedToNetwork && !Fail()) // Call Fail() method when turning on
                isOn = true;
        }

        public virtual void TurnOff()
        {
            isOn = false;
        }

        public void ToggleNetworkConnection(bool state)
        {
            isConnectedToNetwork = state;
        }


        // Protected only methods within this class or its descendants can call this method.
        protected virtual bool Fail() // regler le pourcentage de Failure of being broken el lampe 
        {
            // Simulate failure based on the number of bulbs
            var random = new Random();
            var failChance = random.Next(0, 100); // Generate a random number between 0 and 100
            if (failChance < bulbsCount * 2) // The failure chance is 2% per bulb so 12% for the chandelier and floor and 2% for the others
            {
                Break(); // broken lamp
                return true;
            }
            return false;
        }


        protected virtual void Break()
        {
            isBroken = true;
            OnBroken();
        }

        protected virtual void OnBroken()
        {
            Broken?.Invoke(this, EventArgs.Empty);
        }

        public virtual bool IsOn
        {
            get { return isOn; }
        }

        public bool IsConnectedToNetwork
        {
            get { return isConnectedToNetwork; }
        }

        public bool IsBroken
        {
            get { return isBroken; }
        }
    }
}