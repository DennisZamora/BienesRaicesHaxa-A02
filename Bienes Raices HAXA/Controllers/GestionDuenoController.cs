using Bienes_Raices_HAXA.Models;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    //[VerificacionSesion]
    public class GestionDuenoController : Controller
    {
        // GET: GestionDueños
        public ActionResult GestionDueno()
        {
            GestionDuenosModel model = new GestionDuenosModel();

            var listaDueños = model.ObtenerDuenos();

            return View(listaDueños);
        }

        public ActionResult AgregarPropiedad()
        {
            GestionUsuariosModel model = new GestionUsuariosModel();

            var listaDueños = model.ObenerDueños();

            return View(listaDueños);
        }

        public ActionResult EliminarDueno(string identificacion)
        {
            GestionDuenosModel model = new GestionDuenosModel();

            var dueño = model.buscarDueno(identificacion);

            if (dueño != null)
            {
                ViewData["dueño"] = dueño;
                return View();
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public ActionResult Eliminar(Usuario dueño)
        {
            GestionDuenosModel model = new GestionDuenosModel();
            model.eliminarDueno(dueño);

            return RedirectToAction("GestionDueno", "GestionDueno");
        }

        public ActionResult ActualizarDueno(string identificacion)
        {
            GestionDuenosModel model = new GestionDuenosModel();

            var dueño = model.buscarDueno(identificacion);

            if (dueño != null)
            {
                ViewData["dueño"] = dueño;
                return View();
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public ActionResult Actualizar(Usuario dueño)
        {
            GestionDuenosModel model = new GestionDuenosModel();
            model.actualizaDueno(dueño);

            return RedirectToAction("GestionDueno", "GestionDueno");
        }

        public ActionResult RegistrarDueno()
        {
            return View();
        }

        public ActionResult Registrar(Usuario dueno)
        {
            GestionDuenosModel model = new GestionDuenosModel();

            model.registrarDueno(dueno);

            return RedirectToAction("GestionDueno", "GestionDueno");
        }
    }
}