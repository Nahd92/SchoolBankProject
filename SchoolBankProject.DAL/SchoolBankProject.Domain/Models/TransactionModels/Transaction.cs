using SchoolBankProject.Domain.AccountModels;
using SchoolBankProject.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolBankProject.Domain.TransactionModels
{

    public class Transaction : ITransaction
    {
        [Key]
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string AccountNumber { get; set; }
        public string Action { get; set; }

        public Guid BankAccountId { get; set; }
        public BankAccount BankAccounts { get; set; }

    }
}
