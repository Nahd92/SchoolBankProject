using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.DTOs.AccountDTOs.Response
{
    public class TransactionReponse
    {
        public string AccountNumber { get; set; }
        public float Balance { get; set; }
        public int Amount { get; set; }
        public string Date { get; set; }
        public int BankAccountId { get; set; }
        public string Action { get; set; }
    }
}
