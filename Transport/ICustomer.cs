using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public interface ICustomer
    {
        ITicket BuyTicket (APerson person);
        void ExtendTicket (ITicket ITicket);
    }
}