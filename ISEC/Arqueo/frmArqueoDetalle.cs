using DataAccess.Local;
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

namespace ISEC.Arqueo
{
    public partial class frmArqueoDetalle : Form
    {

        private readonly int _arqueoId;
        private ArqueoLocal _arqueo;
        private CobranzaLocal _cobranzaLocal;
        private UsuarioLocal _usuarioLocal;
        private readonly UsuarioLocalRepository _usuarioLocalRepository;
        private readonly CobranzaLocalRepository _cobranzaLocalRepository;
        private readonly GastoLocalRepository _gastoLocalRepository;
        private readonly ArqueoLocalRepository _arqueoRepository;

        public frmArqueoDetalle(int arqueoId)
        {
            InitializeComponent();
            _arqueoId = arqueoId;
            _usuarioLocalRepository = new UsuarioLocalRepository();
            _cobranzaLocalRepository = new CobranzaLocalRepository();
            _gastoLocalRepository = new GastoLocalRepository();
            _arqueoRepository = new ArqueoLocalRepository();
        }

        private void frmArqueoDetalle_Load(object sender, EventArgs e)
        {
            _arqueo = _arqueoRepository.Get(_arqueoId);
            _cobranzaLocal = _cobranzaLocalRepository.Get(_arqueo.FkCobranza);
            _usuarioLocal = _usuarioLocalRepository.Get(_arqueo.FkUsuario);

            lblFolio.Text = _arqueo.Folio.ToString();

            txtCantidadP10.Value = _arqueo.NM10C;
            txtCantidadP20.Value = _arqueo.NM20C;
            txtCantidadP50.Value = _arqueo.NM50C;
            txtCantidad1Peso.Value = _arqueo.NM1P;
            txtCantidad2Pesos.Value = _arqueo.NM2P;
            txtCantidad5Pesos.Value = _arqueo.NM5P;
            txtCantidad10Pesos.Value = _arqueo.NM10P;
            txtBillete20.Value = _arqueo.NB20P;
            txtBillete50.Value = _arqueo.NB50P;
            txtBillete100.Value = _arqueo.NB100P;
            txtBillete200.Value = _arqueo.NB200P;
            txtBillete1000.Value = _arqueo.NB1000P;


            SetQuantity(0.10m, txtCantidadP10, txtImporteMP1);
            SetQuantity(0.20m, txtCantidadP20, txtImporteMP2);
            SetQuantity(0.50m, txtCantidadP50, txtImporteMP5);
            SetQuantity(1.0m, txtCantidad1Peso, txtImporteM1);
            SetQuantity(2.0m, txtCantidad2Pesos, txtImporteM2);
            SetQuantity(5.0m, txtCantidad5Pesos, txtImporteM5);
            SetQuantity(10.0m, txtCantidad10Pesos, txtImporteM10);
            SetQuantity(20.0m, txtBillete20, txtImporteB20);
            SetQuantity(50.0m, txtBillete50, txtImporteB50);
            SetQuantity(100.0m, txtBillete100, txtImporteB100);
            SetQuantity(200.0m, txtBillete200, txtImporteB200);
            SetQuantity(500.0m, txtBillete500, txtImporteB500);
            SetQuantity(1000.0m, txtBillete1000, txtImporteB1000);
            Reload();
        }


        private void SetQuantity(decimal moneyValue, NumericUpDown upDown, TextBox textBox)
        {
            var total = moneyValue * upDown.Value;
            textBox.Text = "$" + total.ToString("n2");
        }
        private void Reload()
        {
            var usuarioCobranza = _usuarioLocalRepository.GetByUsername(_cobranzaLocal.Username);
            lblUsuarioCobranza.Text = usuarioCobranza.Nombre.ToUpper();
            lblCobranzaImporte.Text = "$" + _cobranzaLocal.SaldoCaja.ToString("n2");
            lblSecretaria.Text = _usuarioLocal.Nombre.ToUpper(); ;
            lblGastos.Text = "$" + _gastoLocalRepository.getGastoTotalByCobranza(_cobranzaLocal.Id).ToString("n2");
            gvGastos.DataSource = null;
            gvGastos.DataSource = _gastoLocalRepository.GetAllByCobranza(_cobranzaLocal.Id);
             
            lblTotalEfectivo.Text = "$" + _arqueo.TotalEfectivo.ToString("n2");
            lblSubtotalEfectivo.Text = "$" + _arqueo.SubtotalEfectivo.ToString("n2");
            lblSubtotal.Text = "$" + _arqueo.TotalEfectivo.ToString("n2");
            lblSubtotal.Text = "$" + _arqueo.TotalEfectivo.ToString("n2");
            lblRetiroEfectivo.Text = "$" + _arqueo.RetiroEfectivo.ToString("n2");
            lblFondoDeCaja.Text = "$" + _arqueo.FondoEnCaja.ToString("n2");
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
