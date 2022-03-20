using Bienes_Raices_HAXA.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    public class HomeController : Controller
    {

        [HttpPost]
        public ActionResult FiltrarPropiedad(string Property_idCategoria,string Property_provincia, string Property_canton,
               int? Property_pisos,int? Property_habitacion, int? Property_garage,int min_price, int? Property_ba_os,int max_price)
        {
            GestionPropiedadModel action = new GestionPropiedadModel();
            int idCategoria = Convert.ToInt32(Property_idCategoria);
            int? pisos = (Property_pisos);
            int? habitacion = (Property_habitacion);
            int? baños = (Property_ba_os);
            int? garage = (Property_garage); 
            int precioMin = (min_price);
            int precioMax = (max_price);

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
                    var propiedades = new List<PropiedadV>();
                    propiedades = action.filtrarPropiedad(idCategoria, Property_provincia, Property_canton, pisos, habitacion, baños, garage, precioMin, precioMax);

                    // Verificar si la lista esta vacía
                    if (propiedades.Count > 0)
                    {
                        return PartialView("Propiedades",propiedades);
                    }
                    else
                    {
                        return PartialView("Propiedades", new List<PropiedadV>());
                    }
            }
            catch (Exception ee)
            {
                return View("~/Shared/Error.cshtml");
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            GestionPropiedadModel action = new GestionPropiedadModel();
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

       

        [HttpGet]
        public ActionResult Propiedad(long id)
        {
            GestionPropiedadModel action = new GestionPropiedadModel();
            try
            {
                var propiedad = action.getPropiedad(id);
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