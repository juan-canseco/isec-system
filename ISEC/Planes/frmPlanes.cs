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

namespace ISEC.Planes
{
    public partial class frmPlanes : Form
    {
        PlanLocalRepository planLocalRepository = new PlanLocalRepository();

        public frmPlanes()
        {
            InitializeComponent();
            Reload();
        }
        public void Reload()
        {
            gvPlanes.DataSource = planLocalRepository.GetAll();
            gvPlanes.Columns["Id"].Visible = false;
            gvPlanes.Columns["EsActivo"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAgregarPlan add = new frmAgregarPlan();
            add.RefreshAll = Reload;
            add.ShowDialog();
        }

        private void gvPlanes_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var plan = gvPlanes.Rows[e.RowIndex].DataBoundItem as PlanLocal;
            if (plan != null)
            {
                frmActualizarPlan update = new frmActualizarPlan(plan);
                update.RefreshAll = Reload;
                update.ShowDialog();
            }
        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        { 
            var data = planLocalRepository.GetAll();
            if (!string.IsNullOrEmpty(txtFiltro.Text))
            {
                string texto = txtFiltro.Text.ToUpper();
                data = data.FindAll(s => s.Descripcion.ToString().ToUpper().Contains(texto) || s.Fecha.ToUpper().Contains(texto)
                );
                gvPlanes.DataSource = data;
            }
            else
            {
                gvPlanes.DataSource = planLocalRepository.GetAll();
            }
        }
    }
}
