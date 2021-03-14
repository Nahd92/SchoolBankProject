using Microsoft.AspNet.Identity.EntityFramework;
using SchoolBankProject.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolBankProject.Domain.CustomerModels
{
    public class Customer : ICustomer
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int PhoneNumber { get; set; }

    }
}
