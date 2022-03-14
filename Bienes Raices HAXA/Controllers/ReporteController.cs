using Bienes_Raices_HAXA.Filters;
using Bienes_Raices_HAXA.Models;
using REPORTES.Models;
using Rotativa;
using System.Collections.Generic;
using System.Web.Mvc;

namespace REPORTES.Controllers
{
    public class ReporteController : Controller
    {
        
        public ActionResult Reporte()
        {
            ReporteModel rm = new ReporteModel();
            //var categoria = rm.categoria();
            List<Categoria> listaCategoria = rm.listaCategoria();

            List<SelectListItem> droplist = listaCategoria.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.nombre.ToString(),
                    Value = x.idCategoria.ToString(),
                };
            });
            ViewBag.dropList = droplist;
            //List<SelectListItem> dropList = new List<SelectListItem>();
            //foreach (var item in categoria)
            //{
            //    dropList.Add(new SelectListItem
            //    {
            //        Text = item.Categoria.nombre,
            //        Value = item.Categoria.idCategoria.ToString(),
            //    });
            //}
            //ViewBag.dropList = dropList;
            var respuesta = rm.obtenerPropiedad();
            return View(respuesta);
        }

        [HttpPost]
        public ActionResult BusquedaCategoria(Propiedad propiedad)
        {
            ReporteModel modelo = new ReporteModel();
            List<Categoria> listaCategoria = modelo.listaCategoria();

            List<SelectListItem> droplist = listaCategoria.ConvertAll(x =>
                {
                    return new SelectListItem()
                    {
                        Text = x.nombre.ToString(),
                        Value = x.idCategoria.ToString(),
                    };
                });
            var respuesta = modelo.obtenerPropiedadFiltrada(propiedad);

            /*List<SelectListItem> dropList = new List<SelectListItem>();
              foreach (var item in categoria)
              {
                  dropList.Add(new SelectListItem
                  {
                      Text = item.nombre,
                      Value = item.idCategoria.ToString(),
                  });
              }*/
            ViewBag.dropList = droplist;
            /*var respuestaFiltrada = categoria.Where(x => x.idCategoria == propiedad.idCategoria).ToList();*/
            return View("Reporte", respuesta); //"Reporte", respuestaFiltrada
        }

        public ActionResult descargaPDF()
        {
            return new ActionAsPdf("Reporte", new { nombre = "CARO" }) { FileName = "ReportePropiedad.pdf" };
        }
    }
}