using SchoolBankProject.Domain.AccountModels;
using SchoolBankProject.Domain.CustomerModels;
using SchoolBankProject.Domain.Routes;
using SchoolBankProject.DTOs.CustomerDTOs.Request;
using SchoolBankProject.DTOs.CustomerDTOs.Response;
using SchoolBankProject.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchoolBankProject.CustomerAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly IRepositoryWrapper _repository;
        public CustomerController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [Route(RoutesAPI.Customers.GetAllCustomers)]
        public async Task<IHttpActionResult> GetAllCustomers()
        {
            var customers = await _repository.Customers.GetAllCustomers();

            if (customers == null)
                return NotFound();

            return Json(customers);
        }

        [HttpGet]
        [Route(RoutesAPI.Customers.GetCustomerById)]
        public async Task<IHttpActionResult> GetById(Guid? id)
        {
            var customer = await _repository.Customers.GetCustomerById((Guid)id);

            if (customer == null)
                return NotFound();

            return Json(customer);
        }

        [HttpPost]
        [Route(RoutesAPI.Customers.CreateCustomer)]
        public async Task<IHttpActionResult> CreateCustomer([FromBody] CreateCustomerRequest createCustomerRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest("Some fields was not inputed");

            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                FirstName = createCustomerRequest.FirstName,
                LastName = createCustomerRequest.LastName,
                Address = createCustomerRequest.Address,
                Country = createCustomerRequest.Country,
                PhoneNumber = createCustomerRequest.PhoneNumber
            };

            var created = await _repository.Customers.CreateCustomer(customer);

            if (!created)
                return BadRequest("Something went wrong in creating customer");


            var accountType = await _repository.BankAccount.GetAccountTypeByName(createCustomerRequest.Type);

            if (accountType == null)
                return BadRequest("No AccountType exist with that name");


            var createdBankAccount = await _repository.BankAccount.CreateBankAccount(
                 new BankAccount()
                 {
                     Id = Guid.NewGuid(),
                     Balance = 0,
                     AccountNumber = _repository.BankAccount.CreateAccountNumber(),
                     ClearingNumber = _repository.BankAccount.ReturnClearingNumber(),
                     CustomerId = customer.Id,
                     IBANNumber = _repository.BankAccount.CreateIBANNumber(),
                     AccountTypeId = accountType.Id,
                 });

            var response = new CreatedCustomerResponse
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Country = customer.Country,
                PhoneNumber = customer.PhoneNumber,
                AccountNumber = createdBankAccount.AccountNumber,
                Balance = createdBankAccount.Balance,
                ClearingNumber = createdBankAccount.ClearingNumber,
                IBANNumber = createdBankAccount.IBANNumber
            };

            return Json(response);
        }
    }
}
