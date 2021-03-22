using SchoolBankProject.DTOs.AccountDTOs.Response;
using SchoolBankProject.LinqSql.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<bool> TransferMoney(string accountNumber, string transferAccountNumber, int amount);
        float GetBalance(string accountNumber);
        BankAccount GetAccountById(int id);
        IEnumerable<BankAccount> GetAccounts();
        BankAccount CreateBankAccount(BankAccount account);
        AccountType GetAccountTypeByName(string name);
        bool Deposit(int Id, int amount);
        bool Withdraw(int id, int amount);
        bool AccountNumberExist(string accountNumber);
        bool IBANNumberExist(string Iban);
        string CreateAccountNumber();
        string CreateIBANNumber();
    }
}
