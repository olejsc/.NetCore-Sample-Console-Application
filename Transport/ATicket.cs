using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public abstract class ATicket
    {
        private ushort _price;
        private DateTime _duration;
        private bool _extendable;
    }
}