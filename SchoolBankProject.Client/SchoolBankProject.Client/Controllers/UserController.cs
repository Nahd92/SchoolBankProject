using SchoolBankProject.Domain.Models.UserModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SchoolBankProject.Client.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login model, string returnUrl)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
    }
}