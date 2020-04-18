using System;
using System.Collections.Generic;
using System.Text;

namespace Transport.Abstractions
{
    class Busstop : AStop
    {
        private List<IObserver<ABus>> _observers;
        private List<ABus> _busses;

        public Busstop () : base()
        {
            Observers = new List<IObserver<ABus>>();
            Busses = new List<ABus>();
        }

        public List<IObserver<ABus>> Observers
        {
            get
            {
                return _observers;
            }

            set
            {
                _observers = value;
            }
        }

        public List<ABus> Busses
        {
            get
            {
                return _busses;
            }

            set
            {
                _busses = value;
            }
        }

        public override IDisposable Subscribe (IObserver<ABus> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!Observers.Contains(observer))
            {
                Observers.Add(observer);
                // Provide observer with existing data.
                foreach (var bus in Busses)
                    observer.OnNext(bus);
            }
            return new Unsubscriber<ABus>(observers, observer);
        }
    }
}
