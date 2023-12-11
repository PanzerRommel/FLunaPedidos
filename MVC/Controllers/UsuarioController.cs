using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class Usuario : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll();

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            if (IdUsuario == null)
            {
                return View(usuario);
            }
            else
            {
                ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                if (result.Correct)
                {
                    usuario = ((ML.Usuario)result.Object);
                    return View(usuario);
                }
                else
                {
                    ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                    return View("Modal");
                }
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            if (usuario.IdUsuario == 0)
            {
                result = BL.Usuario.Add(usuario);
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
                result = BL.Usuario.Update(usuario);
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
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            result = BL.Usuario.Delete(IdUsuario);
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
