using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public class EngineEventArgs : EventArgs
    {
        public bool Stopped { get; set; }
    }
}