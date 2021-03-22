using SchoolBankProject.Domain.Routes;
using SchoolBankProject.DTOs.AccountDTOs.Request;
using SchoolBankProject.DTOs.AccountDTOs.Response;
using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace SchoolBankProjet.API.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IRepositoryWrapper _repositorywrapper;
        public AccountController(IRepositoryWrapper repositorywrapper)
        {
            _repositorywrapper = repositorywrapper;
        }

        [HttpGet]
        [Route(RoutesAPI.Accounts.GetAllAccounts)]
        public IHttpActionResult GetBankAccounts()
        {
            var accounts = _repositorywrapper.BankAccount.GetAccounts();

            if (accounts == null)
                return NotFound();

            return Json(accounts);
        }


        [HttpGet]
        [Route(RoutesAPI.Accounts.GetAccount)]
        public IHttpActionResult GetBankAccount(int id)
        {
            var account = _repositorywrapper.BankAccount.GetAccountById(id);

            if (account == null)
                return NotFound();

            return Json(account);
        }

        [HttpPost]
        [Route(RoutesAPI.Accounts.CreateAccount)]
        public IHttpActionResult Create([FromBody] CreateBankAccountRequest request)
        {
            var customer = _repositorywrapper.Customers.GetCustomerById(request.CustomerId);

            if (customer == null)
                return BadRequest("Theres no Customer with that Id");
            var accountType = _repositorywrapper.BankAccount.GetAccountTypeByName(request.AccountType);


            var newAccount = _repositorywrapper.BankAccount.CreateBankAccount(new BankAccount
            {
                Balance = 0,
                AccountNumber = _repositorywrapper.BankAccount.CreateAccountNumber(),
                ClearingNumber = _repositorywrapper.BankAccountService.ReturnClearingNumber(),
                CustomerId = customer.Id,
                IBANNumber = _repositorywrapper.BankAccount.CreateIBANNumber(),
                AccountTypeId = accountType.Id
            });

            var response = CreatedBankAccountResponse.CreatedResponse(customer, newAccount);
            return Json(response);
        }

        [HttpPost]
        [Route(RoutesAPI.Accounts.Deposit)]
        public IHttpActionResult Deposit([FromBody] CreateDepositRequest request)
        {
            var account =  _repositorywrapper.BankAccount.GetAccountById(request.Id);

            if (account == null)
                return NotFound();

            var IsDeposit = _repositorywrapper.BankAccount.Deposit(account.Id, request.Amount);
            var transaction = _repositorywrapper.Transactions.AddTransaction(
                                                              account.AccountNumber,
                                                              request.Amount,
                                                              DateTime.Now,
                                                              account.Id,
                                                              IsDeposit ? nameof(Deposit) : "Nothing have been deposit");

            var response = new TransactionReponse
            {
                AccountNumber = transaction.AccountNumber,
                Balance = account.Balance,
                Amount = transaction.Amount,
                Date = transaction.Date.ToString("dd-MM-yyyy"),
                BankAccountId = transaction.BankAccountId,
                Action =  transaction.Action
            };
            return Json(response);
        }

        [HttpPost]
        [Route(RoutesAPI.Accounts.Withdraw)]
        public  IHttpActionResult Withdraw([FromBody] CreateWithdrawRequest request)
        {
            var account =  _repositorywrapper.BankAccount.GetAccountById(request.Id);

            if (account == null)
                return NotFound();

            var IsWithdrawn = _repositorywrapper.BankAccount.Withdraw(account.Id, request.Amount);
            var transaction =  _repositorywrapper.Transactions.AddTransaction(
                                                                account.AccountNumber, 
                                                                request.Amount, 
                                                                DateTime.Now, 
                                                                account.Id,
                                                                IsWithdrawn ? nameof(Withdraw) : "Not enough money");


            var response = new TransactionReponse
            {
                AccountNumber = transaction.AccountNumber,
                Balance = account.Balance,
                Amount = transaction.Amount,
                Date = transaction.Date.ToString("dd-MM-yyyy"),
                BankAccountId = transaction.BankAccountId,
                Action = transaction.Action
            };

            return Json(response);
        }
    }
}