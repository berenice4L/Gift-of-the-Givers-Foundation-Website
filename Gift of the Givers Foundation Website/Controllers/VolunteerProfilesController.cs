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
    public class VolunteerProfilesController : Controller
    {
        private readonly WebAppDbContext _context;

        public VolunteerProfilesController(WebAppDbContext context)
        {
            _context = context;
        }

        // GET: VolunteerProfiles
        public async Task<IActionResult> Index()
        {
            var webAppDbContext = _context.VolunteerProfiles.Include(v => v.User);
            return View(await webAppDbContext.ToListAsync());
        }

        // GET: VolunteerProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteerProfile = await _context.VolunteerProfiles
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.ProfileID == id);
            if (volunteerProfile == null)
            {
                return NotFound();
            }

            return View(volunteerProfile);
        }

        // GET: VolunteerProfiles/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email");
            return View();
        }

        // POST: VolunteerProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileID,UserID,Skills,Availability")] VolunteerProfile volunteerProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volunteerProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", volunteerProfile.UserID);
            return View(volunteerProfile);
        }

        // GET: VolunteerProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteerProfile = await _context.VolunteerProfiles.FindAsync(id);
            if (volunteerProfile == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", volunteerProfile.UserID);
            return View(volunteerProfile);
        }

        // POST: VolunteerProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfileID,UserID,Skills,Availability")] VolunteerProfile volunteerProfile)
        {
            if (id != volunteerProfile.ProfileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volunteerProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerProfileExists(volunteerProfile.ProfileID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", volunteerProfile.UserID);
            return View(volunteerProfile);
        }

        // GET: VolunteerProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteerProfile = await _context.VolunteerProfiles
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.ProfileID == id);
            if (volunteerProfile == null)
            {
                return NotFound();
            }

            return View(volunteerProfile);
        }

        // POST: VolunteerProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volunteerProfile = await _context.VolunteerProfiles.FindAsync(id);
            if (volunteerProfile != null)
            {
                _context.VolunteerProfiles.Remove(volunteerProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteerProfileExists(int id)
        {
            return _context.VolunteerProfiles.Any(e => e.ProfileID == id);
        }
    }
}
