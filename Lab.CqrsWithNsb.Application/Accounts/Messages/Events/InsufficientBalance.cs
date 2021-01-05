using System;
using Lab.CqrsWithNsb.Application.EventStore.Messages.Events;

namespace Lab.CqrsWithNsb.Application.Accounts.Messages.Events
{
    public class InsufficientBalance : Event
    {
        private readonly Guid transactionId;
        private readonly Guid accountId;
        private readonly decimal amount;
        private readonly decimal balance;

        public InsufficientBalance(Guid transactionId, Guid accountId, decimal amount, decimal balance)
        {
            this.TransactionId = transactionId;
            this.AccountId = accountId;
            this.Amount = amount;
            this.Balance = balance;
        }

        public Guid TransactionId { get => transactionId; init => transactionId = value; }
        public Guid AccountId { get => accountId; init => accountId = value; }
        public decimal Amount { get => amount; init => amount = value; }
        public decimal Balance { get => balance; init => balance = value; }
    }
}