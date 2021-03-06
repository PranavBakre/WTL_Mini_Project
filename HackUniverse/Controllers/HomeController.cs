﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HackUniverse.Models;
using Microsoft.AspNetCore.Http;
using HackUniverse.Models.User;
using HackUniverse.Models.Hackathon_User_Interactions;
using System.Dynamic;

namespace HackUniverse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            ViewData["Name"]=HttpContext.Session.GetString("UName");
            ViewData["Type"] = HttpContext.Session.GetString("Type");
            UserContext userContext = HttpContext.RequestServices.GetService(typeof(UserContext)) as UserContext;
            HackathonContext context = HttpContext.RequestServices.GetService(typeof(HackathonContext)) as HackathonContext;
            Hackathon_UserContext HuContext = HttpContext.RequestServices.GetService(typeof(Hackathon_UserContext)) as Hackathon_UserContext;
            dynamic user=null;
            var UserHackathons = new List<Hackathon>();
            if (ViewData["Name"] != null)
            {
                user = userContext.GetUserByUserName(ViewData["Name"].ToString());
                var UserHackathonList = HuContext.GetUserHackathons(user);
                foreach (int i in UserHackathonList)
                {
                    UserHackathons.Add(context.GetByID(i));
                }

            }

            
            dynamic Model = new ExpandoObject();
            Model.UserHackathonList=UserHackathons ;
            Model.NextHackathons = context.GetNextHackathons();
            Model.PreviousHackathons = context.GetPreviousHackathons();
            Model.CurrentHackathons = context.GetCurrentHackathons();
            return View(Model);
        }


        public IActionResult Login()
        {
            ViewData["Name"] = HttpContext.Session.GetString("UName");
            ViewData["Type"] = HttpContext.Session.GetString("Type");
            if (ViewData["Name"] != null)
            {
                return Redirect("~/Home/Index");
            }
            return View();
        }

        public IActionResult Register()
        {
            ViewData["Name"] = HttpContext.Session.GetString("UName");
            ViewData["Type"] = HttpContext.Session.GetString("Type");
            if (ViewData["Name"] != null)
            {
                return Redirect("~/Home/Index");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Name"] = HttpContext.Session.GetString("UName");
            ViewData["Type"] = HttpContext.Session.GetString("Type");
            return View();
        }

        public IActionResult Search(string S)
        {
            ViewData["Name"] = HttpContext.Session.GetString("UName");
            ViewData["Type"] = HttpContext.Session.GetString("Type");

            HackathonContext hContext = HttpContext.RequestServices.GetService(typeof (HackathonContext)) as HackathonContext;
            
            return View(hContext.Search(S));
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
