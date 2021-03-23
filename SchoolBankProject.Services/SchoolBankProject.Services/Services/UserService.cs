using SchoolBankProject.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Services
{

    public class UserService : IUserService
    {
        //VäRRRY BAD Encryptione
        public string EncryptPassword(string password)
        {
            var b = ASCIIEncoding.ASCII.GetBytes(password);
            var encryptPassword = Convert.ToBase64String(b);
            return encryptPassword;
        }


        //VäRRRY BAD Decryptione
        public string DecryptPassword(string password)
        {
            var b = Convert.FromBase64String(password);
            var decryptedPassword = ASCIIEncoding.ASCII.GetString(b);
            return decryptedPassword;
        }

 
    }
}
