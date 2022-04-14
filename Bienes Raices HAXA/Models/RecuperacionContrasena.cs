using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bienes_Raices_HAXA.Models
{
    public class RecuperacionContrasena
    {
        public Usuario recuperar(Models.ViewModel.RecuperarViewModel model, string token)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var oUser = (from x in contexto.Usuario
                             where x.email == model.email
                             select x).FirstOrDefault();

                if (oUser != null)
                {
                    oUser.token_recovery = token;
                    contexto.SaveChanges();
                }
                return oUser;
            }
            return new Usuario();
        }
        public Boolean recuperarContrasena(Models.ViewModel.RecuperarContrasenaViewModel model)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var oUser = (from x in contexto.Usuario
                             where x.token_recovery == model.token
                             select x).FirstOrDefault();

                if (oUser != null)
                {
                    oUser.password = model.contrasena;
                    oUser.token_recovery = null;
                    contexto.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}