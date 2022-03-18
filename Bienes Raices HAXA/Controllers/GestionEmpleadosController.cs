using Bienes_Raices_HAXA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    public class GestionEmpleadosController : Controller
    {
        public ActionResult GestionEmpleados()
        {
            GestionEmpleadosModel model = new GestionEmpleadosModel();

            var listaEmpleados = model.ObtenerEmpleados();

            return View(listaEmpleados);
        }


        public ActionResult EliminarEmpleados(string identificacion)
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

        public ActionResult ActualizarEmpleados(string identificacion)
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

        public ActionResult Actualizar(Usuario empleado)
        {
            GestionEmpleadosModel model = new GestionEmpleadosModel();
            model.actualizaEmpleado(empleado);

            return RedirectToAction("GestionEmpleados", "GestionEmpleados");
        }

        public ActionResult RegistrarEmpleados()
        {
            return View();
        }

        public ActionResult Registrar(Usuario empleado)
        {
            GestionEmpleadosModel model = new GestionEmpleadosModel();

            model.registrarEmpleado(empleado);

            return RedirectToAction("GestionEmpleados", "GestionEmpleados");
        }
    }
}