using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class TransaccionController : Controller
    {
        [HttpGet]
        public ActionResult GetAll() 
        {
            ML.Transaccion transaccion = new ML.Transaccion(); // instancia para las propiedades
            ML.Result result = BL.Transaccion.GetAll();

            if (result.Correct)
            {
                transaccion.Transacciones = result.Objects;
                return View(transaccion);
            }
            return View(transaccion);
        }
        [HttpGet]
        public ActionResult Form(int? IdTransaccion)
        {
            ML.Transaccion transaccion = new ML.Transaccion();
            ML.Result resultUsuario = BL.Usuario.GetAll();
            ML.Result resultProducto = BL.Producto.GetAll();

            transaccion.Usuario = new ML.Usuario();
            transaccion.Producto = new ML.Producto();

            if(IdTransaccion == null)
            {
                transaccion.Usuario.Usuarios = resultUsuario.Objects;
                transaccion.Producto.Productos = resultProducto.Objects;
                return View(transaccion);
            }
            else
            {
                ML.Result result = BL.Transaccion.GetById(IdTransaccion.Value);

                if (result.Correct)
                {
                    transaccion = ((ML.Transaccion)result.Object);

                    transaccion.Usuario.Usuarios = resultUsuario.Objects;
                    transaccion.Producto.Productos = resultProducto.Objects;

                    return View(transaccion);
                }
                else
                {
                    ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                    return View("Modal");
                }
            }
        }
        public ActionResult Form(ML.Transaccion transaccion)
        {
            ML.Result result = new ML.Result();
            if(transaccion.IdTransaccion == 0)
            {
                result = BL.Transaccion.Add(transaccion);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro de manera existoso";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un problema";
                    return PartialView("Modal");
                }
            }
            else
            {
                result = BL.Transaccion.Update(transaccion);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Actialiazacion con exito";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un problema";
                    return PartialView("Modal");
                }
            }
            return View(transaccion);
        }
        public ActionResult Delete(int IdTransaccion)
        {
            ML.Result result = new ML.Result();
            result = BL.Transaccion.Delete(IdTransaccion);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Transaccion eliminada";
            }
            else
            {
                ViewBag.Mensaje = "Se ha producido un error" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult ObtenerPrecio(int idProducto)
        {
            // Lógica para obtener el precio del producto con el id proporcionado
            var precio = ObtenerPrecioDesdeBaseDeDatos(idProducto);

            return Json(precio);
        }
        private decimal ObtenerPrecioDesdeBaseDeDatos(int idProducto)
        {
            // Aquí deberías realizar la consulta a tu base de datos y devolver el precio
            // Este es solo un ejemplo, debes implementarlo según tus necesidades

            // Supongamos que tienes una entidad Producto con un DbSet<Producto> en tu DbContext
            using (DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
            {
                var producto = context.Productos.FirstOrDefault(p => p.IdProducto == idProducto);

                // Verificar si el producto existe y devolver su precio
                return producto != null ? producto.PrecioUnitario : 0.0m;
            }
        }
    }
}
