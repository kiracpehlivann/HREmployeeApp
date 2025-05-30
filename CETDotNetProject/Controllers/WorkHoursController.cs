using Microsoft.AspNetCore.Mvc;
using System;

namespace CETDotNetProject.Controllers
{
    public class WorkHoursController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 