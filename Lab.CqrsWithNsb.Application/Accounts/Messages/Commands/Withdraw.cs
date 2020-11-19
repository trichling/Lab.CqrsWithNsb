using System;

namespace Lab.CqrsWithNsb.Application.Accounts.Messages.Commands
{
    public class Withdraw
    {

        public Withdraw(Guid transactionId, Guid accountId, decimal amount)
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