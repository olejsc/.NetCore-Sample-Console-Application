using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    /// <summary>
    /// Interface representing driving logic for vehicles.
    /// </summary>
    public interface IDriveable
    {
        event EventHandler<EventArgs>DayStart;
        event EventHandler<EventArgs>DayEnd;
        event EventHandler<EventArgs>EndRoute;
        event EventHandler<EventArgs>ChangeRoute;
        event EventHandler<EventArgs>StartRoute;

        /// <summary>
        /// Start driving for a given amount of time.
        /// </summary>
        /// <param name="time">The time to drive.</param>
        void Drive (object time);
        void Stop ();
    }
}