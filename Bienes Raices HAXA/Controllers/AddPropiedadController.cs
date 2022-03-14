using Bienes_Raices_HAXA.Filters;
using Bienes_Raices_HAXA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    [VerificacionSesion]
    public class AddPropiedadController : Controller
    {
        [HttpGet]
        public ActionResult AgregarPropiedad(long idUsuario)
        {
            GestionPropiedadModel pm = new GestionPropiedadModel();

            ViewBag.usuario = idUsuario;

            var categorias = pm.CategoriasSeleccion();

            List<SelectListItem> comboCat = new List<SelectListItem>();
            foreach (var item in categorias)
            {
                comboCat.Add(new SelectListItem
                {
                    Value = item.idCategoria.ToString(),
                    Text = item.nombre.ToString(),
                });
            }
            ViewBag.cat = comboCat;

            var estados = pm.EstadoSeleccion();

            List<SelectListItem> comboEstados = new List<SelectListItem>();
            foreach (var item in estados)
            {
                comboEstados.Add(new SelectListItem
                {
                    Value = item.idEstado.ToString(),
                    Text = item.nombre.ToString(),
                });
            }
            ViewBag.est = comboEstados;

            return View();
        }

        public ActionResult Propiedades()
        {
            try
            {
                GestionPropiedadModel pm = new GestionPropiedadModel();
                var propiedades = pm.ListarPropiedades();
                Session["Propiedades"] = propiedades;

                // Verificar si la lista esta vacía
                if (propiedades.Count > 0)
                {
                    return View("Propiedades", propiedades);
                }
                else
                {
                    return View("Propiedades", new List<PropiedadV>());
                }
            }
            catch (Exception)
            {
                return View("~/Shared/Error.cshtml");
            }
        }

        [HttpGet]
        public ActionResult EditarPropiedad(long id)
        {
            GestionPropiedadModel pm = new GestionPropiedadModel();

            try
            {
                var propiedad = pm.getPropiedad(id, new List<PropiedadV>()).Property;
                var categorias = pm.CategoriasSeleccion();

                List<SelectListItem> comboCat = new List<SelectListItem>();
                foreach (var item in categorias)
                {
                    comboCat.Add(new SelectListItem
                    {
                        Value = item.idCategoria.ToString(),
                        Text = item.nombre.ToString(),
                    });
                }
                ViewBag.cat = comboCat;

                var estados = pm.EstadoSeleccion();

                List<SelectListItem> comboEstados = new List<SelectListItem>();
                foreach (var item in estados)
                {
                    comboEstados.Add(new SelectListItem
                    {
                        Value = item.idEstado.ToString(),
                        Text = item.nombre.ToString(),
                    });
                }
                ViewBag.est = comboEstados;

                if (propiedad == null)
                {
                    return View("Error");
                }
                else
                {
                    return View("EditarPropiedad", propiedad);
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ActualizarPropiedad(Propiedad propiedad)
        {
            GestionPropiedadModel model = new GestionPropiedadModel();
            try
            {
                var respuesta = model.ActualizarPropiedad(propiedad);
                if (respuesta == true)
                {
                    return View("SubirImagenes", propiedad.idPropiedad);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult SubirImg(HttpPostedFileBase UpImage, long idPropiedad)
        {
            GestionPropiedadModel img = new GestionPropiedadModel();
            try
            {
                
                    //var imagen = Path.GetFileName(UpImage.FileName);
                    //string logoPath = Server.MapPath("~/Content/Uploads/");
                    //string archivo = idPropiedad + (DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + imagen).ToLower();
                    //img.agregarImagen(logoPath + archivo, idPropiedad);
                    //UpImage.SaveAs(logoPath + archivo);
                
                return RedirectToAction("Propiedades");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult SubirImagenes(long idPropiedad)
        {
            try
            {
                ViewBag.items = idPropiedad;
                return View();
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AddPropiedades(Propiedad propiedad)
        {
            try
            {
                GestionPropiedadModel registrar = new GestionPropiedadModel();
                registrar.agregarPropiedad(propiedad);
                return RedirectToAction("Propiedades");
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }

        public ActionResult seleccionUsDueño(long idUsuario)
        {
            GestionPropiedadModel sum = new GestionPropiedadModel();
            var resultado = sum.consultaTU(idUsuario);

            if (resultado != null)
            {
                var respuesta = sum.seleccionDueño(resultado);
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.DenyGet);
            }
        }
    }
}