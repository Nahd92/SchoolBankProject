using SchoolBankProject.DTOs.CustomerDTOs.Request;
using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LinqDataDataContext _database;
        public CustomerRepository()
        {
            _database = new LinqDataDataContext();
        }

        public Customer CreateCustomer(CreateCustomerRequest customerRequest)
        {
            var customer = new Customer()
            {
                FirstName = customerRequest.FirstName,
                LastName = customerRequest.LastName,
                Address = customerRequest.Address,
                Country = customerRequest.Country,
                PhoneNumber = customerRequest.PhoneNumber,
                PersonalNumber = customerRequest.PersonalNumber
            };

            _database.Customers.InsertOnSubmit(customer);
            _database.SubmitChanges();
            return customer;
        }

        public void DeleteCustomer(int id)
        {
            var customer =  GetCustomerById(id);
            _database.Customers.DeleteOnSubmit(customer);
            _database.SubmitChanges();
        }

        public IEnumerable<Customer> GetAllCustomers() =>  _database.Customers.ToList();

        public Customer GetCustomerById(int id) =>  _database.Customers.FirstOrDefault(x => x.Id == id);

        public IEnumerable<string> GetCustomersBankAccounts(int id) => 
                    _database.BankAccounts.Where(x => x.CustomerId == id).Select(x => x.AccountNumber).ToList();


        //public float GetBalance(string accountNumber) => _database.BankAccounts
        //                       .Where(x => x.AccountNumber == accountNumber).Select(x => x.Balance).FirstOrDefault();

        public void UpdateCustomer(int id, Customer customers)
        {
            var customer = GetCustomerById(id);

            if (customer != null)
                customer.FirstName = customers.FirstName;
                customer.LastName = customers.LastName;
                customer.Address = customers.Address;
                customer.PersonalNumber = customers.PersonalNumber;
                customer.PhoneNumber = customers.PhoneNumber;
            _database.SubmitChanges();
        }
    }
}
