﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public class Senior : APerson
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