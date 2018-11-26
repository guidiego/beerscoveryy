using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using beerscovery.Data;
using beerscovery.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace beerscovery.Controllers
{
    public class Stats {
        public int Id { get; set; }
        public string Action { get; set; }
        public string PlaceName { get; set; }
        public string BeerName { get; set; }


        public string GetAction() {
            switch (this.Action)
            {
                case "FIND":
                    return "Encontrou";
                case "DRINK":
                    return "Bebeu";
                default:
                    return "Vazia";
            }
        }
    }

    [Authorize]
    public class UserStatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserStatsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserStats
        public async Task<IActionResult> Index()
        {
            var model = new List<Stats>();
            var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            var places = await _context.Place.ToListAsync();
            var beers = await _context.Beer.ToListAsync();
            var stats = await _context.UserStats.Where(us => us.UserId == userId).ToListAsync();

            foreach (UserStats us in stats) {
                model.Add(new Stats {
                    Id = us.Id,
                    Action = us.Action,
                    PlaceName = (await _context.Place.Where(p => p.Id == us.PlaceId).SingleOrDefaultAsync()).LocalName,
                    BeerName = (await _context.Beer.Where(b => b.Id == us.BeerId).SingleOrDefaultAsync()).Name,
                });
            }

            return View(model);
        }
    }
}
