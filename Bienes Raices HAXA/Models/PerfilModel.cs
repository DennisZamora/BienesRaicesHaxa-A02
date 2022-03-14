using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bienes_Raices_HAXA.Models
{
    public class PerfilModel
    {

        public Usuario obtenerUsuario(long idUsuario)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var usuario = (from x in contexto.Usuario
                               where x.idUsuario == idUsuario
                               select x).FirstOrDefault();

                return usuario;
            }
        }

        public List<PerfilPartial> listaCitas(long idUsuario)
        {
            List<PerfilPartial> perfilPartial = new List<PerfilPartial>();
            using (var contexto = new BRHaxaEntities())
            {
                var cita = (from x in contexto.Citas
                            join p in contexto.Propiedad on x.idPropiedad equals p.idPropiedad
                            where x.idUsuario == idUsuario
                            select new
                            {
                                x.idCita,
                                p.provincia,
                                p.canton,
                                p.direccion,
                                x.fechaInicio,
                                x.fechaFinal
                            });

                int cantidad = cita.AsQueryable().Count();
                if (cantidad > 0)
                {
                    foreach (var item in cita)
                    {
                        perfilPartial.Add(new PerfilPartial
                        {
                            idCita = (int)item.idCita,
                            provincia = item.provincia,
                            canton = item.canton,
                            direccion = item.direccion,
                            fechaFinal = item.fechaFinal,
                            fechaInicio = item.fechaInicio
                        });
                    }
                }
                return perfilPartial;

            }
        }

        public Boolean editarPerfil(Usuario perfil, string primerApellido, string segundoApellido, string contrasena,string correo)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var usuario = (from x in contexto.Usuario
                               where x.idUsuario == perfil.idUsuario
                               select x).FirstOrDefault();

                if (usuario != null)
                {
                    usuario.cedula_identificacion = perfil.cedula_identificacion;
                    usuario.nombre = perfil.nombre;
                    usuario.apellido1 = primerApellido;
                    usuario.apellido2 = segundoApellido;
                    usuario.telefono = perfil.telefono;
                    usuario.email = correo;
                    usuario.password = contrasena;
                    contexto.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}