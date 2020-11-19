using System;
using System.Threading.Tasks;
using Lab.CqrsWithNsb.Application.EventStore.Messages.Events;
using NServiceBus;

namespace Lab.CqrsWithNsb.Application.EventStore
{
    public class EventPersister : IHandleMessages<Event>
    {
        public Task Handle(Event message, IMessageHandlerContext context)
        {
            Console.WriteLine(message.GetType());
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(message));

            return Task.CompletedTask;
        }
    }
}