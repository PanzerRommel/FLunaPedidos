using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
