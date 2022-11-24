using ISEC.Converciones;
using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
using ISEC.Modelos;
using ISEC.Recibos;
using ISEC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISEC.Alumnos;

namespace ISEC
{
    public partial class frmPago : Form
    {
        AlumnoLocalRepository alumnoLocalRepository = new AlumnoLocalRepository();
        ClaveLocalRepository claveLocalRepository = new ClaveLocalRepository();
        AlumnoLocal alumnoLocal = null;
        ClaveViewModel clavevm = new ClaveViewModel();
        GrupoLocalRepository grupoLocalRepository = new GrupoLocalRepository();
        CobranzaLocalRepository cobranzaLocalRepository = new CobranzaLocalRepository();
        CobranzaDetalleLocalRepository cobranzaDetalleLocalRepository = new CobranzaDetalleLocalRepository();
        List<SemanaViewModel> semanasvm = new List<SemanaViewModel>();
        int weekStart = 0;
        int weekEnd = 0;
        DateTime dtIngreso;
        DateTime dtWeek2;
        public bool EsInscripcion { get; set; }
        void getLastFolio()
        {
            long foliosum = 0;
            if (!string.IsNullOrEmpty(cobranzaDetalleLocalRepository.Last()))
            {

                foliosum = long.Parse(cobranzaDetalleLocalRepository.Last()) + 1;
            }
            else
            {
                foliosum = 86001;
            }
            txtFactura.Text = foliosum.ToString();
        }
        public frmPago(AlumnoLocal _alumnoLocal)
        {
            InitializeComponent();
            if (_alumnoLocal != null)
            {
                EsInscripcion = true;
                alumnoLocal = _alumnoLocal;
                var grupo = grupoLocalRepository.Get(alumnoLocal.FkGrupo);
                txtCredencial.Text = alumnoLocal.Credencial.ToString();
                txtNombre.Text = alumnoLocal.Nombre;
                txtRecibi.Text = UserSession.Instancia.UsuarioAccion.Nombre;
                txtGrupo.Text = String.Format("{0} {1}", grupo.Numero, grupo.Letra);
                txtHorario.Text = grupo.Horario;
                txtCiudad.Text = "Nogales, sonora.";
                txtSemanas.Text = "I";
                getLastFolio();
            }
            else
            {
                txtCredencial.Enabled = true;
                txtCredencial.Focus();
                txtSemanas.Enabled = true;
                EsInscripcion = false;
            }
        }
        private void txtSemanas_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void ReloadImportes(List<ClaveViewModel> claves)
        {
            gvDetallePago.DataSource = null;
            gvDetallePago.DataSource = claves;
            txtTotal.Text = claves[0].Importe.ToString();
            txtCantidadLetra.Text = claves[0].Importe.NumeroALetras();
            clavevm = claves[0];
        }
        private void txtSemanas_TextChanged(object sender, EventArgs e)
        {
            //&& txtSemanas.Text != "0" && txtSemanas.Text != "I"
            if (!string.IsNullOrEmpty(txtSemanas.Text) )
            {
                btnCobrar.Enabled = true;
                if (EsInscripcion)
                {
                    //PAGO INSCRIPCION
                    if (!string.IsNullOrEmpty(txtSemanas.Text))
                    {
                        if (claveLocalRepository.GetByClave(txtSemanas.Text) != null)
                        {
                            var c = claveLocalRepository.GetByClave(txtSemanas.Text);
                            frmTotal total = new frmTotal(c, null);
                            total.RefreshData = ReloadImportes;
                            total.ShowDialog();
                        }
                        else
                        {
                            gvDetallePago.DataSource = null;
                        }
                    }
                    else
                    {
                        gvDetallePago.DataSource = null;
                    }
                }
                else
                {
                    semanasvm.Clear();
                    int ejem = 0;
                    if (int.TryParse(txtSemanas.Text, out ejem))
                    {
                        //SEMANAS
                        if (claveLocalRepository.GetByClave("S") != null)
                        {
                            var c = claveLocalRepository.GetByClave("S");
                            int quantity = int.Parse(txtSemanas.Text);
                            dtWeek2 = CultureInfo.InvariantCulture.Calendar.AddWeeks(dtIngreso, quantity);
                            //quantity >1 ? quantity-1 : 0)
                            //weekEnd = Functions.Week(CultureInfo.InvariantCulture.Calendar.AddWeeks(dtIngreso,weekStart ));
                            weekEnd = quantity == 1 ? weekStart : (weekStart + quantity)-1;
                            
                            string result = $"Pago semanal {weekStart}-{weekEnd}";
                            semanasvm.Add(new SemanaViewModel()
                            {
                                Cantidad = quantity,
                                Cuota = result,
                                Importe = c.Precio * quantity
                            });
                            txtTotal.Text = semanasvm[0].Importe.ToString();
                            txtCantidadLetra.Text = semanasvm[0].Importe.NumeroALetras();
                            gvDetallePago.DataSource = null;
                            gvDetallePago.DataSource = semanasvm;
                        }
                    }
                    else
                    {
                        //OTRO PAGO
                        if (claveLocalRepository.GetByClave(txtSemanas.Text) != null)
                        {
                            var c = claveLocalRepository.GetByClave(txtSemanas.Text);
                            semanasvm.Add(new SemanaViewModel()
                            {
                                Cantidad = 1,
                                Cuota = c.Cuota,
                                Importe = c.Precio
                            });
                            txtTotal.Text = semanasvm[0].Importe.ToString();
                            txtCantidadLetra.Text = semanasvm[0].Importe.NumeroALetras();
                            gvDetallePago.DataSource = null;
                            gvDetallePago.DataSource = semanasvm;
                        }
                    }

                }
            }
            else
            {
                btnCobrar.Enabled = false;
                semanasvm.Clear();
                gvDetallePago.DataSource = null;
                txtTotal.Text = string.Empty;
                txtCantidadLetra.Text = string.Empty;
            }
        }

