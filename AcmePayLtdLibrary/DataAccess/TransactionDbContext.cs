using AcmePayLtdLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AcmePayLtdLibrary.DataAccess
{
    public  class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {        
        }

        public DbSet<TransactionModel> Transactions { get; set; }
    }
}
