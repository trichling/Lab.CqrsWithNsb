using System;
using Lab.CqrsWithNsb.Application.EventStore.Messages.Events;

namespace Lab.CqrsWithNsb.Application.Transactions.Messages.Events
{
    public class TransactionStarted : Event
    {
        public Guid TransactionId { get; set; }
    }
}