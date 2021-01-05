using System;

namespace Lab.CqrsWithNsb.Application.Accounts.Messages.Commands
{
    public class Open
    {

        public Open(Guid accountId, Guid accountOwnerId, decimal initialBalance)
        {
            AccountOwnerId = accountOwnerId;
            AccountId = accountId;
            InitialBalance = initialBalance;
        }

        public Guid AccountId { get; set; }

        public Guid AccountOwnerId { get; set; }

        public decimal InitialBalance { get; set; }

    }

}