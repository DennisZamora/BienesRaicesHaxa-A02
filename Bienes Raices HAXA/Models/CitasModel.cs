using System;
using System.Collections.Generic;
using System.Linq;

namespace Bienes_Raices_HAXA.Models
{
    public class CitasModel
    {
        public List<Citas2> GetAllHorario()
        {
            List<Citas2> citas2 = new List<Citas2>();
            using (var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Citas
                             select x).ToList();

                if (lista.Count > 0)
                {
                    foreach (var item in lista)
                    {
                        citas2.Add(new Citas2
                        {
                            idPropiedad = (int)item.idPropiedad,
                            idUsuario = (int)item.idUsuario,
                            titulo = item.titulo,
                            fechaInicio = item.fechaInicio,
                            fechaFinal = item.fechaFinal
                        });
                    }
                }

                return citas2;
            }
        }

        public Boolean agregarCita(Citas nuevaCita, object idUsuario, object idPropiedad, string hora)
        {
            using (var contexto = new BRHaxaEntities())
            {
                hora = hora + ":00:00";
                string fechaInicioFinal = nuevaCita.fechaInicio + " " + hora;

                var resultado = (from x in contexto.Citas
                                 where x.fechaInicio == fechaInicioFinal && x.fechaFinal == nuevaCita.fechaFinal
                                 select x).FirstOrDefault();

                var id = idUsuario.ToString();
                int idUser = Convert.ToInt32(id);

                var idProp = idPropiedad.ToString();
                int propidad = Convert.ToInt32(idProp);
                var usuario = (from x in contexto.Usuario
                               where x.idUsuario == idUser
                               select x).FirstOrDefault();

                if (resultado == null)
                {
                    Citas cita = new Citas();
                    cita.idPropiedad = propidad;
                    cita.idUsuario = idUser;
                    cita.fechaInicio = fechaInicioFinal;
                    cita.fechaFinal = nuevaCita.fechaFinal;
                    cita.titulo = "Presentar propiedad a " + usuario.nombre;
                    contexto.Citas.Add(cita);
                    contexto.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Boolean actualizarCita(Citas nuevaCita)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var resultado = (from x in contexto.Citas
                                 where x.idCita == nuevaCita.idCita
                                 select x).FirstOrDefault();
                var usuario = (from x in contexto.Usuario
                               where x.idUsuario == 1
                               select x).FirstOrDefault();

                if (resultado == null)
                {
                    resultado.idPropiedad = nuevaCita.idPropiedad;
                    resultado.idUsuario = nuevaCita.idUsuario;
                    resultado.fechaInicio = nuevaCita.fechaInicio;
                    resultado.fechaFinal = nuevaCita.fechaFinal;
                    resultado.titulo = "Presentar propiedad a " + usuario.nombre;
                    contexto.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Boolean eliminarCita(Citas nuevaCita)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var resultado = (from x in contexto.Citas
                                 where x.idCita == nuevaCita.idCita
                                 select x).FirstOrDefault();

                if (resultado == null)
                {
                    contexto.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}