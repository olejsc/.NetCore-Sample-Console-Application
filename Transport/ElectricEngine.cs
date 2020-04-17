using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public class ElectricEngine : AEngine
    {
        public override ushort HorsePower
        {
            get
            {
                throw new NotImplementedException();
            }

            protected set
            {
                throw new NotImplementedException();
            }
        }

        public override byte Speed
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override float FuelConsumption
        {
            get
            {
                throw new NotImplementedException();
            }

            protected set
            {
                throw new NotImplementedException();
            }
        }

        public override ushort FuelCapacity
        {
            get
            {
                throw new NotImplementedException();
            }

            protected set
            {
                throw new NotImplementedException();
            }
        }

        public override ushort Fuel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override event EventHandler<EngineEventArgs> FuelEmpty;
        public override event EventHandler<EngineEventArgs> FuelFull;

        public override void Refuel (int amount)
        {
            throw new NotImplementedException();
        }

        public override void Run (object time)
        {
            throw new NotImplementedException();
        }

        public override void Start ()
        {
            throw new NotImplementedException();
        }

        public override void Stop ()
        {
            throw new NotImplementedException();
        }
    }
}