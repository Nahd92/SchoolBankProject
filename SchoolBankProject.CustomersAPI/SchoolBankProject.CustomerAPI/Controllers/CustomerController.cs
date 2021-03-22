using SchoolBankProject.Domain.Routes;
using SchoolBankProject.DTOs.CustomerDTOs.Request;
using SchoolBankProject.DTOs.CustomerDTOs.Response;
using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using System;
using System.Collections.Generic;
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
        public IHttpActionResult GetAllCustomers()
        {
            var customers =  _repository.Customers.GetAllCustomers();

            if (customers == null)
                return NotFound();

            return Json(customers);
        }

        [HttpGet]
        [Route(RoutesAPI.Customers.GetAllCustomersBankAccounts)]
        public IHttpActionResult GetAllCustomersBankAccounts(int id)
        {
            var customer = _repository.Customers.GetCustomersBankAccounts(id);

            var response = new List<string>();
            foreach (var c in customer)
            {
                response.Add($"BankAccount: {c}");
            }
            return Json(response);
        }

        [HttpGet]
        [Route(RoutesAPI.Customers.GetCustomerById)]
        public IHttpActionResult GetById(int? id)
        {
            var customer = _repository.Customers.GetCustomerById((int)id);

            if (customer == null)
                return NotFound();

            return Json(customer);
        }

        [HttpPost]
        [Route(RoutesAPI.Customers.CreateCustomer)]
        public IHttpActionResult CreateCustomer([FromBody] CreateCustomerRequest createCustomerRequest)
        {

          var createdCustomer =  _repository.Customers.CreateCustomer(createCustomerRequest);
          var accountType = _repository.BankAccount.GetAccountTypeByName(createCustomerRequest.Type);

           if (accountType == null)
                return BadRequest("No AccountType exist with that name");


            var createdBankAccount = _repository.BankAccount.CreateBankAccount(
                 new BankAccount()
                 {
                     Balance = 0,
                     AccountNumber = _repository.BankAccount.CreateAccountNumber(),
                     ClearingNumber = _repository.BankAccountService.ReturnClearingNumber(),
                     CustomerId = createdCustomer.Id,
                     IBANNumber = _repository.BankAccount.CreateIBANNumber(),
                     AccountTypeId = accountType.Id,
                 });


           var response = CreateCustomerResponse.Response(createdCustomer, createdBankAccount);
            return Ok(response);
        }
    }
}
