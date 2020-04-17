using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public class GasolineBus : ABus
    {
        public override event EventHandler<EventArgs> DayStart;
        public override event EventHandler<EventArgs> DayEnd;
        public override event EventHandler<EventArgs> EndRoute;
        public override event EventHandler<EventArgs> ChangeRoute;
        public override event EventHandler<EventArgs> StartRoute;

        public override void Drive (object time)
        {
            throw new NotImplementedException();
        }

        public override void Stop ()
        {
            throw new NotImplementedException();
        }
    }
}