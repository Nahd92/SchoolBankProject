using SchoolBankProject.DTOs.AccountDTOs.Response;
using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using SchoolBankProject.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly LinqDataDataContext _database;
        private readonly AccountServices _accountService;
        public BankAccountRepository()
        {
            _database = new LinqDataDataContext();
            _accountService = new AccountServices();
        }


        public BankAccount CreateBankAccount(BankAccount account)
        {
            _database.BankAccounts.InsertOnSubmit(account);
            _database.SubmitChanges();
            return account;
        }

        public bool Deposit(int id, int amount)
        {
            var account = GetAccountById(id);
            if (_accountService.DepositIsPossible(account, amount))
                _database.SubmitChanges();
                    return true;
        }



        public bool Withdraw(int id, int amount)
        {
            var account = GetAccountById(id);
            if (_accountService.WithdrawIsPossible(account, amount))
                _database.SubmitChanges();
                 return true;
        }



        public string CreateAccountNumber()
        {
            var result = new int[6];
            var random = new Random();
            string resultString;
            do
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = random.Next(0, 9);
                }
                resultString = string.Join("", result);
            } while (AccountNumberExist(resultString));

            return resultString;
        }

        public string CreateIBANNumber()
        {
            var result = new int[20];
            var random = new Random();
            string resultString;
            do
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = random.Next(0, 9);
                }
                resultString = string.Join("", result);
            } while (IBANNumberExist(resultString));

            return resultString;
        }



        public bool AccountNumberExist(string accountNumber) => _database.BankAccounts.Any(x => x.AccountNumber == accountNumber);
        public bool IBANNumberExist(string Iban) => _database.BankAccounts.Any(x => x.IBANNumber == Iban);

        public BankAccount GetAccountById(int id) => _database.BankAccounts.SingleOrDefault(x => x.Id == id);

        public IEnumerable<BankAccount> GetAccounts() => _database.BankAccounts.ToList();

        public float GetBalance(string accountNumber) => _database.BankAccounts
                                .Where(x => x.AccountNumber == accountNumber).Select(x => x.Balance).FirstOrDefault();


        public Task<bool> TransferMoney(string accountNumber, string transferAccountNumber, int amount)
        {
            throw new NotImplementedException();
        }

        public AccountType GetAccountTypeByName(string name) =>
                                 _database.AccountTypes.Where(x => x.Type == name).FirstOrDefault();
    }
}
