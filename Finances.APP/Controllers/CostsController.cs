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
    public class CostsController : Controller
    {
        private readonly DatabaseContext _context;

        public CostsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Costs
        public async Task<IActionResult> Index()
        {
            return _context.Costs != null ?
                        View(await _context.Costs.ToListAsync()) :
                        Problem("Entity set 'DatabaseContext.Costs'  is null.");
        }

        // GET: Costs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Costs == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // GET: Costs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Costs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Type,Name,Description,Id,DateCreated,LastUpdate")] Cost cost)
        {
            if (ModelState.IsValid)
            {
                cost.Id = Guid.NewGuid();
                _context.Add(cost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cost);
        }

        // GET: Costs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Costs == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs.FindAsync(id);
            if (cost == null)
            {
                return NotFound();
            }
            return View(cost);
        }

        // POST: Costs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Amount,Type,Name,Description,Id,DateCreated,LastUpdate")] Cost cost)
        {
            if (id != cost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostExists(cost.Id))
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
            return View(cost);
        }

        // GET: Costs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Costs == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // POST: Costs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Costs == null)
            {
                return Problem("Entity set 'DatabaseContext.Costs'  is null.");
            }
            var cost = await _context.Costs.FindAsync(id);
            if (cost != null)
            {
                _context.Costs.Remove(cost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostExists(Guid id)
        {
            return (_context.Costs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
