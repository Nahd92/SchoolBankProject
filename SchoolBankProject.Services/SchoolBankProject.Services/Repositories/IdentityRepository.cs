using SchoolBankProject.LinqSql.Data;
using SchoolBankProject.Services.Interfaces;
using SchoolBankProject.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Services.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly LinqDataDataContext _database;
        private readonly UserService _userService;
        public IdentityRepository()
        {
            _userService = new UserService();
            _database = new LinqDataDataContext();
        }


        public bool Login(string email, string password)
        {
            var loginResult = _database.UserLogins.Where(x => x.Email == email && 
                                x.Password == _userService.EncryptPassword(password)).FirstOrDefault();
   
            if (loginResult != null)
                return true;

            return false;
        }

        public bool Register(string email, string password)
        {
            if (EmailAlreadyExists(email))
                return false;
            
            var user = new UserLogin()
            {
                Email = email,
                Password = _userService.EncryptPassword(password)
            };
            _database.UserLogins.InsertOnSubmit(user);
            _database.SubmitChanges();

            return true;
        }


        public bool EmailAlreadyExists(string email)
        {
            if (_database.UserLogins.Any(x => x.Email == email))
                    return true;

            return false;
        }
    }
}
