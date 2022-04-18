using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace Bienes_Raices_HAXA.Models
{
    public class Security
    {
        public void Registrar(Usuario model)
        {
            using (BRHaxaEntities db = new BRHaxaEntities())
            {
                //!!!if(model.cpassword == model.password) revisar esta linea para comprobar que las contraseñas sean iguales
                if (model.password == model.password)
                {
                    try
                    {
                        var usuario = new Usuario();

                        usuario.cedula_identificacion = model.cedula_identificacion;
                        usuario.nombre = model.nombre;
                        usuario.apellido1 = model.apellido1;
                        usuario.apellido2 = model.apellido2;
                        usuario.telefono = model.telefono;
                        usuario.email = model.email;
                        usuario.password = model.password;
                        usuario.idRol = 1;

                        db.Usuario.Add(usuario);
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        StringBuilder sb = new StringBuilder();

                        foreach (var failure in ex.EntityValidationErrors)
                        {
                            sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                            foreach (var error in failure.ValidationErrors)
                            {
                                sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                                sb.AppendLine();
                            }
                        }

                        throw new DbEntityValidationException(
                            "Entity Validation Failed - errors follow:\n" +
                            sb.ToString(), ex
                        ); // Add the original exception as the innerException
                    }
                }
            }
        }

        public Usuario validarLogin(string email, string password)
        {
            using (BRHaxaEntities contexto = new BRHaxaEntities())
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return null;
                }

                var userFound = (from x in contexto.Usuario
                                 where x.email == email &&
                                 x.password == password
                                 select x).FirstOrDefault();

                return userFound;
            }
        }
    }
}