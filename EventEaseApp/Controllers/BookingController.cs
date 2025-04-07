using EventEaseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventEaseApp.Controllers
{

    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var booking = await _context.Booking.Include(b => b.Venue).Include(b => b.Events).ToListAsync();
            return View(booking);
        }

        public IActionResult Create()
        {
            ViewBag.VenueID = new SelectList(_context.Venue, "VenueID", "Locations");
            ViewBag.EventID = new SelectList(_context.Events, "EventID", "EventName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(booking);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Events)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }



        public async Task<IActionResult> Delete(int? id)
        {

            var booking = await _context.Booking.FirstOrDefaultAsync(m => m.BookingID == id);

            if (booking == null)
            {

                return NotFound();

            }
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            
                var booking = await _context.Booking.FindAsync(id);
                _context.Remove(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

                return View(booking);
            }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingID == id);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            // ✅ Repopulate dropdowns
            ViewBag.VenueID = new SelectList(_context.Venue, "VenueID", "Locations", booking.VenueID);
            ViewBag.EventID = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
            return View(booking);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingID))
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
            // ✅ Repopulate dropdowns again in case of form error
            ViewBag.VenueID = new SelectList(_context.Venue, "VenueID", "Locations", booking.VenueID);
            ViewBag.EventID = new SelectList(_context.Events, "EventID", "EventName", booking.EventID);
            return View(booking);
        }
    }
}


