using Microsoft.AspNet.Identity.EntityFramework;
using SchoolBankProject.Domain.CustomerModels;
using SchoolBankProject.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolBankProject.Domain.AccountModels
{
    public  class BankAccount : IBankAccount
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public float Balance { get; set;  }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        public string ClearingNumber { get; set; }
        [Required]
        public string IBANNumber { get; set; }



        public int AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }


        public Guid CustomerId { get; set; }
        public Customer Customers { get; set; }

    }
}
