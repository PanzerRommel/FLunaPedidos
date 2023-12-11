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
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un problema";
                }
            }
            else
            {
                result = BL.Transaccion.Update(transaccion);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Actialiazacion con exito";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un problema";
                }
                return PartialView("Modal");
            }
            return View(transaccion);
        }
        public ActionResult Delete(int IdTransaccion)
        {
            ML.Result result = new ML.Result();
            result = BL.Transaccion.Delete(IdTransaccion);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Usuario Eliminado con Éxito";
            }
            else
            {
                ViewBag.Mensaje = "Se ha producido un error" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }

        //[HttpGet]
        //public ActionResult ObtenerPrecioProducto(int idProducto)
        //{
        //    using (DL.FlunaExpendedoraContext context = new DL.FlunaExpendedoraContext())
        //    {
        //        var precio = context.Productos
        //            .Where(p => p.IdProducto == idProducto)
        //            .Select(p => p.PrecioUnitario)
        //            .FirstOrDefault();

        //        return Json(new { precio });
        //    }
        //}

    }
}
