using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISEC.Arqueo
{
    public partial class frmArqueos : Form
    {
        private GastoLocalRepository gastoRepo = new GastoLocalRepository();
        private ArqueoLocalRepository arqueoLocalRepository  = new ArqueoLocalRepository();
        private CobranzaLocal cobranza = UserSession.Instancia.Cobranza;
        private frmMenu menu;

        public frmArqueos(frmMenu _menu)
        {
            InitializeComponent();
            _menu = menu;
        }

        public void Reload()
        {
            gvArqueos.DataSource = null;
            gvArqueos.DataSource = arqueoLocalRepository.GetAllByFolio(txtFiltro.Text);
            lblTotalEnCaja.Text = cobranza.SaldoCaja.ToString("n2");
            lblGastosTotales.Text = gastoRepo.getGastoTotalByCobranza(UserSession.Instancia.Cobranza.Id).ToString("n2");
        }


        private void ReloadGrid()
        {

            gvArqueos.DataSource = null;
            gvArqueos.DataSource = arqueoLocalRepository.GetAllByFolio(txtFiltro.Text);
        }

        private void frmArqueos_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addForm = new frmAgregarArqueo(menu, Reload);
            addForm.ShowDialog();
        }

        private void txtFiltro_KeyPress(object sender, KeyPressEventArgs e)
        {
            ReloadGrid();
        }

        private void gvArqueos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var selected = gvArqueos.Rows[e.RowIndex].DataBoundItem as ArqueoLocal;
                var arqueoId = selected.Id;

                var arqueoDetalle = new frmArqueoDetalle(arqueoId);
                arqueoDetalle.ShowDialog();
            }
        }
    }
}
