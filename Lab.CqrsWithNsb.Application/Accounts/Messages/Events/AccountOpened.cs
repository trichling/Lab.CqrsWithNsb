using System;
using Lab.CqrsWithNsb.Application.EventStore.Messages.Events;

namespace Lab.CqrsWithNsb.Application.Accounts.Messages.Events
{
    public class AccountOpened : Event
    {
        
        public AccountOpened(Guid accountId, decimal initialBalance)
        {
            AccountId = accountId;
            InitialBalance = initialBalance;
        }

        public Guid AccountId { get; set; }
        public decimal InitialBalance { get; set; }

    }
}