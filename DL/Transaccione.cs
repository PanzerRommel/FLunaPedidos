using System;
using System.Collections.Generic;

namespace DL;

public partial class Transaccione
{
    public int IdTransaccion { get; set; }

    public DateTime Fecha { get; set; }

    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; }
    public int IdProducto { get; set; }
    public string NombreProducto { get; set; }
    public int Cantidad { get; set; }

    public decimal Total { get; set; }

    public decimal MontoIngresado { get; set; }

    public decimal Cambio { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
