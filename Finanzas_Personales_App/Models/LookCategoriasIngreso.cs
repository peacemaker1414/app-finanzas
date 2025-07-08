using System;
using System.Collections.Generic;

namespace Finanzas_Personales_App.Models;

public partial class LookCategoriasIngreso
{
    public int IdCatIngreso { get; set; }

    public string? CategoriaIngreso { get; set; }

    public string? SubcategoriaIngreso { get; set; }

    public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
}
