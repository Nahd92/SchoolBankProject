using SchoolBankProject.Domain.CustomerModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Interfaces
{
    public interface ICustomerServices
    {
        Task<bool> CreateCustomer(Customer customer);
        Task<bool> UpdateCustomer(Guid id, Customer customer);
        Task<bool> DeleteCustomer(Guid id);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(Guid id);

    }
}
