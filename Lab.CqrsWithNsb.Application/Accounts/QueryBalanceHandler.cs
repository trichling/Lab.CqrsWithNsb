using System;
using System.Threading.Tasks;
using Lab.CqrsWithNsb.Application.Accounts.Messages;
using NServiceBus;

namespace Lab.CqrsWithNsb.Application.Accounts
{
    public class QueryBalanceHandler : IHandleMessages<QueryBalanceReply>
    {
        public Task Handle(QueryBalanceReply message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Balance of {message.AccountId} is {message.Balance}");

            return Task.CompletedTask;
        }
    }
}