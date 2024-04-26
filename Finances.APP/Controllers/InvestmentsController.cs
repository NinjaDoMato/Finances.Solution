using Finances.APP.Models.Investment;
using Finances.Database.Context;
using Finances.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

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
            PopulateSelectList();

            // Initialize the view model and add an initial entry for SelectedReserves
            var viewModel = new InvestmentCrudViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvestmentCrudViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (viewModel.EndDate < DateTime.Now)
                        throw new Exception("Data de resgate não é válida.");

                    // Create a new Investment entity based on the view model data
                    var investment = new Investment
                    {
                        Name = viewModel.Name,
                        Account = viewModel.Account,
                        Rentability = viewModel.Rentability,
                        Type = viewModel.Type,
                        CurrentAmount = viewModel.SelectedReserves.Sum(r => r.Amount),
                        StartAmount = viewModel.SelectedReserves.Sum(r => r.Amount),
                        EndDate = viewModel.EndDate,
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

                    TempData["success"] = "Investimento registrado com sucesso.";

                    return RedirectToAction("Index"); // Redirect to a list of investments or wherever you want
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"There was an error creating the investment.\n{ex.Message}";
            }

            // If ModelState is not valid, re-populate the dropdown
            PopulateSelectList();
            return View(viewModel);
        }

        // GET: Investments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Investments == null)
            {
                return NotFound();
            }

            var investment = await _context.Investments
                .Include(i => i.SourceReserves)
                .ThenInclude(s => s.Reserve)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (investment == null)
            {
                return NotFound();
            }

            PopulateSelectList();

            return View(new UpdateInvestmentViewModel
            {
                Id = investment.Id,
                Name = investment.Name,
                Account = investment.Account,
                Type = investment.Type,
                EndDate = investment.EndDate,
                CurrentAmount = investment.CurrentAmount,
                Rentability = investment.Rentability,
                SelectedReserves = investment.SourceReserves.Select(s => new UpdateReserveAmountViewModel
                {
                    ReserveId = s.ReserveId,
                    ReserveName = s.Reserve.Name + " - " + s.Reserve.Owner,
                    Amount = Math.Round(s.Amount, 2)
                }).ToList()
            });
        }

        // POST: Investments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateInvestmentViewModel viewModel)
        {
            var investment = await _context.Investments
                .Include(i => i.SourceReserves)
                .FirstOrDefaultAsync(i => i.Id == viewModel.Id);

            if (investment == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    investment.Name = viewModel.Name;
                    investment.CurrentAmount = viewModel.CurrentAmount;
                    investment.EndDate = viewModel.EndDate;

                    if (investment.CurrentAmount < investment.StartAmount)
                        investment.CurrentAmount = investment.StartAmount;

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

                TempData["success"] = "Investimento alterado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // POST: Investments/UpdateCurrentAmount/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCurrentAmount(UpdateInvestmentViewModel viewModel)
        {
            var investment = await _context.Investments
                .Include(i => i.SourceReserves)
                .FirstOrDefaultAsync(i => i.Id == viewModel.Id);

            if (investment == null)
            {
                return NotFound();
            }

            investment.CurrentAmount = viewModel.CurrentAmount;

            if (investment.CurrentAmount < investment.StartAmount)
                investment.CurrentAmount = investment.StartAmount;

            _context.Update(investment);
            await _context.SaveChangesAsync();

            TempData["success"] = "Investimento alterado com sucesso.";

            // Check if the method was called from the Delete page
            if (Request.Headers["Referer"].ToString().Contains("/Investments/Delete"))
            {
                return RedirectToAction("Delete", new { id = viewModel.Id });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Investments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Investments == null)
            {
                return NotFound();
            }

            var investment = await _context.Investments
                .Include(r => r.SourceReserves)
                    .ThenInclude(r => r.Reserve)
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
                .ThenInclude(s => s.Reserve)
                .ThenInclude(s => s.Entries)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (investment != null)
            {
                // Check if there is any amount to distribute in the source reserves.

                var income = investment.CurrentAmount - investment.StartAmount;
                if (income > 0)
                {
                    // Calculates the proportional amount for each source reserve

                    foreach (var reserve in investment.SourceReserves)
                    {
                        var proportional = (100 * reserve.Amount / investment.StartAmount) / 100;

                        var proportionalReserveAmount = proportional * income;

                        reserve.Reserve.Entries.Add(new()
                        {
                            Observation = $"Distribuíção dos lucros do investimento {investment.Name}",
                            Amount = Math.Round(proportionalReserveAmount, 2)
                        });
                    }
                }

                _context.ReserveInvestmentMaps.RemoveRange(investment.SourceReserves);
                _context.Investments.Remove(investment);

                await _context.SaveChangesAsync();
            }

            TempData["success"] = "Investimento liquidado com sucesso.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddReserve(Guid investmentId, Guid reserveId, decimal amount)
        {
            if (amount < 0)
                return Json(new { success = false, message = "Valor não pode ser menor que zero." });

            var reserve = _context.Reserves
                .Include(r => r.Entries)
                .Include(r => r.LinkedInvestments)
                .FirstOrDefault(r => r.Id == reserveId);

            var investment = _context.Investments
                .Include(r => r.SourceReserves)
                .FirstOrDefault(r => r.Id == investmentId);

            if (investment == null)
                return Json(new { success = false, message = "Investimento não encontrado." });

            if (reserve == null)
                return Json(new { success = false, message = "Reserva não encontrada." });

            var available = reserve.Entries.Sum(r => r.Amount) - reserve.LinkedInvestments.Sum(r => r.Amount);

            if (available < amount)
                throw new Exception("Saldo disponível insuficiente.");

            investment.SourceReserves.Add(new()
            {
                Amount = amount,
                Reserve = reserve
            });

            investment.StartAmount = investment.SourceReserves.Sum(r => r.Amount);

            if (investment.CurrentAmount < investment.StartAmount)
                investment.CurrentAmount = investment.StartAmount;

            await _context.SaveChangesAsync();

            TempData["success"] = "Reserva vinculada com sucesso.";

            return Json(new { success = true }); // Return a success response
        }

        [HttpPost]
        public IActionResult RemoveReserve(Guid investmentId, Guid reserveId)
        {
            var investment = _context.Investments
                .Include(i => i.SourceReserves)
                .FirstOrDefault(i => i.Id == investmentId);

            if (investment == null)
                return Json(new { success = false, message = "Investimento não encontrado." });

            var linkToRemove = investment.SourceReserves.FirstOrDefault(s => s.ReserveId == reserveId);

            if (linkToRemove != null)
            {
                investment.SourceReserves.Remove(linkToRemove);
                investment.StartAmount = investment.SourceReserves.Sum(r => r.Amount);
                _context.SaveChanges();
            }

            TempData["success"] = "Reserva desvinculada com sucesso.";

            return Json(new { success = true }); // Return a success response
        }


        private bool InvestmentExists(Guid id)
        {
            return (_context.Investments?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void PopulateSelectList()
        {
            var reserves = _context.Reserves.OrderBy(r => r.Name);

            Dictionary<string, string> selectValules = new();
            selectValules.AddRange(reserves.Select(r => new KeyValuePair<string, string>(r.Id.ToString(), $"{r.Name} - {r.Owner}")));

            ViewBag.AllReserves = new SelectList(selectValules, "Key", "Value");
        }
    }
}
