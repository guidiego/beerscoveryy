using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using beerscovery.Data;
using beerscovery.Models;

namespace beerscovery.Controllers
{

    public class BeerViewData {
        public int Id;
        public string Name;
        public string Kind;
        public string Photo;
        public int ScoreQtd;
        public bool HasProfileScore;
        public bool HasPlace;
        public Score AverageScore;
        public Score ProfileScore;
        public List<Place> Places;
    }

    [Authorize]
    public class BeerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BeerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Beer
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Beer.ToListAsync());
        }

        // GET: Beer/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (beer == null)
            {
                return NotFound();
            }

            var model = new BeerViewData();
            var stats =  _context.UserStats.Where(s => s.BeerId == beer.Id).ToList();
            var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;
            var scores = _context.Score.Where(s => s.Beer == beer.Id).ToList();

            var finalScore = new Score {
                Price = 0,
                Quality = 0,
                Bitterness = 0
            };

            model.HasProfileScore = false;
            model.Id = beer.Id;
            model.Name = beer.Name;
            model.Kind = beer.Kind;
            model.Photo = beer.Photo;
            model.ScoreQtd = 0;
            model.Places = new List<Place>();

            foreach (UserStats s in stats) {
                var place = await _context.Place.Where(p => p.Id == s.PlaceId).SingleOrDefaultAsync();
                model.Places.Add(place);
            }

            if (model.Places.Count > 0) {
                model.HasPlace = true;
            } else {
                model.HasPlace = false;
            }

            foreach (Score s in scores) {
                model.ScoreQtd++;
                finalScore.Price += s.Price;
                finalScore.Quality += s.Quality;
                finalScore.Bitterness += s.Bitterness;

                if (s.User == userId) {
                    model.HasProfileScore = true;
                    model.ProfileScore = s;
                }
            }

            if (model.ScoreQtd > 0) {
                model.AverageScore = new Score {
                    Price = finalScore.Price / model.ScoreQtd,
                    Quality = finalScore.Quality / model.ScoreQtd,
                    Bitterness = finalScore.Bitterness / model.ScoreQtd,
                };
            } else {
               model.AverageScore = finalScore;
            }

            return View(model);
        }

        // GET: Beer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Beer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Photo,Kind")] Beer beer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beer);
        }

        // GET: Beer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beer.SingleOrDefaultAsync(m => m.Id == id);
            if (beer == null)
            {
                return NotFound();
            }
            return View(beer);
        }

        // POST: Beer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Photo,Kind")] Beer beer)
        {
            if (id != beer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerExists(beer.Id))
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
            return View(beer);
        }

        // GET: Beer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beer
                .SingleOrDefaultAsync(m => m.Id == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // POST: Beer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beer = await _context.Beer.SingleOrDefaultAsync(m => m.Id == id);
            _context.Beer.Remove(beer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeerExists(int id)
        {
            return _context.Beer.Any(e => e.Id == id);
        }
    }
}
