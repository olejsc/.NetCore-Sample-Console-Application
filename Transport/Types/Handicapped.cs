﻿using System;

namespace Transport
{
    public class Handicapped : APerson
    {
        public override ITicket BuyTicket (APerson person)
        {
            throw new NotImplementedException();
        }

        public override void ExtendTicket (ITicket ITicket)
        {
            throw new NotImplementedException();
        }
    }
}