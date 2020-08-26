using System;

namespace Transport
{
    public class Adult : APerson
    {
        public override ITicket BuyTicket (APerson person)
        {
            ITicket adultTicket = new AdultTicket();
            Ticket = adultTicket;
            return Ticket;
        }

        public override void ExtendTicket (ITicket ITicket)
        {
            throw new NotImplementedException();
        }
    }
}