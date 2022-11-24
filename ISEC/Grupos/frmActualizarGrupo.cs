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
    public partial class frmActualizarGrupo : Form
    {
        HorarioLocalRepository horarioLocalRepository = new HorarioLocalRepository();
        GrupoLocalRepository grupoLocalRepository = new GrupoLocalRepository();
        public Action RefreshAll { get; set; }
        GrupoLocal grupo = null;
        public frmActualizarGrupo(GrupoLocal _grupo)
        {
            InitializeComponent();
            ReloadHorarios();
            this.grupo = _grupo;
            txtNumero.Text = this.grupo.Numero.ToString();
            txtLetra.Text = this.grupo.Letra;
            chActivo.Checked = this.grupo.EsActivo>0 ? true : false;
            if (this.grupo.FkHorario>0)
            {
                cbHorario.SelectedValue =  this.grupo.FkHorario;
            }
        }
        public void ReloadHorarios()
        {
            cbHorario.DataSource = horarioLocalRepository.GetAll();
            cbHorario.DisplayMember = "Descripcion";
            cbHorario.ValueMember = "Id";
        } 
        private void btnAddHorario_Click(object sender, EventArgs e)
        {
            frmAgregarHorario addHorario = new frmAgregarHorario();
            addHorario.RefreshAll = ReloadHorarios;
            addHorario.ShowDialog();
        } 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumero.Text))
            {
                MessageBox.Show($"Ingrese un número", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtLetra.Text))
            {
                MessageBox.Show($"Ingrese una letra", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                var idhorario = int.Parse(cbHorario.SelectedValue.ToString());
                GrupoLocal grupoLocal = new GrupoLocal()
                {
                    Id = this.grupo.Id,
                    Numero = int.Parse(txtNumero.Text),
                    Letra = txtLetra.Text,
                    FkHorario = idhorario
                };
                bool inserted = grupoLocalRepository.Update(grupoLocal);
                if (inserted)
                {
                    MessageBox.Show($"Se ha actualizado grupo {string.Format("{0} {1}", grupoLocal.Numero, grupoLocal.Letra)} correctamente!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"No se logro actualizar grupo", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
