using SchoolBankProject.Data;
using SchoolBankProject.Domain.TransactionModels;
using SchoolBankProject.Services.Interfaces;
using System;

namespace SchoolBankProject.Services.Services
{
    public class TransactionsService : ITransactionServices
    {
        private readonly SchoolBankContext _database;
        public TransactionsService(SchoolBankContext database)
        {
            _database = database;
        }

        public bool AddTransaction(string accountNumber, int amount, DateTime date, Guid bankAccountId, string action)
        {
            var transaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                AccountNumber = accountNumber,
                Amount = amount,
                Date = date,
                BankAccountId = bankAccountId,
                Action = action
            };
            _database.Transactions.Add(transaction);
            var saved = _database.SaveChanges();
            return saved > 0;
        }
    }
}
