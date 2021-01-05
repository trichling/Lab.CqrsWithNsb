using System;
using System.Threading.Tasks;
using Lab.CqrsWithNsb.Application.Accounts.Messages;
using Lab.CqrsWithNsb.Application.Accounts.Messages.Commands;
using Lab.CqrsWithNsb.Application.Accounts.Messages.Events;
using NServiceBus;

namespace Lab.CqrsWithNsb.Application.Accounts
{
    public class Account : Saga<AccountData>,
        IAmStartedByMessages<Open>,
        IHandleMessages<Deposit>,
        IHandleMessages<Withdraw>,
        IHandleMessages<QueryBalanceRequest>
    {
        public Account()
        {
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<AccountData> mapper)
        {
            mapper.ConfigureMapping<Open>(m => m.AccountId).ToSaga(s => s.AccountId);
            mapper.ConfigureMapping<Deposit>(m => m.AccountId).ToSaga(s => s.AccountId);
            mapper.ConfigureMapping<Withdraw>(m => m.AccountId).ToSaga(s => s.AccountId);

            mapper.ConfigureMapping<QueryBalanceRequest>(m => m.AccountId).ToSaga(s => s.AccountId);
        }

        public async Task Handle(QueryBalanceRequest message, IMessageHandlerContext context)
        {
            await context.Reply(new QueryBalanceReply(message.AccountId, Data.Balance));
        }

        public async Task Handle(Open message, IMessageHandlerContext context)
        {
            await Causes(new AccountOpened(message.AccountId, message.InitialBalance), context);
        }

        public async Task Handle(Deposit message, IMessageHandlerContext context)
        {
            await Causes(new AmountDeposited(message.TransactionId, message.AccountId, message.Amount), context);
        }

        public async Task Handle(Withdraw message, IMessageHandlerContext context)
        {
            if (Data.Balance < message.Amount)
            {
                await Causes(new InsufficientBalance(message.TransactionId, message.AccountId, message.Amount, Data.Balance), context);
                return;
            }

            await Causes(new AmountWithdrawn(message.TransactionId, message.AccountId, message.Amount), context);
        }
        
        private async Task Causes(object e, IMessageHandlerContext context)
        {
            Data.Apply((dynamic)e);
            await context.Publish(e);
        }

    }

    public class AccountData : ContainSagaData
    {
        public Guid AccountId { get; set; }

        public decimal Balance { get; set; }

        public void Apply(dynamic e)
        {
            try
            {
                ((dynamic)this).Apply(e);
            }
            catch 
            {
               
            }
        }

        public void Apply(AccountOpened e)
        {
            AccountId = e.AccountId;
            Balance = e.InitialBalance;
        }

        public void Apply(AmountDeposited e)
        {
            Balance += e.Amount;
        }

        public void Apply(AmountWithdrawn e)
        {
            Balance -= e.Amount;
        }

        
    }

   
}