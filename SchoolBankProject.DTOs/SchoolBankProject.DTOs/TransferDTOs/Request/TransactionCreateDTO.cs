using SchoolBankProject.Domain.Enums;
using System;

namespace SchoolBankProject.DTOs.TransactionsDTOs.Request
{
    public class TransactionCreateDTO
    {
       public int CustomerId { get; set; }
        public float AccountNumber { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
    }
}
