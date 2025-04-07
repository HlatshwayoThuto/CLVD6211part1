using EventEaseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EventEaseApp.Controllers
{
    public class VenueController1 : Controller
    {

        public readonly ApplicationDBContext _context;

        public VenueController1(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var venue = await _context.Venue.ToListAsync();
            return View(venue);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
