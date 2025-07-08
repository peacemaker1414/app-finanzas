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
            var vm = new OperacionesVM();

            if (!Guid.TryParse(idUsuarioStr, out var idUsuario))
            {
                // Usuario no logeado correctamente o sesión vencida
                return Unauthorized();
            }
            else
            {


                var ingresos = _DbContext.Ingresos
                    .Where(i => i.IdUsuario == idUsuario)
                    .Include(i => i.IdCatIngresoNavigation)
                    .ToList()
                    .Select(i => new OperacionUnificada
                    {
                        Fecha = i.Fecha,
                        TipoOperacion = "Ingreso",
                        Categoria = i.IdCatIngresoNavigation.CategoriaIngreso,
                        Monto = i.Monto,
                        IdIngreso = i.IdIngreso
                    });
                var egresos = _DbContext.Egresos
                       .Where(e => e.IdUsuario == idUsuario)
                       .Include(e => e.IdCatEgresoNavigation)
                       .ToList()
                       .Select(e => new OperacionUnificada
                       {
                           Fecha = e.Fecha,
                           TipoOperacion = "Egreso",
                           Categoria = e.IdCatEgresoNavigation.CategoriaEgreso,
                           Monto = e.Monto,
                           IdEgreso = e.IdEgreso
                       })
                   .OrderByDescending(o => o.Fecha); // Orden descendente para ver lo más reciente primero

                vm.OperacionesCombinadas = ingresos
                .Concat(egresos)
                .OrderByDescending(o => o.Fecha)
                .ToList();
                return View(vm);
            }
        }

        public IActionResult Modificar(string tipoOperacion, int idOperacion)
        {
            if (tipoOperacion == "Ingreso")
            {
                
                return RedirectToAction("Modificar", "Ingreso", new { idIngreso = idOperacion });
            }
            else
            {
                return RedirectToAction("Modificar", "Egreso", new { idEgreso = idOperacion });
            }
        }

		public IActionResult Eliminar(string tipoOperacion, int idOperacion)
		{
			if (tipoOperacion == "Ingreso")
			{

				return RedirectToAction("Eliminar", "Ingreso", new { idIngreso = idOperacion });
			}
			else
			{
				return RedirectToAction("Eliminar", "Egreso", new { idEgreso = idOperacion });
			}
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
