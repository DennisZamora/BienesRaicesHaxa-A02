using Bienes_Raices_HAXA.Models;
using System.Collections.Generic;
using System.Linq;

namespace REPORTES.Models
{
    public class ReporteModel
    {
        public List<Propiedad> obtenerPropiedad()
        {
            using (var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Propiedad
                             orderby x.idPropiedad descending
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

        public List<Propiedad> obtenerPropiedadFiltrada(Propiedad propiedad)
        {
            using (var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Propiedad
                             where x.idCategoria == propiedad.idCategoria
                             orderby x.idPropiedad descending
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