using System;

namespace Transport
{
    public class EngineEventArgs : EventArgs
    {
        public bool Stopped { get; set; }
    }
}