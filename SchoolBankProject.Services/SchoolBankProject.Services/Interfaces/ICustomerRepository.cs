using SchoolBankProject.DTOs.CustomerDTOs.Request;
using SchoolBankProject.LinqSql.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Interfaces
{
    public interface ICustomerRepository
    {
        Customer CreateCustomer(CreateCustomerRequest customer);
        void UpdateCustomer(int id, Customer customer);
        void DeleteCustomer(int id);
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<string> GetCustomersBankAccounts(int id);
        Customer GetCustomerById(int id);

    }
}
