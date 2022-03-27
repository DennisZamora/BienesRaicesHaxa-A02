using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;

namespace Bienes_Raices_HAXA.Models
{
    public class GestionEmpleadosModel
    {
        //metodo para obtener todos los empoleados
        public List<Usuario> ObtenerEmpleados()
        {
            using (var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Usuario
                             where x.idRol == 2
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

        //metodo para buscar el empoleado deseado
        public Usuario buscarEmpleado(string identificacion)
        {
            using (var contexto = new BRHaxaEntities())
            {
                if (string.IsNullOrEmpty(identificacion))
                {
                    return null;
                }

                var dueno = (from x in contexto.Usuario
                             where x.cedula_identificacion == identificacion
                             && x.idRol == 2
                             select x).FirstOrDefault();

                return dueno;
            }
        }

        //metodo para actualizar el empoleado
        public void actualizaEmpleado(Usuario empleado, string primerApellido, string segundoApellido, string password, string correo)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var dueno = (from x in contexto.Usuario
                             where x.idUsuario == empleado.idUsuario
                             select x).FirstOrDefault();

                dueno.cedula_identificacion = empleado.cedula_identificacion;
                dueno.nombre = empleado.nombre;
                dueno.apellido1 = primerApellido;
                dueno.apellido2 = segundoApellido;
                dueno.telefono = empleado.telefono;
                dueno.email = correo;
                dueno.password = password;
                dueno.idRol = empleado.idRol;

                contexto.SaveChanges();
            }
        }

        //metodo para eliminar el empoleado seleccionado
        public void eliminarEmpleado(Usuario empleadoSeleccionado)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var empleado = (from x in contexto.Usuario
                                where x.idUsuario == empleadoSeleccionado.idUsuario
                                select x).FirstOrDefault();

                contexto.Usuario.Remove(empleado);
                contexto.SaveChanges();
            }
        }

        public void registrarEmpleado(Usuario empleado, string primerApellido, string segundoApellido, string password, string correo)
        {
            using (BRHaxaEntities db = new BRHaxaEntities())
            {
                //!!!if(model.cpassword == model.password) revisar esta linea para comprobar que las contraseñas sean iguales
                if (empleado.password == empleado.password)
                {
                    try
                    {
                        var usuario = new Usuario();

                        usuario.cedula_identificacion = empleado.cedula_identificacion;
                        usuario.nombre = empleado.nombre;
                        usuario.apellido1 = primerApellido;
                        usuario.apellido2 = segundoApellido;
                        usuario.telefono = empleado.telefono;
                        usuario.email = correo;
                        usuario.password = password;
                        usuario.idRol = 2;

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

        public List<Usuario> ObtenerEmpleadosCedula(string cedula)
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

        public List<Usuario> ObtenerEmpleadosNombre(string nombre)
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