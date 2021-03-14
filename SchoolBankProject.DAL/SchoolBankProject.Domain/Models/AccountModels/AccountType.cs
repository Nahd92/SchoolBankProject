using SchoolBankProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Domain.AccountModels
{
    public class AccountType : IAccountType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
