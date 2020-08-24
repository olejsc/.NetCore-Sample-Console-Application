using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public async void DriveAsync(object time)
        {
            Task CheckEngine = 
            /*
            drive : 
            1. Start engine
            1.1 Notify the 3 next busstops on the route continiuously, concurrently 24/7 while driving about your current speed and position

            2. Drive towards stop.
            2.1 Check if anyone pressed the stop button.
            2.2 Check if stop have passengers (skip if last stop of route)
            2.3 Stop engine at stop if anyone pressed the stop or if stop have passengers ( and bus is not full)
            2.4 Open the doors of the bus
            2.5 Let passengers exit the bus
            2.6 Let new passengers join the bus.
            2.7 Drive towards next stop, if any. If not, take break if driver havent taken break.

                         * 
                         * 
                         */
        }

        public override void Drive (object time)
        {
            // Task startTask driveToStop();
            // Task 

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