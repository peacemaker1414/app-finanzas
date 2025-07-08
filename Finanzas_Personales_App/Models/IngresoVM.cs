using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finanzas_Personales_App.Models
{
	public class IngresoVM
	{
		public Ingreso Ingreso { get; set; } = new Ingreso();

		public List<SelectListItem> Categorias_Ingresos { get; set; } = new List<SelectListItem>();
	}
}
