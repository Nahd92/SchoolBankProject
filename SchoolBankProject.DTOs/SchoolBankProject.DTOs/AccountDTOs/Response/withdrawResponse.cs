using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.DTOs.AccountDTOs.Response
{
    public class WithdrawResponse
    {
        public float Balance { get; set; }
        public string WithdrawResponses { get; set; }
    }
}
