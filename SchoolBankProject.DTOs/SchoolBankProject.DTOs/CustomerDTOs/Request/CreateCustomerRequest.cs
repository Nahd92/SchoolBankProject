using SchoolBankProject.Domain.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace SchoolBankProject.DTOs.CustomerDTOs.Request
{
    public class CreateCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public int PhoneNumber { get; set; }
        public string Type { get; set; }
        public string PersonalNumber { get; set; }

    }
}
