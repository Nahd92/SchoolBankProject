using Newtonsoft.Json;
using SchoolBankProject.DTOs.ClientsDTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchoolBankProject.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();



        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Deposit(CreateDepositRequest request)
        {
            var depositRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(depositRequest, Encoding.UTF8, "application/json");
            var requestUrl = "http://localhost:20000/Deposit";
            var result = await _httpClient.PostAsync(requestUrl, content);
            return result;
        }

        [HttpGet]
        public ActionResult MyAccount()
        {
            return View();
        }
    }
}
