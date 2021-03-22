using SchoolBankProject.LinqSql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.DTOs.CustomerDTOs.Response
{
    public static class CreateCustomerResponse
    {
        public static CreatedCustomerResponse Response(Customer customer, BankAccount bankAccount)
        {
            var response = new CreatedCustomerResponse
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Country = customer.Country,
                PhoneNumber = customer.PhoneNumber,
                AccountNumber = bankAccount.AccountNumber,
                Balance = bankAccount.Balance,
                ClearingNumber = bankAccount.ClearingNumber,
                IBANNumber = bankAccount.IBANNumber
            };

            return response;
        }
    }
}
