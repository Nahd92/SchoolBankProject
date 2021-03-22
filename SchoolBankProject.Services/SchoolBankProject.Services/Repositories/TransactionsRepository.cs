using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Repositories
{
    public class TransactionsRepository : ITransactionRepository
    {
        private readonly LinqDataDataContext _database;
        public TransactionsRepository()
        {
            _database = new LinqDataDataContext();
        }

        public Transaction AddTransaction(string accountNumber, int amount, DateTime date, int bankAccountId, string action)
        {

            var transaction = new Transaction()
            {
                AccountNumber = accountNumber,
                Amount = amount,
                Date = date,
                BankAccountId = bankAccountId,
                Action = action
            };
            _database.Transactions.InsertOnSubmit(transaction);
            _database.SubmitChanges();

            return transaction;
        }
    }
}
