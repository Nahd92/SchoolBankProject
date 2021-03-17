using SchoolBankProject.Domain.Routes;
using SchoolBankProject.DTOs.AccountDTOs.Request;
using SchoolBankProject.DTOs.AccountDTOs.Response;
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
        public async Task<IHttpActionResult> GetBankAccounts()
        {
            var accounts = await _repositorywrapper.BankAccount.GetAccounts();

            if (accounts == null)
                return NotFound();

            return Json(accounts);
        }


        [HttpGet]
        [Route(RoutesAPI.Accounts.GetAccount)]
        public async Task<IHttpActionResult> GetBankAccount(Guid id)
        {
            var account = await _repositorywrapper.BankAccount.GetAccountById(id);

            if (account == null)
                return NotFound();

            return Json(account);
        }




        [HttpPost]
        [Route(RoutesAPI.Accounts.Deposit)]
        public async Task<IHttpActionResult> Deposit([FromBody] CreateDepositRequest request)
        {
            var account = await _repositorywrapper.BankAccount.GetAccountById(request.Id);

            var depositResult = await _repositorywrapper.BankAccount.Deposit(account.Id, account, request.Amount);

            if (depositResult)
            { 
                _repositorywrapper.Transactions.AddTransaction(account.AccountNumber, request.Amount, DateTime.Now, account.Id, nameof(Deposit));

                var response = new depositResponse()
                {
                    Balance = request.Amount,
                    SucessfullyDeposit = "Successfully deposit",
                };

                return Json(response);
            }

            return BadRequest("Something happened");
        }

        [HttpPost]
        [Route(RoutesAPI.Accounts.Withdraw)]
        public async Task<IHttpActionResult> Withdraw([FromBody] CreateWithdrawRequest request)
        {
            var account = await _repositorywrapper.BankAccount.GetAccountById(request.Id);

            if (account == null)
                return NotFound();

            var depositResult = await _repositorywrapper.BankAccount.Withdraw(account.Id, account, request.Amount);

            if (depositResult)
            {
                _repositorywrapper.Transactions.AddTransaction(account.AccountNumber, request.Amount, DateTime.Now, account.Id, nameof(Withdraw));

                var withdrawResponse = new withdrawResponse()
                {
                    Balance = account.Balance,
                    SucessfullyWithdraw = "Successfully withdraw"
                };
                return Json(withdrawResponse);
            }
            return BadRequest("Not enough money on your account!");
        }
    }
}