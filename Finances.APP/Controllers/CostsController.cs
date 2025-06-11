using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finances.Database.Context;
using Finances.Database.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using Finances.Database.Enums;
using Microsoft.AspNetCore.Authorization;
using Finances.Database;

namespace Finances.APP.Controllers
{
    [Authorize]
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
                        View(await _context.Costs.Include(c => c.Reserve).ToListAsync()) :
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
                .Include(c => c.Reserve)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // GET: Costs/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Reserves = await _context.Reserves
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = $"{r.Name} - {r.Owner}"
                })
                .ToListAsync();
            return View();
        }

        // POST: Costs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,Type,Name,Description,Id,DanielPercentage,CassiaPercentage,DateCreated,LastUpdate,ReserveId")] Cost cost)
        {
            if (ModelState.IsValid)
            {
                cost.Id = Guid.NewGuid();
                _context.Add(cost);
                await _context.SaveChangesAsync();

                TempData["success"] = "Custo Fixo criado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Reserves = await _context.Reserves
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = $"{r.Name} - {r.Owner}"
                })
                .ToListAsync();
            return View(cost);
        }

        // GET: Costs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Costs == null)
            {
                return NotFound();
            }

            var cost = await _context.Costs.Include(c => c.Payments).FirstOrDefaultAsync(c => c.Id == id);
            if (cost == null)
            {
                return NotFound();
            }

            ViewBag.Reserves = await _context.Reserves
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = $"{r.Name} - {r.Owner}"
                })
                .ToListAsync();
            return View(cost);
        }

        // POST: Costs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Amount,Type,Name,Description,Id,DanielPercentage,CassiaPercentage,DateCreated,LastUpdate,ReserveId")] Cost cost)
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

                TempData["success"] = "Custo Fixo alterado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Reserves = await _context.Reserves
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = $"{r.Name} - {r.Owner}"
                })
                .ToListAsync();
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
                .Include(c => c.Reserve)
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

            TempData["success"] = "Custo Fixo excluído com sucesso.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Costs/{id}/Payment")]
        public async Task<IActionResult> Payment(Guid id, decimal paidAmount, DateTime paidDate)
        {
            var cost = await _context.Costs
                .Include(c => c.Payments)
                .Include(c => c.Reserve)
                    .ThenInclude(r => r.Entries)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cost == null)
            {
                return NotFound();
            }

            var payment = new Payment
            {
                PaidAmount = paidAmount,
                DatePaid = paidDate,
                DateCreated = DateTime.Now,
                LastUpdate = DateTime.Now
            };

            cost.Payments.Add(payment);

            // Se o custo tem uma reserva vinculada, adiciona um lançamento negativo
            if (cost.Reserve != null)
            {
                // Verifica se a reserva tem saldo suficiente
                var reserveBalance = cost.Reserve.Entries.Sum(e => e.Amount);

                if (reserveBalance < paidAmount)
                {
                    return BadRequest("Saldo insuficiente na reserva para realizar o pagamento.");
                }

                var entry = new Entry
                {
                    Amount = -paidAmount, // Valor negativo
                    Observation = $"Pagamento - {cost.Name}",
                    DateCreated = DateTime.Now,
                    LastUpdate = DateTime.Now
                };

                cost.Reserve.Entries.Add(entry);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = id });
        }

        [HttpDelete]
        [Route("Costs/DeletePayment/{paymentId}")]
        public async Task<IActionResult> DeletePayment(Guid paymentId)
        {
            var payment = await _context.CostPayments
                .Include(p => p.Cost)
                    .ThenInclude(c => c.Reserve)
                        .ThenInclude(r => r.Entries)
                .FirstOrDefaultAsync(p => p.Id == paymentId);

            if (payment == null)
            {
                return NotFound();
            }

            // Se o custo tem uma reserva vinculada, adiciona um lançamento positivo
            if (payment.Cost.Reserve != null)
            {
                var entry = new Entry
                {
                    Amount = payment.PaidAmount, // Valor positivo
                    Observation = $"Estorno pagamento - {payment.Cost.Name}",
                    DateCreated = DateTime.Now,
                    LastUpdate = DateTime.Now
                };

                payment.Cost.Reserve.Entries.Add(entry);
            }

            _context.CostPayments.Remove(payment);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> PendingMonthlyCosts()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var pendingCosts = await _context.Costs
                .Include(c => c.Payments)
                .Where(c => c.Type == CostType.Month)
                .Where(c => !c.Payments.Any(p => p.DatePaid.Month == currentMonth && p.DatePaid.Year == currentYear))
                .Select(c => new {
                    id = c.Id,
                    name = c.Name,
                    amount = c.Amount
                })
                .ToListAsync();

            return Json(pendingCosts);
        }

        private bool CostExists(Guid id)
        {
            return (_context.Costs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
