namespace Finanzas_Personales_App.Models
{
    public class OperacionUnificada
    {
		public int? IdOperacion => TipoOperacion == "Ingreso" ? IdIngreso : IdEgreso;
		public DateOnly Fecha { get; set; }
        public string TipoOperacion { get; set; } // "Ingreso" o "Egreso"
        public string Categoria { get; set; }
        public decimal? Monto { get; set; }

        // Propiedades útiles para la vista
        public string SignoMonto => TipoOperacion == "Ingreso" ? "+" : "-";
        public string ClaseCSS => TipoOperacion == "Ingreso" ? "text-success" : "text-danger";

        // Referencias a los objetos originales (opcional)
        public int? IdIngreso { get; set; }
        public int? IdEgreso { get; set; }
    }
}
