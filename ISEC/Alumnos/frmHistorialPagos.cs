using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
using ISEC.Recibos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISEC.Alumnos
{
    public partial class frmHistorialPagos : Form
    {
        List<PagoLocalViewModel> pagos;
        AlumnoLocal alumnoLocal = null;
        CobranzaDetalleLocalRepository cobranzaDetalleLocalRepository = new CobranzaDetalleLocalRepository();
       
        public frmHistorialPagos(List<PagoLocalViewModel> _pagos, AlumnoLocal _alumno)
        {
            InitializeComponent();
            pagos = _pagos;
            alumnoLocal = _alumno;
            txtCredencial.Text = alumnoLocal.Credencial;
            txtCarrera.Text = _pagos[0].Carrera;
            txtNombre.Text = alumnoLocal.Nombre;
            txtFechaIngreso.Text = alumnoLocal.FechaIngreso;
            txtSemanas.Text = cobranzaDetalleLocalRepository.GetWeekForPay(alumnoLocal.Id).ToString();
            Reload();
        }
        public void Reload()
        {
            gvHistorial.DataSource = pagos;
            gvHistorial.Columns["Credencial"].Visible = false;
            gvHistorial.Columns["Nombre"].Visible = false;
            gvHistorial.Columns["Direccion"].Visible = false;
            gvHistorial.Columns["FkAlumno"].Visible = false;
            gvHistorial.Columns["Clave"].Visible = false;
            gvHistorial.Columns["Cuota"].Visible = false;
            gvHistorial.Columns["Cantidad"].Visible = false;

        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                int j = 0, i = 0;

                //Write Headers
                for (j = 0; j < gvHistorial.Columns.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = gvHistorial.Columns[j].HeaderText;
                }

                StartRow++;

                //Write datagridview content
                for (i = 0; i < gvHistorial.Rows.Count; i++)
                {
                    for (j = 0; j < gvHistorial.Columns.Count; j++)
                    {
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = gvHistorial[j, i].Value == null ? "" : gvHistorial[j, i].Value;
                        }
                        catch
                        {
                            ;
                        }
                    }
                }


                //    workbook.SaveAs(@"\\3.19.229.180\Files\" + DateTime.Now.ToString() + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                //false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                //Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //    workbook.Close();
                excel.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gvHistorial_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var pago = gvHistorial.Rows[e.RowIndex].DataBoundItem as PagoLocalViewModel;
            if(pago != null)
            {
                ReciboGeneral.Create(pago); 
            }
        }
    }
}
