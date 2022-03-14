using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace Bienes_Raices_HAXA.Models
{
    public class GestionDuenosModel
    {
        //metodo para obtener todos los duenos
        public List<Usuario> ObtenerDuenos()
        {
            using (var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Usuario
                             where x.idRol == 4
                             select x).ToList();

                if (lista.Count > 0)
                {
                    return lista;
                }
                else
                {
                    return new List<Usuario>();
                }
            }
        }

        //metodo para buscar el dueno deseado
        public Usuario buscarDueno(string identificacion)
        {
            using (var contexto = new BRHaxaEntities())
            {
                if (string.IsNullOrEmpty(identificacion))
                {
                    return null;
                }

                var dueno = (from x in contexto.Usuario
                             where x.cedula_identificacion == identificacion
                             && x.idRol == 4
                             select x).FirstOrDefault();

                return dueno;
            }
        }

        //metodo para actualizar el dueno
        public void actualizaDueno(Usuario duenoSeleccionado)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var dueno = (from x in contexto.Usuario
                             where x.idUsuario == duenoSeleccionado.idUsuario
                             select x).FirstOrDefault();

                dueno.cedula_identificacion = duenoSeleccionado.cedula_identificacion;
                dueno.nombre = duenoSeleccionado.nombre;
                dueno.apellido1 = duenoSeleccionado.apellido1;
                dueno.apellido2 = duenoSeleccionado.apellido2;
                dueno.telefono = duenoSeleccionado.telefono;
                dueno.email = duenoSeleccionado.email;
                dueno.password = duenoSeleccionado.password;
                dueno.idRol = dueno.idRol;

                contexto.SaveChanges();
            }
        }

        //metodo para eliminar el dueno seleccionado
        public void eliminarDueno(Usuario duenoSeleccionado)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var dueno = (from x in contexto.Usuario
                             where x.idUsuario == duenoSeleccionado.idUsuario
                             select x).FirstOrDefault();

                contexto.Usuario.Remove(dueno);
                contexto.SaveChanges();
            }
        }

        public void registrarDueno(Usuario dueno)
        {
            using (BRHaxaEntities db = new BRHaxaEntities())
            {
                //!!!if(model.cpassword == model.password) revisar esta linea para comprobar que las contraseñas sean iguales
                if (dueno.password == dueno.password)
                {
                    try
                    {
                        var usuario = new Usuario();

                        usuario.cedula_identificacion = dueno.cedula_identificacion;
                        usuario.nombre = dueno.nombre;
                        usuario.apellido1 = dueno.apellido1;
                        usuario.apellido2 = dueno.apellido2;
                        usuario.telefono = dueno.telefono;
                        usuario.email = dueno.email;
                        usuario.password = dueno.password;
                        usuario.idRol = 4;

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

        public List<Usuario> ObtenerDuenosCedula(string cedula)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Usuario
                             where x.idRol == 4
                             && x.cedula_identificacion == cedula
                             select x).ToList();

                return lista;
            }
        }

        public List<Usuario> ObtenerDuenosNombre(string nombre)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Usuario
                             where x.idRol == 4
                             && x.nombre == nombre
                             select x).ToList();

                return lista;
            }
        }
    }
}