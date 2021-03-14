using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.DTOs.AccountDTOs.Request
{
    public class CreateWithdrawRequest : CreateRequest
    {
        public int Amount { get; set; }
    }
}
