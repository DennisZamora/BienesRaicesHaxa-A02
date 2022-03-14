using Bienes_Raices_HAXA.Models;
using System;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public ActionResult Register()
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
        public ActionResult agregar(Usuario usuario)
        {
            try
            {
                Security register = new Security();
                register.Registrar(usuario);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}