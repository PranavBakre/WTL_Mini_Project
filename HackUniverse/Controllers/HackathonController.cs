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
namespace HackUniverse.Controllers
{
    [Route("api/[controller]")]
    public class HackathonController : Controller
    {

        ILogger _logger;
        

        
 

        public HackathonController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }




        public IActionResult Index(int hid)
        {
            
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