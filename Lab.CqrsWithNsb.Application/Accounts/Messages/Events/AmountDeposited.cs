using System;
using Lab.CqrsWithNsb.Application.EventStore.Messages.Events;

namespace Lab.CqrsWithNsb.Application.Accounts.Messages.Events
{
     public class AmountDeposited : Event
    {
        public AmountDeposited(Guid transactionId, Guid accountId, decimal amount)
        {
            TransactionId = transactionId;
            AccountId = accountId;
            Amount = amount;
        }

        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }

    }
}