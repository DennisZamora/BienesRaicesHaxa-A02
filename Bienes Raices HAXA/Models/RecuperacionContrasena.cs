using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bienes_Raices_HAXA.Models
{
    public class RecuperacionContrasena
    {
        public Usuario recuperar(string correo, string token)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var oUser = (from x in contexto.Usuario
                             where x.email == correo
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
        public Boolean recuperarContrasena(string token, string contrasena)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var oUser = (from x in contexto.Usuario
                             where x.token_recovery == token
                             select x).FirstOrDefault();

                if (oUser != null)
                {
                    oUser.password = contrasena;
                    oUser.token_recovery = null;
                    contexto.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}