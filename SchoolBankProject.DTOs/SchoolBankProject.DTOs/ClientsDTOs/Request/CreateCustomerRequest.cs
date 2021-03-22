using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.DTOs.ClientsDTOs.Request
{
   public class CreateCustomerRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string PersonalNumber { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
