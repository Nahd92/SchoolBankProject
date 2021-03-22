using SchoolBankProject.Data;
using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using SchoolBankProject.Services.Repositories;
using SchoolBankProject.Services.Services;

namespace SchoolBankProject.Services.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private  IBankAccountRepository _AccountRepo;
        private  ICustomerRepository _customerRepo;
        private  ITransactionRepository _transactionRepo;
        private IBankAccountService _bankService;

        public IBankAccountRepository BankAccount
        {
            get 
            {
                if (_AccountRepo == null)
                {
                    _AccountRepo = new BankAccountRepository();
                }
                return _AccountRepo;
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (_customerRepo == null)
                {
                    _customerRepo = new CustomerRepository();
                }
                return _customerRepo;
            }
        }

        public ITransactionRepository Transactions
        {
            get
            {
                if (_transactionRepo == null)
                {
                    _transactionRepo = new TransactionsRepository();
                }
                return _transactionRepo;
            }
        }

        public IBankAccountService BankAccountService
        {
            get
            {
                if (_bankService == null)
                {
                    _bankService = new AccountServices();
                }
                return _bankService;
            }
        }
        public RepositoryWrapper(IBankAccountRepository accountRepo)
        {
         
            this._AccountRepo = accountRepo;
        }
    }
}
