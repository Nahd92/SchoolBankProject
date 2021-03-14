using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Domain.Interfaces
{
    public interface ICustomer
    {
         Guid Id { get; set; }
         string FirstName { get; set; }
         string LastName { get; set; }
         string Address { get; set; }
         string Country { get; set; }
         int PhoneNumber { get; set; }
    }
}
