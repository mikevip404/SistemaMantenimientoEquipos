using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMantenimientoEquipos.Data;
using SistemaMantenimientoEquipos.Models;
using System.Diagnostics;

namespace SistemaMantenimientoEquipos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SistemaMantenimientoEquiposContext _context;
        private readonly UserManager<Usuario> _userManager;

        public HomeController(ILogger<HomeController> logger, SistemaMantenimientoEquiposContext context, UserManager<Usuario> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.EquiposCount = await _context.Equipos.CountAsync();
            ViewBag.ReportesCount = await _context.Reportes.CountAsync();

            // Contar equipos por estado
            var equiposActivos = await _context.Equipos.CountAsync(e => e.Estado == "Activo");
            var equiposEnReparacion = await _context.Equipos.CountAsync(e => e.Estado == "En Reparación");
            var equiposFueraServicio = await _context.Equipos.CountAsync(e => e.Estado == "Fuera de Servicio");

            ViewBag.EquiposActivos = equiposActivos;
            ViewBag.EquiposEnReparacion = equiposEnReparacion;
            ViewBag.EquiposFueraServicio = equiposFueraServicio;

            // Reportes recientes
            var reportesRecientes = await _context.Reportes
                .Include(r => r.Equipo)
                .OrderByDescending(r => r.FechaReporte)
                .ThenByDescending(r => r.Id)
                .Take(5)
                .ToListAsync();
            ViewBag.ReportesRecientes = reportesRecientes;

            // Tipos de equipos
            var tiposEquipos = await _context.Equipos
                .GroupBy(e => e.Tipo)
                .Select(g => new { Tipo = g.Key, Cantidad = g.Count() })
                .ToListAsync();
            ViewBag.TiposEquipos = tiposEquipos;

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
