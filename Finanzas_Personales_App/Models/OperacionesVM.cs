namespace Finanzas_Personales_App.Models
{
	// Esta clase va a ser la encargada de mostrar un resumen de las operaciones
	public class OperacionesVM
	{
		public List<Ingreso> _listaIngresos { get; set; } = new List<Ingreso>();

		public List<Egreso> _listaEgresos { get; set; } = new List<Egreso>();
	}
}
