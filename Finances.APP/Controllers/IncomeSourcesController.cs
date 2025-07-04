using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Finances.Database.Context;
using Finances.Database.Entities;
using Finances.APP.Models.IncomeSource;
using Microsoft.AspNetCore.Authorization;

namespace Finances.APP.Controllers
{
    [Authorize]
    public class IncomeSourcesController : Controller
    {
        private readonly DatabaseContext _context;

        public IncomeSourcesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: IncomeSources
        public async Task<IActionResult> Index()
        {
            var incomeSources = await _context.IncomeSources
                .Where(i => i.IsActive)
                .Select(i => new IncomeSourceViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Amount = i.Amount,
                    Owner = i.Owner,
                    Description = i.Description,
                    IsActive = i.IsActive,
                    DateCreated = i.DateCreated,
                    LastUpdate = i.LastUpdate ?? DateTime.UtcNow
                })
                .ToListAsync();

            return View(incomeSources);
        }

        // GET: IncomeSources/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.IncomeSources == null)
            {
                return NotFound();
            }

            var incomeSource = await _context.IncomeSources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomeSource == null)
            {
                return NotFound();
            }

            return View(incomeSource);
        }

        // GET: IncomeSources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IncomeSources/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Amount,Owner,Description,IsActive")] CreateIncomeSourceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var incomeSource = new IncomeSource
                {
                    Id = Guid.NewGuid(),
                    Name = viewModel.Name,
                    Amount = viewModel.Amount,
                    Owner = viewModel.Owner,
                    Description = viewModel.Description,
                    IsActive = viewModel.IsActive
                };

                _context.Add(incomeSource);
                await _context.SaveChangesAsync();

                TempData["success"] = "Fonte de renda criada com sucesso.";

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: IncomeSources/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.IncomeSources == null)
            {
                return NotFound();
            }

            var incomeSource = await _context.IncomeSources.FindAsync(id);
            if (incomeSource == null)
            {
                return NotFound();
            }

            var viewModel = new EditIncomeSourceViewModel
            {
                Id = incomeSource.Id,
                Name = incomeSource.Name,
                Amount = incomeSource.Amount,
                Owner = incomeSource.Owner,
                Description = incomeSource.Description,
                IsActive = incomeSource.IsActive
            };

            return View(viewModel);
        }

        // POST: IncomeSources/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Amount,Owner,Description,IsActive")] EditIncomeSourceViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var incomeSource = await _context.IncomeSources.FindAsync(id);
                    if (incomeSource == null)
                    {
                        return NotFound();
                    }

                    incomeSource.Name = viewModel.Name;
                    incomeSource.Amount = viewModel.Amount;
                    incomeSource.Owner = viewModel.Owner;
                    incomeSource.Description = viewModel.Description;
                    incomeSource.IsActive = viewModel.IsActive;

                    _context.Update(incomeSource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeSourceExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["success"] = "Fonte de renda alterada com sucesso.";

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: IncomeSources/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.IncomeSources == null)
            {
                return NotFound();
            }

            var incomeSource = await _context.IncomeSources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomeSource == null)
            {
                return NotFound();
            }

            return View(incomeSource);
        }

        // POST: IncomeSources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.IncomeSources == null)
            {
                return Problem("Entity set 'DatabaseContext.IncomeSources' is null.");
            }

            var incomeSource = await _context.IncomeSources.FindAsync(id);
            if (incomeSource != null)
            {
                incomeSource.IsActive = false;
                _context.Update(incomeSource);
            }

            await _context.SaveChangesAsync();

            TempData["success"] = "Fonte de renda excluída com sucesso.";

            return RedirectToAction(nameof(Index));
        }

        private bool IncomeSourceExists(Guid id)
        {
            return (_context.IncomeSources?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
} 