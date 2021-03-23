using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.DTOs.UserDTOs.Response
{
    public class UserLoggedInResponse
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ResponseMessage { get; set; }
    }
}
