using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finances.Database.Context;
using Finances.Database.Entities;
using Finances.APP.Models.Investment;

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
            // Fetch all reserves to populate the dropdown
            ViewBag.AllReserves = new SelectList(_context.Reserves, "Id", "Name");

            // Initialize the view model and add an initial entry for SelectedReserves
            var viewModel = new InvestmentCreateViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvestmentCreateViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Create a new Investment entity based on the view model data
                    var investment = new Investment
                    {
                        Name = viewModel.Name,
                        Account = viewModel.Account,
                        Rentability = viewModel.Rentability,
                        Type = viewModel.Type,
                        CurrentAmount = viewModel.SelectedReserves.Sum(r => r.Amount),
                        StartAmount = viewModel.SelectedReserves.Sum(r => r.Amount),
                        SourceReserves = new List<ReserveInvestment>()
                    };

                    foreach (var selectedReserve in viewModel.SelectedReserves)
                    {
                        var reserve = _context.Reserves
                                .Include(r => r.Entries)
                                .Include(r => r.LinkedInvestments)
                                .FirstOrDefault(r => r.Id == selectedReserve.ReserveId);

                        if (reserve == null)
                            throw new Exception("Reserva não encontrada.");

                        var available = reserve.Entries.Sum(r => r.Amount) - reserve.LinkedInvestments.Sum(l => l.Amount);

                        if (available < selectedReserve.Amount)
                            throw new Exception("Saldo disponível insuficiente.");

                        investment.SourceReserves.Add(new()
                        {
                            Reserve = reserve,
                            Amount = selectedReserve.Amount,
                        });
                    }

                    _context.Add(investment);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index"); // Redirect to a list of investments or wherever you want
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"There was an error creating the investment.\n{ex.Message}";
            }

            // If ModelState is not valid, re-populate the dropdown
            ViewBag.AllReserves = new SelectList(_context.Reserves, "Id", "Name", viewModel.SelectedReserves);
            return View(viewModel);
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

            var investment = await _context.Investments
                .Include(s => s.SourceReserves)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (investment != null)
            {
                _context.ReserveInvestmentMaps.RemoveRange(investment.SourceReserves);
                _context.Investments.Remove(investment);
                
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool InvestmentExists(Guid id)
        {
            return (_context.Investments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
