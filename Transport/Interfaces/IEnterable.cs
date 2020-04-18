using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
    public interface IEnterable
    {
        /// <summary>
        /// Is the vehicle full?
        /// </summary>
        bool IsAtMaximumCapacity { get; }
        /// <summary>
        /// Can the passenger with the given ticket enter?
        /// </summary>
        /// <remarks>This might return true for some tickets, and false for other ticket types. For example handicap spot might be taken, so some passenger with handicap ticket might not be able to enter while regular passengers are able to enter.</remarks>
        /// <returns>bool</returns>
        bool CanEnter (ITicket Ticket);
    }
}