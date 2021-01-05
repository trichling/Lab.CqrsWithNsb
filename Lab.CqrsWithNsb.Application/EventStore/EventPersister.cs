using System;
using System.Threading.Tasks;
using Lab.CqrsWithNsb.Application.EventStore.Messages.Events;
using NServiceBus;

namespace Lab.CqrsWithNsb.Application.EventStore
{
    public class EventPersister : IHandleMessages<IEvent>
    {
        public Task Handle(IEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine(message.GetType());
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(message));

            return Task.CompletedTask;
        }
    }
}