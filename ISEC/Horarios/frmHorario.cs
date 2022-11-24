using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
using ISEC.Horarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISEC
{
    public partial class frmHorario : Form
    {
        HorarioLocalRepository horarioLocalRepository = new HorarioLocalRepository(); 
        public frmHorario()
        {
            InitializeComponent(); 
            Reload(); 
        }
        public void Reload()
        {
            gvHorarios.DataSource = horarioLocalRepository.GetAll();
            this.gvHorarios.Columns[0].Visible = false;
            this.gvHorarios.Columns[9].Visible = false;
            this.gvHorarios.Columns[12].Visible = false;
            this.gvHorarios.Columns[13].Visible = false;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAgregarHorario frmAddHorario = new frmAgregarHorario();
            frmAddHorario.RefreshAll = Reload;
            frmAddHorario.ShowDialog();
        }

        private void gvHorarios_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var selectedHorario = gvHorarios.Rows[e.RowIndex].DataBoundItem as HorarioLocal;
            if (selectedHorario!= null)
            {
                frmActualizarHorario frmActualizarHorario = new frmActualizarHorario(selectedHorario);
                frmActualizarHorario.RefreshAll = Reload;
                frmActualizarHorario.ShowDialog();
            }
        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            var data = horarioLocalRepository.GetAll();
            if (!string.IsNullOrEmpty(txtFiltro.Text))
            {
                string texto = txtFiltro.Text.ToUpper();
                data = data.FindAll(s => s.Descripcion.ToUpper().Contains(texto) || s.Inicio.ToUpper().Contains(texto)
                || s.Fin.ToUpper().Contains(texto) || s.HoraFinal.ToString().ToUpper().Contains(texto));
                gvHorarios.DataSource = data;
            }
            else
            {
                gvHorarios.DataSource = horarioLocalRepository.GetAll();
            }
        }
    }
}
