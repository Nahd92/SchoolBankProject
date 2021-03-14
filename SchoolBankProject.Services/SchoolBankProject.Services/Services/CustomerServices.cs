using SchoolBankProject.Data;
using SchoolBankProject.Domain.CustomerModels;
using SchoolBankProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly SchoolBankContext _database;
        public CustomerServices(SchoolBankContext database)
        {
            _database = database;
        }

        public async Task<bool> CreateCustomer(Customer customer)
        {
            _database.Customers.Add(customer);
           var created = await _database.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteCustomer(Guid id)
        {
            var customer = await GetCustomerById(id);
           
            if (customer == null)
                return false;

            _database.Customers.Remove(customer);
            var deleted = await _database.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers() => await _database.Customers.ToListAsync();

        public async Task<Customer> GetCustomerById(Guid id) => await _database.Customers.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<bool> UpdateCustomer(Guid id, Customer customers)
        {
            var customer = await GetCustomerById(id);

            if (customer == null)
                return false;

            _database.Entry(customer).CurrentValues.SetValues(customers);
            var updated = await _database.SaveChangesAsync();
            return updated > 0;        
        }
    }
}
