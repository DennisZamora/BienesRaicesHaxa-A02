using Bienes_Raices_HAXA.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    public class HomeController : Controller
    {
        private GestionPropiedadModel action = new GestionPropiedadModel();

        public ActionResult Index()
        {
            try
            {
                var categorias = action.CategoriasSeleccion();

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

                var propiedades = action.ListarPropiedades();
                Session["Propiedades"] = propiedades;

                // Verificar si la lista esta vacía
                if (propiedades.Count > 0)
                {
                    return View("Index", propiedades);
                }
                else
                {
                    return View("Index", new List<PropiedadV>());
                }
            }
            catch (Exception)
            {
                return View("~/Shared/Error.cshtml");
            }
        }

        [HttpPost]
        public ActionResult Filtrar(int idCategoria,string provincia,string canton,int? pisos,int? habitacion,int? baños,int? garage,int precioMin,int precioMax)
        {
            try
            {
                var categorias = action.CategoriasSeleccion();

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

                var propiedades = action.filtrarPropiedad(idCategoria, provincia, canton, pisos, habitacion, baños, garage, precioMin, precioMax);
                Session["Propiedades"] = propiedades;

                // Verificar si la lista esta vacía
                if (propiedades.Count > 0)
                {
                    return View("Index", propiedades);
                }
                else
                {
                    return View("Index", new List<PropiedadV>());
                }
            }
            catch (Exception ee)
            {
                return View("~/Shared/Error.cshtml");
            }
        }

        [HttpGet]
        public ActionResult Propiedad(long id)
        {
            try
            {
                var propiedad = action.getPropiedad(id, (List<PropiedadV>)Session["Propiedades"]);
                if (propiedad == null)
                {
                    return View("Error");
                }
                else
                {
                    Session["idPropiedad"] = propiedad.Property.idPropiedad;
                    return View("Propiedad", propiedad);
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Index", "Home");
        }
    }
}