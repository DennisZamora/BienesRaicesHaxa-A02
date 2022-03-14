using Bienes_Raices_HAXA.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    //[VerificacionSesion]
    public class CitasController : Controller
    {
        #region Index method

        /// <summary>
        /// GET: Citas/Index method.
        /// </summary>
        /// <returns>Returns - index view page</returns>
        public ActionResult Index()
        {
            // Info.
            return this.View();
        }

        #endregion Index method

        #region Get Calendar data method.

        /// <summary>
        /// GET: /Citas/GetCalendarData
        /// </summary>
        /// <returns>Return data</returns>
        public ActionResult GetCalendarData()
        {
            // Initialization.
            JsonResult result = new JsonResult();

            try
            {
                // Loading.
                List<Citas2> data = this.LoadData();

                // Processing.
                result = this.Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Return info.
            return result;
        }

        #endregion Get Calendar data method.

        #region Helpers

        #region Load Data

        /// <summary>
        /// Load data method.
        /// </summary>
        /// <returns>Returns - Data</returns>
        private List<Citas2> LoadData()
        {
            // Initialization.
            List<Citas2> lst = new List<Citas2>();
            CitasModel modelo = new CitasModel();
            try
            {
                var lista = modelo.GetAllHorario();

                // Read file.

                return lista;
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion Load Data

        #endregion Helpers

        [HttpGet]
        public ActionResult AgregarCita()
        {
            return View("AgregarCita", null);
        }

        [HttpPost]
        public ActionResult AgregarCita(Citas nuevaCita, string hora)
        {
            try
            {
                object idPropiedad = Session["idPropiedad"];
                object idUsuario = Session["id"];
                CitasModel modelo = new CitasModel();
                var resultado = modelo.agregarCita(nuevaCita, idUsuario, idPropiedad, hora);
                if (resultado == true)
                {
                    return View("Index");
                }
                else
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public ActionResult ConsultaFechaFinal(string fechaInicio, string hora)
        {
            try
            {
                int horaFinal = Convert.ToInt32(hora);
                horaFinal = horaFinal + 2;
                string fechaHoraFinal = Convert.ToString(horaFinal) + ":" + "00:00";
                var resultado = fechaInicio + " " + fechaHoraFinal;

                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Se presentó un error", JsonRequestBehavior.DenyGet);
            }
        }
    }
}