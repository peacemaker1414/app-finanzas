using System;
using System.Collections.Generic;

namespace Finanzas_Personales_App.Models;

public partial class Egreso
{
    public int IdEgreso { get; set; }

    public Guid? IdUsuario { get; set; }

    public DateOnly Fecha { get; set; }

    public int IdCatEgreso { get; set; }

    public string? DetalleEgreso { get; set; }

    public decimal? Monto { get; set; }

    public virtual LookCategoriasEgreso IdCatEgresoNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
