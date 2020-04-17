using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Transport
{
    public class DieselEngine : AEngine
    {
        // readonly because we might want to have the option to use engines which are older (or different types of diesel engines) at some point.
        private readonly float _fuelConsumptionPerTick = 1.3F;
        private readonly Byte _maxSpeed = 110;
        private bool _started;

        public DieselEngine (ushort fuelCapacity = 60, ushort fuel = 60, ushort horsePower = 400, bool started = false)
        {
            FuelCapacity = fuelCapacity;
            Fuel = fuel;
            HorsePower = horsePower;
            Speed = 0;
            FuelConsumption = 0;
            Started = started;
        }

        public override byte Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
            }
        }

        public override float FuelConsumption
        {
            get
            {
                return _fuelConsumption;
            }
            protected set
            {
                _fuelConsumption = value;
            }
        }


        public override ushort FuelCapacity
        {
            get
            {
                return _fuelCapacity;
            }
            protected set
            {
                _fuelCapacity = value;
            }
        }

        public override ushort Fuel
        {
            get
            {
                return _fuel;
            }

            set
            {
                if(value > FuelCapacity)
                {
                    _fuel = FuelCapacity;
                }
                else
                {
                    _fuel = value;
                }
            }
        }

        public override ushort HorsePower
        {
            get
            {
                return _horsePower;
            }

            protected set
            {
                if(_horsePower == 0)
                {
                    _horsePower = value;
                }
            }
        }

        private bool Started
        {
            get
            {
                return _started;
            }

            set
            {
                _started = value;
            }
        }

        public override event EventHandler<EngineEventArgs> FuelEmpty;
        public override event EventHandler<EngineEventArgs> FuelFull;

        public override void Refuel (int amount)
        {
            if(amount >= Fuel)
            {
                Fuel = FuelCapacity;
            }
            else
            {
                Fuel = Convert.ToUInt16(amount);
            }
        }

        public override void Run (object time)
        {
            Console.WriteLine($"Current fuel is: {Fuel.ToString()}");
            BusTime busTime = (BusTime)time;
            DateTime now = new DateTime();
            DateTime target = now.AddMilliseconds(busTime.Duration);
            UInt16 numberOfTicks = (ushort)((target.Millisecond - now.Millisecond)/busTime.Timestep);

            if (!Started && Fuel >= 0f)
            {
                Start();
            }
            else if(Fuel == 0f)
            {
                OnFuelEmpty(this, new EngineEventArgs { Stopped = true });
            }
            else
            {
                while(now < target)
                {
                    for (int i = 0; i < numberOfTicks; i++)
                    {
                        Thread.Sleep(busTime.Timestep);
                        CalculateFuelConsumption();
                        Fuel -= (ushort)FuelConsumption;
                        if(Fuel == 0)
                        {
                            OnFuelEmpty(this, new EngineEventArgs { Stopped = true });
                            break;
                        }
                    }
                }
            }
            Console.WriteLine($"Current fuel is: {Fuel.ToString()}");
            Console.WriteLine($"Finnished running on DieselEngine, on thread :  {Thread.CurrentThread.Name.ToString()}!");
        }

        public override void Start ()
        {
            Started = true;
        }

        public override void Stop ()
        {
            throw new NotImplementedException();
        }

        void OnFuelEmpty(object sender, EngineEventArgs args)
        {
            Stop();
            FuelFull.Invoke(null, new EngineEventArgs { Stopped = true });
        }

        public void OnFuelFull (object sender, EngineEventArgs args)
        {

        }

        protected override void CalculateFuelConsumption ()
        {
            FuelConsumption = _horsePower / Speed;
        }
    }
}