using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace Transport
{
    public abstract class ABus : ITicketable, IEnterable, IDriveable
    {
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

        public ABus ()
        {
            throw new System.NotImplementedException();
        }

        protected ABus (byte wheels = 6, bool isAtMaximumCapacity = false, byte handicapSeats = 2, byte seats = 20, byte standingSpots = 8)
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
            Console.WriteLine($"Bus created: {BusID.ToString()}" );

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
        public Transport.IEngineController Engine
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

        public bool IsAtMaximumCapacity
        {
            get
            {
                return People.Count < Seats + HandicapSeats + StandingSpots + 1;
            }

            protected set
            {
                _isAtMaximumCapacity = value;
            }
        }

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

        public Thread EngineThread
        {
            get
            {
                return _engineThread;
            }
        }

        public Guid BusID => _busID;

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

        public abstract void Drive (object time);

        public void RegisterEntrance (ITicket ticket)
        {
            People.Add(ticket);
        }

        public abstract void Stop ();
    }
}