using Newtonsoft.Json;
using SchoolBankProject.Domain.Models.UserModels;
using SchoolBankProject.Gateway.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Gateway.Services.IdentityService
{
    public class IdentityGatewayServices : IIdentityGateway
    {
        public async Task<HttpResponseMessage> RegisterUser(RegisterRequest request)
        {
            using (var _httpClient = new HttpClient())
            {
                var customer = JsonConvert.SerializeObject(request);
                var content = new StringContent(customer, Encoding.UTF8, "Application/json");
                var CustomerUrl = "https://localhost:44351/api/Identity";
                var customeREsult = await _httpClient.PostAsync(CustomerUrl, content);
                return customeREsult;
            }        
        }
    }
}
