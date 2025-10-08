using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gift_of_the_Givers_Foundation_Website.Data;
using Gift_of_the_Givers_Foundation_Website.Models;
using Microsoft.AspNetCore.Identity;

namespace Gift_of_the_Givers_Foundation_Website.Controllers
{
    public class DisasterAlertsController : Controller
    {
        private readonly WebAppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DisasterAlertsController(WebAppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: DisasterAlerts
        public async Task<IActionResult> Index()
        {
            var webAppDbContext = _context.DisasterAlerts.Include(d => d.User);
            return View(await webAppDbContext.ToListAsync());
        }

        // GET: DisasterAlerts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disasterAlert = await _context.DisasterAlerts
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.AlertId == id);
            if (disasterAlert == null)
            {
                return NotFound();
            }

            return View(disasterAlert);
        }

        // GET: DisasterAlerts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserID", "Email");
            return View();
        }

        // POST: DisasterAlerts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlertId,Title,Description,Location,UserId,Date")] DisasterAlert disasterAlert)
        {
            var userloggedIn = await _userManager.GetUserAsync(User);
            var user = _context.Users.FirstOrDefault(u => u.Email == userloggedIn.Email);
            disasterAlert.UserId = user.UserID;
            disasterAlert.User = user;

            _context.Add(disasterAlert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: DisasterAlerts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disasterAlert = await _context.DisasterAlerts.FindAsync(id);
            if (disasterAlert == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserID", "Email", disasterAlert.UserId);
            return View(disasterAlert);
        }

        // POST: DisasterAlerts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlertId,Title,Description,Location,UserId,Date")] DisasterAlert disasterAlert)
        {
            if (id != disasterAlert.AlertId)
            {
                return NotFound();
            }

            var userloggedIn = await _userManager.GetUserAsync(User);
            var user = _context.Users.FirstOrDefault(u => u.Email == userloggedIn.Email);
            disasterAlert.UserId = user.UserID;
            disasterAlert.User = user;
            try
                {
                    _context.Update(disasterAlert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisasterAlertExists(disasterAlert.AlertId))
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

        // GET: DisasterAlerts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disasterAlert = await _context.DisasterAlerts
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.AlertId == id);
            if (disasterAlert == null)
            {
                return NotFound();
            }

            return View(disasterAlert);
        }

        // POST: DisasterAlerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disasterAlert = await _context.DisasterAlerts.FindAsync(id);
            if (disasterAlert != null)
            {
                _context.DisasterAlerts.Remove(disasterAlert);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisasterAlertExists(int id)
        {
            return _context.DisasterAlerts.Any(e => e.AlertId == id);
        }
    }
}
