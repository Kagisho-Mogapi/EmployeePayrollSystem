using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeePayrollSystem.Areas.Identity.Data;
using EmployeePayrollSystem.Models;

namespace EmployeePayrollSystem.Controllers
{
    public class AddEmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddEmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddEmployees
        public async Task<IActionResult> Index()
        {
              return _context.AddEmployee != null ? 
                          View(await _context.AddEmployee.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AddEmployee'  is null.");
        }

        // GET: AddEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AddEmployee == null)
            {
                return NotFound();
            }

            var addEmployee = await _context.AddEmployee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (addEmployee == null)
            {
                return NotFound();
            }

            return View(addEmployee);
        }

        // GET: AddEmployees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FullName,City,Pincode,Mobile,Qualification,Role,Department,RoleName,Salary,PercentageIncrease,BankAccount,Email,Password,ConfirmPassword")] AddEmployee addEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addEmployee);
        }

        // GET: AddEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AddEmployee == null)
            {
                return NotFound();
            }

            var addEmployee = await _context.AddEmployee.FindAsync(id);
            if (addEmployee == null)
            {
                return NotFound();
            }
            return View(addEmployee);
        }

        // POST: AddEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FullName,City,Pincode,Mobile,Qualification,Role,Department,RoleName,Salary,PercentageIncrease,BankAccount,Email,Password,ConfirmPassword")] AddEmployee addEmployee)
        {
            if (id != addEmployee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddEmployeeExists(addEmployee.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(addEmployee);
        }

        // GET: AddEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AddEmployee == null)
            {
                return NotFound();
            }

            var addEmployee = await _context.AddEmployee
                .FirstOrDefaultAsync(m => m.ID == id);
            if (addEmployee == null)
            {
                return NotFound();
            }

            return View(addEmployee);
        }

        // POST: AddEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AddEmployee == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AddEmployee'  is null.");
            }
            var addEmployee = await _context.AddEmployee.FindAsync(id);
            if (addEmployee != null)
            {
                _context.AddEmployee.Remove(addEmployee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddEmployeeExists(int id)
        {
          return (_context.AddEmployee?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
