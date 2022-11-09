using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eTicketShop.Areas.Identity.Data;
using eTicketShop.Models;

namespace eTicketShop.Controllers
{
    public class EventsController : Controller
    {
        private readonly TicketShopDB2Context _context;

        public EventsController(TicketShopDB2Context context)
        {
            _context = context;
        }

        // GET: Events
    /*    public async Task<IActionResult> Index()
        {
            var allEvents = _context.Events.Include(c => c.Category);
            return View(allEvents);
        }
    */
        public async Task<IActionResult> Filter(string searchString)
        {
            var allEvents = _context.Events.Include(n => n.Category);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = allEvents.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

               // var filteredResultNew = allEvents.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allEvents);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }


        // GET: Events/Create
        public IActionResult Create()
        {
             ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,Name,StartDate,Price,Address,Description,ImageUrl, Image")] Event @event, List<IFormFile> Image)
        {
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        @event.Image = stream.ToArray();
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", @event.CategoryId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", @event.CategoryId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,Name,StartDate,Price,Address,Description,ImageUrl, Image")] Event @event, List<IFormFile> Image)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        @event.Image = stream.ToArray();
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", @event.CategoryId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'TicketShopDB2Context.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
          return _context.Events.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Index(string categorySlug = "", int p = 1)
        {
            int pageSize = 3;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.CategorySlug = categorySlug;

            if (categorySlug == "")
            {
                ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Events.Count() / pageSize);

                return View(await _context.Events.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
            }

            Category category = await _context.Categories.Where(c => c.Name == categorySlug).FirstOrDefaultAsync();
            if (category == null) return RedirectToAction("Index");

            var eventsByCategory = _context.Events.Where(p => p.CategoryId == category.Id);
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)eventsByCategory.Count() / pageSize);

            return View(await eventsByCategory.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
        }
    }
}
