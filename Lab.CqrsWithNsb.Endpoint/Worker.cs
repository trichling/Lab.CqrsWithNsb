using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lab.CqrsWithNsb.Application.Accounts.Messages;
using Lab.CqrsWithNsb.Application.Accounts.Messages.Commands;
using Lab.CqrsWithNsb.Application.Transactions.Messages.Commands;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Lab.CqrsWithNsb.Endpoint
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageSession _messageSession;

        public Worker(ILogger<Worker> logger, IMessageSession messageSession)
        {
            _logger = logger;
            _messageSession = messageSession;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SimpleTransfer(_messageSession);
                //await TheFundraiser(_messageSession);
                //await TheMillionairesGame(_messageSession);
            }

        }

        private static async Task SimpleTransfer(IMessageSession endpoint)
        {
            var samId = Guid.NewGuid();
            var bobId = Guid.NewGuid();

            var accountOfBob = Guid.NewGuid();
            var accountOfSam = Guid.NewGuid();

            await endpoint.Send(new Open(accountOfBob, samId, 1000000.0M));
            await endpoint.Send(new Open(accountOfSam, bobId, 1000000.0M));

            await Task.Delay(1000);

            await endpoint.Send(new Transfer(Guid.NewGuid(), accountOfBob, accountOfSam, 1.0M));

            await Task.Delay(1000);

            await endpoint.Send(new QueryBalanceRequest(accountOfSam));

            string input = string.Empty;
            do
            {
                await endpoint.Send(new QueryBalanceRequest(accountOfSam));
                await endpoint.Send(new QueryBalanceRequest(accountOfBob));
                input = Console.ReadLine();
            } while (input != "q");
        }

        private static async Task TheFundraiser(IMessageSession endpoint)
        {
            var teslaId = Guid.NewGuid();
            var accountOfTesla = Guid.NewGuid();
            await endpoint.Send(new Open(accountOfTesla, teslaId, 0.0M));

            await Task.Delay(1000);

            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await endpoint.Send(new Deposit(Guid.NewGuid(), accountOfTesla, 1.0M));
                }));
            }

            Task.WaitAll(tasks.ToArray());

            string input = string.Empty;
            do {
                await endpoint.Send(new QueryBalanceRequest(accountOfTesla));
                input = Console.ReadLine();
            } while ( input != "q" );
        }

        private static async Task TheMillionairesGame(IMessageSession endpoint)
        {
            var transactionCount = 10;

            var samId = Guid.NewGuid();
            var bobId = Guid.NewGuid();
            var accountOfBob = Guid.NewGuid();
            var accountOfSam = Guid.NewGuid();

            await endpoint.Send(new Open(accountOfBob, bobId, 1000000.0M));
            await endpoint.Send(new Open(accountOfSam, samId, 1000000.0M));

            Console.WriteLine("Open accounts");
            Console.ReadLine();

            var bobToSam = Task.Run(async () =>
            {
                for (int i = 0; i < transactionCount; i++)
                {
                    await endpoint.Send(new Transfer(Guid.NewGuid(), accountOfBob, accountOfSam, 1.0M));
                }
            });

            var samToBob = Task.Run(async () =>
            {
                for (int i = 0; i < transactionCount; i++)
                {
                    await endpoint.Send(new Transfer(Guid.NewGuid(), accountOfSam, accountOfBob, 1.0M));
                }
            });

            Task.WaitAll(bobToSam, samToBob);

            string input = string.Empty;
            do
            {
                await endpoint.Send(new QueryBalanceRequest(accountOfSam));
                await endpoint.Send(new QueryBalanceRequest(accountOfBob));
                input = Console.ReadLine();
            } while (input != "q");


            await Task.Delay(1000);
        }
    }
}
