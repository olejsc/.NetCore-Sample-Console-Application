using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Transport.Types;

namespace Transport
{
    public abstract class ABus : ITicketable, IEnterable
    {
        #region Fields


        private ADriver _driver;
        private LinkedList<int> _route;
        private LinkedListNode<int> _nextStop;
        private readonly Guid _busID;
        private readonly int _busSpeed = 1;
        private readonly int timeStepPerUnitOfSpeed = 10; // in seconds

        private CancellationTokenSource _cancellationDrivingTokenSource;

        /// <summary>
        /// The maximum capacity of people on the bus, including the driver.
        /// </summary>
        private readonly Byte _capacity;

        /// <summary>
        /// The engine on the bus.
        /// </summary>
        private IEngineController _engine;

        /// <summary>
        /// Number of handicapseats.
        /// </summary>
        private Byte _handicapSeats;

        private bool _isAtMaximumCapacity;
        /// <summary>
        /// A integer representing this bus' current location.
        /// </summary>
        private int _location;

        /// <summary>
        /// People on the buss.
        /// </summary>
        private List<ITicket> _people;

        private Byte _seats;
        private Byte _standingSpots;
        private bool _stopButtonPressed;
        /// <summary>
        /// The number of wheels on the bus.
        /// </summary>
        private Byte _wheels;

        private CancellationToken busTaskCancellationToken;
        private TaskFactory _busTaskFactory;
        private BusTaskScheduler _busTaskScheduler = new BusTaskScheduler();
        private CancellationTokenSource _DrivingCTS = new CancellationTokenSource();
        private volatile bool disposed;

        #endregion Fields

        #region Events

        public abstract event EventHandler<EventArgs> ChangeRoute;

        public abstract event EventHandler<EventArgs> DayEnd;

        public abstract event EventHandler<EventArgs> DayStart;

        public abstract event EventHandler<EventArgs> EndRoute;

        public abstract event EventHandler<EventArgs> StartRoute;

        #endregion Events

        #region Properties

        public Guid BusID => _busID;

        public int BusSpeed => _busSpeed;


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

