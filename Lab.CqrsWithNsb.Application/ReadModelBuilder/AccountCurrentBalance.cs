using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.CqrsWithNsb.Application.ReadModelBuilder
{
    public class AccountCurrentBalance
    {

        public Guid AccountId { get; set; }

        public decimal Balance { get; set; }


    }
}
