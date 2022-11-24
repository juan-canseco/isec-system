using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
using ISEC.DbLocal.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISEC.Puestos
{
    public partial class frmPuestos : Form
    {

        PuestoLocalRepository puestoLocalRepository = new PuestoLocalRepository(); 
        public frmPuestos()
        {
            InitializeComponent();
            Reload();
        }
        public void Reload()
        {
            GenericService.Load(gvPuestos, puestoLocalRepository.GetAll(), "", "");
            gvPuestos.Columns["Id"].Visible = false;
        } 
        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            var data = puestoLocalRepository.GetAll();
            if (!string.IsNullOrEmpty(txtFiltro.Text))
            {
                string texto = txtFiltro.Text.ToUpper();
                data = data.FindAll(s => s.Descripcion.ToString().ToUpper().Contains(texto));
                gvPuestos.DataSource = data;
            }
            else
            {
                gvPuestos.DataSource = puestoLocalRepository.GetAll();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAgregarPuesto p = new frmAgregarPuesto();
            p.RefreshAll = Reload;
            p.ShowDialog();
        }

        private void gvPuestos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
             if (e.RowIndex != -1)
            {
                var selected = gvPuestos.Rows[e.RowIndex].DataBoundItem as PuestoLocal;
                if (selected != null)
                {
                    frmActualizarPuesto up = new frmActualizarPuesto(selected);
                    up.RefreshAll = Reload;
                    up.ShowDialog(); 
                }
            }
        }
    }
}
