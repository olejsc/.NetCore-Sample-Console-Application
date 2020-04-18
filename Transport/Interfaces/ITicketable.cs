using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public interface ITicketable
    {
        void RegisterEntrance (ITicket ticket);
    }
}