using SchoolBankProject.LinqSql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.DTOs.AccountDTOs.Response
{
    public class BankAccountResponse
    {
        public int Id { get; set; }
        public float Balance { get; set; }
        public string AccountNumber { get; set; }
        public string ClearingNumber { get; set; }
        public string IBANNumber { get; set; }
        public string AccountTypeName { get; set; }
        public string customerName { get; set; }
    }


    public static class CreatedBankAccountResponse
    {
        public static BankAccountResponse CreatedResponse(Customer customer, BankAccount bankaccount)
        {
            var response = new BankAccountResponse
            {
                Id = bankaccount.Id,
                AccountNumber = bankaccount.AccountNumber,
                AccountTypeName = bankaccount.AccountType.Type,
                IBANNumber = bankaccount.IBANNumber,
                Balance = bankaccount.Balance,
                ClearingNumber = bankaccount.ClearingNumber,
                customerName = $"Firstname {customer.FirstName} and lastname {customer.LastName}",
            };

            return response;
        }
    }
}
