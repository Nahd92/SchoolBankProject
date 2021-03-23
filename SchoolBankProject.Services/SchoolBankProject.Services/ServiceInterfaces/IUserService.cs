using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.ServiceInterfaces
{
    public interface IUserService
    {
        string EncryptPassword(string password);
        string DecryptPassword(string password);
    }
}
