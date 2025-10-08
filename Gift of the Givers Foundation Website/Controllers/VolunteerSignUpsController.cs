using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gift_of_the_Givers_Foundation_Website.Data;
using Gift_of_the_Givers_Foundation_Website.Models;

namespace Gift_of_the_Givers_Foundation_Website.Controllers
{
    public class VolunteerSignUpsController : Controller
    {
        private readonly WebAppDbContext _context;

        public VolunteerSignUpsController(WebAppDbContext context)
        {
            _context = context;
        }

        // GET: VolunteerSignUps
        public async Task<IActionResult> Index()
        {
            var webAppDbContext = _context.VolunteerSignUps.Include(v => v.Project).Include(v => v.User);
            return View(await webAppDbContext.ToListAsync());
        }

        // GET: VolunteerSignUps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteerSignUp = await _context.VolunteerSignUps
                .Include(v => v.Project)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.SignUpID == id);
            if (volunteerSignUp == null)
            {
                return NotFound();
            }

            return View(volunteerSignUp);
        }

        // GET: VolunteerSignUps/Create
        public IActionResult Create()
        {
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "Title");
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email");
            return View();
        }

        // POST: VolunteerSignUps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SignUpID,UserID,ProjectID,SignUpDate")] VolunteerSignUp volunteerSignUp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volunteerSignUp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "Title", volunteerSignUp.ProjectID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", volunteerSignUp.UserID);
            return View(volunteerSignUp);
        }

        // GET: VolunteerSignUps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteerSignUp = await _context.VolunteerSignUps.FindAsync(id);
            if (volunteerSignUp == null)
            {
                return NotFound();
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "Title", volunteerSignUp.ProjectID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", volunteerSignUp.UserID);
            return View(volunteerSignUp);
        }

        // POST: VolunteerSignUps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SignUpID,UserID,ProjectID,SignUpDate")] VolunteerSignUp volunteerSignUp)
        {
            if (id != volunteerSignUp.SignUpID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volunteerSignUp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerSignUpExists(volunteerSignUp.SignUpID))
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
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "Title", volunteerSignUp.ProjectID);
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", volunteerSignUp.UserID);
            return View(volunteerSignUp);
        }

        // GET: VolunteerSignUps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteerSignUp = await _context.VolunteerSignUps
                .Include(v => v.Project)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.SignUpID == id);
            if (volunteerSignUp == null)
            {
                return NotFound();
            }

            return View(volunteerSignUp);
        }

        // POST: VolunteerSignUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteerSignUp = await _context.VolunteerSignUps.FindAsync(id);
            if (volunteerSignUp != null)
            {
                _context.VolunteerSignUps.Remove(volunteerSignUp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteerSignUpExists(int id)
        {
            return _context.VolunteerSignUps.Any(e => e.SignUpID == id);
        }
    }
}
