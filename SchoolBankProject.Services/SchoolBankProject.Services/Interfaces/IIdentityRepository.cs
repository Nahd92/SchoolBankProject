using SchoolBankProject.LinqSql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Interfaces
{
    public interface IIdentityRepository
    {
        bool EmailAlreadyExists(string email);
        bool Register(string email, string password);
        bool Login(string email, string password);
    }
}
