using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.DTOs.AccountDTOs.Request
{
    public class CreateBankAccountRequest
    {
        public string AccountType { get; set; }
        public int CustomerId { get; set; }
    }
}
