using Finanzas_Personales_App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finanzas_Personales_App.Controllers
{
    public class OperacionesController : Controller
    {
        private readonly FinanzasDbContext _DbContext;

        public OperacionesController(FinanzasDbContext context)
        {
            _DbContext = context;
        }
        // GET: OperacionesController
        [HttpGet]
        public ActionResult Resumen()
        {
            var idUsuarioStr = User.FindFirst("UsuarioId")?.Value;
            OperacionesVM operaciones = new OperacionesVM();

            if (!Guid.TryParse(idUsuarioStr, out var idUsuario))
            {
                // Usuario no logeado correctamente o sesión vencida
                return Unauthorized();
            }
            else
            {
                
                
                operaciones._listaIngresos = _DbContext.Ingresos
                    .Where(i => i.IdUsuario == idUsuario)
                    .Include(i=>i.IdCatIngresoNavigation)
                    .OrderBy(i => i.Fecha)
                    .ToList();
            }

            
            return View(operaciones);
        }

        // GET: OperacionesController/Details/5
        public ActionResult Details(int id) 
        {
            return View();
        }

        // GET: OperacionesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OperacionesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OperacionesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OperacionesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OperacionesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OperacionesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
