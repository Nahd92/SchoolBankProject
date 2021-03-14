using SchoolBankProject.Data;
using SchoolBankProject.Domain.AccountModels;
using SchoolBankProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly SchoolBankContext _database;
        public AccountServices(SchoolBankContext database)
        {
            _database = database;
        }

        public bool AccountNumberExist(string accountNumber) => _database.BankAccounts.Any(x => x.AccountNumber == accountNumber);
        public bool IBANNumberExist(string Iban) => _database.BankAccounts.Any(x => x.IBANNumber == Iban);


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


        public async Task<BankAccount> CreateBankAccount(BankAccount account)
        {
            var createdBankAccount = _database.BankAccounts.Add(account);
            await _database.SaveChangesAsync();
            return createdBankAccount;
        }

        public string ReturnClearingNumber() => "8550";

        public async Task<bool> Deposit(Guid Id, BankAccount bankAccount, int amount)
        {
            var account = await GetAccountById(Id);
          
            account.Balance += amount;

            _database.Entry(account).CurrentValues.SetValues(bankAccount);
            var deposited = await _database.SaveChangesAsync();
            return deposited > 0;
        }

        public async Task<BankAccount> GetAccountById(Guid id) => await _database.BankAccounts.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<BankAccount>> GetAccounts() => await _database.BankAccounts.ToListAsync();

        public async Task<float> GetBalance(string accountNumber) => await _database.BankAccounts
                                .Where(x => x.AccountNumber == accountNumber).Select(x => x.Balance).FirstOrDefaultAsync();


        public Task<bool> TransferMoney(string accountNumber, string transferAccountNumber, int amount)
        {
            //var transferAccount = AccountNumberExist(transferAccountNumber);

            throw new NotImplementedException();
        }

        public async Task<bool> Withdraw(Guid id, BankAccount bankAccount, int amount)
        {
            var account = await GetAccountById(id);

            if (account == null)
                return false;

            if (account.Balance < amount)
                    return false;

            account.Balance -= amount;

            _database.Entry(account).CurrentValues.SetValues(bankAccount);
            var withrewed = await _database.SaveChangesAsync();
            return withrewed > 0;
        }

        public async Task<AccountType> GetAccountTypeByName(string name) => 
                                await _database.AccountType.Where(x => x.Type == name).FirstOrDefaultAsync();
    }
}
