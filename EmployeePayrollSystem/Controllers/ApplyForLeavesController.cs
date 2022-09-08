using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeePayrollSystem.Areas.Identity.Data;
using EmployeePayrollSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeePayrollSystem.Controllers
{
    public class ApplyForLeavesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplyForLeavesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ApplyForLeaves
        public async Task<IActionResult> Index()
        {
              return _context.ApplyForLeave != null ? 
                          View(await _context.ApplyForLeave.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ApplyForLeave'  is null.");
        }

        // GET: ApplyForLeaves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ApplyForLeave == null)
            {
                return NotFound();
            }

            var applyForLeave = await _context.ApplyForLeave
                .FirstOrDefaultAsync(m => m.ID == id);
            if (applyForLeave == null)
            {
                return NotFound();
            }

            return View(applyForLeave);
        }

        // GET: ApplyForLeaves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplyForLeaves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FromDate,TillDate,Reason")] ApplyForLeave applyForLeave)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            applyForLeave.ApplicantID = currentUser.FirstName+" "+currentUser.LastName;
            applyForLeave.Status = "Pending";

            if (ModelState.IsValid)
            {
                _context.Add(applyForLeave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applyForLeave);
        }

        // GET: ApplyForLeaves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ApplyForLeave == null)
            {
                return NotFound();
            }

            var applyForLeave = await _context.ApplyForLeave.FindAsync(id);
            if (applyForLeave == null)
            {
                return NotFound();
            }
            return View(applyForLeave);
        }

        // POST: ApplyForLeaves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FromDate,TillDate,Reason,ApplicantID,Status")] ApplyForLeave applyForLeave)
        {
            if (id != applyForLeave.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applyForLeave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplyForLeaveExists(applyForLeave.ID))
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
            return View(applyForLeave);
        }

        // GET: ApplyForLeaves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ApplyForLeave == null)
            {
                return NotFound();
            }

            var applyForLeave = await _context.ApplyForLeave
                .FirstOrDefaultAsync(m => m.ID == id);
            if (applyForLeave == null)
            {
                return NotFound();
            }

            return View(applyForLeave);
        }

        // POST: ApplyForLeaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ApplyForLeave == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ApplyForLeave'  is null.");
            }
            var applyForLeave = await _context.ApplyForLeave.FindAsync(id);
            if (applyForLeave != null)
            {
                _context.ApplyForLeave.Remove(applyForLeave);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplyForLeaveExists(int id)
        {
          return (_context.ApplyForLeave?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
