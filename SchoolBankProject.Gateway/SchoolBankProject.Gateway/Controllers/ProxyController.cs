using SchoolBankProject.DTOs.AccountDTOs.Request;
using SchoolBankProject.DTOs.CustomerDTOs.Request;
using SchoolBankProject.Gateway.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SchoolBankProject.Gateway.Controllers
{

    public class ProxyController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();


        private readonly IAccountGateway _accountServices;
        private readonly ICustomerGateway _customerService;
        public ProxyController(IAccountGateway accountServices, ICustomerGateway customerService)
        {
            _accountServices = accountServices;
            _customerService = customerService;
        }


        //Account
        [HttpGet]
        public async Task<ActionResult> GetAccounts() => await ProxyTo("https://localhost:44303/api/Accounts");
        [HttpGet]
        public async Task<ActionResult> GetAccount(Guid id) => await ProxyTo("https://localhost:44303/api/Accounts" + $"/{id}");


        //Customers
        [HttpGet]
        public async Task<ActionResult> GetCustomers() => await ProxyTo("https://localhost:44319/api/Customers");
        [HttpGet]
        public async Task<ActionResult> GetCustomer(Guid id) => await ProxyTo("https://localhost:44319/api/Customers" + $"/{id}");

        private async Task<ContentResult> ProxyTo(string url) => Content(await _httpClient.GetStringAsync(url));




        //POST 


        //Account
        [HttpPost]
        public async Task<HttpResponseMessage> Withdraw(CreateWithdrawRequest request) => await _accountServices.WithdrawMoney(request);
        [HttpPost]
        public async Task<HttpResponseMessage> Deposit(CreateDepositRequest request) => await _accountServices.DepositMoney(request);
        [HttpPost]
        public async Task<HttpResponseMessage> Transfer(CreateDepositRequest request) => await _accountServices.TransferMoney(request);

        
        //Customers
        [HttpPost]
        public async Task<HttpResponseMessage> CreateCustomer(CreateCustomerRequest request) => await _customerService.CreateNewCustomer(request);
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteCustomer(Guid id) => await _customerService.DeleteACustomer(id);
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateCustomer(UpdateCustomerRequest request) => await _customerService.UpdateACustomer(request);

    }
}