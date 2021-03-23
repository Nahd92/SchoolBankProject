﻿using SchoolBankProject.Domain.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace SchoolBankProject.DTOs.CustomerDTOs.Request
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
    }
}
