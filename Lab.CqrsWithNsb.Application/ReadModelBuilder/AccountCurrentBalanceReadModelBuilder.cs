using Lab.CqrsWithNsb.Application.Accounts.Messages.Events;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.CqrsWithNsb.Application.ReadModelBuilder
{
    public class AccountCurrentBalanceReadModelBuilder :
        IHandleMessages<AccountOpened>,
        IHandleMessages<AmountDeposited>,
        IHandleMessages<AmountWithdrawn>
    {
        private readonly ReadModelContext readModelContext;

        public AccountCurrentBalanceReadModelBuilder(ReadModelContext context)
        {
            this.readModelContext = context;
        }

        public async Task Handle(AccountOpened message, IMessageHandlerContext context)
        {
            readModelContext.AccountsWithBalance.Add(new AccountCurrentBalance() { AccountId = message.AccountId, Balance = message.InitialBalance });
            await readModelContext.SaveChangesAsync();
        }

        public async Task Handle(AmountWithdrawn message, IMessageHandlerContext context)
        {
            var account = readModelContext.AccountsWithBalance.Single(account => account.AccountId == message.AccountId);
            account.Balance -= message.Amount;
            await readModelContext.SaveChangesAsync();
        }

        public async Task Handle(AmountDeposited message, IMessageHandlerContext context)
        {
            var account = readModelContext.AccountsWithBalance.Single(account => account.AccountId == message.AccountId);
            account.Balance += message.Amount;
            await readModelContext.SaveChangesAsync();
        }
    }
}
