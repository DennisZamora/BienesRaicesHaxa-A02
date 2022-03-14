using Bienes_Raices_HAXA.Models;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    public class LogInController : Controller
    {
        public ActionResult Login()
        {
            if (Session["email"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult validacionLogin(string email, string password)
        {
            Security model = new Security();

            var validacion = model.validarLogin(email, password);

            if (validacion != null)
            {
                Session["email"] = validacion.email;
                Session["rol"] = validacion.idRol;
                Session["id"] = validacion.idUsuario;
                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Shared/Error.cshtml");
        }
    }
}