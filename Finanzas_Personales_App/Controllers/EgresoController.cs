using Finanzas_Personales_App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finanzas_Personales_App.Controllers
{
    public class EgresoController : Controller
    {
        private readonly FinanzasDbContext _DbContext;

        public EgresoController(FinanzasDbContext context)
        {
            _DbContext = context;
        }


		[HttpGet]
		public ActionResult Agregar()
        {
            EgresoVM modelEgreso = new EgresoVM();
            modelEgreso.Categorias_Egresos = _DbContext.LookCategoriasEgresos.Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Text = categoria.SubcategoriaEgreso,
                Value = categoria.IdCatEgreso.ToString()
            }).ToList();


            return View(modelEgreso);
            
        }


		[HttpPost]
		public ActionResult Agregar(EgresoVM model)
		{
			if (model.Egreso.IdUsuario == null)
			{
				var idUsuarioStr = User.FindFirst("UsuarioId")?.Value;


				if (!Guid.TryParse(idUsuarioStr, out var idUsuario))
				{
					// Usuario no logeado correctamente o sesión vencida
					return Unauthorized();
				}
				else
				{
					model.Egreso.IdUsuario = idUsuario;
				}
			}
			_DbContext.Egresos.Add(model.Egreso);
			_DbContext.SaveChanges();

			TempData["MensajeSuccess"] = $"¡Se cargó correctamente el Egreso!";

			EgresoVM modelEgreso = new EgresoVM();
			modelEgreso.Categorias_Egresos = _DbContext.LookCategoriasEgresos.Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
			{
				Text = categoria.CategoriaEgreso,
				Value = categoria.IdCatEgreso.ToString()
			}).ToList();


			return View(modelEgreso);
		}

        [HttpGet]
        public IActionResult Modificar(int idEgreso)
        {  
                EgresoVM modelEgreso = new EgresoVM();
                modelEgreso.Egreso = _DbContext.Egresos
				                      .Include(i => i.IdCatEgresoNavigation)
				                      .FirstOrDefault(e => e.IdEgreso == idEgreso);
                modelEgreso.Categorias_Egresos = _DbContext.LookCategoriasEgresos.Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Text = categoria.SubcategoriaEgreso,
                    Value = categoria.IdCatEgreso.ToString()
                }).ToList();

                return View(modelEgreso);
            
        }

        [HttpPost]
        public IActionResult Modificar(EgresoVM model)
        {

			if (model.Egreso.IdUsuario == null)
			{
				var idUsuarioStr = User.FindFirst("UsuarioId")?.Value;


				if (!Guid.TryParse(idUsuarioStr, out var idUsuario))
				{
					// Usuario no logeado correctamente o sesión vencida
					return Unauthorized();
				}
				else
				{
					model.Egreso.IdUsuario = idUsuario;
				}
			}
			_DbContext.Egresos.Update(model.Egreso);
			_DbContext.SaveChanges();

			TempData["MensajeSuccess"] = $"¡Se modificó correctamente el Egreso!";

			EgresoVM modelEgreso = new EgresoVM();
			modelEgreso.Categorias_Egresos = _DbContext.LookCategoriasEgresos.Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
			{
				Text = categoria.CategoriaEgreso,
				Value = categoria.IdCatEgreso.ToString()
			}).ToList();


			return RedirectToAction("Resumen", "Operaciones");
		}

        [HttpGet]
        public IActionResult Eliminar(int idEgreso)
        {
            EgresoVM modelEgreso = new EgresoVM();
            modelEgreso.Egreso = _DbContext.Egresos
				                .Include(i => i.IdCatEgresoNavigation)
				                .FirstOrDefault(e => e.IdEgreso == idEgreso);
            modelEgreso.Categorias_Egresos = _DbContext.LookCategoriasEgresos.Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Text = categoria.SubcategoriaEgreso,
                Value = categoria.IdCatEgreso.ToString()
            }).ToList();

            return View(modelEgreso);

        }

        [HttpPost]
        public IActionResult Eliminar(EgresoVM model)
        {

            if (model.Egreso.IdUsuario == null)
            {
                var idUsuarioStr = User.FindFirst("UsuarioId")?.Value;


                if (!Guid.TryParse(idUsuarioStr, out var idUsuario))
                {
                    // Usuario no logeado correctamente o sesión vencida
                    return Unauthorized();
                }
                else
                {
                    model.Egreso.IdUsuario = idUsuario;
                }
            }
            _DbContext.Egresos.Remove(model.Egreso);
            _DbContext.SaveChanges();

            TempData["MensajeSuccess"] = $"¡Se modificó correctamente el Egreso!";

            EgresoVM modelEgreso = new EgresoVM();
            modelEgreso.Categorias_Egresos = _DbContext.LookCategoriasEgresos.Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Text = categoria.CategoriaEgreso,
                Value = categoria.IdCatEgreso.ToString()
            }).ToList();


            return RedirectToAction("Resumen", "Operaciones");
		}


        // POST: EgresoController/Create
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

        // GET: EgresoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EgresoController/Edit/5
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

        // GET: EgresoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EgresoController/Delete/5
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
