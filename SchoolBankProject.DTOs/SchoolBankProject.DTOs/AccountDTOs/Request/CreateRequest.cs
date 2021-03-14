using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.DTOs.AccountDTOs.Request
{
    public abstract class CreateRequest
    {
        public Guid Id { get; set; }
        public float Balance { get; set; }
        public string AccountNumber { get; set; }

        public string ClearingNumber { get; set; }
        public string IBANNumber { get; set; }

    }
}
