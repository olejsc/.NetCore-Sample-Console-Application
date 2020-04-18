using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public interface ICustomer
    {
        ITicket Ticket { get; set; }
        IEnterable BusEntrance { get; set; }

        ITicket BuyTicket (APerson person);
        void ExtendTicket (ITicket ITicket);
    }
}