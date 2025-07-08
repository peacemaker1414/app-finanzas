namespace Finanzas_Personales_App.Models
{
    // Esta clase va a ser la encargada de mostrar un resumen de las operaciones
    public class OperacionesVM
    {
        // Elimina las listas separadas y reemplaza por:
        public List<OperacionUnificada> OperacionesCombinadas { get; set; } = new List<OperacionUnificada>();

        // Propiedades adicionales para cálculos
        public decimal? BalanceTotal => OperacionesCombinadas.Sum(o => o.TipoOperacion == "Ingreso" ? o.Monto : -o.Monto);
        public decimal? TotalIngresos => OperacionesCombinadas.Where(o => o.TipoOperacion == "Ingreso").Sum(o => o.Monto);
        public decimal? TotalEgresos => OperacionesCombinadas.Where(o => o.TipoOperacion == "Egreso").Sum(o => o.Monto);
    }
}
