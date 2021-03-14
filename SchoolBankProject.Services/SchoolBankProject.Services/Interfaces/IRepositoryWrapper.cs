namespace SchoolBankProject.Services.Interfaces
{
    public interface IRepositoryWrapper
    {
        IAccountServices BankAccount { get; }
        ICustomerServices Customers { get; }
        ITransactionServices Transactions { get; }
    }
}
