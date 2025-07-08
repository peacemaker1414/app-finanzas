using Finanzas_Personales_App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finanzas_Personales_App.Controllers
{
	public class IngresoController : Controller
	{
		private readonly FinanzasDbContext _DbContext;

		public IngresoController(FinanzasDbContext context)
		{
			_DbContext = context;
		}

		[HttpGet]
		public ActionResult Agregar()
		{
			IngresoVM modelIngreso = new IngresoVM();
			modelIngreso.Categorias_Ingresos = _DbContext.LookCategoriasIngresos.Distinct().Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
			{
				Text = categoria.SubcategoriaIngreso,
				Value = categoria.IdCatIngreso.ToString()
			}).ToList();

			
			return View(modelIngreso);
		}

		[HttpPost]
		public ActionResult Agregar(IngresoVM model)
		{
			if(model.Ingreso.IdUsuario == null)
			{
				var idUsuarioStr = User.FindFirst("UsuarioId")?.Value;


				if (!Guid.TryParse(idUsuarioStr, out var idUsuario))
				{
					// Usuario no logeado correctamente o sesión vencida
					return Unauthorized();
				}
				else
				{
					model.Ingreso.IdUsuario = idUsuario;
				}
			}
			_DbContext.Ingresos.Add(model.Ingreso);
			_DbContext.SaveChanges();

			TempData["MensajeSuccess"] = $"¡Se cargó correctamente el ingreso!";

			IngresoVM modelIngreso = new IngresoVM();
			modelIngreso.Categorias_Ingresos = _DbContext.LookCategoriasIngresos.Distinct().Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
			{
				Text = categoria.SubcategoriaIngreso,
				Value = categoria.IdCatIngreso.ToString()
			}).ToList();


			return View(modelIngreso);
		}

		[HttpGet]
		public IActionResult Modificar(int idIngreso)
		{
				IngresoVM modelIngreso = new IngresoVM();
				modelIngreso.Ingreso = _DbContext.Ingresos
										.Include(i => i.IdCatIngresoNavigation)
										.FirstOrDefault(i => i.IdIngreso == idIngreso);
				modelIngreso.Categorias_Ingresos = _DbContext.LookCategoriasIngresos.Distinct().Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
				{
					Text = categoria.SubcategoriaIngreso,
					Value = categoria.IdCatIngreso.ToString()
				}).ToList();

				return View(modelIngreso);
		}

		[HttpPost]
		public IActionResult Modificar(IngresoVM model)
		{
			if (model.Ingreso.IdUsuario == null)
			{
				var idUsuarioStr = User.FindFirst("UsuarioId")?.Value;


				if (!Guid.TryParse(idUsuarioStr, out var idUsuario))
				{
					// Usuario no logeado correctamente o sesión vencida
					return Unauthorized();
				}
				else
				{
					model.Ingreso.IdUsuario = idUsuario;
				}
			}
			_DbContext.Ingresos.Update(model.Ingreso);
			_DbContext.SaveChanges();

			TempData["MensajeSuccess"] = $"¡Se modificó correctamente el ingreso!";

			IngresoVM modelIngreso = new IngresoVM();
			modelIngreso.Categorias_Ingresos = _DbContext.LookCategoriasIngresos.Distinct().Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
			{
				Text = categoria.SubcategoriaIngreso,
				Value = categoria.IdCatIngreso.ToString()
			}).ToList();


			return RedirectToAction("Resumen", "Operaciones");
		}

        [HttpGet]
        public IActionResult Eliminar(int idIngreso)
        {
            IngresoVM modelIngreso = new IngresoVM();
            modelIngreso.Ingreso = _DbContext.Ingresos
									.Include(i => i.IdCatIngresoNavigation)					
									.FirstOrDefault(i => i.IdIngreso == idIngreso);
            modelIngreso.Categorias_Ingresos = _DbContext.LookCategoriasIngresos.Distinct().Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Text = categoria.SubcategoriaIngreso,
                Value = categoria.IdCatIngreso.ToString()
            }).ToList();

            return View(modelIngreso);
        }

        [HttpPost]
        public IActionResult Eliminar(IngresoVM model)
        {
            if (model.Ingreso.IdUsuario == null)
            {
                var idUsuarioStr = User.FindFirst("UsuarioId")?.Value;


                if (!Guid.TryParse(idUsuarioStr, out var idUsuario))
                {
                    // Usuario no logeado correctamente o sesión vencida
                    return Unauthorized();
                }
                else
                {
                    model.Ingreso.IdUsuario = idUsuario;
                }
            }
            _DbContext.Ingresos.Remove(model.Ingreso);
            _DbContext.SaveChanges();

            TempData["MensajeSuccess"] = $"¡Se modificó correctamente el ingreso!";

            IngresoVM modelIngreso = new IngresoVM();
            modelIngreso.Categorias_Ingresos = _DbContext.LookCategoriasIngresos.Distinct().Select(categoria => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Text = categoria.SubcategoriaIngreso,
                Value = categoria.IdCatIngreso.ToString()
            }).ToList();


            return RedirectToAction("Resumen", "Operaciones");
        }

        // GET: IngresoController/Details/5
        public ActionResult Details(int id)
		{
			return View();
		}

		// GET: IngresoController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: IngresoController/Create
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

		// GET: IngresoController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: IngresoController/Edit/5
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

		// GET: IngresoController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: IngresoController/Delete/5
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
