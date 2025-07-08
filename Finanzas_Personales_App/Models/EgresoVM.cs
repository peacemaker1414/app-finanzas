using Microsoft.AspNetCore.Mvc.Rendering;

namespace Finanzas_Personales_App.Models
{
    public class EgresoVM
    {
        public Egreso Egreso { get; set; } = new Egreso();

        public List<SelectListItem> Categorias_Egresos { get; set; } = new List<SelectListItem>();
    }
}
