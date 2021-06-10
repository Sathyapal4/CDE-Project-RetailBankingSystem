using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AccountManagementModule.Models
{
    public partial class AccountDBContext:DbContext
    {
        public AccountDBContext(DbContextOptions<AccountDBContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public DbSet<Statement> Statements { get; set; }
    }
}
