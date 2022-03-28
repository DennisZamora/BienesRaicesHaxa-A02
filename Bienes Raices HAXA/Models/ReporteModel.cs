using Bienes_Raices_HAXA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bienes_Raices_HAXA.Models
{
    public class ReporteModel
    {
        public List<Propiedad> obtenerPropiedad()
        {
            using(var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Propiedad
                             orderby x.idPropiedad ascending
                             select x).ToList();

                if(lista.Count > 0)
                {
                    return lista;
                }
                else
                {
                    return new List<Propiedad>();
                }
            }
        }

        public List<Citas> obtenerCitas()
        {
            using (var contexto = new BRHaxaEntities())
            {
                var listaCitas = (from x in contexto.Citas
                                  select x).ToList();
                if (listaCitas.Count > 0)
                {
                    return listaCitas;
                }
                else
                {
                    return new List<Citas>();
                }
            }
        }

        //public List<Citas> obtenerCitas()
        //{
        //    using(var contexto = new BRHaxaEntities())
        //    {
        //        var listaCitas = (from x in contexto.Citas
        //                          join p in contexto.Propiedad on x.idPropiedad equals p.idPropiedad
        //                          join t in contexto.Usuario on x.idUsuario equals t.idUsuario
        //                          select new { 
        //                            x.idCita,
        //                            t.nombree,
        //                            p.nombre,
        //                            x.titulo,
        //                            x.fechaInicio,
        //                            x.fechaFinal
        //                          }).ToList();
        //        if (listaCitas.Count > 0)
        //        {
        //            return listaCitas;
        //        }
        //        else
        //        {
        //            return new List<Citas>();
        //        }
        //    }
        //}

        public List<Propiedad> obtenerPropiedadFiltrada(Propiedad propiedad)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Propiedad
                             where x.idCategoria == propiedad.idCategoria
                             orderby x.idPropiedad ascending
                             select x).ToList();

                if (lista.Count > 0)
                {
                    return lista;
                }
                else
                {
                    return new List<Propiedad>();
                }
            }
        }

        public List<Categoria> listaCategoria()
        {
            using (var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Categoria
                             select x).ToList();
                return lista;
            }
        }
      //public List<Categoria> categoria()
      //  {
      //      using (var contexto = new BRHaxaEntities())
      //      {
      //          var lista = (from x in contexto.Categoria

      //                       select x).ToList();
      //          var lisc = (from x in contexto.Categoria
      //                      select x).ToList();

      //          foreach(var item in lista)
      //          {
      //              foreach (var item2 in lisc)
      //              {
      //                 // item.Categoria.nombre = item2.nombre;
      //                  //item.Categoria.idCategoria = item2.idCategoria;
      //              }
                    
      //          }

      //          if(lista.Count > 0)
      //          {
      //              return lista;
      //          }
      //          else
      //          {
      //              return new List<Propiedad>();
      //          }
      //      }
      //  }
    }
}