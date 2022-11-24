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

namespace ISEC.Claves
{
    public partial class frmClaves : Form
    {
        ClaveLocalRepository  claveLocalRepository = new ClaveLocalRepository();
        public frmClaves()
        {
            InitializeComponent();
            Reload();
        }
        public void Reload()
        {
            gvClaves.DataSource = claveLocalRepository.GetAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAgregarClave addClave = new frmAgregarClave();
            addClave.RefreshAll = Reload;
            addClave.ShowDialog();
        }

        private void gvClaves_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var clave = gvClaves.Rows[e.RowIndex].DataBoundItem as ClaveLocal;
            if (clave != null)
            {
                MessageBox.Show(clave.Clave);
                frmActualizarClave up = new frmActualizarClave(clave);
                up.RefreshAll = Reload;
                up.ShowDialog();
            }
        }
    }
}
