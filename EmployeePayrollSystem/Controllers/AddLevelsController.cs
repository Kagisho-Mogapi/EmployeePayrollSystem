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
    public class AddLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddLevels
        public async Task<IActionResult> Index()
        {
              return _context.AddLevel != null ? 
                          View(await _context.AddLevel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AddLevel'  is null.");
        }

        // GET: AddLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AddLevel == null)
            {
                return NotFound();
            }

            var addLevel = await _context.AddLevel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (addLevel == null)
            {
                return NotFound();
            }

            return View(addLevel);
        }

        // GET: AddLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LevelName,Salary,YearlySalaryIncreasePercentage,TravelAllowance,MedicalAllowance,InternetAllowance")] AddLevel addLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addLevel);
        }

        // GET: AddLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AddLevel == null)
            {
                return NotFound();
            }

            var addLevel = await _context.AddLevel.FindAsync(id);
            if (addLevel == null)
            {
                return NotFound();
            }
            return View(addLevel);
        }

        // POST: AddLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LevelName,Salary,YearlySalaryIncreasePercentage,TravelAllowance,MedicalAllowance,InternetAllowance")] AddLevel addLevel)
        {
            if (id != addLevel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddLevelExists(addLevel.ID))
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
            return View(addLevel);
        }

        // GET: AddLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AddLevel == null)
            {
                return NotFound();
            }

            var addLevel = await _context.AddLevel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (addLevel == null)
            {
                return NotFound();
            }

            return View(addLevel);
        }

        // POST: AddLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AddLevel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AddLevel'  is null.");
            }
            var addLevel = await _context.AddLevel.FindAsync(id);
            if (addLevel != null)
            {
                _context.AddLevel.Remove(addLevel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddLevelExists(int id)
        {
          return (_context.AddLevel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
