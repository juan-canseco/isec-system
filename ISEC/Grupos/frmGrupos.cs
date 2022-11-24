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

namespace ISEC.Grupos
{
    public partial class frmGrupos : Form
    {
        GrupoLocalRepository grupoLocalRepository = new GrupoLocalRepository(); 
        public frmGrupos()
        {
            InitializeComponent();
            Reload();
        } 
        public void Reload()
        {
            gvGrupos.DataSource = grupoLocalRepository.GetAll();
            this.gvGrupos.Columns["Id"].Visible = false;
            this.gvGrupos.Columns["EsActivo"].Visible = false;
            this.gvGrupos.Columns["HorarioObj"].Visible = false;
            this.gvGrupos.Columns["FkHorario"].Visible = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAgregarGrupo ADD = new frmAgregarGrupo();
            ADD.RefreshAll = Reload;
            ADD.ShowDialog();
        }

        private void gvGrupos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var grupo = gvGrupos.Rows[e.RowIndex].DataBoundItem as GrupoLocal;
            if (grupo != null)
            {
                frmActualizarGrupo update = new frmActualizarGrupo(grupo);
                update.RefreshAll = Reload;
                update.ShowDialog();
            }
        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            var data = grupoLocalRepository.GetAll();
            if (!string.IsNullOrEmpty(txtFiltro.Text))
            {
                string texto = txtFiltro.Text.ToUpper();
                data = data.FindAll(s => s.Numero.ToString().ToUpper().Contains(texto) || s.Inicio.ToUpper().Contains(texto)
                || s.Fin.ToUpper().Contains(texto) || s.Letra.ToString().ToUpper().Contains(texto));
                gvGrupos.DataSource = data;
            }
            else
            {
                gvGrupos.DataSource = grupoLocalRepository.GetAll();
            }
        }  
    }
}
