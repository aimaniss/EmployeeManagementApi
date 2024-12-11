using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace EmployeeManagementAPI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;

        // Constructor to inject the EmployeeContext
        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }

        // Index action to list all employees with their departments
        public async Task<IActionResult> Index()
        {
            // Include Department navigation property and fetch employees with their departments
            var employees = await _context.Employees.Include(e => e.Department).ToListAsync();
            return View(employees);
        }

        // Create action to show the form for creating a new employee
        public async Task<IActionResult> Create()
        {
            // Fetch all departments to display in a dropdown
            var departments = await _context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(departments, "DepartmentID", "DepartmentName");
            return View();
        }

        // POST: Create action to handle form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeName, DateBirth, Email, PhoneNumber, HireDate, DepartmentID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Add new employee to the database
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));  // Redirect to the Index page after successful creation
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}"); // Log any errors to the console
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            // If validation fails, re-pass departments to the view
            var departments = await _context.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(departments, "DepartmentID", "DepartmentName");
            return View(employee);  // Return the view with validation errors
        }

        // Inline Edit: Handles saving updates directly from the Index view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInline(int EmployeeID, string EmployeeName, string Email, string PhoneNumber, DateTime HireDate, DateTime DateBirth, int DepartmentID)
        {
            var employee = await _context.Employees.FindAsync(EmployeeID);

            if (employee != null)
            {
                employee.EmployeeName = EmployeeName;
                employee.Email = Email;
                employee.PhoneNumber = PhoneNumber;
                employee.HireDate = HireDate;
                employee.DateBirth = DateBirth;
                employee.DepartmentID = DepartmentID;

                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Employees.Any(e => e.EmployeeID == EmployeeID))
                        return NotFound();
                    else
                        throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)  // Include Department for displaying related data
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);  // Return the employee details for confirmation
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);  // Remove the employee
                await _context.SaveChangesAsync();  // Save the changes
            }

            return RedirectToAction(nameof(Index));  // Redirect to the Index page after deletion
        }
    }
}
