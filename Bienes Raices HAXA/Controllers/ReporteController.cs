using Bienes_Raices_HAXA.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Bienes_Raices_HAXA.Controllers
{
    public class ReporteController : Controller
    {
        public ActionResult Reporte()
        {
            ReporteModel rm = new ReporteModel();
            //var categoria = rm.categoria();
            List<Categoria> listaCategoria = rm.listaCategoria();

            List<SelectListItem> droplist = listaCategoria.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.nombre.ToString(),
                    Value = x.idCategoria.ToString(),
                };
            });
            ViewBag.dropList = droplist;
            //List<SelectListItem> dropList = new List<SelectListItem>();
            //foreach (var item in categoria)
            //{
            //    dropList.Add(new SelectListItem
            //    {
            //        Text = item.Categoria.nombre,
            //        Value = item.Categoria.idCategoria.ToString(),
            //    });
            //}
            //ViewBag.dropList = dropList;
            var respuesta = rm.obtenerPropiedad();
            return View(respuesta);
        }

        [HttpPost]
        public ActionResult BusquedaCategoria(Propiedad propiedad)
        {
            ReporteModel modelo = new ReporteModel();
            List<Categoria> listaCategoria = modelo.listaCategoria();

            List<SelectListItem> droplist = listaCategoria.ConvertAll(x =>
                {
                    return new SelectListItem()
                    {
                        Text = x.nombre.ToString(),
                        Value = x.idCategoria.ToString(),
                    };
                });
            var respuesta = modelo.obtenerPropiedadFiltrada(propiedad);


            /*List<SelectListItem> dropList = new List<SelectListItem>();
              foreach (var item in categoria)
              {
                  dropList.Add(new SelectListItem
                  {
                      Text = item.nombre,
                      Value = item.idCategoria.ToString(),
                  });
              }*/
            ViewBag.dropList = droplist;
            /*var respuestaFiltrada = categoria.Where(x => x.idCategoria == propiedad.idCategoria).ToList();*/
            return View("Reporte", respuesta); //"Reporte", respuestaFiltrada
        }

        public ActionResult ReporteCitas()
        {
            ReporteModel rm = new ReporteModel();
            var respuesta = rm.obtenerCitas();
            return View(respuesta);
        }

        public ActionResult ReporteEmpleados()
        {
            ReporteModel rm = new ReporteModel();
            var respuesta = rm.obtenerEmpleados();
            return View(respuesta);
        }

        public ActionResult ReporteUsuarios()
        {
            ReporteModel rm = new ReporteModel();
            var respuesta = rm.obtenerUsuarios();
            return View(respuesta);
        }

        public ActionResult generarPDFCitas(object sender, EventArgs e)
        {
            try
            {
                ReporteModel modelo = new ReporteModel();
                List<Citas> listaCitas = modelo.obtenerCitas();

                FileStream fs = new FileStream(Server.MapPath("~/Content/") + "ReporteCitas.pdf", FileMode.Create);
                Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
                PdfWriter pw = PdfWriter.GetInstance(doc, fs);

                doc.Open();
                //TITULO Y AUTOR 
                doc.AddAuthor("Reporte");
                doc.AddCreator("Bienes raíces Haxa");

                //DEFINIR FUENTE DEL DOCUMENTO
                iTextSharp.text.Font standarFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font standarFonts = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);
                iTextSharp.text.Font standarFontss = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);

                //ENCABEZADO DEL DOCUMENTO              
                doc.Add(new Paragraph("Reporte citas"));
                doc.Add(Chunk.NEWLINE);
                //Chunk linea = new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(5f, 100f, BaseColor.DARK_GRAY, Element.ALIGN_CENTER, 50F));
                //doc.Add(linea);

                //ENCABEZADO COLUMNAS
                PdfPTable tbl = new PdfPTable(6);
                tbl.WidthPercentage = 100;

                //ORDEN DE NOMBRE DE LAS COLUMNAS
                PdfPCell clId = new PdfPCell(new Phrase("ID", standarFont));
                clId.BorderWidth = 0;
                clId.BorderWidthBottom = 0.75f;
                clId.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell clIdUsuario = new PdfPCell(new Phrase("ID Usuario", standarFont));
                clIdUsuario.BorderWidth = 0;
                clIdUsuario.BorderWidthBottom = 0.75f;
                clIdUsuario.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell clIdPropiedad = new PdfPCell(new Phrase("ID Propiedad", standarFont));
                clIdPropiedad.BorderWidth = 0;
                clIdPropiedad.BorderWidthBottom = 0.75f;
                clIdPropiedad.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell clTitulo = new PdfPCell(new Phrase("Título", standarFont));
                clTitulo.BorderWidth = 0;
                clTitulo.BorderWidthBottom = 0.75f;
                clTitulo.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell clFecha_Hora_I = new PdfPCell(new Phrase("Fecha/hora inicio", standarFont));
                clFecha_Hora_I.BorderWidth = 0;
                clFecha_Hora_I.BorderWidthBottom = 0.75f;
                clFecha_Hora_I.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell clFecha_Hora_F = new PdfPCell(new Phrase("Fecha/hora final", standarFont));
                clFecha_Hora_F.BorderWidth = 0;
                clFecha_Hora_F.BorderWidthBottom = 0.75f;
                clFecha_Hora_F.BackgroundColor = new BaseColor(89, 89, 89);

                tbl.AddCell(clId);
                tbl.AddCell(clIdUsuario);
                tbl.AddCell(clIdPropiedad);
                tbl.AddCell(clTitulo);
                tbl.AddCell(clFecha_Hora_I);
                tbl.AddCell(clFecha_Hora_F);

                var i = 0;
                //AGREGAR DATOS AL PDF
                foreach (var cita in listaCitas)
                {
                    if (i % 2 == 0)
                    {
                        clId = new PdfPCell(new Phrase(cita.idCita.ToString(), standarFontss));
                        clId.BorderWidth = 0;
                        //clId.BackgroundColor = BaseColor.LIGHT_GRAY;

                        clIdUsuario = new PdfPCell(new Phrase(cita.idUsuario.ToString(), standarFontss));
                        clIdUsuario.BorderWidth = 0;
                        //clIdUsuario.BackgroundColor = BaseColor.LIGHT_GRAY;

                        clIdPropiedad = new PdfPCell(new Phrase(cita.idPropiedad.ToString(), standarFontss));
                        clIdPropiedad.BorderWidth = 0;
                        //clIdPropiedad.BackgroundColor = BaseColor.LIGHT_GRAY;

                        clTitulo = new PdfPCell(new Phrase(cita.titulo.ToString(), standarFontss));
                        clTitulo.BorderWidth = 0;
                        //clTitulo.BackgroundColor = BaseColor.LIGHT_GRAY;

                        clFecha_Hora_I = new PdfPCell(new Phrase(cita.fechaInicio.ToString(), standarFontss));
                        clFecha_Hora_I.BorderWidth = 0;
                        //clFecha_Hora_I.BackgroundColor = BaseColor.LIGHT_GRAY;

                        clFecha_Hora_F = new PdfPCell(new Phrase(cita.fechaFinal.ToString(), standarFontss));
                        clFecha_Hora_F.BorderWidth = 0;
                        //clFecha_Hora_F.BackgroundColor = BaseColor.LIGHT_GRAY;

                        //tbl.AddCell(clId);
                        //tbl.AddCell(clIdUsuario);
                        //tbl.AddCell(clIdPropiedad);
                        //tbl.AddCell(clTitulo);
                        //tbl.AddCell(clFecha_Hora_I);
                        //tbl.AddCell(clFecha_Hora_F);
                    }
                    else
                    {
                        clId = new PdfPCell(new Phrase(cita.idCita.ToString(), standarFonts));
                        clId.BorderWidth = 0;
                        clId.BackgroundColor = new BaseColor(89, 89, 89);

                        clIdUsuario = new PdfPCell(new Phrase(cita.idUsuario.ToString(), standarFonts));
                        clIdUsuario.BorderWidth = 0;
                        clIdUsuario.BackgroundColor = new BaseColor(89, 89, 89);

                        clIdPropiedad = new PdfPCell(new Phrase(cita.idPropiedad.ToString(), standarFonts));
                        clIdPropiedad.BorderWidth = 0;
                        clIdPropiedad.BackgroundColor = new BaseColor(89, 89, 89);

                        clTitulo = new PdfPCell(new Phrase(cita.titulo.ToString(), standarFonts));
                        clTitulo.BorderWidth = 0;
                        clTitulo.BackgroundColor = new BaseColor(89, 89, 89);

                        clFecha_Hora_I = new PdfPCell(new Phrase(cita.fechaInicio.ToString(), standarFonts));
                        clFecha_Hora_I.BorderWidth = 0;
                        clFecha_Hora_I.BackgroundColor = new BaseColor(89, 89, 89);

                        clFecha_Hora_F = new PdfPCell(new Phrase(cita.fechaFinal.ToString(), standarFonts));
                        clFecha_Hora_F.BorderWidth = 0;
                        clFecha_Hora_F.BackgroundColor = new BaseColor(89, 89, 89);

                        //tbl.AddCell(clId);
                        //tbl.AddCell(clIdUsuario);
                        //tbl.AddCell(clIdPropiedad);
                        //tbl.AddCell(clTitulo);
                        //tbl.AddCell(clFecha_Hora_I);
                        //tbl.AddCell(clFecha_Hora_F);
                    }
                    //clId = new PdfPCell(new Phrase(cita.idCita.ToString(), standarFont));
                    //clId.BorderWidth = 0;
                    //clIdUsuario = new PdfPCell(new Phrase(cita.idUsuario.ToString(), standarFont));
                    //clIdUsuario.BorderWidth = 0;
                    //clIdPropiedad = new PdfPCell(new Phrase(cita.idPropiedad.ToString(), standarFont));
                    //clIdPropiedad.BorderWidth = 0;
                    //clTitulo = new PdfPCell(new Phrase(cita.titulo.ToString(), standarFont));
                    //clTitulo.BorderWidth = 0;
                    //clFecha_Hora_I = new PdfPCell(new Phrase(cita.fechaInicio.ToString(), standarFont));
                    //clFecha_Hora_I.BorderWidth = 0;
                    //clFecha_Hora_F = new PdfPCell(new Phrase(cita.fechaFinal.ToString(), standarFont));
                    //clFecha_Hora_F.BorderWidth = 0;

                    tbl.AddCell(clId);
                    tbl.AddCell(clIdUsuario);
                    tbl.AddCell(clIdPropiedad);
                    tbl.AddCell(clTitulo);
                    tbl.AddCell(clFecha_Hora_I);
                    tbl.AddCell(clFecha_Hora_F);

                    i++;
                }

                doc.Add(tbl);
                doc.Close();
                pw.Close();

                return Redirect(@"https://bienesraiceshaxa.azurewebsites.net/Content/ReporteCitas.pdf");

            }
            catch (Exception ee)
            {
                throw (ee);
            }


        }


        public ActionResult generarPDFPropiedad(object sender, EventArgs e)
        {
            try
            {
                ReporteModel modelo = new ReporteModel();
                List<Propiedad> listaPropiedades = modelo.obtenerPropiedad();

                FileStream fs = new FileStream(Server.MapPath("~/Content/") + "ReportePropiedades.pdf", FileMode.Create);
                Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
                PdfWriter pw = PdfWriter.GetInstance(doc, fs);

                doc.Open();
                //TITULO Y AUTOR 
                doc.AddAuthor("Reporte");
                doc.AddCreator("Bienes raíces Haxa");

                //DEFINIR FUENTE DEL DOCUMENTO
                iTextSharp.text.Font standarFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font standarFonts = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);
                iTextSharp.text.Font standarFontss = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);


                //ENCABEZADO DEL DOCUMENTO
                doc.Add(new Paragraph("Reporte Propiedades"));
                doc.Add(Chunk.NEWLINE);

                //ENCABEZADO COLUMNAS
                PdfPTable tbl = new PdfPTable(16);
                tbl.WidthPercentage = 100;

                //ORDEN DE NOMBRE DE LAS COLUMNAS
                PdfPCell cl1 = new PdfPCell(new Phrase("ID", standarFont));
                cl1.BorderWidth = 0;
                cl1.BorderWidthBottom = 0.75f;
                cl1.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl2 = new PdfPCell(new Phrase("Nombre", standarFont));
                cl2.BorderWidth = 0;
                cl2.BorderWidthBottom = 0.75f;
                cl2.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl3 = new PdfPCell(new Phrase("Provincia", standarFont));
                cl3.BorderWidth = 0;
                cl3.BorderWidthBottom = 0.75f;
                cl3.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl4 = new PdfPCell(new Phrase("Cantón", standarFont));
                cl4.BorderWidth = 0;
                cl4.BorderWidthBottom = 0.75f;
                cl4.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl5 = new PdfPCell(new Phrase("Distrito", standarFont));
                cl5.BorderWidth = 0;
                cl5.BorderWidthBottom = 0.75f;
                cl5.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl6 = new PdfPCell(new Phrase("Direccion", standarFont));
                cl6.BorderWidth = 0;
                cl6.BorderWidthBottom = 0.75f;
                cl6.BackgroundColor = new BaseColor(89, 89, 89);

                //PdfPCell cl7 = new PdfPCell(new Phrase("Descripción", standarFont));
                //cl7.BorderWidth = 0;
                //cl7.BorderWidthBottom = 0.75f;
                //cl7.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl8 = new PdfPCell(new Phrase("ID Categoría", standarFont));
                cl8.BorderWidth = 0;
                cl8.BorderWidthBottom = 0.75f;
                cl8.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl9 = new PdfPCell(new Phrase("ID Estado", standarFont));
                cl9.BorderWidth = 0;
                cl9.BorderWidthBottom = 0.75f;
                cl9.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl10 = new PdfPCell(new Phrase("Pisos", standarFont));
                cl10.BorderWidth = 0;
                cl10.BorderWidthBottom = 0.75f;
                cl10.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl11 = new PdfPCell(new Phrase("M2", standarFont));
                cl11.BorderWidth = 0;
                cl11.BorderWidthBottom = 0.75f;
                cl11.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl12 = new PdfPCell(new Phrase("Habitación", standarFont));
                cl12.BorderWidth = 0;
                cl12.BorderWidthBottom = 0.75f;
                cl12.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl13 = new PdfPCell(new Phrase("Baños", standarFont));
                cl13.BorderWidth = 0;
                cl13.BorderWidthBottom = 0.75f;
                cl13.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl14 = new PdfPCell(new Phrase("Garaje", standarFont));
                cl14.BorderWidth = 0;
                cl14.BorderWidthBottom = 0.75f;
                cl14.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl15 = new PdfPCell(new Phrase("ID Usuario", standarFont));
                cl15.BorderWidth = 0;
                cl15.BorderWidthBottom = 0.75f;
                cl15.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl16 = new PdfPCell(new Phrase("ID Vendedor", standarFont));
                cl16.BorderWidth = 0;
                cl16.BorderWidthBottom = 0.75f;
                cl16.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl17 = new PdfPCell(new Phrase("Precio", standarFont));
                cl17.BorderWidth = 0;
                cl17.BorderWidthBottom = 0.75f;
                cl17.BackgroundColor = new BaseColor(89, 89, 89);

                tbl.AddCell(cl1);
                tbl.AddCell(cl2);
                tbl.AddCell(cl3);
                tbl.AddCell(cl4);
                tbl.AddCell(cl5);
                tbl.AddCell(cl6);
                //tbl.AddCell(cl7);
                tbl.AddCell(cl8);
                tbl.AddCell(cl9);
                tbl.AddCell(cl10);
                tbl.AddCell(cl11);
                tbl.AddCell(cl12);
                tbl.AddCell(cl13);
                tbl.AddCell(cl14);
                tbl.AddCell(cl15);
                tbl.AddCell(cl16);
                tbl.AddCell(cl17);

                var i = 0;
                //AGREGAR DATOS AL PDF
                foreach (var propiedad in listaPropiedades)
                {
                    if (i % 2 == 0)
                    {
                        cl1 = new PdfPCell(new Phrase(propiedad.idPropiedad.ToString(), standarFontss));
                        cl1.BorderWidth = 0;
                        //cl1.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl2 = new PdfPCell(new Phrase(propiedad.nombre.ToString(), standarFontss));
                        cl2.BorderWidth = 0;
                        //cl2.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl3 = new PdfPCell(new Phrase(propiedad.provincia.ToString(), standarFontss));
                        cl3.BorderWidth = 0;
                        //cl3.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl4 = new PdfPCell(new Phrase(propiedad.canton.ToString(), standarFontss));
                        cl4.BorderWidth = 0;
                        //cl4.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl5 = new PdfPCell(new Phrase(propiedad.distrito.ToString(), standarFontss));
                        cl5.BorderWidth = 0;
                        //cl5.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl6 = new PdfPCell(new Phrase(propiedad.direccion.ToString(), standarFontss));
                        cl6.BorderWidth = 0;
                        //cl6.BackgroundColor = BaseColor.LIGHT_GRAY;

                        //cl7 = new PdfPCell(new Phrase(propiedad.descripcion.ToString(), standarFontss));
                        //cl7.BorderWidth = 0;
                        ////cl7.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl8 = new PdfPCell(new Phrase(propiedad.idCategoria.ToString(), standarFontss));
                        cl8.BorderWidth = 0;
                        //cl8.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl9 = new PdfPCell(new Phrase(propiedad.idEstado.ToString(), standarFontss));
                        cl9.BorderWidth = 0;
                        //cl9.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl10 = new PdfPCell(new Phrase(propiedad.pisos.ToString(), standarFontss));
                        cl10.BorderWidth = 0;
                        //cl10.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl11 = new PdfPCell(new Phrase(propiedad.m2.ToString(), standarFontss));
                        cl11.BorderWidth = 0;
                        //cl11.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl12 = new PdfPCell(new Phrase(propiedad.habitacion.ToString(), standarFontss));
                        cl12.BorderWidth = 0;
                        //cl12.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl13 = new PdfPCell(new Phrase(propiedad.baños.ToString(), standarFontss));
                        cl13.BorderWidth = 0;
                        //cl13.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl14 = new PdfPCell(new Phrase(propiedad.garage.ToString(), standarFontss));
                        cl14.BorderWidth = 0;
                        //cl14.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl15 = new PdfPCell(new Phrase(propiedad.idUsuario.ToString(), standarFontss));
                        cl15.BorderWidth = 0;
                        //cl15.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl16 = new PdfPCell(new Phrase(propiedad.idVendedor.ToString(), standarFontss));
                        cl16.BorderWidth = 0;
                        //cl16.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl17 = new PdfPCell(new Phrase(propiedad.precio.ToString(), standarFontss));
                        cl17.BorderWidth = 0;
                        //cl17.BackgroundColor = BaseColor.LIGHT_GRAY;

                    }
                    else
                    {
                        cl1 = new PdfPCell(new Phrase(propiedad.idPropiedad.ToString(), standarFonts));
                        cl1.BorderWidth = 0;
                        cl1.BackgroundColor = new BaseColor(89, 89, 89);

                        cl2 = new PdfPCell(new Phrase(propiedad.nombre.ToString(), standarFonts));
                        cl2.BorderWidth = 0;
                        cl2.BackgroundColor = new BaseColor(89, 89, 89);

                        cl3 = new PdfPCell(new Phrase(propiedad.provincia.ToString(), standarFonts));
                        cl3.BorderWidth = 0;
                        cl3.BackgroundColor = new BaseColor(89, 89, 89);

                        cl4 = new PdfPCell(new Phrase(propiedad.canton.ToString(), standarFonts));
                        cl4.BorderWidth = 0;
                        cl4.BackgroundColor = new BaseColor(89, 89, 89);

                        cl5 = new PdfPCell(new Phrase(propiedad.distrito.ToString(), standarFonts));
                        cl5.BorderWidth = 0;
                        cl5.BackgroundColor = new BaseColor(89, 89, 89);

                        cl6 = new PdfPCell(new Phrase(propiedad.direccion.ToString(), standarFonts));
                        cl6.BorderWidth = 0;
                        cl6.BackgroundColor = new BaseColor(89, 89, 89);

                        //cl7 = new PdfPCell(new Phrase(propiedad.descripcion.ToString(), standarFonts));
                        //cl7.BorderWidth = 0;
                        //cl7.BackgroundColor = new BaseColor(89, 89, 89);

                        cl8 = new PdfPCell(new Phrase(propiedad.idCategoria.ToString(), standarFonts));
                        cl8.BorderWidth = 0;
                        cl8.BackgroundColor = new BaseColor(89, 89, 89);

                        cl9 = new PdfPCell(new Phrase(propiedad.idEstado.ToString(), standarFonts));
                        cl9.BorderWidth = 0;
                        cl9.BackgroundColor = new BaseColor(89, 89, 89);

                        cl10 = new PdfPCell(new Phrase(propiedad.pisos.ToString(), standarFonts));
                        cl10.BorderWidth = 0;
                        cl10.BackgroundColor = new BaseColor(89, 89, 89);

                        cl11 = new PdfPCell(new Phrase(propiedad.m2.ToString(), standarFonts));
                        cl11.BorderWidth = 0;
                        cl11.BackgroundColor = new BaseColor(89, 89, 89);

                        cl12 = new PdfPCell(new Phrase(propiedad.habitacion.ToString(), standarFonts));
                        cl12.BorderWidth = 0;
                        cl12.BackgroundColor = new BaseColor(89, 89, 89);

                        cl13 = new PdfPCell(new Phrase(propiedad.baños.ToString(), standarFonts));
                        cl13.BorderWidth = 0;
                        cl13.BackgroundColor = new BaseColor(89, 89, 89);

                        cl14 = new PdfPCell(new Phrase(propiedad.garage.ToString(), standarFonts));
                        cl14.BorderWidth = 0;
                        cl14.BackgroundColor = new BaseColor(89, 89, 89);

                        cl15 = new PdfPCell(new Phrase(propiedad.idUsuario.ToString(), standarFonts));
                        cl15.BorderWidth = 0;
                        cl15.BackgroundColor = new BaseColor(89, 89, 89);

                        cl16 = new PdfPCell(new Phrase(propiedad.idVendedor.ToString(), standarFonts));
                        cl16.BorderWidth = 0;
                        cl16.BackgroundColor = new BaseColor(89, 89, 89);

                        cl17 = new PdfPCell(new Phrase(propiedad.precio.ToString(), standarFonts));
                        cl17.BorderWidth = 0;
                        cl17.BackgroundColor = new BaseColor(89, 89, 89);

                    }

                    tbl.AddCell(cl1);
                    tbl.AddCell(cl2);
                    tbl.AddCell(cl3);
                    tbl.AddCell(cl4);
                    tbl.AddCell(cl5);
                    tbl.AddCell(cl6);
                    //tbl.AddCell(cl7);
                    tbl.AddCell(cl8);
                    tbl.AddCell(cl9);
                    tbl.AddCell(cl10);
                    tbl.AddCell(cl11);
                    tbl.AddCell(cl12);
                    tbl.AddCell(cl13);
                    tbl.AddCell(cl14);
                    tbl.AddCell(cl15);
                    tbl.AddCell(cl16);
                    tbl.AddCell(cl17);

                    i++;
                }

                doc.Add(tbl);
                doc.Close();
                pw.Close();
                return Redirect(@"https://bienesraiceshaxa.azurewebsites.net/Content/ReportePropiedades.pdf");

            }
            catch (Exception ee)
            {
                throw (ee);
            }
        }

        public ActionResult generarPDFUsuarios(object sender, EventArgs e)
        {
            try
            {
                ReporteModel modelo = new ReporteModel();
                List<Usuario> listaUsuarios = modelo.obtenerUsuarios();

                FileStream fs = new FileStream(Server.MapPath("~/Content/") + "ReporteUsuarios.pdf", FileMode.Create);
                Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
                PdfWriter pw = PdfWriter.GetInstance(doc, fs);

                doc.Open();
                //TITULO Y AUTOR 
                doc.AddAuthor("Reporte");
                doc.AddCreator("Bienes raíces Haxa");

                //DEFINIR FUENTE DEL DOCUMENTO
                iTextSharp.text.Font standarFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font standarFonts = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);
                iTextSharp.text.Font standarFontss = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);


                //ENCABEZADO DEL DOCUMENTO
                doc.Add(new Paragraph("Reporte Usuarios"));
                doc.Add(Chunk.NEWLINE);

                //ENCABEZADO COLUMNAS
                PdfPTable tbl = new PdfPTable(7);
                tbl.WidthPercentage = 100;

                //ORDEN DE NOMBRE DE LAS COLUMNAS
                PdfPCell cl1 = new PdfPCell(new Phrase("ID", standarFont));
                cl1.BorderWidth = 0;
                cl1.BorderWidthBottom = 0.75f;
                cl1.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl2 = new PdfPCell(new Phrase("Nombre", standarFont));
                cl2.BorderWidth = 0;
                cl2.BorderWidthBottom = 0.75f;
                cl2.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl3 = new PdfPCell(new Phrase("Primer apellido", standarFont));
                cl3.BorderWidth = 0;
                cl3.BorderWidthBottom = 0.75f;
                cl3.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl4 = new PdfPCell(new Phrase("Segundo apellido", standarFont));
                cl4.BorderWidth = 0;
                cl4.BorderWidthBottom = 0.75f;
                cl4.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl5 = new PdfPCell(new Phrase("Teléfono", standarFont));
                cl5.BorderWidth = 0;
                cl5.BorderWidthBottom = 0.75f;
                cl5.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl6 = new PdfPCell(new Phrase("Email", standarFont));
                cl6.BorderWidth = 0;
                cl6.BorderWidthBottom = 0.75f;
                cl6.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl7 = new PdfPCell(new Phrase("Password", standarFont));
                cl7.BorderWidth = 0;
                cl7.BorderWidthBottom = 0.75f;
                cl7.BackgroundColor = new BaseColor(89, 89, 89);

                tbl.AddCell(cl1);
                tbl.AddCell(cl2);
                tbl.AddCell(cl3);
                tbl.AddCell(cl4);
                tbl.AddCell(cl5);
                tbl.AddCell(cl6);
                tbl.AddCell(cl7);

                var i = 0;
                //AGREGAR DATOS AL PDF
                foreach (var usuario in listaUsuarios)
                {
                    if (i % 2 == 0)
                    {
                        cl1 = new PdfPCell(new Phrase(usuario.cedula_identificacion.ToString(), standarFontss));
                        cl1.BorderWidth = 0;
                        //cl1.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl2 = new PdfPCell(new Phrase(usuario.nombre.ToString(), standarFontss));
                        cl2.BorderWidth = 0;
                        //cl2.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl3 = new PdfPCell(new Phrase(usuario.apellido1.ToString(), standarFontss));
                        cl3.BorderWidth = 0;
                        //cl3.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl4 = new PdfPCell(new Phrase(usuario.apellido2.ToString(), standarFontss));
                        cl4.BorderWidth = 0;
                        //cl4.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl5 = new PdfPCell(new Phrase(usuario.telefono.ToString(), standarFontss));
                        cl5.BorderWidth = 0;
                        //cl5.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl6 = new PdfPCell(new Phrase(usuario.email.ToString(), standarFontss));
                        cl6.BorderWidth = 0;
                        //cl6.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl7 = new PdfPCell(new Phrase(usuario.password.ToString(), standarFontss));
                        cl7.BorderWidth = 0;
                        //cl7.BackgroundColor = BaseColor.LIGHT_GRAY;
                    }
                    else
                    {
                        cl1 = new PdfPCell(new Phrase(usuario.cedula_identificacion.ToString(), standarFonts));
                        cl1.BorderWidth = 0;
                        cl1.BackgroundColor = new BaseColor(89, 89, 89);

                        cl2 = new PdfPCell(new Phrase(usuario.nombre.ToString(), standarFonts));
                        cl2.BorderWidth = 0;
                        cl2.BackgroundColor = new BaseColor(89, 89, 89);

                        cl3 = new PdfPCell(new Phrase(usuario.apellido1.ToString(), standarFonts));
                        cl3.BorderWidth = 0;
                        cl3.BackgroundColor = new BaseColor(89, 89, 89);

                        cl4 = new PdfPCell(new Phrase(usuario.apellido2.ToString(), standarFonts));
                        cl4.BorderWidth = 0;
                        cl4.BackgroundColor = new BaseColor(89, 89, 89);

                        cl5 = new PdfPCell(new Phrase(usuario.telefono.ToString(), standarFonts));
                        cl5.BorderWidth = 0;
                        cl5.BackgroundColor = new BaseColor(89, 89, 89);

                        cl6 = new PdfPCell(new Phrase(usuario.email.ToString(), standarFonts));
                        cl6.BorderWidth = 0;
                        cl6.BackgroundColor = new BaseColor(89, 89, 89);

                        cl7 = new PdfPCell(new Phrase(usuario.password.ToString(), standarFonts));
                        cl7.BorderWidth = 0;
                        cl7.BackgroundColor = new BaseColor(89, 89, 89);

                    }

                    tbl.AddCell(cl1);
                    tbl.AddCell(cl2);
                    tbl.AddCell(cl3);
                    tbl.AddCell(cl4);
                    tbl.AddCell(cl5);
                    tbl.AddCell(cl6);
                    tbl.AddCell(cl7);

                    i++;
                }

                doc.Add(tbl);
                doc.Close();
                pw.Close();
                return Redirect(@"https://bienesraiceshaxa.azurewebsites.net/Content/ReporteUsuarios.pdf");
                //return Redirect(@"https://localhost:44335/Content/ReporteUsuarios.pdf");
            }
            catch (Exception ee)
            {
                throw (ee);
            }
        }

        public ActionResult generarPDFEmpleados(object sender, EventArgs e)
        {
            try
            {
                ReporteModel modelo = new ReporteModel();
                List<Usuario> listaEmpleados = modelo.obtenerEmpleados();

                FileStream fs = new FileStream(Server.MapPath("~/Content/") + "ReporteEmpleados.pdf", FileMode.Create);
                Document doc = new Document(PageSize.LETTER, 5, 5, 7, 7);
                PdfWriter pw = PdfWriter.GetInstance(doc, fs);

                doc.Open();
                //TITULO Y AUTOR 
                doc.AddAuthor("Reporte");
                doc.AddCreator("Bienes raíces Haxa");

                //DEFINIR FUENTE DEL DOCUMENTO
                iTextSharp.text.Font standarFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
                iTextSharp.text.Font standarFonts = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.WHITE);
                iTextSharp.text.Font standarFontss = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);


                //ENCABEZADO DEL DOCUMENTO
                doc.Add(new Paragraph("Reporte Empleados"));
                doc.Add(Chunk.NEWLINE);

                //ENCABEZADO COLUMNAS
                PdfPTable tbl = new PdfPTable(7);
                tbl.WidthPercentage = 100;

                //ORDEN DE NOMBRE DE LAS COLUMNAS
                PdfPCell cl1 = new PdfPCell(new Phrase("ID", standarFont));
                cl1.BorderWidth = 0;
                cl1.BorderWidthBottom = 0.75f;
                cl1.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl2 = new PdfPCell(new Phrase("Nombre", standarFont));
                cl2.BorderWidth = 0;
                cl2.BorderWidthBottom = 0.75f;
                cl2.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl3 = new PdfPCell(new Phrase("Primer apellido", standarFont));
                cl3.BorderWidth = 0;
                cl3.BorderWidthBottom = 0.75f;
                cl3.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl4 = new PdfPCell(new Phrase("Segundo apellido", standarFont));
                cl4.BorderWidth = 0;
                cl4.BorderWidthBottom = 0.75f;
                cl4.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl5 = new PdfPCell(new Phrase("Teléfono", standarFont));
                cl5.BorderWidth = 0;
                cl5.BorderWidthBottom = 0.75f;
                cl5.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl6 = new PdfPCell(new Phrase("Email", standarFont));
                cl6.BorderWidth = 0;
                cl6.BorderWidthBottom = 0.75f;
                cl6.BackgroundColor = new BaseColor(89, 89, 89);

                PdfPCell cl7 = new PdfPCell(new Phrase("Password", standarFont));
                cl7.BorderWidth = 0;
                cl7.BorderWidthBottom = 0.75f;
                cl7.BackgroundColor = new BaseColor(89, 89, 89);

                tbl.AddCell(cl1);
                tbl.AddCell(cl2);
                tbl.AddCell(cl3);
                tbl.AddCell(cl4);
                tbl.AddCell(cl5);
                tbl.AddCell(cl6);
                tbl.AddCell(cl7);

                var i = 0;
                //AGREGAR DATOS AL PDF
                foreach (var empleado in listaEmpleados)
                {
                    if (i % 2 == 0)
                    {
                        cl1 = new PdfPCell(new Phrase(empleado.cedula_identificacion.ToString(), standarFontss));
                        cl1.BorderWidth = 0;
                        //cl1.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl2 = new PdfPCell(new Phrase(empleado.nombre.ToString(), standarFontss));
                        cl2.BorderWidth = 0;
                        //cl2.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl3 = new PdfPCell(new Phrase(empleado.apellido1.ToString(), standarFontss));
                        cl3.BorderWidth = 0;
                        //cl3.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl4 = new PdfPCell(new Phrase(empleado.apellido2.ToString(), standarFontss));
                        cl4.BorderWidth = 0;
                        //cl4.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl5 = new PdfPCell(new Phrase(empleado.telefono.ToString(), standarFontss));
                        cl5.BorderWidth = 0;
                        //cl5.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl6 = new PdfPCell(new Phrase(empleado.email.ToString(), standarFontss));
                        cl6.BorderWidth = 0;
                        //cl6.BackgroundColor = BaseColor.LIGHT_GRAY;

                        cl7 = new PdfPCell(new Phrase(empleado.password.ToString(), standarFontss));
                        cl7.BorderWidth = 0;
                        //cl7.BackgroundColor = BaseColor.LIGHT_GRAY;
                    }
                    else
                    {
                        cl1 = new PdfPCell(new Phrase(empleado.cedula_identificacion.ToString(), standarFonts));
                        cl1.BorderWidth = 0;
                        cl1.BackgroundColor = new BaseColor(89, 89, 89);

                        cl2 = new PdfPCell(new Phrase(empleado.nombre.ToString(), standarFonts));
                        cl2.BorderWidth = 0;
                        cl2.BackgroundColor = new BaseColor(89, 89, 89);

                        cl3 = new PdfPCell(new Phrase(empleado.apellido1.ToString(), standarFonts));
                        cl3.BorderWidth = 0;
                        cl3.BackgroundColor = new BaseColor(89, 89, 89);

                        cl4 = new PdfPCell(new Phrase(empleado.apellido2.ToString(), standarFonts));
                        cl4.BorderWidth = 0;
                        cl4.BackgroundColor = new BaseColor(89, 89, 89);

                        cl5 = new PdfPCell(new Phrase(empleado.telefono.ToString(), standarFonts));
                        cl5.BorderWidth = 0;
                        cl5.BackgroundColor = new BaseColor(89, 89, 89);

                        cl6 = new PdfPCell(new Phrase(empleado.email.ToString(), standarFonts));
                        cl6.BorderWidth = 0;
                        cl6.BackgroundColor = new BaseColor(89, 89, 89);

                        cl7 = new PdfPCell(new Phrase(empleado.password.ToString(), standarFonts));
                        cl7.BorderWidth = 0;
                        cl7.BackgroundColor = new BaseColor(89, 89, 89);

                    }

                    tbl.AddCell(cl1);
                    tbl.AddCell(cl2);
                    tbl.AddCell(cl3);
                    tbl.AddCell(cl4);
                    tbl.AddCell(cl5);
                    tbl.AddCell(cl6);
                    tbl.AddCell(cl7);

                    i++;
                }

                doc.Add(tbl);
                doc.Close();
                pw.Close();
                return Redirect(@"https://bienesraiceshaxa.azurewebsites.net/Content/ReporteEmpleados.pdf");
                //return Redirect(@"https://localhost:44335/Content/ReporteEmpleados.pdf");
            }
            catch (Exception ee)
            {
                throw (ee);
            }
        }
    }
}
