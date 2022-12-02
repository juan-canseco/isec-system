using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISEC.Arqueo
{
    public partial class frmAgregarArqueo : Form
    {
        private readonly frmMenu _menu;
        private readonly Action _reloadArqueos;
        private readonly CobranzaLocal _cobranzaLocal;
        private readonly UsuarioLocalRepository _usuarioLocalRepository;
        private readonly GastoLocalRepository _gastoLocalRepository;
        private readonly ArqueoLocalRepository _arqueoLocalRepository;
        private decimal _subtotalEfectivo = 0;
        private decimal _totalEfectivo = 0;
        private decimal _quedaEfectivo = 0;
        private decimal _retiroEfectivo = 0;
        private decimal _fondoCaja;
    

        private readonly UsuarioLocal _usuarioLocal;
        public frmAgregarArqueo(frmMenu menu, Action reloadArqueos)
        {
            InitializeComponent();
            _reloadArqueos = reloadArqueos;
            _menu = menu;
            _usuarioLocalRepository = new UsuarioLocalRepository();
            _cobranzaLocal = UserSession.Instancia.Cobranza;
            _usuarioLocal = UserSession.Instancia.Usuario;
            _gastoLocalRepository = new GastoLocalRepository();
            _arqueoLocalRepository = new ArqueoLocalRepository();
        }

        private void Reload()
        {
            var usuarioCobranza = _usuarioLocalRepository.GetByUsername(_cobranzaLocal.Username);
            lblUsuarioCobranza.Text = usuarioCobranza.Nombre.ToUpper();
            lblCobranzaImporte.Text = "$" + _cobranzaLocal.SaldoCaja.ToString("n2");
            lblSecretaria.Text = _usuarioLocal.Nombre.ToUpper(); ;
            lblGastos.Text = "$" +_gastoLocalRepository.getGastoTotalByCobranza(_cobranzaLocal.Id).ToString("n2");
            gvGastos.DataSource = null;
            gvGastos.DataSource = _gastoLocalRepository.GetAllByCobranza(_cobranzaLocal.Id);
        }

        private void frmAgregarArqueo_Load(object sender, EventArgs e)
        {
            Reload();
            Sum();
        }

        private ArqueoLocal BuildArqueo()
        {
            return new ArqueoLocal
            {
                Folio = _arqueoLocalRepository.GenerateFolio(),
                FkUsuario = _usuarioLocal.Id,
                FkCobranza = _cobranzaLocal.Id,
                NM10C = (int)txtCantidadP10.Value,
                NM20C = (int)txtCantidadP20.Value,
                NM50C = (int)txtCantidadP50.Value,
                NM1P = (int)txtCantidad1Peso.Value,
                NM2P = (int)txtCantidad2Pesos.Value,
                NM5P = (int)txtCantidad5Pesos.Value,
                NM10P = (int)txtCantidad10Pesos.Value,
                NB20P = (int)txtBillete20.Value,
                NB50P = (int)txtBillete50.Value,
                NB100P = (int)txtBillete100.Value,
                NB200P = (int)txtBillete200.Value,
                NB500P = (int)txtBillete500.Value,
                NB1000P = (int)txtBillete1000.Value,
                RetiroEfectivo = _retiroEfectivo,
                FondoEnCaja = _fondoCaja,
                SubtotalEfectivo = _subtotalEfectivo,
                TotalEfectivo = _totalEfectivo,
                FechaCreacion = DateTime.Now
            };
        }

        private void btnAddArqueo_Click(object sender, EventArgs e)
        {
            // Retiro efectivo cuando dinero vas a retirar
            // Fondo de caja, cuanto dinero quieres dejar en la caja 



        }

        #region NumericUpDown Quantity Updates
        public void Sum()
        {
            var sum = (decimal)0.10 * txtCantidadP10.Value
                + (decimal)0.20 * txtCantidadP20.Value
                + (decimal)0.50 * txtCantidadP50.Value
                + (decimal)1.00 * txtCantidad1Peso.Value
                + (decimal)2.00 * txtCantidad2Pesos.Value
                + (decimal)5.00 * txtCantidad5Pesos.Value
                + (decimal)10.00 * txtCantidad10Pesos.Value
                + (decimal)20.00 * txtBillete20.Value
                + (decimal)50.00 * txtBillete50.Value
                + (decimal)100.00 * txtBillete100.Value
                + (decimal)200.00 * txtBillete200.Value
                + (decimal)500.00 * txtBillete500.Value
                + (decimal)1000.00 * txtBillete1000.Value;

            _subtotalEfectivo = sum;

            var retirofEfectivoParsed = decimal.TryParse(txtRetiroEfecrtivo.Text, out _retiroEfectivo);
            if (!retirofEfectivoParsed)
            {
                _retiroEfectivo = 0;
            }

            var fondoCajaParsed = decimal.TryParse(txtFondoCaja.Text, out _fondoCaja);
            if (!fondoCajaParsed)
            {
                _fondoCaja = 0;
            }

            _totalEfectivo = _subtotalEfectivo - (_retiroEfectivo + _fondoCaja);
            _quedaEfectivo = _totalEfectivo;

            lblTotalEfectivo.Text = "$" + _totalEfectivo.ToString("n2");
            lblSubtotalEfectivo.Text ="$"+ sum.ToString("n2");
            lblQuedaEfectivo.Text = "$" + _quedaEfectivo.ToString("n2");
            lblSubtotal.Text = "$" + _quedaEfectivo.ToString("n2");
        }
        private void UpdateQuantity(decimal moneyValue, NumericUpDown upDown, TextBox textBox)
        {
            if (upDown.Value > 0)
            {
                var total = moneyValue * upDown.Value;
                textBox.Text = "$"+total.ToString("n2");
            }
            else
            {
                textBox.Text = "";
            }
            Sum();
        }

        private void txtCantidadP10_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(0.10m, txtCantidadP10, txtImporteMP1);
        }

        private void txtCantidadP20_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(0.20m, txtCantidadP20, txtImporteMP2);
        }

        private void txtCantidadP50_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(0.50m, txtCantidadP50, txtImporteMP5);
        }

        private void txtCantidad1Peso_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(1.0m, txtCantidad1Peso, txtImporteM1);
        }

        private void txtCantidad2Pesos_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(2.0m, txtCantidad2Pesos, txtImporteM2);
        }

        private void txtCantidad5Pesos_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(5.0m, txtCantidad5Pesos, txtImporteM5);
        }

        private void txtCantidad10Pesos_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(10.0m, txtCantidad10Pesos, txtImporteM10);
        }

        private void txtBillete20_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(20.0m, txtBillete20, txtImporteB20);
        }

        private void txtBillete50_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(50.0m, txtBillete50, txtImporteB50);
        }

        private void txtBillete100_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(100.0m, txtBillete100, txtImporteB100);
        }

        private void txtBillete200_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(200.0m, txtBillete200, txtImporteB200);
        }

        private void txtBillete500_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(500.0m, txtBillete500, txtImporteB500);
        }

        private void txtBillete1000_ValueChanged(object sender, EventArgs e)
        {
            UpdateQuantity(1000.0m, txtBillete1000, txtImporteB1000);
        }

        #endregion

        private void txtRetiroEfecrtivo_TextChanged(object sender, EventArgs e)
        {
            Sum();
        }

        private void txtFondoCaja_TextChanged(object sender, EventArgs e)
        {
            Sum();
        }

        private void ValidateNumbersInput(object sender, KeyPressEventArgs e)
        {
            string senderText = (sender as TextBox).Text;
            string[] splitByDecimal = senderText.Split('.');
            int cursorPosition = (sender as TextBox).SelectionStart;

            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }


            if (e.KeyChar == '.'
                && senderText.IndexOf('.') > -1)
            {
                e.Handled = true;
            }


            if (!char.IsControl(e.KeyChar)
                && senderText.IndexOf('.') < cursorPosition
                && splitByDecimal.Length > 1
                && splitByDecimal[1].Length == 2)
            {
                e.Handled = true;
            }
        }

        private void txtRetiroEfecrtivo_KeyPress(object sender, KeyPressEventArgs e)
        {
           ValidateNumbersInput(sender, e);
        }

        private void txtFondoCaja_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidateNumbersInput(sender, e);
        }
    }
}
