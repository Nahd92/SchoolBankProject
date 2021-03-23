using SchoolBankProject.Data;
using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using SchoolBankProject.Services.Repositories;
using SchoolBankProject.Services.ServiceInterfaces;
using SchoolBankProject.Services.Services;

namespace SchoolBankProject.Services.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private  IBankAccountRepository _AccountRepo;
        private  ICustomerRepository _customerRepo;
        private  ITransactionRepository _transactionRepo;
        private IBankAccountService _bankService;
        private IIdentityRepository _identityRepo;
        private IUserService _userService;


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

        public IUserService UserService
        {
            get
            {
                if (_userService == null)
                {
                    _userService = new UserService();
                }
                return _userService;
            }
        }

        public IIdentityRepository Identity 
        {
            get
            {
                if (_identityRepo == null)
                {
                    _identityRepo = new IdentityRepository();
                }
                return _identityRepo;
            }
        }

        public RepositoryWrapper(IBankAccountRepository accountRepo)
        {
         
            this._AccountRepo = accountRepo;
        }
    }
}
