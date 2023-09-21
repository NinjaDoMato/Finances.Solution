using Finances.APP.Models.Entry;
using Finances.APP.Models.Investment;
using Finances.APP.Models.Reserve;
using Finances.Database.Context;
using Finances.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finances.APP.Controllers
{
    public class ReservesController : Controller
    {
        private readonly DatabaseContext _context;

        public ReservesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Reserves
        public async Task<IActionResult> Index()
        {
            var reserves = _context.Reserves
                .Include(r => r.LinkedInvestments)
                .Include(r => r.Entries);

            if (reserves == null)
                return Problem("Entity set 'DatabaseContext.Reserves'  is null.");

            var reserveList = await reserves
                .Include(r => r.Entries)
                .Include(r => r.LinkedInvestments)
                    .ThenInclude(l => l.Investment)
                .Select(r => new ReserveViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Owner = r.Owner,
                    Goal = r.Goal,
                    CurrentAmount = r.Entries.Sum(e => e.Amount),
                    InvestedAmount = r.LinkedInvestments.Sum(i => i.Amount)
                }).ToListAsync();

            return View(reserveList);
        }

        // GET: Reserves/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Reserves == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves
                .Include(r => r.Entries)
                .Include(r => r.LinkedInvestments)
                    .ThenInclude(l => l.Investment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserve == null)
            {
                return NotFound();
            }

            return View(new ReserveViewModel
            {
                Id = reserve.Id,
                Name = reserve.Name,
                Description = reserve.Description,
                Owner = reserve.Owner,
                CurrentAmount = reserve.Entries.Sum(e => e.Amount),
                InvestedAmount = reserve.LinkedInvestments.Sum(l => l.Amount),
                Entries = reserve.Entries.Select(e => new EntryViewModel { Amount = e.Amount, Observation = e.Observation }).ToList(),
                LinkedInvestments = reserve.LinkedInvestments.Select(l => new InvestmentViewModel { Account = l.Investment.Account, CurrentAmount = l.Investment.CurrentAmount, Name = l.Investment.Name, Rentability = l.Investment.Rentability, StartAmount = l.Investment.StartAmount, Type = l.Investment.Type }).ToList()
            });
        }

        // GET: Reserves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reserves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Owner,Goal,Id,DateCreated,LastUpdate")] Reserve reserve)
        {
            if (ModelState.IsValid)
            {
                reserve.Id = Guid.NewGuid();
                _context.Add(reserve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reserve);
        }

        // GET: Reserves/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Reserves == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves.FindAsync(id);
            if (reserve == null)
            {
                return NotFound();
            }
            return View(reserve);
        }

        // POST: Reserves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Description,Owner,Id,DateCreated,LastUpdate")] Reserve reserve)
        {
            if (id != reserve.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveExists(reserve.Id))
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
            return View(reserve);
        }

        // GET: Reserves/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Reserves == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserve == null)
            {
                return NotFound();
            }

            return View(reserve);
        }

        // POST: Reserves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Reserves == null)
            {
                return Problem("Entity set 'DatabaseContext.Reserves'  is null.");
            }
            var reserve = await _context.Reserves.FindAsync(id);
            if (reserve != null)
            {
                _context.Reserves.Remove(reserve);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveExists(Guid id)
        {
            return (_context.Reserves?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
