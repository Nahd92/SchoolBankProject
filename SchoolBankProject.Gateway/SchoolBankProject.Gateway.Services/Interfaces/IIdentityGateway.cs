using SchoolBankProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Gateway.Services.Interfaces
{
    public interface IIdentityGateway
    {
        Task<HttpResponseMessage> RegisterUser(RegisterRequest request);
    }
}
