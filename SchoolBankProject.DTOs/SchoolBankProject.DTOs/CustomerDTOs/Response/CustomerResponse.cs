using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.DTOs.CustomerDTOs.Response
{
    public abstract class CustomerResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int PhoneNumber { get; set; }

        public float Balance { get; set; }
        public string AccountNumber { get; set; }
        public string ClearingNumber { get; set; }
        public string IBANNumber { get; set; }
    }
}
