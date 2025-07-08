using System;
using System.Collections.Generic;

namespace Finanzas_Personales_App.Models;

public partial class Ingreso
{
    public int IdIngreso { get; set; }

    public Guid? IdUsuario { get; set; }

    public DateOnly Fecha { get; set; }

    public int IdCatIngreso { get; set; }

    public string? DetalleIngreso { get; set; }

    public decimal? Monto { get; set; }

    public virtual LookCategoriasIngreso? IdCatIngresoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
