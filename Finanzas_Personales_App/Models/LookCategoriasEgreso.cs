using System;
using System.Collections.Generic;

namespace Finanzas_Personales_App.Models;

public partial class LookCategoriasEgreso
{
    public int IdCatEgreso { get; set; }

    public string? CategoriaEgreso { get; set; }

    public string? SubcategoriaEgreso { get; set; }

    public virtual ICollection<Egreso> Egresos { get; set; } = new List<Egreso>();
}
