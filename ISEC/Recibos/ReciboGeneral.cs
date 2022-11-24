using ISEC.Converciones;
using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
using ISEC.ITextSHARP;
using ISEC.Modelos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEC.Recibos
{
    public class ReciboGeneral
    {
        //[System.Runtime.InteropServices.DllImport("shell32. dll")]
        //private static extern long ShellExecute(Int32 hWnd, string lpOperation,
        //                                  string lpFile, string lpParameters,
        //
        //             string lpDirectory, long nShowCmd);


        public static void Create(PagoLocalViewModel lastPago)
        {
            //MessageBox.Show(JsonConvert.SerializeObject(lastPago));
            string filepath = @"C:\isec\" + lastPago.Folio + ".pdf";
            System.IO.FileStream fs = new FileStream(filepath, FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.AddAuthor("Micke Blomquist");
            document.AddCreator("Sample application using iTextSharp");
            document.AddKeywords("PDF tutorial education");
            document.AddSubject("Document subject - Describing the steps creating a PDF document");
            document.AddTitle("The document title - PDF creation using iTextSharp");
            document.Open();

            //Logo y fecha
            PdfPTable tblHeaderF = new PdfPTable(2);
            tblHeaderF.WidthPercentage = 100;
            tblHeaderF.SetWidths(new int[] { 25, 75 });
            var bmp = ISEC.Properties.Resources.ISECJPG;
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bmpBytes = ms.ToArray();
            Image img = Image.GetInstance(bmpBytes);
            img.SetAbsolutePosition(0, 0);
            img.ScaleAbsoluteHeight(50);
            img.ScaleAbsoluteWidth(100);
            var cllImg = new PdfPCell(img);
            cllImg.Border = Rectangle.NO_BORDER;
            cllImg.HorizontalAlignment = Element.ALIGN_LEFT;
            tblHeaderF.AddCell(cllImg);


            PdfPCell cellFecha = new PdfPCell(new Phrase(new Chunk($"{DateTime.Parse(lastPago.Fecha).ToLongDateString()} {DateTime.Now.ToString("hh:mm:ss tt")}", GeneralFonts.fontTitleBold())));
            cellFecha.Border = Rectangle.NO_BORDER;
            cellFecha.HorizontalAlignment = Element.ALIGN_RIGHT;
            tblHeaderF.AddCell(cellFecha);
            document.Add(tblHeaderF);

          
            PdfPTable tblfolio = new PdfPTable(2);
            tblfolio.WidthPercentage = 100;

            PdfPCell cellnombre = new PdfPCell(new Phrase(new Chunk($"Alumno: (#{lastPago.Credencial}) {lastPago.Nombre}", GeneralFonts.fontTitleBold())));
            cellnombre.HorizontalAlignment = Element.ALIGN_LEFT;
            cellnombre.Border = Rectangle.NO_BORDER;
            tblfolio.AddCell(cellnombre);

            PdfPCell cellfolio = new PdfPCell(new Phrase(new Chunk($"Folio: {lastPago.Folio}", GeneralFonts.fontTitleBold())));
            cellfolio.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellfolio.Border = Rectangle.NO_BORDER;
            tblfolio.AddCell(cellfolio);

            tblfolio.SpacingBefore = 10;
            tblfolio.SpacingAfter = 10;


            document.Add(tblfolio);

            //if (lastPago.Clave != "I")
            //{
            //    PdfPTable tblWeeks = new PdfPTable(1);
            //    tblWeeks.WidthPercentage = 100;
            //    PdfPCell cellSemanas = new PdfPCell(new Phrase(new Chunk($"Pago semamas: {lastPago.Week1}-{lastPago.Week2}", GeneralFonts.fontTitleBold())));
            //    cellSemanas.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cellSemanas.Border = Rectangle.NO_BORDER;
            //    tblWeeks.AddCell(cellSemanas);
            //    document.Add(tblWeeks);
            //}
           

            //tabla alumno
            PdfPTable tblAlumno = new PdfPTable(4);
            tblAlumno.WidthPercentage = 100;
            PdfPCell cellGrupo = new PdfPCell(new Phrase($"Grupo: {lastPago.Grupo}"));
            cellGrupo.Border = Rectangle.NO_BORDER;
            cellGrupo.HorizontalAlignment = Element.ALIGN_LEFT;
            tblAlumno.AddCell(cellGrupo);

            PdfPCell cellHorario = new PdfPCell(new Phrase($"Horario: {lastPago.Horario}"));
            cellHorario.Border = Rectangle.NO_BORDER;
            cellHorario.HorizontalAlignment = Element.ALIGN_CENTER;
            tblAlumno.AddCell(cellHorario);

            PdfPCell cellSemana = new PdfPCell(new Phrase($"Semana:{Functions.Week(DateTime.Parse(lastPago.Fecha)).ToString()}"));
            cellSemana.Border = Rectangle.NO_BORDER;
            cellSemana.HorizontalAlignment = Element.ALIGN_CENTER;
            tblAlumno.AddCell(cellSemana);

            string debe = lastPago.Resto > 0 ? "S" : "N";
            PdfPCell cellDebe = new PdfPCell(new Phrase($"Debe resto: {debe}"));
            cellDebe.Border = Rectangle.NO_BORDER;
            cellDebe.HorizontalAlignment = Element.ALIGN_RIGHT;
            tblAlumno.AddCell(cellDebe);

            document.Add(tblAlumno);


            PdfPTable tblinfo = new PdfPTable(2);
            tblinfo.WidthPercentage = 100;
            PdfPCell puser = new PdfPCell(new Phrase($"Atendió: {lastPago.Username}"));
            puser.HorizontalAlignment = Element.ALIGN_LEFT;
            puser.Border = Rectangle.NO_BORDER;
            tblinfo.AddCell(puser);

            tblinfo.WidthPercentage = 100;
            PdfPCell pciudad = new PdfPCell(new Phrase(new Chunk("NOGALES, SONORA", GeneralFonts.fontTitleBold())));
            pciudad.HorizontalAlignment = Element.ALIGN_RIGHT;
            pciudad.Border = Rectangle.NO_BORDER;
            tblinfo.AddCell(pciudad);
            tblinfo.SpacingBefore = 10;
            tblinfo.SpacingAfter = 10;
            document.Add(tblinfo);


            //tabla pago
            int columns = 2;
            if (lastPago.Resto > 0)
            {
                columns = 3;
            }
            PdfPTable tblHeader = new PdfPTable(columns);
            tblHeader.WidthPercentage = 100;
            PdfPCell cellCuota = new PdfPCell(new Phrase(new Chunk("Cuota", GeneralFonts.fontTitleNormal())));
            cellCuota.BackgroundColor = new BaseColor(0, 0, 0);
            cellCuota.BorderColorRight = new BaseColor(255, 255, 255);
            cellCuota.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

            tblHeader.AddCell(cellCuota);

            PdfPCell cellCantidad = new PdfPCell(new Phrase(new Chunk("Cantidad", GeneralFonts.fontTitleNormal())));
            cellCantidad.BackgroundColor = new BaseColor(0, 0, 0);
            cellCantidad.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            if (lastPago.Resto > 0)
            {
                cellCantidad.BorderColorLeft = new BaseColor(255, 255, 255);
            }
            else
            {
                cellCantidad.BorderColorRight = new BaseColor(255, 255, 255);
            }
            tblHeader.AddCell(cellCantidad);

            if (lastPago.Resto > 0)
            {
                PdfPCell cellResto = new PdfPCell(new Phrase(new Chunk("Debe", GeneralFonts.fontTitleNormal())));
                cellResto.BackgroundColor = new BaseColor(0, 0, 0);
                cellResto.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                tblHeader.AddCell(cellResto);
            }
            document.Add(tblHeader);
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            int columnsdebe = 2;
            if (lastPago.Resto > 0)
            {
                columnsdebe = 3;
            }
            PdfPTable table = new PdfPTable(columnsdebe);
            table.WidthPercentage = 100;
            table.AddCell($"{lastPago.Cuota}({lastPago.Clave})-{lastPago.Week1}");
            table.AddCell(lastPago.Cantidad.ToString("C", culture));
            if (lastPago.Resto > 0)
            {
                table.AddCell(lastPago.Resto.ToString("C", culture));
            }
            document.Add(table);

            Paragraph p = new Paragraph(new Chunk(lastPago.Resto > 0 ? lastPago.Resto.NumeroALetras() : lastPago.Cantidad.NumeroALetras(), GeneralFonts.fontTitleBold()));
            p.Alignment = Element.ALIGN_LEFT;
            p.SpacingBefore = 5;
            document.Add(p);
            // Close the document  
            document.Close();
            // Close the writer instance  

            writer.Close();
            // Always close open filehandles explicity  
            fs.Close();

            System.Diagnostics.Process.Start(filepath);


        }
    }
}
