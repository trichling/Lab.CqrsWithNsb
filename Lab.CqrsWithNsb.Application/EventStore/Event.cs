using NServiceBus;

namespace Lab.CqrsWithNsb.Application.EventStore.Messages.Events
{
    public class Event : IEvent
    {
        // Wenn conventions configuriert sind, dann wird IEvent nicht mehr berücksichtigt!! Entweder, oder!
    }
}