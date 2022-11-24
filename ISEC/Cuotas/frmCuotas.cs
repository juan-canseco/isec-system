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

namespace ISEC.Cuotas
{
    public partial class frmCuotas : Form
    {
        CuotaLocalRepository cuotaLocalRepository = new CuotaLocalRepository();
        public frmCuotas()
        {
            InitializeComponent();
            Reload();
        }
        public void Reload()
        {
            gvCuotas.DataSource = cuotaLocalRepository.GetAll();
            gvCuotas.Columns["Id"].Visible = false;
        }

        private void gvCuotas_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var cuota = gvCuotas.Rows[e.RowIndex].DataBoundItem as CuotaLocal;
            if (cuota!= null)
            {
                frmActualizarCuota up = new frmActualizarCuota(cuota);
                up.RefreshAll = Reload;
                up.ShowDialog();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAgregarCuota add = new frmAgregarCuota();
            add.RefreshAll = Reload;
            add.ShowDialog();
        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            var data = cuotaLocalRepository.GetAll();
            if (!string.IsNullOrEmpty(txtFiltro.Text))
            {
                string texto = txtFiltro.Text.ToUpper();
                data = data.FindAll(s => s.Descripcion.ToString().ToUpper().Contains(texto) || s.Colegiatura.ToString().ToUpper().Contains(texto)
                );
                gvCuotas.DataSource = data;
            }
            else
            {
                gvCuotas.DataSource = cuotaLocalRepository.GetAll();
            }
        }
    }
}
