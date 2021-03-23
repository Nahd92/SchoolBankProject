using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SchoolBankProject.Data.AppliationUser;
using SchoolBankProject.Domain.Models.UserModels;
using SchoolBankProject.Domain.Routes;
using SchoolBankProject.DTOs.UserDTOs.Request;
using SchoolBankProject.DTOs.UserDTOs.Response;
using SchoolBankProject.Identity.App_Start;
using SchoolBankProject.Services.Interfaces;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SchoolBankProject.Identity.Controllers
{


    public class IdentityController : ApiController
    {
        private readonly IRepositoryWrapper _repository;
        public IdentityController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        
        [HttpPost]
        [Route(RoutesAPI.Identity.Login)]
        public IHttpActionResult Login(LoginRequest request)
        {
            var IsLoggedIn = _repository.Identity.Login(request.Email, request.Password);

            if (IsLoggedIn)
            {
                var response = new UserLoggedInResponse
                {
                    Email = request.Email,
                    Password = request.Password,
                    ResponseMessage = "Logged In"
                };
                return Ok(response);
            }
          
            return BadRequest("Something happened, Try to login again!");
        }


        [HttpPost]
        [Route(RoutesAPI.Identity.Register)]
        public IHttpActionResult Register(RegisterRequest request)
        {
            //This happening on client side? 
            if (request.Password != request.ConfirmPassword)
                return BadRequest("Passwords not equal");

            var registered = _repository.Identity.Register(request.Email, request.Password);

            if (registered)
            {
                var response = new UserRegisteredResponse
                {
                    Email = request.Email,
                    Password = request.Password,
                    ResponseMessage = "You are registered"
                };
                return Json(response);
            }

            return BadRequest("Was not possible to register, try again");             
        }
    }
}