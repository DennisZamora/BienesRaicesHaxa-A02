using Bienes_Raices_HAXA.Controllers;
using Bienes_Raices_HAXA.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Filters
{
    public class VerificacionSesion : ActionFilterAttribute
    {
        private Usuario usuario = new Usuario();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (HttpContext.Current.Session["email"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("~/LogIn/Login");
                }
                else
                {
                    usuario.email = HttpContext.Current.Session["email"].ToString();

                    if (usuario == null)
                    {
                        if (filterContext.Controller is LogInController == false)
                        {
                            filterContext.HttpContext.Response.Redirect("~/LogIn/Login");
                        }
                    }
                    else
                    {
                        if (filterContext.Controller is LogInController == true)
                        {
                            filterContext.HttpContext.Response.Redirect("~/Home/Index");
                        }
                    }
                    base.OnActionExecuting(filterContext);
                }
            }
            catch (Exception e)
            {
                filterContext.Result = new RedirectResult("~/Views/Shared/Error.cshtml");
            }
        }
    }
}