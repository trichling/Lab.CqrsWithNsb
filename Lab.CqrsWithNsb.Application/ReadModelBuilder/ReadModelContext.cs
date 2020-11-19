using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.CqrsWithNsb.Application.ReadModelBuilder
{
    public class ReadModelContext : DbContext
    {

        public ReadModelContext(DbContextOptions<ReadModelContext> options)
            : base(options)
        {

        }

        public DbSet<AccountCurrentBalance> AccountsWithBalance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountCurrentBalance>()
                .HasKey(acb => acb.AccountId);
        }
    }
}
