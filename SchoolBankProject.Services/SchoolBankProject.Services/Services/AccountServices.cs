using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using SchoolBankProject.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Services
{
    public class AccountServices : IBankAccountService
    {
       
        public bool DepositIsPossible(BankAccount bankAccount, int amount)
        {
            if (bankAccount == null)
                return false;

            bankAccount.Balance += amount;
                return true;
        }

        public bool WithdrawIsPossible(BankAccount bankAccount, int amount)
        {
            if (bankAccount.Balance < amount)
                return false;

            CalculateWithdrawFee(bankAccount);

            bankAccount.Balance -= amount;
                return true;
        }


        public string ReturnClearingNumber() => "8550";



        private float CalculateWithdrawFee(BankAccount account) 
        {
            switch (account.AccountType.Id)
            {
                case 1:
                    return account.Balance -= 100;
                case 2:
                    return account.Balance -= 50;
                case 3:
                    return account.Balance -= 25;
                default:
                    return account.Balance -= 0;
            }
        }
    }
}
