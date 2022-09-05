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
using EmployeePayrollSystem.Areas.Identity.Pages.Account;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;

namespace EmployeePayrollSystem.Controllers
{
    public class AddEmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;

        public AddEmployeesController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _emailStore = GetEmailStore();
            _logger = logger;
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
            List<SelectListItem> levelList = new();
            int counter = 1;

            if (_context.AddLevel != null)
            {
                foreach(var lvl in _context.AddLevel)
                {
                    levelList.Add(new SelectListItem { Value = counter.ToString(), Text = lvl.LevelName });
                    counter++;
                }

                ViewBag.levels = levelList;
            }
            else
            {
                Problem("Entity set 'ApplicationDbContext.AddLevel'  is null.");
            }
            return View();
        }

        // POST: AddEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Address,FullName,City,Pincode,Mobile,Qualification,Role,Department,RoleName,Salary,PercentageIncrease,BankAccount,Email,Password,ConfirmPassword")] AddEmployee addEmployee)
        {
            if (ModelState.IsValid && _context.AddLevel != null)
            {
                string selectedRoleFromList = addEmployee.RoleName;
                //addEmployee.RoleName = _context.AddLevel.ToArray()[Convert.ToByte(selectedRoleFromList) - 1].ToString()!;
                Console.WriteLine(_context.AddLevel.ToArray()[Convert.ToByte(selectedRoleFromList) - 1].ToString()!);

                _context.Add(addEmployee);
                await _context.SaveChangesAsync();
                await CreateUserDb(addEmployee);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Problem("Entity set 'ApplicationDbContext.AddLevel'  is null.");
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


        /////////////////////
        ///

        /////

        public async Task<IActionResult> CreateUserDb([Bind("ID,FullName,City,Pincode,Mobile,Qualification,Role,Department,RoleName,Salary,PercentageIncrease,BankAccount,Email,Password,ConfirmPassword")] AddEmployee addEmployee)
        {
            string returnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.FirstName = addEmployee.FullName;
                user.LastName = addEmployee.FullName;

                await _userStore.SetUserNameAsync(user, addEmployee.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, addEmployee.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, addEmployee.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = addEmployee.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
