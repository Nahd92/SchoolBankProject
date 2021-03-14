using SchoolBankProject.Domain.AccountModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Interfaces
{
    public interface IAccountServices
    {
        Task<bool> TransferMoney(string accountNumber, string transferAccountNumber, int amount);
        Task<bool> Deposit(Guid Id, BankAccount bankAccount, int amount);
        Task<bool> Withdraw(Guid id, BankAccount bankAccount, int amount);
        Task<float> GetBalance(string accountNumber);
        Task<BankAccount> GetAccountById(Guid id);
        Task<IEnumerable<BankAccount>> GetAccounts();
        string CreateAccountNumber();
        string ReturnClearingNumber();
        string CreateIBANNumber();
        Task<BankAccount> CreateBankAccount(BankAccount account);
        bool AccountNumberExist(string accountNumber);
        bool IBANNumberExist(string Iban);
        Task<AccountType> GetAccountTypeByName(string name);


    }
}
