using ISEC.DbLocal.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess.Local;
namespace ISEC.Gastos
{
    public partial class frmAgregarGasto : Form
    {
        public Action RefreshAll { get; set; }
        private CobranzaLocal cobranza = UserSession.Instancia.Cobranza;
        GastoLocalRepository gastoRepo = new GastoLocalRepository();
        CobranzaLocalRepository cobranzaRepo = new CobranzaLocalRepository();
        ConceptoLocalRepository conceptoRepo = new ConceptoLocalRepository();
        frmMenu menu = null;
        public frmAgregarGasto(frmMenu _menu)
        {
            InitializeComponent();
            this.menu = _menu;
            ReloadConceptos();
        }
        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPrecio.Text) && !string.IsNullOrEmpty(txtCantidad.Text))
            {
                var result = (decimal.Parse(txtPrecio.Text) * int.Parse(txtCantidad.Text));
                txtTotal.Text = result.ToString("n2");
            }
        }
        public void ReloadConceptos()
        {
            cbConcepto.DataSource = conceptoRepo.GetAll();
            cbConcepto.ValueMember = "Id";
            cbConcepto.DisplayMember = "Descripcion";
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (decimal.Parse(txtTotal.Text) > cobranza.SaldoCaja)
            {
                MessageBox.Show("heyy");
                return;
            }
            else
            {
                var gasto = new GastoLocal()
                {
                    Descripcion = txtDescripcion.Text,
                    Precio = decimal.Parse(txtPrecio.Text),
                    Cantidad = int.Parse(txtCantidad.Text),
                    Total = decimal.Parse(txtTotal.Text),
                    Fecha = DateTime.Now.ToString(),
                    FkConcepto = 1,
                    FkCobranza = cobranza.Id
                };
                var inserted = gastoRepo.Add(gasto);
                if (inserted)
                {
                    MessageBox.Show("Se registro gasto correctamente");
                    var resta = cobranza.SaldoCaja - gasto.Total;
                    cobranzaRepo.RestaQuantity(resta,cobranza.Id);
                    this.menu.ReloadSaldo();
                    RefreshAll();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("no se logro insertar gasto");
                }
            }
           
         
        }
    }
}
