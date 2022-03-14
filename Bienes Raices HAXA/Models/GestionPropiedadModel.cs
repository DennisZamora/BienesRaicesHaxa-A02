using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace Bienes_Raices_HAXA.Models
{
    public class GestionPropiedadModel
    {
        public List<PropiedadV> properties = new List<PropiedadV>();

        public List<PropiedadV> ListarPropiedades()
        {
            properties = new List<PropiedadV>();
            using (var contexto = new BRHaxaEntities())
            {
                var propiedad = contexto;
                foreach (var item in propiedad.Propiedad)
                {
                    var imagenes = (from x in propiedad.Imagenes
                                    where x.idPropiedad == item.idPropiedad
                                    select x).ToList();
                    properties.Add(new PropiedadV
                    {
                        Property = item,
                        Img = imagenes
                    });
                }

                return properties;
            }
        }

        public PropiedadV getPropiedad(long id, List<PropiedadV> prop)
        {
            if (prop.Count == 0)
            {
                prop = ListarPropiedades();
                return prop.FirstOrDefault(x => x.Property.idPropiedad == id);
            }
            else
            {
                return prop.FirstOrDefault(x => x.Property.idPropiedad == id);
            }
        }

        public void agregarImagen(string imagen, long idPropiedad)
        {
            using (BRHaxaEntities bd = new BRHaxaEntities())
            {
                Imagenes img = new Imagenes();
                img.idPropiedad = idPropiedad;
                img.link = imagen;
                bd.Imagenes.Add(img);
                bd.SaveChanges();
            }
        }

        public void agregarPropiedad(Propiedad model)
        {
            //REGISTRA PROPIEDAD
            try
            {
                using (BRHaxaEntities bd = new BRHaxaEntities())
                {
                    Propiedad propiedad = new Propiedad();

                    propiedad.idUsuario = model.idUsuario;
                    propiedad.idVendedor = model.idVendedor;
                    propiedad.nombre = model.nombre;
                    propiedad.precio = model.precio;
                    propiedad.idCategoria = model.idCategoria;
                    propiedad.descripcion = model.descripcion;
                    propiedad.canton = model.canton;
                    propiedad.idEstado = model.idEstado;
                    propiedad.direccion = model.direccion;
                    propiedad.distrito = model.distrito;
                    propiedad.m2 = model.m2;
                    propiedad.habitacion = model.habitacion;
                    propiedad.baños = model.baños;
                    propiedad.pisos = model.pisos;
                    propiedad.provincia = model.provincia;
                    propiedad.garage = model.garage;

                    bd.Propiedad.Add(propiedad);
                    bd.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();

                //foreach (var failure in ex.EntityValidationErrors)
                //{
                //    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                //    foreach (var error in failure.ValidationErrors)
                //    {
                //        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                //        sb.AppendLine();
                //    }
                //}

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }

        public Boolean ActualizarPropiedad(Propiedad model)
        {
            //Actualiza PROPIEDAD
            try
            {
                using (BRHaxaEntities bd = new BRHaxaEntities())
                {
                    var propiedad = (from x in bd.Propiedad
                                     where x.idPropiedad == model.idPropiedad
                                     select x).FirstOrDefault();
                    if (propiedad != null)
                    {
                        propiedad.idUsuario = model.idUsuario;
                        propiedad.idVendedor = model.idVendedor;
                        propiedad.nombre = model.nombre;
                        propiedad.precio = model.precio;
                        propiedad.idCategoria = model.idCategoria;
                        propiedad.descripcion = model.descripcion;
                        propiedad.canton = model.canton;
                        propiedad.idEstado = model.idEstado;
                        propiedad.direccion = model.direccion;
                        propiedad.distrito = model.distrito;
                        propiedad.m2 = model.m2;
                        propiedad.habitacion = model.habitacion;
                        propiedad.baños = model.baños;
                        propiedad.pisos = model.pisos;
                        propiedad.provincia = model.provincia;
                        propiedad.garage = model.garage;
                        bd.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //CONSULTAMOS EL LISTADO DE DUEÑOS
        public List<Usuario> ConsultarUsuario()
        {
            try
            {
                using (BRHaxaEntities contexto = new BRHaxaEntities())
                {
                    var lista = (from x in contexto.Usuario
                                 where x.idRol == 3
                                 select x).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Usuario> consultarDueño()
        {
            try
            {
                using (BRHaxaEntities cd = new BRHaxaEntities())
                {
                    var consulta = (from x in cd.Usuario
                                    select x).ToList();
                    return consulta;
                }
            }
            catch
            {
                throw;
            }
        }

        public Usuario consultaTU(long idUsuario)
        {
            using (BRHaxaEntities d = new BRHaxaEntities())
            {
                var consulta = (from x in d.Usuario
                                where x.idUsuario == idUsuario
                                select x).FirstOrDefault();
                return consulta;
            }
        }

        public decimal seleccionDueño(Usuario usuario)
        {
            using (BRHaxaEntities sd = new BRHaxaEntities())
            {
                var consulta = (from x in sd.Usuario
                                where x.idUsuario == usuario.idUsuario
                                select x.idUsuario).FirstOrDefault();
                return consulta;
            }
        }

        public List<Categoria> CategoriasSeleccion()
        {
            using (var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Categoria
                             select x).ToList();
                if (lista.Count > 0)
                {
                    return lista;
                }
                else
                {
                    return new List<Categoria>();
                }
            }
        }

        public List<Estado> EstadoSeleccion()
        {
            using (var contexto = new BRHaxaEntities())
            {
                var lista = (from x in contexto.Estado
                             select x).ToList();
                if (lista.Count > 0)
                {
                    return lista;
                }
                else
                {
                    return new List<Estado>();
                }
            }
        }
    }
}