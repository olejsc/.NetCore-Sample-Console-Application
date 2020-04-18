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
            Console.WriteLine($"Current people in bus are: { People.Count}");
            DateTime target = new DateTime();
            target = DateTime.Now.AddMilliseconds(busTime.Duration);
            UInt16 numberOfTicks = (ushort)((target.Millisecond - DateTime.Now.Millisecond)/busTime.Timestep);
            Random rnd = new Random(654668);
            while( DateTime.Now.Millisecond < target.Millisecond) {
                for (int i = 0; i < numberOfTicks; i++)
                {
                    double temp = rnd.NextDouble();
                    //Thread.Sleep(busTime.Timestep);
                    if(temp >= 0.5)
                    {
                        APerson person = new Adult();
                        // person.stop = new Stop()

                        person.Ticket = person.BuyTicket(person);
                        person.BusEntrance = (IEnterable)this;
                        bool canjoin = person.BusEntrance.CanEnter(person.Ticket);
                        if (canjoin)
                        {
                            person.Ticket.RegisterEntrance(this);
                        }

                    }
                }
            }
            Console.WriteLine($"Current people in bus are: { People.Count}");
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