using System.Collections.Generic;

namespace Transport.Abstractions
{
    class Busstop : AStop
    {
        private List<ABus> _busses;

        public Busstop () : base()
        {
            Busses = new List<ABus>();
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
    }
}
