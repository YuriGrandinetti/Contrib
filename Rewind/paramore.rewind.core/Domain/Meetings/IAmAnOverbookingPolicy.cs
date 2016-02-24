using Paramore.Rewind.Core.Domain.Venues;

namespace Paramore.Rewind.Core.Domain.Meetings
{
    public interface IAmAnOverbookingPolicy
    {
        Tickets AllocateTickets(Capacity capacity);
    }
}