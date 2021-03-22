using SchoolBankProject.Domain.AccountModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Domain.Interfaces
{
    public interface IBankAccount
    {
         int Id { get; set; }
         float Balance { get; set; }
         string AccountNumber { get; set; }
         string ClearingNumber { get; set; }     
         string IBANNumber { get; set; }

    }
}
