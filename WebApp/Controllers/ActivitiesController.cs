using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using System.Linq;

namespace WebApp.Controllers
{
    [Authorize]
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Activities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TourismActivities
                .Include(t => t.City)
                .Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TourismActivities == null)
            {
                return NotFound();
            }

            var tourismActivity = await _context.TourismActivities
                .Include(t => t.City)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tourismActivity == null)
            {
                return NotFound();
            }

            return View(tourismActivity);
        }

        // GET: Activities/Create
        public IActionResult Create()
        {
            
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            var activityTypes = Enum.GetValues(typeof(ActivityType))
                .Cast<ActivityType>()
                .Select(v => new KeyValuePair<int, string>((int)v, v.ToString()))
                .ToList();
            ViewData["ActivityType"] = new SelectList(activityTypes, "Key", "Value");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CityId,ActivityType,Title,Details")] TourismActivity tourismActivity)
        {
            if (ModelState.IsValid)
            {
                tourismActivity.UserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                _context.Add(tourismActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", tourismActivity.CityId);

            var activityTypes = Enum.GetValues(typeof(ActivityType))
                .Cast<ActivityType>()
                .Select(v => new KeyValuePair<int, string>((int)v, v.ToString()))
                .ToList();
            ViewData["ActivityType"] = new SelectList(activityTypes, "Key", "Value");

            return View(tourismActivity);
        }

        // GET: Activities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TourismActivities == null)
            {
                return NotFound();
            }

            var tourismActivity = await _context.TourismActivities.FindAsync(id);
            if (tourismActivity == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", tourismActivity.CityId);

            var activityTypes = Enum.GetValues(typeof(ActivityType))
                .Cast<ActivityType>()
                .Select(v => new KeyValuePair<int, string>((int)v, v.ToString()))
                .ToList();
            ViewData["ActivityType"] = new SelectList(activityTypes, "Key", "Value");

            return View(tourismActivity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CityId,ActivityType,Title,Details")] TourismActivity tourismActivity)
        {
            if (id != tourismActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tourismActivity.UserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    _context.Update(tourismActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourismActivityExists(tourismActivity.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", tourismActivity.CityId);
            return View(tourismActivity);
        }

        // GET: Activities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TourismActivities == null)
            {
                return NotFound();
            }

            var tourismActivity = await _context.TourismActivities
                .Include(t => t.City)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tourismActivity == null)
            {
                return NotFound();
            }

            return View(tourismActivity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TourismActivities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TourismActivities'  is null.");
            }
            var tourismActivity = await _context.TourismActivities.FindAsync(id);
            if (tourismActivity != null)
            {
                _context.TourismActivities.Remove(tourismActivity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourismActivityExists(int id)
        {
          return (_context.TourismActivities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
