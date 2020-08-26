namespace Transport
{
    /// <summary>
    /// Represents the logic of bus customers.
    /// </summary>
    public interface ICustomer
    {
        /// <summary>
        /// Property to retrive the ticket of a customer, if any.
        /// </summary>
        ITicket Ticket { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IEnterable BusEntrance { get; set; }

        /// <summary>
        /// Buy a ticket. 
        /// </summary>
        /// <param name="person"></param>
        /// <returns><see cref="ITicket"/></returns>
        ITicket BuyTicket (APerson person);

        /// <summary>
        /// Extend a ticket.
        /// </summary>
        /// <param name="ITicket"></param>
        void ExtendTicket (ITicket ITicket);
    }
}