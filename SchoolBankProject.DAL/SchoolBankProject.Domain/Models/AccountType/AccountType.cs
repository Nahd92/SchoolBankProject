using SchoolBankProject.Domain.Interfaces;

namespace SchoolBankProject.Domain.AccountTypes
{ 
    public class AccountType : IAccountType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
