using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finances.Database.Context;
using Finances.Database.Entities;

namespace Finances.APP.Controllers
{
    public class InvestmentsController : Controller
    {
        private readonly DatabaseContext _context;

        public InvestmentsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Investments
        public async Task<IActionResult> Index()
        {
              return _context.Investments != null ? 
                          View(await _context.Investments.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.Investments'  is null.");
        }

        // GET: Investments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Investments == null)
            {
                return NotFound();
            }

            var investment = await _context.Investments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investment == null)
            {
                return NotFound();
            }

            return View(investment);
        }

        // GET: Investments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Investments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,StartAmount,CurrentAmount,Rentability,Type,Account,Id,DateCreated,LastUpdate")] Investment investment)
        {
            if (ModelState.IsValid)
            {
                investment.Id = Guid.NewGuid();
                _context.Add(investment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(investment);
        }

        // GET: Investments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Investments == null)
            {
                return NotFound();
            }

            var investment = await _context.Investments.FindAsync(id);
            if (investment == null)
            {
                return NotFound();
            }
            return View(investment);
        }

        // POST: Investments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,StartAmount,CurrentAmount,Rentability,Type,Account,Id,DateCreated,LastUpdate")] Investment investment)
        {
            if (id != investment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestmentExists(investment.Id))
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
            return View(investment);
        }

        // GET: Investments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Investments == null)
            {
                return NotFound();
            }

            var investment = await _context.Investments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (investment == null)
            {
                return NotFound();
            }

            return View(investment);
        }

        // POST: Investments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Investments == null)
            {
                return Problem("Entity set 'DatabaseContext.Investments'  is null.");
            }
            var investment = await _context.Investments.FindAsync(id);
            if (investment != null)
            {
                _context.Investments.Remove(investment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestmentExists(Guid id)
        {
          return (_context.Investments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
