using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Lab.CqrsWithNsb.Application.Accounts.Messages;
using Lab.CqrsWithNsb.Application.Accounts.Messages.Commands;
using Lab.CqrsWithNsb.Application.EventStore;
using Lab.CqrsWithNsb.Application.EventStore.Messages.Events;
using Lab.CqrsWithNsb.Application.ReadModelBuilder;
using Lab.CqrsWithNsb.Application.Transactions.Messages.Commands;
using Lab.CqrsWithNsb.Application.Transactions.Messages.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Lab.CqrsWithNsb.Endpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               
                .UseNServiceBus(context =>
                {
                    var endpointConfiguration = new EndpointConfiguration("Lab.CqrsWithNsb.Endpoint");

                    var transport = endpointConfiguration.UseTransport<LearningTransport>();

                    var routing = transport.Routing();
                    routing.RouteToEndpoint(typeof(QueryBalanceRequest).Assembly, "Lab.CqrsWithNsb.Endpoint");

                    var presistence = endpointConfiguration.UsePersistence<SqlPersistence>();
                    presistence.ConnectionBuilder(() => new SqlConnection("Server=(local);Database=LabCqrsNsb;Trusted_Connection=true"));
                    presistence.SqlDialect<SqlDialect.MsSqlServer>();
                    

                    var conventions = endpointConfiguration.Conventions();
                    conventions.DefiningMessagesAs(t => t.Namespace?.EndsWith("Messages") ?? false);
                    conventions.DefiningCommandsAs(t => t.Namespace?.Contains("Commands") ?? false);
                    conventions.DefiningEventsAs(t => t.Namespace?.Contains("Events") ?? false);

                    endpointConfiguration.EnableInstallers();

                    return endpointConfiguration;
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<ReadModelContext>(options =>
                    {
                        options.UseSqlServer("Server=(local);Database=LabCqrsNsb;Trusted_Connection=true");
                    });
                    services.AddHostedService<Worker>();
                });


     
    }
}
