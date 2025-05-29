using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CETDotNetProject.Data;
using CETDotNetProject.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CETDotNetProject.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LeaveController(ApplicationDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
            base.OnActionExecuting(context);
        }

        // Tüm izinleri listele
        public async Task<IActionResult> Index()
        {
            var leaves = _context.Leaves.Include(l => l.Employee);
            return View(await leaves.ToListAsync());
        }

        // İzin ekleme formu
        public IActionResult Create()
        {
            ViewBag.Employees = new SelectList(_context.Employees, "Id", "FirstName");
            return View();
        }

        // İzin ekleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,StartDate,EndDate")] Leave leave)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine($"EmployeeId: {leave.EmployeeId}, StartDate: {leave.StartDate}, EndDate: {leave.EndDate}");
                _context.Add(leave);
                await _context.SaveChangesAsync();
                TempData["Success"] = "İzin başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            // Hataları logla
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            ViewBag.Employees = new SelectList(_context.Employees, "Id", "FirstName", leave.EmployeeId);
            return View(leave);
        }
    }
} 