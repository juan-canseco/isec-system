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

namespace ISEC.Alumnos
{
    public partial class frmActualizarAlumno : Form
    {
        AlumnoLocalRepository alumnoLocalRepository = new AlumnoLocalRepository();
        AlumnoLocal alumnoLocal = null;
        GrupoLocalRepository grupoLocalRepository = new GrupoLocalRepository();
        PlanLocalRepository planLocalRepository = new PlanLocalRepository();
        CarreraLocalRepository carreraLocalRepository = new CarreraLocalRepository();
        CuotaLocalRepository cuotaLocalRepository = new CuotaLocalRepository();
        public Action RefreshAll { get; set; }
        public frmActualizarAlumno(AlumnoLocal _alumnoLocal)
        {
            InitializeComponent();
            ReloadCarreras();
            ReloadCuotas();
            ReloadGrupos();
            ReloadPlanes();
            alumnoLocal = _alumnoLocal;
            txtCredencial.Text = alumnoLocal.Credencial;
            txtSemana.Text= alumnoLocal.Semana.ToString();
            txtNombre.Text = alumnoLocal.Nombre;
            cbCarrera.SelectedValue = alumnoLocal.CarreraId;
            cbCuota.SelectedValue = alumnoLocal.CuotaId;
            cbGrupo.SelectedValue = alumnoLocal.FkGrupo;
            cbPlanes.SelectedValue = alumnoLocal.FkPlan;
            chBaja.Checked = alumnoLocal.Baja > 0 ? true : false;
            chReingreso.Checked = alumnoLocal.Reingreso > 0 ? true : false;
            chActivo.Checked = alumnoLocal.EsActivo > 0 ? true : false;
            txtConsejero.Text = UserSession.Instancia.Usuario.Username;
            dt.Value = DateTime.Parse(alumnoLocal.FechaIngreso);
            txtDireccion.Text = alumnoLocal.Direccion;
            txtCelular.Text = alumnoLocal.Telefono;
            lblBaja.Text = chBaja.Checked ? alumnoLocal.FechaBaja : String.Empty;
            lblReingreso.Text = chReingreso.Checked ? alumnoLocal.FechaReingreso : String.Empty;
        }
        public void ReloadCarreras()
        {
            cbCarrera.DataSource = carreraLocalRepository.GetAll();
            cbCarrera.ValueMember = "Id";
            cbCarrera.DisplayMember = "Nombre";
        }
        public void ReloadCuotas()
        {
            cbCuota.DataSource = cuotaLocalRepository.GetAll();
            cbCuota.ValueMember = "Id";
            cbCuota.DisplayMember = "Descripcion";
        }
        public void ReloadGrupos()
        {
            cbGrupo.DataSource = grupoLocalRepository.GetAll();
            cbGrupo.DisplayMember = "Grupo";
            cbGrupo.ValueMember = "Id";
        }
        public void ReloadPlanes()
        {
            cbPlanes.DataSource = planLocalRepository.GetAll();
            cbPlanes.DisplayMember = "Descripcion";
            cbPlanes.ValueMember = "Id";
        }
        private void frmActualizarAlumno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Escape)
            {
                this.Hide();
            }
        } 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        } 
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            //try
            //{
                int fkgrupo = int.Parse(cbGrupo.SelectedValue.ToString());
                int fkplan = int.Parse(cbPlanes.SelectedValue.ToString());
                int fkcarrera = int.Parse(cbCarrera.SelectedValue.ToString());
                int fkcuota = int.Parse(cbCuota.SelectedValue.ToString());
                var alumnoo = new AlumnoLocal()
                {
                    Id=  alumnoLocal.Id,
                    Credencial = txtCredencial.Text,
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    CarreraId = fkcarrera,
                    CuotaId = fkcuota,
                    FechaIngreso = dt.Value.Date.ToShortDateString(),
                    Consejero = txtConsejero.Text,
                    Telefono = txtCelular.Text,
                    Semana = Convert.ToInt32(txtSemana.Text),
                    EsActivo = chActivo.Checked ? 1 : 0,
                    Usuario = txtConsejero.Text,
                    Baja = chBaja.Checked ? 1 : 0,
                    FechaBaja = chBaja.Checked ? DateTime.Now.ToString() : null,
                    Reingreso = chReingreso.Checked ? 1 : 0,
                    FechaReingreso = chReingreso.Checked ? DateTime.Now.ToString() : null,
                    FkGrupo = fkgrupo,
                    FkPlan = fkplan
                };
                bool inserted = alumnoLocalRepository.Update(alumnoo);
                if (inserted)
                {
                    MessageBox.Show("Se ha actualizado alumno correctamente!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                    this.Hide(); 
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un problema, no se ha logrado insertar al alumno", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            //}
            //catch (Exception ex)
            //{   
            //    MessageBox.Show($"Error: {ex.GetBaseException().Message}", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        } 
        private void cbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var grupo = cbGrupo.SelectedItem as GrupoLocal;
            if (grupo!= null)
            {
                if (grupo.HorarioObj != null)
                {
                    txtHorario.Text = string.Format("{0} ({1}-{2})", String.Format("{0}", grupo.HorarioObj.Descripcion), grupo.HorarioObj.Inicio, grupo.HorarioObj.Fin);
                }
            }
           
        }

        private void cbCuota_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCuota.SelectedValue != null)
            {
                var cuota = cbCuota.SelectedItem as CuotaLocal;
                txtColegiatura.Text = cuota.Colegiatura.ToString();
            }
        }
    }
}
