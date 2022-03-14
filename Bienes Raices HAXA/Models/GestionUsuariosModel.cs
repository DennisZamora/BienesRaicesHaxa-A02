using System.Collections.Generic;
using System.Linq;

namespace Bienes_Raices_HAXA.Models
{
    public class GestionUsuariosModel
    {
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

        public Usuario buscarEmpleado(string identificacion)
        {
            using (var contexto = new BRHaxaEntities())
            {
                if (string.IsNullOrEmpty(identificacion))
                {
                    return null;
                }

                var empleado = (from x in contexto.Usuario
                                where x.cedula_identificacion == identificacion
                                && x.idRol == 2
                                select x).FirstOrDefault();

                return empleado;
            }
        }

        public void actualizarEmpleado(Usuario empleado)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var usuario = (from x in contexto.Usuario
                               where x.idUsuario == empleado.idUsuario
                               select x).FirstOrDefault();

                usuario.idUsuario = empleado.idUsuario;
                usuario.nombre = empleado.nombre;
                usuario.apellido1 = empleado.apellido1;
                usuario.apellido2 = empleado.apellido2;
                usuario.email = empleado.email;
                usuario.password = empleado.password;
                usuario.idRol = empleado.idRol;

                contexto.SaveChanges();
            }
        }

        public void eliminarEmpleado(Usuario empleado)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var usuario = (from x in contexto.Usuario
                               where x.cedula_identificacion == empleado.cedula_identificacion
                               select x).FirstOrDefault();

                contexto.Usuario.Remove(usuario);
                contexto.SaveChanges();
            }
        }

        public List<Usuario> ObenerDueños()
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

        public Usuario buscarDueño(string identificacion)
        {
            using (var contexto = new BRHaxaEntities())
            {
                if (string.IsNullOrEmpty(identificacion))
                {
                    return null;
                }

                var dueño = (from x in contexto.Usuario
                             where x.cedula_identificacion == identificacion
                             && x.idRol == 4
                             select x).FirstOrDefault();

                return dueño;
            }
        }

        public void actualizarDueño(Usuario dueñoSeleccionado)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var dueño = (from x in contexto.Usuario
                             where x.idUsuario == dueñoSeleccionado.idUsuario
                             select x).FirstOrDefault();

                dueño.idUsuario = dueñoSeleccionado.idUsuario;
                dueño.nombre = dueñoSeleccionado.nombre;
                dueño.apellido1 = dueñoSeleccionado.apellido1;
                dueño.password = dueñoSeleccionado.password;
                dueño.email = dueñoSeleccionado.email;
                dueño.idRol = 3;

                contexto.SaveChanges();
            }
        }

        public void eliminarDueño(Usuario dueñpSeleccionado)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var dueño = (from x in contexto.Usuario
                             where x.idUsuario == dueñpSeleccionado.idUsuario
                             select x).FirstOrDefault();

                contexto.Usuario.Remove(dueño);
                contexto.SaveChanges();
            }
        }
    }
}