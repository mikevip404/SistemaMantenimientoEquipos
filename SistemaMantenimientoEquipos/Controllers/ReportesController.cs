using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaMantenimientoEquipos.Data;
using SistemaMantenimientoEquipos.Models;

namespace SistemaMantenimientoEquipos.Controllers
{
    [Authorize]
    public class ReportesController : Controller
    {
        private readonly SistemaMantenimientoEquiposContext _context;
        private readonly UserManager<Usuario> _userManager;

        public ReportesController(SistemaMantenimientoEquiposContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reportes
        public async Task<IActionResult> Index(string estado, string tecnicoAsignadoId, DateTime? fechaDesde, DateTime? fechaHasta, int? equipoId)
        {
            IQueryable<Reporte> query = _context.Reportes
                .Include(r => r.Usuario)
                .Include(r => r.Equipo);

            // Filter reports based on user role
            if (!User.IsInRole("Admin") && !User.IsInRole("Coordinador"))
            {
                // Regular users can only see their own reports
                var userId = _userManager.GetUserId(User);
                query = query.Where(r => r.UsuarioId == userId);
            }

            // Apply filters
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(r => r.Estado == estado);
            }
            if (fechaDesde.HasValue)
            {
                query = query.Where(r => r.FechaReporte >= fechaDesde.Value);
            }
            if (fechaHasta.HasValue)
            {
                query = query.Where(r => r.FechaReporte <= fechaHasta.Value);
            }
            if (equipoId.HasValue)
            {
                query = query.Where(r => r.EquipoId == equipoId.Value);
            }

            // Pass filter values to view
            ViewData["Estado"] = estado;
            ViewData["FechaDesde"] = fechaDesde?.ToString("yyyy-MM-dd");
            ViewData["FechaHasta"] = fechaHasta?.ToString("yyyy-MM-dd");
            ViewData["EquipoId"] = equipoId;

            // Populate dropdowns
            ViewBag.Equipos = new SelectList(_context.Equipos, "EquipoId", "Nombre", equipoId);

            return View(await query.ToListAsync());
        }

        // GET: Reportes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporte = await _context.Reportes
                .Include(r => r.Equipo)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reporte == null)
            {
                return NotFound();
            }

            return View(reporte);
        }

        // GET: Reportes/Create
        public IActionResult Create()
        {
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "Nombre");
            return View();
        }

        // POST: Reportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,FechaReporte,Estado,EquipoId")] Reporte reporte)
        {
            if (ModelState.IsValid)
            {
                // Capturar el usuario actual
                var userId = _userManager.GetUserId(User);
                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError("UsuarioId", "No se pudo obtener el usuario actual.");
                    ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "Nombre", reporte.EquipoId);
                    return View(reporte);
                }
                reporte.UsuarioId = userId;
            
                _context.Add(reporte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "Nombre", reporte.EquipoId);
            return View(reporte);
        }

        // GET: Reportes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporte = await _context.Reportes
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (reporte == null)
            {
                return NotFound();
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "Nombre", reporte.EquipoId);
            return View(reporte);
        }

        // POST: Reportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,FechaReporte,Estado,EquipoId")] Reporte reporte)
        {
            if (id != reporte.Id)
            {
                return NotFound();
            }

            // Preserve the existing UsuarioId
            var existingReporte = await _context.Reportes.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if (existingReporte != null)
            {
                reporte.UsuarioId = existingReporte.UsuarioId;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reporte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReporteExists(reporte.Id))
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
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", reporte.EquipoId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", reporte.UsuarioId);
            return View(reporte);
        }

        // GET: Reportes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporte = await _context.Reportes
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reporte == null)
            {
                return NotFound();
            }

            return View(reporte);
        }

        // POST: Reportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte != null)
            {
                _context.Reportes.Remove(reporte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReporteExists(int id)
        {
            return _context.Reportes.Any(e => e.Id == id);
        }
    }
}
