using Newtonsoft.Json;
using SchoolBankProject.DTOs.CustomerDTOs.Request;
using SchoolBankProject.Gateway.Services.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Gateway.Services.CustomerService
{
    public class CustomerGatewayServices :  ICustomerGateway
    {
        private readonly HttpClient _httpClient = new HttpClient();
     

        public async Task<HttpResponseMessage> UpdateACustomer(UpdateCustomerRequest request)
        {
            var update = JsonConvert.SerializeObject(request);
            var content = new StringContent(update, Encoding.UTF8, "Application/json");
            var requestUrl = "https://localhost:44319/api/Customer" + $"/{request.Id}";
            var result = await _httpClient.PutAsync(requestUrl, content);
            return result;
        }

        public async Task<HttpResponseMessage> DeleteACustomer(Guid id)
        {
            var deleteId = JsonConvert.SerializeObject(id);
            var content = new StringContent(deleteId, Encoding.UTF8, "Application/json");
            var requestUrl = "https://localhost:44319/api/Customers" + $"/{deleteId}";
            var result = await _httpClient.PostAsync(requestUrl, content);
            return result;
        }


        public async Task<HttpResponseMessage> CreateNewCustomer(CreateCustomerRequest request)
        {
            var customer = JsonConvert.SerializeObject(request);
            var content = new StringContent(customer, Encoding.UTF8, "Application/json");
            var CustomerUrl = "https://localhost:44319/api/customer";
            var customeREsult = await _httpClient.PostAsync(CustomerUrl, content);
            return customeREsult;
        }
    }
}
