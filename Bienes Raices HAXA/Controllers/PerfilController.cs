using Bienes_Raices_HAXA.Filters;
using Bienes_Raices_HAXA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    [VerificacionSesion]
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Perfil()
        {
            string id = Session["id"].ToString();
            long idUsuario = Convert.ToInt64(id);
            PerfilModel modelo = new PerfilModel();
            var respuesta = modelo.obtenerUsuario(idUsuario);
            modelo.listaCitas(idUsuario);
            return View(respuesta);
        }
        [HttpPost]
        public ActionResult EditarPerfil(Usuario usuario,string primerApellido,string segundoApellido,string contrasena,string correo)
        {
            PerfilModel modelo = new PerfilModel();
            var respuesta = modelo.editarPerfil(usuario,primerApellido,segundoApellido,contrasena,correo);
            if (respuesta == true)
            {
                return View("Perfil");
            } else
            {
                return View("Perfil");
            }
        }

    }
}