using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SchoolBankProject.Data.AppliationUser;
using SchoolBankProject.Domain.Models.UserModels;
using SchoolBankProject.Identity.App_Start;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SchoolBankProject.Identity.Controllers
{
    [RoutePrefix("api/Identity")]
    public class IdentityController : ApiController
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public IdentityController()
        {

        }

        public IdentityController(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext()
                    .Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Register(RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
                var result = await UserManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return Ok();
                }
                AddErrors(result);

            }
            return BadRequest("Could not register User");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}