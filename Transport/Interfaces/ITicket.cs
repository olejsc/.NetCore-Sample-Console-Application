namespace Transport
{
    /// <summary>
    /// A interface for registering entrance to a vehicle.
    /// </summary>
    public interface ITicket
    {
        void RegisterEntrance (ITicketable bus);
    }
}