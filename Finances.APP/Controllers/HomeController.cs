using Finances.APP.Models;
using Finances.APP.Models.Reserve;
using Finances.Database.Context;
using Finances.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Finances.APP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Total Acumulado e Investido (somatório das reservas)
            var reservas = _context.Reserves.Include(r => r.Entries).Include(r => r.LinkedInvestments);

            var totalAcumulado = reservas.Sum(e => e.Entries.Sum(s => s.Amount));
            var totalInvestido = reservas.Sum(r => r.LinkedInvestments.Sum(i => i.Amount));
            
            // Total de Entradas (IncomeSources)
            var totalEntradas = _context.IncomeSources.Where(e => e.IsActive).Sum(e => e.Amount);

            // Total de Custos Fixos (Costs)
            var totalCustosFixos = _context.Costs.Sum(c => c.Amount);
            
            // Meta de Investimento (somatório das metas de investimento de todas as reservas)
            var metaInvestimento = _context.Reserves.Sum(r => (r.MonthlyGoal ?? 0));

            var saldoDisponivel = totalEntradas - (metaInvestimento + totalCustosFixos);

            var totals = new DashboardTotalsViewModel
            {
                TotalAcumulado = totalAcumulado,
                TotalInvestido = totalInvestido,
                TotalEntradas = totalEntradas,
                TotalCustosFixos = totalCustosFixos,
                MetaInvestimento = metaInvestimento,
                SaldoDisponivel = saldoDisponivel
            };

            ViewBag.DashboardTotals = totals;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}