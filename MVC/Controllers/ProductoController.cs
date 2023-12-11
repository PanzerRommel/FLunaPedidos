using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.GetAll();

            if (result.Correct)
            {
                producto.Productos = result.Objects;
            }
            return View(producto);
        }
        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            if (IdProducto == null)
            {
                return View(producto);
            }
            else
            {
                ML.Result result = BL.Producto.GetById(IdProducto.Value);
                if (result.Correct)
                {
                    producto = ((ML.Producto)result.Object);
                    return View(producto);
                }
                else
                {
                    ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                    return View("Modal");
                }
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            if (producto.IdProducto == 0)
            {
                result = BL.Producto.Add(producto);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro agregado exitosamente";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un problema";
                }
                return PartialView("Modal");
            }
            else
            {
                result = BL.Producto.Update(producto);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Actualizacion extisosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un problema al actualizar descripcion";
                }
                return PartialView("Modal");
            }

        }
        public ActionResult Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result = BL.Producto.Delete(IdProducto);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Registro Eliminado Con Exitoso";
            }
            else
            {
                ViewBag.Mensaje = "Se A Producido Un Error" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
    }
}
