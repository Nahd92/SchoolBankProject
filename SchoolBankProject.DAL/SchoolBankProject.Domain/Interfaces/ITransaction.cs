using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Domain.Interfaces
{
    public interface ITransaction
    {
         Guid Id { get; set; }
         string AccountNumber { get; set; }
         int Amount { get; set; }
         DateTime Date { get; set; }
         string Action { get; set; }
    }
}
