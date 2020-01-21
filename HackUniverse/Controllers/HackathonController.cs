using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using HackUniverse.Models;
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
            HackathonContext context = HttpContext.RequestServices.GetService(typeof(HackathonContext)) as HackathonContext;
            
                return View(context.GetByID(hid));
        }
    }
}