using SchoolBankProject.LinqSql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Interfaces
{
    public interface IBankAccountService
    {
        string ReturnClearingNumber();
        bool DepositIsPossible(BankAccount bankAccount, int amount);
        bool WithdrawIsPossible(BankAccount bankAccount, int amount);
        float CalculateWithdrawFee(BankAccount bankAccount);
    }
}
