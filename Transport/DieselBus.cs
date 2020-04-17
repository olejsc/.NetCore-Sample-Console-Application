using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Transport
{
    public class DieselBus : ABus
    {
        public DieselBus () : base(6, false,2,20,8)
        {
            //base.Engine.FuelFull += OnFuelFull;
            //base.Engine.FuelEmpty += OnFuelEmpty;
        }

        public override event EventHandler<EventArgs> DayStart;
        public override event EventHandler<EventArgs> DayEnd;
        public override event EventHandler<EventArgs> EndRoute;
        public override event EventHandler<EventArgs> ChangeRoute;
        public override event EventHandler<EventArgs> StartRoute;

        public override void Drive (object time)
        {
            BusTime busTime = (BusTime)time;
            EngineThread.Start(time);
            //Thread.Sleep(Convert.ToInt32(busTime.Timestep));
        }

        public override void Stop ()
        {
            throw new NotImplementedException();
        }

        public void OnDayStart(object sender, EventArgs args)
        {

        }

        public void OnFuelEmpty (object sender, EngineEventArgs args)
        {
            Console.WriteLine("Fuel is empty!");
        }

        public void OnFuelFull (object sender, EngineEventArgs args)
        {
            Console.WriteLine("Fuel is full!");
        }
    }
}