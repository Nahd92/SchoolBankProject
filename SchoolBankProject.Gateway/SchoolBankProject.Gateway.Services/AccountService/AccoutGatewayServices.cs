using Newtonsoft.Json;
using SchoolBankProject.DTOs.AccountDTOs.Request;
using SchoolBankProject.Gateway.Services.Interfaces;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBankProject.Gateway.Services.AccountService
{
    public class AccoutGatewayServices : IAccountGateway
    {

        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<HttpResponseMessage> TransferMoney(CreateDepositRequest request)
        {
            var transfer = JsonConvert.SerializeObject(request);
            var content = new StringContent(transfer, Encoding.UTF8, "Application/json");
            var requestUrl = "https://localhost:44303/api/Accounts";
            var result = await _httpClient.PostAsync(requestUrl, content);
            return result;
        }


        public async Task<HttpResponseMessage> DepositMoney(CreateDepositRequest request)
        {
            var deposit = JsonConvert.SerializeObject(request);
            var content = new StringContent(deposit, Encoding.UTF8, "Application/json");
            var requestUrl = "https://localhost:44303/api/Account/Deposit";
            var result = await _httpClient.PostAsync(requestUrl, content);
            return result;
        }


        public async Task<HttpResponseMessage> WithdrawMoney(CreateWithdrawRequest request)
        {
            var withdrew = JsonConvert.SerializeObject(request);
            var content = new StringContent(withdrew, Encoding.UTF8, "Application/json");
            var requstUrl = "https://localhost:44303/api/Account/Withdraw";
            var result = await _httpClient.PostAsync(requstUrl, content);
            return result;
        }

    }
}
