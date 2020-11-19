using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lab.CqrsWithNsb.Application.ReadModelBuilder
{
    public class ReadModelContextFactory : IDesignTimeDbContextFactory<ReadModelContext>
    {
        public ReadModelContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReadModelContext>();
            optionsBuilder.UseSqlServer("Server=(local);Database=LabCqrsNsb;Trusted_Connection=true");

            return new ReadModelContext(optionsBuilder.Options);
        }
    }
}