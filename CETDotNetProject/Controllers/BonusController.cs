using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CETDotNetProject.Data;
using CETDotNetProject.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CETDotNetProject.Controllers
{
    public class BonusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BonusController> _logger;

        public BonusController(ApplicationDbContext context, ILogger<BonusController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Prim listesi
        public async Task<IActionResult> Index()
        {
            var bonuses = _context.Bonuses.Include(b => b.Employee);
            return View(await bonuses.ToListAsync());
        }

        // Prim ekleme formu
        public IActionResult Create()
        {
            var employees = _context.Employees.ToList();
            var viewModel = new BonusViewModel
            {
                Employees = new SelectList(employees, "Id", "FirstName")
            };
            return View(viewModel);
        }

        // Prim ekleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BonusViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Add logging before creating bonus object
                    _logger.LogInformation("Creating bonus object...");
                    
                    var bonus = new Bonus
                    {
                        EmployeeId = viewModel.EmployeeId,
                        Amount = viewModel.Amount,
                        BonusDate = viewModel.BonusDate
                    };
                    
                    // Add logging before adding to context
                    _logger.LogInformation("Adding bonus to context...");
                    _context.Add(bonus);
                    
                    // Add logging before saving changes
                    _logger.LogInformation("Saving changes to database...");
                    await _context.SaveChangesAsync();
                    
                    // Add logging here to confirm save
                    _logger.LogInformation("Bonus saved successfully!");
                    
                    TempData["Success"] = "Prim başarıyla eklendi.";
                    
                    // Add logging before redirect
                    _logger.LogInformation("Redirecting to Index...");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving bonus.");
                    ModelState.AddModelError("", "An error occurred while saving the bonus. Please try again.");
                }
            }

            // If ModelState is not valid (or an exception occurred), repopulate the employees list and return the view
            var employees = _context.Employees.ToList();
            viewModel.Employees = new SelectList(employees, "Id", "FirstName", viewModel.EmployeeId);

            // Explicitly remove the error for the 'Employees' SelectList
            ModelState.Remove("Employees");

            // Add logging here:
            _logger.LogInformation($"ModelState is valid: {ModelState.IsValid}");
            foreach (var state in ModelState)
            {
                _logger.LogInformation($"Key: {state.Key}, State: {state.Value.ValidationState}");
                foreach (var error in state.Value.Errors)
                {
                    _logger.LogError($"Error: {error.ErrorMessage}");
                }
            }
            _logger.LogInformation($"Received EmployeeId: {viewModel.EmployeeId}");
            return View(viewModel);
        }
    }
} 