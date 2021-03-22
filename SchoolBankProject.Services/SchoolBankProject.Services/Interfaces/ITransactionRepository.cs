using SchoolBankProject.LinqSql.Data;
using System;

namespace SchoolBankProject.Services.Interfaces
{
    public interface ITransactionRepository
    {
        Transaction AddTransaction(string accountNumber, int amount, DateTime date, int bankAccountId, string action);
    }
}
