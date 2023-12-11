using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Piezas { get; set; }
        public decimal PrecioUnitario { get; set; }

        public List<object> Productos { get; set; }
    }
}
