using System;

namespace SchoolBankProject.Services.Interfaces
{
    public interface ITransactionServices
    {
        bool AddTransaction(string accountNumber, int amount, DateTime date, Guid bankAccountId, string action);
    }
}
