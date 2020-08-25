using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transport.Types;

namespace Transport
{

    public abstract class ABus : ITicketable, IEnterable, IDriveable
    {
        private ADriver _driver;

        private readonly Guid _busID;
        /// <summary>
        /// The number of wheels on the bus.
        /// </summary>
        private Byte _wheels;
        /// <summary>
        /// The engine on the bus.
        /// </summary>
        private IEngineController _engine;
        /// <summary>
        /// The maximum capacity of people on the bus, including the driver.
        /// </summary>
        private Byte _capacity;

        private bool _isAtMaximumCapacity;

        /// <summary>
        /// People on the buss.
        /// </summary>
        private List<ITicket> _people;
        /// <summary>
        /// Number of handicapseats.
        /// </summary>
        private Byte _handicapSeats;
        private Byte _seats;
        private Byte _standingSpots;

        private readonly Thread _engineThread;

        private BusTaskScheduler _busTaskScheduler;

        volatile bool disposed;

        public ABus ()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// An abstract base class with all the common parameters any type of bus needs.
        /// </summary>
        /// <param name="wheels"> The number of wheels this bus have</param>
        /// <param name="isAtMaximumCapacity">Represents if the buss if currently full or not</param>
        /// <param name="handicapSeats">Number of handicapseats in the bus</param>
        /// <param name="seats">Number of regular seats in the bus</param>
        /// <param name="standingSpots">Number of standing spots in the bus</param>
        /// <param name="busTaskScheduler">The task scheduler for this bus</param>
        protected ABus (byte wheels = 6,
                        bool isAtMaximumCapacity = false,
                        byte handicapSeats = 2,
                        byte seats = 20,
                        byte standingSpots = 8,
                        BusTaskScheduler busTaskScheduler = null)
        {
            // buss "id"
            _busID = Guid.NewGuid();

            // bus tecnical variables
            Wheels = wheels;
            Engine = new DieselEngine();


            // Bus capacity
            IsAtMaximumCapacity = isAtMaximumCapacity;
            HandicapSeats = handicapSeats;
            Seats = seats;
            StandingSpots = standingSpots;
            People = new List<ITicket>(seats + handicapSeats + standingSpots + 1);


            // Thread
            _engineThread = new Thread(new ParameterizedThreadStart(Engine.Run));
            _engineThread.Name = $"Thread {BusID.ToString()}";
            _engineThread.IsBackground = true;
            Console.WriteLine($"Bus created: {BusID.ToString()}");
            BusTaskScheduler = busTaskScheduler;
        }

        /// <summary>
        /// The number of wheels on the bus.
        /// </summary>
        public byte Wheels
        {
            get
            {
                return _wheels;
            }

            set
            {
                _wheels = value;
            }
        }

        /// <summary>
        /// The engine on the bus.
        /// </summary>
        public IEngineController Engine
        {
            get
            {
                return _engine;
            }

            set
            {
                _engine = value;
            }
        }
        /// <summary>
        /// Checks if the bus is full or not.
        /// </summary>
        // TODO : Add driver seat property.
        public bool IsAtMaximumCapacity
        {
            get
            {
                // +1 for the driver.
                return People.Count < Seats + HandicapSeats + StandingSpots + 1;
            }

            protected set
            {
                _isAtMaximumCapacity = value;
            }
        }

        /// <summary>
        /// Sets or get the number of handicapseats in the bus.
        /// </summary>
        public byte HandicapSeats
        {
            get
            {
                return _handicapSeats;
            }

            set
            {
                _handicapSeats = value;
            }
        }

        public byte Seats
        {
            get
            {
                return _seats;
            }

            set
            {
                _seats = value;
            }
        }

        /// <summary>
        /// A <see cref="List{T}"/> of <see cref="ITicket"/> on the bus. Represents people, through the ITicket interface.
        /// </summary>
        public List<ITicket> People
        {
            get
            {
                return _people;
            }

            set
            {
                _people = value;
            }
        }

        public byte StandingSpots
        {
            get
            {
                return _standingSpots;
            }

            set
            {
                _standingSpots = value;
            }
        }


        public Guid BusID => _busID;

        internal BusTaskScheduler BusTaskScheduler
        {
            get
            {
                return _busTaskScheduler;
            }

            set
            {
                _busTaskScheduler = value;
            }
        }

        public virtual void Execute ()
        {
            int distanceToNextStop = 5+2; // TODO: Add Bus route access and point system to calculate distance between stops.
            Engine.Start();
            Drive(false, distanceToNextStop);
            // Inside Drive() :
                //CheckIfStopPressed();
                // NotifyBusstops(3);
            // If(Location.current == route.NextStop && StopPressed || Route.NextStop.Passengers > 0)
            //      StopEngine();
            //      Route.nextstop = route.pop();
            //      Exit Drivemethod..
            //OpenDoors();
                //PassengerExit();
                //WaitNewPassengerJoin();
                //CloseDoors();
            //







            //Task CheckEngine =
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

                         */
        }

        public abstract event EventHandler<EventArgs> DayStart;
        public abstract event EventHandler<EventArgs> DayEnd;
        public abstract event EventHandler<EventArgs> EndRoute;
        public abstract event EventHandler<EventArgs> ChangeRoute;
        public abstract event EventHandler<EventArgs> StartRoute;

        public bool CanEnter (ITicket Ticket)
        {
            // TODO: Add conditional logic to check if handicapped seats are taken for handicap-tickets etc.
            // TODO: Check if ticket expired.
            if (!IsAtMaximumCapacity)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public abstract void Drive (bool lastStopIsNextStop, int distanceToNextStop);

        public void RegisterEntrance (ITicket ticket)
        {
            People.Add(ticket);
        }

        public abstract void Stop ();

        public abstract bool CheckIfStopButtonPressed ();

    }
}