        public int Location
        {
            get
            {
                return _location;
            }

            set
            {
                Console.WriteLine($"Location value: {value} in : {Task.CurrentId} in thread: {Thread.CurrentThread.ManagedThreadId} on Bus: {BusID}");
                _location = value;
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

        public bool StopButtonPressed
        {
            get
            {
                return _stopButtonPressed;
            }

            set
            {
                _stopButtonPressed = value;
            }
        }

        public int TimeStepPerUnitOfSpeed => timeStepPerUnitOfSpeed;

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

        public ADriver Driver
        {
            get
            {
                return _driver;
            }

            set
            {
                _driver = value;
            }
        }

        public LinkedList<int> Route
        {
            get
            {
                return _route;
            }

            set
            {
                _route = value;
            }
        }

        public LinkedListNode<int> NextStop
        {
            get
            {
                return _nextStop;
            }

            set
            {
                _nextStop = value;
            }
        }

        public CancellationTokenSource CancellationDrivingTokenSource
        {
            get
            {
                return _cancellationDrivingTokenSource;
            }

            set
            {
                _cancellationDrivingTokenSource = value;
            }
        }

        public CancellationToken BusTaskCancellationToken
        {
            get
            {
                return busTaskCancellationToken;
            }

            set
            {
                busTaskCancellationToken = value;
            }
        }

        public TaskFactory BusTaskFactory
        {
            get
            {
                return _busTaskFactory;
            }

            set
            {
                _busTaskFactory = value;
            }
        }

        public BusTaskScheduler BusTaskScheduler
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

        public CancellationTokenSource DrivingCTS
        {
            get
            {
                return _DrivingCTS;
            }

            set
            {
                _DrivingCTS = value;
            }
        }

        #endregion Properties

        #region Constructors

        public ABus ()
        {
            TaskFactory busTaskFactory = new TaskFactory(BusTaskCancellationToken,TaskCreationOptions.AttachedToParent, TaskContinuationOptions.LazyCancellation,BusTaskScheduler);
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
                        byte standingSpots = 8)
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


            BusTaskFactory = new TaskFactory(BusTaskCancellationToken,TaskCreationOptions.AttachedToParent, TaskContinuationOptions.LazyCancellation,BusTaskScheduler);
        }

        #endregion Constructors

        #region Methods

        public abstract bool ArrivedAtTargetStop ();

        public abstract int CalculateDistanceToTarget ();

        public abstract void OpenDoors ();

        public abstract void CloseDoors ();

        public abstract void WaitPassengerJoining ();

        public abstract void WaitPassengersLeaving ();


        public abstract void LeaveStop ();

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

        /// <summary>
        /// Checks if any of the passengers have pressed the stop button.
        /// </summary>

        public abstract bool CheckIfStopButtonPressed ();

        public abstract void Drive (bool lastStopIsNextStop, CancellationToken token);

        public virtual async void Execute ()
        {
            Task engineTask = BusTaskFactory.StartNew(() =>
            {
                Engine.Start();
            }, DrivingCTS.Token, BusTaskFactory.CreationOptions, BusTaskScheduler);
            engineTask.Wait(DrivingCTS.Token);
            Task DriveOrganizingtask = BusTaskFactory.StartNew(() =>
            {
                while (!CancellationDrivingTokenSource.Token.IsCancellationRequested)
                {
                    var DriveTask = BusTaskFactory.StartNew(()=>
                    {

                    },CancellationDrivingTokenSource.Token, TaskCreationOptions.AttachedToParent,BusTaskScheduler)
                    .ContinueWith(NotifyBussStopsTask =>
                    {
                        // TODO
                        // notify 3 next busstops on the route about our current position, speed and time.
                    }, CancellationDrivingTokenSource.Token,
                        TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.AttachedToParent,
                        BusTaskScheduler)
                    .ContinueWith(CheckLocationTask =>
                    {
                        // Check if We've arrived at location. In case, exit these continuation tasks in the drive logic.
                        if (ArrivedAtTargetStop() && ShouldStopAtTargetStop())
                        {
                            Engine.Stop();
                            Task.Delay(2000);
                            CancellationDrivingTokenSource.Cancel();
                        }
                        // Arrived at stop but we shouldn't stop because no passengers to pick up.
                        else if (ArrivedAtTargetStop())
                        {
                            CancellationDrivingTokenSource.Cancel();
                        }
                        // We've not arrived at the stop yet, so keep driving.
                        else
                        {

                        }
                    }, CancellationDrivingTokenSource.Token, TaskContinuationOptions.AttachedToParent| TaskContinuationOptions.ExecuteSynchronously| TaskContinuationOptions.NotOnCanceled, BusTaskScheduler);

                        Drive(Route.Count > 1, CancellationDrivingTokenSource.Token);
                    }
            }, CancellationDrivingTokenSource.Token, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning, BusTaskScheduler);
            Task StopPressedTask = DriveOrganizingtask.ContinueWith(StopPressedTask =>
                {
                    // If there is no people, no button is pressed.
                    if(People.Count == 0){
                        StopButtonPressed = false;
                    }
                    // If there are people, but only one stop left, the button is pressed.
                    else if(Route.Count == 1 && People.Count > 0)
                    {
                        StopButtonPressed = true;
                        CancellationDrivingTokenSource.Cancel();
                    }
                    //
                    else
                    {
                        if(((Route.Count * People.Count) / 100) > 0.5f)
                        {
                            StopButtonPressed = true;
                            CancellationDrivingTokenSource.Cancel();
                        }
                        else
                        {
                            StopButtonPressed = false;
                        }
                    }
                    // check if stop button is pressed
                }, DrivingCTS.Token, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.AttachedToParent, BusTaskScheduler);

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

        /// <summary>
        /// Notifies the next busstopps in advance about this bus estamited time of arrival to them.
        /// </summary>
        /// <param name="bussStopsInAdvanceToNofity"> The number of busstops in advance to notify.</param>
        /// <remarks> If no busstops are left on the route, this method does nothing.</remarks>
        public abstract void NotifyNextBusstops (int bussStopsInAdvanceToNofity);
        public void RegisterEntrance (ITicket ticket)
        {
            People.Add(ticket);
        }

        public abstract void ResetStopVariables ();

        public abstract bool ShouldStopAtTargetStop ();
        public abstract void Stop ();

        #endregion Methods
    }
}