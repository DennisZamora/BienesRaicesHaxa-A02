using Bienes_Raices_HAXA.Models;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    public class GestionEmpleadosController : Controller
    {
        [HttpPost]
        public ActionResult Actualizar(Usuario empleado, string primerApellido, string segundoApellido, string password, string correo)
        {
            GestionEmpleadosModel model = new GestionEmpleadosModel();
            model.actualizaEmpleado(empleado, primerApellido, segundoApellido, password, correo);

            return RedirectToAction("GestionEmpleados", "GestionEmpleados");
        }

        public ActionResult GestionEmpleados()
        {
            GestionEmpleadosModel model = new GestionEmpleadosModel();

            var listaEmpleados = model.ObtenerEmpleados();

            return View(listaEmpleados);
        }


        public ActionResult EliminarEmpleado(string identificacion)
        {
            GestionEmpleadosModel model = new GestionEmpleadosModel();

            var empleado = model.buscarEmpleado(identificacion);

            if (empleado != null)
            {
                ViewData["empleado"] = empleado;
                return View();
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public ActionResult Eliminar(Usuario empleado)
        {
            GestionEmpleadosModel model = new GestionEmpleadosModel();
            model.eliminarEmpleado(empleado);

            return RedirectToAction("GestionEmpleados", "GestionEmpleados");
        }

        public ActionResult ActualizarEmpleado(string identificacion)
        {
            GestionEmpleadosModel model = new GestionEmpleadosModel();

            var empleado = model.buscarEmpleado(identificacion);

            if (empleado != null)
            {
                ViewData["empleado"] = empleado;
                return View();
            }
            else
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }



        public ActionResult RegistrarEmpleados()
        {
            return View();
        }

        public ActionResult Registrar(Usuario empleado, string primerApellido, string segundoApellido, string password, string correo)
        {
            GestionEmpleadosModel model = new GestionEmpleadosModel();

            model.registrarEmpleado(empleado, primerApellido, segundoApellido, password, correo);

            return RedirectToAction("GestionEmpleados", "GestionEmpleados");
        }
    }
}