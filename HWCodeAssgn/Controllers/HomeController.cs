using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HWCodeAssgn.Models;
using HWCodeAssgn.Data;
using Microsoft.EntityFrameworkCore;

namespace HWCodeAssgn.Controllers
{
    public class HomeController : Controller
    {

        private readonly SiteContext _context;

        public HomeController(SiteContext context){
            _context = context;
        }

        public async Task<IActionResult> Index() /* Display Page Action */
        {
            return View(await _context.Profile.ToListAsync());

            
        }

        /* Create Profile Controller */
        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( 
            [Bind("Name, Email, BirthDate, Password, ConfirmPassword, City, State, Zip")] Profile profile)
        {
            /* Add Profile Action */
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(profile);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View();
        }


        /* Update Profile Controller */
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileToUpdate = await _context.Profile
                .SingleOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Profile>(profileToUpdate,
                "",
                p => p.Name, p => p.Email, p => p.BirthDate, p => p.City, p => p.State, p => p.Zip))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(profileToUpdate);
        }
        


        /* Delete Profile 'Controller' */
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id); 
                
            if (profile == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(profile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profile
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);   //selects profile with ID to pass it to view
            if (profile == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Profile.Remove(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        
        private bool ProfileExists(int id)
        {
            return _context.Profile.Any(e => e.ID == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