        private void frmPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                cobrar();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void cobrar()
        {
            try
            { 
                var pago = new CobranzaDetalleLocal(); 
                pago.FkCobranza = cobranzaLocalRepository.GetLast();
                pago.Cantidad = decimal.Parse(txtTotal.Text);
                pago.Clave = EsInscripcion ? txtSemanas.Text : "S";
                pago.EsEgreso = 0;
                pago.EsIngreso = 1;
                pago.Fecha = DateTime.Now.ToString();
                pago.FkAlumno = alumnoLocal.Id;
                pago.Folio = txtFactura.Text;
                pago.Username = UserSession.Instancia.UsuarioAccion.Username;
                pago.Resto = EsInscripcion ? clavevm.Abono : 0;
                pago.Semanas = EsInscripcion ? 0 : int.Parse(txtSemanas.Text);
                pago.Week1 = EsInscripcion ? 0 : weekStart;
                pago.Week2 = EsInscripcion ? 0 : weekEnd;
                pago.DtWeek1 = EsInscripcion ? null : dtIngreso.ToString();
                pago.DtWeek2 = EsInscripcion ? null : dtWeek2.ToString(); 

                if (EsInscripcion)
                {
                    //inscripcion
                    var pagoI = new CobranzaDetalleLocal()
                    {
                        FkCobranza = cobranzaLocalRepository.GetLast(),
                        Cantidad = decimal.Parse(txtTotal.Text),
                        Clave = txtSemanas.Text,
                        EsEgreso = 0,
                        EsIngreso = 1,
                        Fecha = DateTime.Now.ToString(),
                        FkAlumno = alumnoLocal.Id,
                        Folio = txtFactura.Text,
                        Username = UserSession.Instancia.UsuarioAccion.Username,
                        Resto = decimal.Parse(txtTotal.Text) - clavevm.Abono,
                        Semanas = EsInscripcion ? 0 : int.Parse(txtSemanas.Text)

                    };
                    bool added = cobranzaDetalleLocalRepository.Add(pagoI);
                    if (added)
                    {
                        bool sumIngreso = cobranzaLocalRepository.SumQuantity(pagoI.Resto > 0 ? pagoI.Resto : pagoI.Cantidad);
                        var dialog = MessageBox.Show("Desea imprimir recibo?", "Imprimir recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            var lastPago = cobranzaDetalleLocalRepository.GetLastPago();
                            if (lastPago != null)
                            {
                                ReciboGeneral.Create(lastPago);
                                this.Hide();
                                frmRegistroAlumno add = new frmRegistroAlumno();
                                add.ShowDialog();
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    int ejem = 0;
                    if (int.TryParse(txtSemanas.Text, out ejem))
                    {
                        //MessageBox.Show(JsonConvert.SerializeObject(pago));
                        //semenas
                        bool added = cobranzaDetalleLocalRepository.Add(pago);
                        if (added)
                        {
                            bool sumIngreso = cobranzaLocalRepository.SumQuantity(pago.Resto > 0 ? pago.Resto : pago.Cantidad);
                            var dialog = MessageBox.Show("Desea imprimir recibo?", "Imprimir recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dialog == DialogResult.Yes)
                            {
                                var lastPago = cobranzaDetalleLocalRepository.GetLastPago();
                                if (lastPago != null)
                                {
                                    ReciboGeneral.Create(lastPago);
                                    if (lastPago.Week1>0 && lastPago.Week2 > 0)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        this.Hide();
                                        frmRegistroAlumno add = new frmRegistroAlumno();
                                        add.ShowDialog();
                                    }
                                   
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(JsonConvert.SerializeObject(pago));


                        //otro pago
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCobrar_Click(object sender, EventArgs e)
        {
            cobrar();
        }
        private void gvDetallePago_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var clave = gvDetallePago.Rows[e.RowIndex].DataBoundItem as ClaveViewModel;
            if (clave != null)
            {
                MessageBox.Show(clave.Importe.ToString());
                frmTotal t = new frmTotal(null, clave);
                t.RefreshData = ReloadImportes;
                t.ShowDialog();
            }
        }

        private void txtCredencial_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCredencial.Text))
            {
                var alumno = alumnoLocalRepository.GetByCredencial(txtCredencial.Text);
                if (alumno != null)
                {
                    alumnoLocal = alumno;
                    var grupo = grupoLocalRepository.Get(alumno.FkGrupo);
                    txtCredencial.Text = alumno.Credencial.ToString();
                    txtNombre.Text = alumno.Nombre;
                    txtRecibi.Text = UserSession.Instancia.UsuarioAccion.Nombre;
                    txtGrupo.Text = String.Format("{0} {1}", grupo.Numero, grupo.Letra);
                    txtHorario.Text = grupo.Horario;
                    txtCiudad.Text = "Nogales, sonora.";
                    dtIngreso = DateTime.Parse(alumno.FechaIngreso);
                    var weekIngreso = Functions.Week(DateTime.Parse(alumno.FechaIngreso));
                    var weekActual = Functions.Week(DateTime.Now);
                    var weeksForPay = cobranzaDetalleLocalRepository.GetWeekForPay(alumno.Id);
                    int restWeeks = ((weekActual - weekIngreso) - weeksForPay);
                    weekStart = weekIngreso + weeksForPay;
                    if (restWeeks > 0)
                    {
                        txtSemanas.Enabled = true;
                        lblDebe.ForeColor = Color.IndianRed;
                        string sem = restWeeks > 1 ? "SEMANAS" : "SEMANA";
                        lblDebe.Text = $"INGRESO {dtIngreso.ToShortDateString()} SEM:{weekIngreso} DEBE {restWeeks} {sem} - SEMANA ACTUAL:{weekActual}";
                        gvDetallePago.Enabled = true;
                        getLastFolio();
                        txtSemanas.Text = "1";
                        txtSemanas.Focus();
                        txtSemanas.SelectAll();

                    }
                    //else if ()
                    //{

                    //}
                    else
                    {
                        lblDebe.ForeColor = Color.Black;
                        lblDebe.Text = $"Ingreso sem:{weekIngreso} no debe semanas";
                        txtSemanas.Enabled = false;
                        btnCobrar.Enabled = false;
                        gvDetallePago.DataSource = null;
                        gvDetallePago.Enabled = false;
                        txtTotal.Text = string.Empty;
                        txtCantidadLetra.Text = string.Empty;
                    }

                }
                return;
            }
            else
            {
                alumnoLocal = null;
                txtCredencial.Text = String.Empty;
                txtNombre.Text = String.Empty;
                txtRecibi.Text = String.Empty;
                txtGrupo.Text = String.Empty;
                txtHorario.Text = String.Empty;
                txtCiudad.Text = String.Empty;
                txtFactura.Text = String.Empty;
            }
        }

        private void txtCredencial_TextChanged(object sender, EventArgs e)
        {

        } 
        private void btnHistorialPago_Click(object sender, EventArgs e)
        {
            //Historial de pagos por alumno
            if (alumnoLocal !=null)
            {
                var historialPagos = cobranzaDetalleLocalRepository.HistorialPagosPorAlumno(alumnoLocal.Id);
                if (historialPagos.Count >0)
                {
                    frmHistorialPagos historial = new frmHistorialPagos(historialPagos,alumnoLocal);
                    historial.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("No hay pagos para mostrar del alumno", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
