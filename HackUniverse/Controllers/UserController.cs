using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackUniverse.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HackUniverse.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login(string username,string password)
        {

            UserContext context = HttpContext.RequestServices.GetService(typeof(UserContext)) as UserContext;
            var loginSuccess = context.LoginRequest(username, password);
            if (loginSuccess)
            {
                
                HttpContext.Session.SetString("UName",username);
                return Redirect("~/Home/Index");
            }
            return Redirect("~/Home/Login");
        }
        public IActionResult Register(string username,string email,string password, string FirstName,string LastName, string Occupation,
            string OrganizationName,string ContactPhone,object ProfilePicture,char UserType)
        {
            UserContext uContext = HttpContext.RequestServices.GetService(typeof(UserContext)) as UserContext;
            if (uContext.RegisterUser(username, password, email, FirstName, LastName, Occupation, OrganizationName,ContactPhone, ProfilePicture, UserType) == true)
            {
                return Redirect("~/Home/Login");
            }
            return Redirect("~/Home/Register");
        }
    }
}