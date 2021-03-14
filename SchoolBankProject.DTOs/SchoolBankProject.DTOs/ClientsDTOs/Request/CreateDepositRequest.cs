using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.DTOs.ClientsDTOs.Request
{
   public class CreateDepositRequest
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        
    }
}
