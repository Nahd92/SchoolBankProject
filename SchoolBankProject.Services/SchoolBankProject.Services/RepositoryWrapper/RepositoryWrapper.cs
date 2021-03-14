using SchoolBankProject.Data;
using SchoolBankProject.Services.Interfaces;
using SchoolBankProject.Services.Services;

namespace SchoolBankProject.Services.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly SchoolBankContext _database;
        private  IAccountServices _AccountService;
        private  ICustomerServices _customerService;
        private  ITransactionServices _transactionService;


        public IAccountServices BankAccount
        {
            get 
            {
                if (_AccountService == null)
                {
                    _AccountService = new AccountServices(_database);
                }
                return _AccountService;
            }
        }

        public ICustomerServices Customers
        {
            get
            {
                if (_customerService == null)
                {
                    _customerService = new CustomerServices(_database);
                }
                return _customerService;
            }
        }

        public ITransactionServices Transactions
        {
            get
            {
                if (_transactionService == null)
                {
                    _transactionService = new TransactionsService(_database);
                }
                return _transactionService;
            }
        }

        public RepositoryWrapper(SchoolBankContext database)
        {
            _database = database;
        }
    }
}
