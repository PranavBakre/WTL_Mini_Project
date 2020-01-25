using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using HackUniverse.Models;
using HackUniverse.Models.ProblemStatements;
using System.Dynamic;
using Microsoft.AspNetCore.Http;
using HackUniverse.Models.Hackathon_User_Interactions;
using HackUniverse.Models.User;

namespace HackUniverse.Controllers
{
    public class HackathonController : Controller
    {

        ILogger _logger;
        

        
 

        public HackathonController(ILogger<HackathonController> logger)
        {
            _logger = logger;
        }


        public IActionResult Add(string user)
        {
            ViewData["Name"] = HttpContext.Session.GetString("UName");
            ViewData["Type"] = HttpContext.Session.GetString("Type");
            UserContext uContext = HttpContext.RequestServices.GetService(typeof(UserContext)) as UserContext;
            return View(uContext.GetUserByUserName(user));
        }

        public IActionResult AddHackathon(string username, string title, string subtitle, string description, string contactMail, string contactPhone,
            string contactWebsite, byte[] coverPhoto, byte[] thumbnail, DateTime startDate, DateTime endDate)
        {
            ViewData["Name"] = HttpContext.Session.GetString("UName");
            ViewData["Type"] = HttpContext.Session.GetString("Type");

            Hackathon_UserContext hucontext = HttpContext.RequestServices.GetService(typeof(Hackathon_UserContext)) as Hackathon_UserContext;
            UserContext uContext = HttpContext.RequestServices.GetService(typeof(UserContext)) as UserContext;

            if (hucontext.AddHackathon(uContext.GetUserByUserName(username),new Hackathon{
                Title=title,
                Subtitle=subtitle,
                Description=description,
                ContactMail=contactMail,
                ContactPhone=contactPhone,
                ContactWebsite=contactWebsite,
                CoverPhoto=coverPhoto,
                Thumbnail=thumbnail,
                StartDate=startDate,
                EndDate=endDate
            }))
            {
                return Redirect("~/Home");
                //return Redirect($"~/Hackathon/AddProblemStatements");
            }
            return Redirect("~/Hackathon/Add");
        }

        //public IActionResult AddProblemStatements()


        public IActionResult Index(int hid)
        {
            ViewData["Name"] = HttpContext.Session.GetString("UName");
            ViewData["Type"] = HttpContext.Session.GetString("Type");
            TempData["id"] =hid;
            HackathonContext hackathonContext = HttpContext.RequestServices.GetService(typeof(HackathonContext)) as HackathonContext;
            Hackathon Selection = hackathonContext.GetByID(hid);
            ProblemStatementContext problemStatementContext = HttpContext.RequestServices.GetService(typeof(ProblemStatementContext)) as ProblemStatementContext;
            var SelectionPS = problemStatementContext.GetProblemStatementsById(hid);
            
            dynamic JointModel = new ExpandoObject();
            JointModel.ProblemStatements = SelectionPS;
            JointModel.Hackathon = Selection;
            return View(JointModel);
        }
    }
}