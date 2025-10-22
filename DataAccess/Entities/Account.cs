using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OwnerId { get; set; }
        public decimal Balance { get; set; } = 0m;

        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
