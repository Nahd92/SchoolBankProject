using Microsoft.AspNet.Identity.EntityFramework;
using SchoolBankProject.Data.AppliationUser;
using SchoolBankProject.Domain.AccountModels;
using SchoolBankProject.Domain.AccountTypes;
using SchoolBankProject.Domain.CustomerModels;
using SchoolBankProject.Domain.TransactionModels;
using System.Data.Entity;

namespace SchoolBankProject.Data
{
    public class SchoolBankContext : IdentityDbContext<ApplicationUser>
    { 
        public SchoolBankContext() : base("SchoolBankDatabase")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AccountType> AccountType { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public static SchoolBankContext Create()
        {
            return new SchoolBankContext();
        }
    }
}
