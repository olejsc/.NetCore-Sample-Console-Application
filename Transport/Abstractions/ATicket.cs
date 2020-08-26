using System;

namespace Transport
{
    public abstract class ATicket : ITicket
    {
        private ushort _price;
        private DateTime _duration;
        private bool _extendable;

        public virtual void RegisterEntrance (ITicketable bus)
        {
            bus.RegisterEntrance(this);
        }
    }
}