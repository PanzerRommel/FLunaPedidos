using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Transaccion
    {
        public int IdTransaccion { get; set; }
        public DateTime Fecha { get; set; }
        public ML.Usuario Usuario { get; set;}
        public ML.Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public decimal MontoIngresado { get; set; }
        public decimal Cambio { get; set; }

        public List<object> Transacciones { get; set; } 
    }
}
