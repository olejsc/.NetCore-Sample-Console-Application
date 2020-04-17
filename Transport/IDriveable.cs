using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public interface IDriveable
    {
        event System.EventHandler<EventArgs>DayStart;
        event System.EventHandler<EventArgs>DayEnd;
        event System.EventHandler<EventArgs>EndRoute;
        event System.EventHandler<EventArgs>ChangeRoute;
        event System.EventHandler<EventArgs>StartRoute;

        /// <summary>
        /// Start driving for a given amount of time.
        /// </summary>
        /// <param name="time">The time to drive.</param>
        void Drive (object time);
        void Stop ();
    }
}