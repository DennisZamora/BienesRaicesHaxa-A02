using Bienes_Raices_HAXA.Filters;
using Bienes_Raices_HAXA.Models;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    [VerificacionSesion]
    public class GestionEmpleadosController : Controller
    {
        // GET: GestionEmpleados
        public ActionResult GestionEmpleados()
        {
            GestionUsuariosModel model = new GestionUsuariosModel();

            var listaEmpleados = model.ObtenerEmpleados();

            return View(listaEmpleados);
        }

        //[HttpPost]
        public ActionResult ActualizarEmpleado(string identificacion)
        {
            GestionUsuariosModel model = new GestionUsuariosModel();

            var empleado = model.buscarEmpleado(identificacion);

            if (empleado != null)
            {
                ViewData["Empleado"] = empleado;
                return View();
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public ActionResult Actualizar(Usuario empleado)
        {
            GestionUsuariosModel model = new GestionUsuariosModel();
            model.actualizarEmpleado(empleado);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EliminarEmpleado(string identificacion)
        {
            GestionUsuariosModel model = new GestionUsuariosModel();

            var empleado = model.buscarEmpleado(identificacion);

            if (empleado != null)
            {
                ViewData["Empleado"] = empleado;
                return View();
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public ActionResult Eliminar(Usuario empleado)
        {
            GestionUsuariosModel model = new GestionUsuariosModel();
            model.eliminarEmpleado(empleado);

            return RedirectToAction("Index", "Home");
        }
    }
}