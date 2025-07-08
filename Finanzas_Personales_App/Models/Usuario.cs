using System;
using System.Collections.Generic;

namespace Finanzas_Personales_App.Models;

public partial class Usuario
{
    public Guid IdUsuario { get; set; }

    public string Email { get; set; } = null!;

    public string? Nombre { get; set; }

    public DateTime? Fecharegistro { get; set; }

    public virtual ICollection<Egreso> Egresos { get; set; } = new List<Egreso>();

    public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
}
