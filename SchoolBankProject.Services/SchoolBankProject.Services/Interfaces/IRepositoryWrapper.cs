namespace SchoolBankProject.Services.Interfaces
{
    public interface IRepositoryWrapper
    {
        IBankAccountRepository BankAccount { get; }
        ICustomerRepository Customers { get; }
        ITransactionRepository Transactions { get; }
        IBankAccountService BankAccountService { get; }
    }
}
