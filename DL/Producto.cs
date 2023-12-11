using System;
using System.Collections.Generic;

namespace DL;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int Piezas { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual ICollection<Transaccione> Transacciones { get; set; } = new List<Transaccione>();
}
