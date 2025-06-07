using Finances.Database.Context;
using Finances.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using Microsoft.AspNetCore.Authorization;

namespace Finances.APP.Controllers
{
    [Authorize]
    public class EntriesController : Controller
    {
        private readonly DatabaseContext _context;

        public EntriesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Entries
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Entries.Include(e => e.Reserve);
            return View(await databaseContext.OrderByDescending(e => e.DateCreated).ToListAsync());
        }

        // GET: Entries/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Entries == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries
                .Include(e => e.Reserve)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // GET: Entries/Create
        public IActionResult Create()
        {
            PopulateSelectList();

            return View();
        }

        // POST: Entries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,ReserveId,Id,Observation,DateCreated,LastUpdate")] Entry entry)
        {
            entry.Observation ??= string.Empty;
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                entry.Id = Guid.NewGuid();
                var reserve = _context.Reserves.Find(entry.ReserveId);

                if (reserve == null)
                    throw new Exception("Reserva não encontrada.");

                entry.Reserve = reserve;

                _context.Add(entry);
                await _context.SaveChangesAsync();

                TempData["success"] = "Lançamento registrado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            ViewData["ReserveId"] = new SelectList(_context.Reserves, "Id", "Description", entry.ReserveId);
            return View(entry);
        }

        // GET: Entries/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Entries == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            ViewData["ReserveId"] = new SelectList(_context.Reserves, "Id", "Description", entry.ReserveId);
            return View(entry);
        }

        // POST: Entries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Amount,ReserveId,Id,DateCreated,LastUpdate")] Entry entry)
        {
            if (id != entry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntryExists(entry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["success"] = "Lançamento alterado com sucesso.";

                return RedirectToAction(nameof(Index));
            }
            PopulateSelectList();
            return View(entry);
        }

        // GET: Entries/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Entries == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries
                .Include(e => e.Reserve)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // POST: Entries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Entries == null)
            {
                return Problem("Entity set 'DatabaseContext.Entries'  is null.");
            }
            var entry = await _context.Entries.FindAsync(id);
            if (entry != null)
            {
                _context.Entries.Remove(entry);
            }

            await _context.SaveChangesAsync();

            TempData["success"] = "Lançamento excluído com sucesso.";

            return RedirectToAction(nameof(Index));
        }

        private bool EntryExists(Guid id)
        {
            return (_context.Entries?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void PopulateSelectList()
        {
            var reserves = _context.Reserves.OrderBy(r => r.Name);

            Dictionary<string, string> selectValules = new();
            selectValules.AddRange(reserves.Select(r => new KeyValuePair<string, string>(r.Id.ToString(), $"{r.Name} - {r.Owner}")));

            ViewData["ReserveId"] = new SelectList(selectValules, "Key", "Value");
        }
    }
}
