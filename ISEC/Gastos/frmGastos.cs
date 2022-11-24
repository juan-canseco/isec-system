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

namespace ISEC.Gastos
{
    public partial class frmGastos : Form
    {
        private CobranzaLocal cobranza = UserSession.Instancia.Cobranza;
        GastoLocalRepository gastoRepo = new GastoLocalRepository();
        frmMenu menu = null;
        public frmGastos(frmMenu _menu)
        {
            InitializeComponent();
            this.menu = _menu;
            Reload();
        } 
        public void Reload()
        {
            gvGastos.DataSource = null;
            gvGastos.DataSource = gastoRepo.GetAll();
            lblSubtotal.Text = gastoRepo.Suma.ToString("n2");
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAgregarGasto add = new frmAgregarGasto(this.menu);
            add.RefreshAll = Reload;
            add.ShowDialog();
        }
    }
}
