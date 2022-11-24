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

namespace ISEC.Carreras
{
    public partial class frmCarreras : Form
    {
        CarreraLocalRepository carreraLocalRepository = new CarreraLocalRepository();
        public frmCarreras()
        {
            InitializeComponent();
            Reload();
        }
        public void Reload()
        {
            gvCarreras.DataSource = carreraLocalRepository.GetAll();
            gvCarreras.Columns["Id"].Visible = false;
        } 
        private void gvCarreras_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var carrera = gvCarreras.Rows[e.RowIndex].DataBoundItem as CarreraLocal;
            if (carrera != null)
            {
                frmActualizarCarrera up = new frmActualizarCarrera(carrera);
                up.RefreshAll = Reload;
                up.ShowDialog();
            }
        } 
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAgregarCarrera add = new frmAgregarCarrera();
            add.RefreshAll = Reload;
            add.ShowDialog();
        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            var data = carreraLocalRepository.GetAll();
            if (!string.IsNullOrEmpty(txtFiltro.Text))
            {
                string texto = txtFiltro.Text.ToUpper();
                data = data.FindAll(s => s.Nombre.ToString().ToUpper().Contains(texto));
                gvCarreras.DataSource = data;
            }
            else
            {
                gvCarreras.DataSource = carreraLocalRepository.GetAll();
            }
        }
    }
}
