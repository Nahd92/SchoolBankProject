using Newtonsoft.Json;
using SchoolBankProject.DTOs.CustomerDTOs.Request;
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
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();




        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(CreateCustomerRequest request)
        {
            var createCustomer = JsonConvert.SerializeObject(request);
            var content = new StringContent(createCustomer, Encoding.UTF8, "application/json");
            var requestUrl = "http://localhost:20000/CreateCustomer";
            var result = await _httpClient.PostAsync(requestUrl, content);
            return View(result);
        }
    }
}