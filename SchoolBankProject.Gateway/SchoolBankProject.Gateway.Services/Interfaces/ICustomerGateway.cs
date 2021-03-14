using SchoolBankProject.DTOs.CustomerDTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Gateway.Services.Interfaces
{
   public interface ICustomerGateway
    {
        Task<HttpResponseMessage> UpdateACustomer(UpdateCustomerRequest request);
        Task<HttpResponseMessage> DeleteACustomer(Guid id);
        Task<HttpResponseMessage> CreateNewCustomer(CreateCustomerRequest request);
    }
}
