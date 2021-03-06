using Bienes_Raices_HAXA.Models;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    public class LogInController : Controller
    {
        public ActionResult Login()
        {
            if (Session["email"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
<<<<<<< HEAD
        [HttpGet]
        public ActionResult IniciarRecuperar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarRecuperar(Models.ViewModelR.RecuperarViewModel model, string correo)
        {
            try
            {
                string token = GetSha256(Guid.NewGuid().ToString());
                RecuperacionContrasena modelo = new RecuperacionContrasena();
                var respuesta = modelo.recuperar(correo, token);

                if (respuesta != null)
                {
                    SendEmail(respuesta.email, token);
                    return View();
                }
                return View();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpGet]
        public ActionResult Recuperar(string token)
        {
            Models.ViewModelR.RecuperarContrasenaViewModel model = new Models.ViewModelR.RecuperarContrasenaViewModel();

            model.token = token;
            return View(model);
        }

        [HttpPost]
        public ActionResult Recuperar(string token, string contrasena)
        {
            try
            {
                RecuperacionContrasena modelo = new RecuperacionContrasena();
                modelo.recuperarContrasena(token, contrasena);
                return View("Login");
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region HELPERS
        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void SendEmail(string EmailDestino, string token)
        {
            string EmailOrigen = "bienesraices.haxa@gmail.com";
            string contrasena = "Br_haxa2022*";
            string url = urlDomain + "LogIn/Recuperar/?token=" + token;
            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperacion de contraseña",
                "<p>Correo para recuperación de contraseña</p><br>" +
                "<a href='" + url + "''>Click para recuperar</a>");
            oMailMessage.IsBodyHtml = true;

            SmtpClient OsmtpClient = new SmtpClient("smtp.gmail.com");
            OsmtpClient.EnableSsl = true;
            OsmtpClient.UseDefaultCredentials = false;
            OsmtpClient.Port = 587;
            OsmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, contrasena);
            OsmtpClient.Send(oMailMessage);
            OsmtpClient.Dispose();
        }


        #endregion


=======
>>>>>>> parent of d1ffbfe (Merge branch 'main' of https://github.com/DennisZamora/BienesRaicesHaxa-A02)

        [HttpPost]
        public ActionResult validacionLogin(string email, string password)
        {
            Security model = new Security();

            var validacion = model.validarLogin(email, password);

            if (validacion != null)
            {
                Session["email"] = validacion.email;
                Session["rol"] = validacion.idRol;
                Session["id"] = validacion.idUsuario;
                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/LogIn/Login.cshtml");
        }
    }
}