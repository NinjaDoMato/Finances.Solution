using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Finances.Database.Context;
using Finances.Database.Entities;
using Finances.APP.Models.Purchase;
using Finances.Database.Migrations;

namespace Finances.APP.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly DatabaseContext _context;

        public PurchasesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            return _context.Purchases != null ?
                        View(await _context.Purchases.ToListAsync()) :
                        Problem("Entity set 'DatabaseContext.Purchases'  is null.");
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ProductUrl,Amount,Id,DateCreated,Installments,PurchaseDate,LastUpdate,Owner")] CreatePurchaseViewModel purchaseViewModel)
        {
            if (ModelState.IsValid)
            {
                var purchase = new Purchase
                {
                    Id = Guid.NewGuid(),
                    Name = purchaseViewModel.Name,
                    Installments = new List<Installment>(),
                    Owner = purchaseViewModel.Owner,
                    ProductUrl = purchaseViewModel.ProductUrl
                };

                var installmentAmount = Math.Round(purchaseViewModel.Amount / purchaseViewModel.Installments, 2);

                for (int i = 1; i <= purchaseViewModel.Installments; i++)
                {
                    purchase.Installments.Add(new Installment()
                    {
                        InstallmentNumber = i,
                        Paid = false,
                        Amount = installmentAmount,
                        DueDate = DateTime.UtcNow.AddMonths(i),
                        PaidDate = null,
                        PaymentUrl = string.Empty
                    });
                }

                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseViewModel);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.Installments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,ProductUrl,Id,Owner,DateCreated,LastUpdate")] Purchase purchase)
        {
            try
            {
                if (id != purchase.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(purchase);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PurchaseExists(purchase.Id))
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

                TempData["success"] = "Parcelamento alterado com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var purchase = await _context.Purchases.Include(p => p.Installments).FirstOrDefaultAsync(p => p.Id == id);
            try
            {
                if (_context.Purchases == null)
                {
                    return Problem("Entity set 'DatabaseContext.Purchases'  is null.");
                }

                if (purchase != null)
                {
                    foreach (var installment in purchase.Installments)
                    {
                        _context.PurchaseInstallments.Remove(installment);
                    }

                    _context.Purchases.Remove(purchase);
                }

                await _context.SaveChangesAsync();

                TempData["success"] = "Parcelamento excluído com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View(purchase);
            }
        }

        [HttpPost()]
        [Route("/Purchases/{purchaseId}/Payments/{paymentId}/Pay")]
        public async Task<IActionResult> PayInstallment(Guid purchaseId, Guid paymentId)
        {
            try
            {
                var purchase = await _context.Purchases
                    .Include(p => p.Installments)
                    .FirstOrDefaultAsync(p => p.Id == purchaseId);

                if (purchase == null)
                    throw new Exception("Parcelmento não encontrado.");

                var installment = purchase.Installments.FirstOrDefault(p => p.Id == paymentId);

                if (installment == null)
                    throw new Exception("Parcela não encontrado.");

                installment.Paid = true;
                installment.PaidDate = DateTime.UtcNow;

                _context.Update(installment);
                await _context.SaveChangesAsync();

                TempData["success"] = "Pagamento registrado com sucesso!";
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return Json(new { success = false, message = ex.Message });
            }
        }

        private bool PurchaseExists(Guid id)
        {
            return (_context.Purchases?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
