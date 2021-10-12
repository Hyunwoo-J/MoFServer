using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoFModel.Contexts;
using MoFModel.Entities;
using X.PagedList;

namespace MoF.Controllers
{
    public class MovieTheaterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieTheaterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieTheater
        public async Task<IActionResult> Index(int? page)
        {
            var applicationDbContext = _context.MovieTheater
                .OrderBy(m => m.MovieTheaterId);

            return View(await applicationDbContext.ToPagedListAsync(page ?? 1, 15));
        }

        // GET: MovieTheater/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTheater = await _context.MovieTheater
                .FirstOrDefaultAsync(m => m.MovieTheaterId == id);
            if (movieTheater == null)
            {
                return NotFound();
            }

            return View(movieTheater);
        }

        // GET: MovieTheater/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieTheater/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieTheaterId,MetropolitanCouncil,BasicOrganization,Name")] MovieTheater movieTheater)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieTheater);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieTheater);
        }

        // GET: MovieTheater/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTheater = await _context.MovieTheater.FindAsync(id);
            if (movieTheater == null)
            {
                return NotFound();
            }
            return View(movieTheater);
        }

        // POST: MovieTheater/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieTheaterId,MetropolitanCouncil,BasicOrganization,Name")] MovieTheater movieTheater)
        {
            if (id != movieTheater.MovieTheaterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieTheater);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieTheaterExists(movieTheater.MovieTheaterId))
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
            return View(movieTheater);
        }

        // GET: MovieTheater/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTheater = await _context.MovieTheater
                .FirstOrDefaultAsync(m => m.MovieTheaterId == id);
            if (movieTheater == null)
            {
                return NotFound();
            }

            return View(movieTheater);
        }

        // POST: MovieTheater/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieTheater = await _context.MovieTheater.FindAsync(id);
            _context.MovieTheater.Remove(movieTheater);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieTheaterExists(int id)
        {
            return _context.MovieTheater.Any(e => e.MovieTheaterId == id);
        }
    }
}
