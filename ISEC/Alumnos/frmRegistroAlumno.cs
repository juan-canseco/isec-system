using ISEC.Modelos; 
using System;
using System.Windows.Forms;
using Zen.Barcode;
using System.Linq;
using ISEC.DbLocal.Repositorios;
using DataAccess.Local;
using ISEC.Grupos;
using ISEC.Planes;
using ISEC.Cuotas;
using ISEC.Carreras;

namespace ISEC
{
    public partial class frmRegistroAlumno : Form
    {
        //CarreraRepository carreraRepository = new CarreraRepository();
        //CuotaRepository cuotaRepository = new CuotaRepository();
        CarreraLocalRepository carreraLocalRepository  = new CarreraLocalRepository();
        CuotaLocalRepository cuotaLocalRepository = new CuotaLocalRepository(); 
        AlumnoLocalRepository alumnoLocalRepository = new AlumnoLocalRepository();
        GrupoLocalRepository grupoLocalRepository = new GrupoLocalRepository();
        PlanLocalRepository planLocalRepository = new PlanLocalRepository();
        public Action RefreshAll { get; set; }

        public Int64 GetLastCredential()
        {
            Int64 result = 0;
            if (alumnoLocalRepository.GetAll().ToList().Count > 0)
            {
                Int64 maxcrential = alumnoLocalRepository.GetAll().ToList().Max(s => Int64.Parse(s.Credencial));
                result = maxcrential + 1;
            }
            else
            {
                Int64 defaultt = 00001;
                result = defaultt;
            }
            return result;
        }
        public frmRegistroAlumno()
        {
            InitializeComponent();
            txtConsejero.Text = UserSession.Instancia.UsuarioAccion.Username;
            txtCredencial.Text = string.Format("{0:D5}", GetLastCredential());
            dt.Value = DateTime.Now;
            Code128BarcodeDraw brCode = BarcodeDrawFactory.Code128WithChecksum;
            pictureBox1.Image = brCode.Draw(txtCredencial.Text, 40); 
            ReloadCarreras();
            ReloadCuotas();
            ReloadGrupos();
            ReloadPlanes();
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
        private void cbCuota_SelectedValueChanged(object sender, EventArgs e)
        {
        }
        private void cbCuota_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCuota.SelectedValue != null)
            {
                var cuota = cbCuota.SelectedItem as CuotaLocal;
                txtColegiatura.Text = cuota.Colegiatura.ToString();
            }
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            var carrera = cbCarrera.SelectedItem as CarreraLocal;
            var cuota = cbCuota.SelectedItem as CuotaLocal;
            int fkgrupo = int.Parse(cbGrupo.SelectedValue.ToString());
            int fkplan = int.Parse(cbPlanes.SelectedValue.ToString());
            var alumnoo = new AlumnoLocal()
            {
                Credencial = txtCredencial.Text,
                Nombre = txtNombre.Text,
                Direccion = txtDireccion.Text,
                CarreraId = carrera.Id,
                CuotaId = cuota.Id,
                FechaIngreso = dt.Value.Date.ToShortDateString(),
                Consejero = txtConsejero.Text,
                Telefono = txtCelular.Text,
                Semana = Convert.ToInt32(txtSemana.Text),
                EsActivo = 1,
                Usuario = UserSession.Instancia.Usuario.Username,
                Baja = 0,
                FechaBaja = null,
                Reingreso = 0,
                FechaReingreso = null,
                FkGrupo = fkgrupo,
                FkPlan = fkplan
            }; 
            bool inserted = alumnoLocalRepository.Add(alumnoo);
            if (inserted)
            {
                MessageBox.Show("Se ha ingresado al alumno correctamente!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAll();
                this.Hide();
                frmPago frmPago = new frmPago(alumnoLocalRepository.GetLast());
                frmPago.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un problema, no se ha logrado insertar al alumno", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dt_ValueChanged(object sender, EventArgs e)
        {
            txtSemana.Text = Functions.Week(dt.Value).ToString();
        } 
        private void cbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var grupo = cbGrupo.SelectedItem as GrupoLocal;
            if (grupo.HorarioObj != null)
            {
                txtHorario.Text = string.Format("{0} ({1}-{2})", String.Format("{0}",grupo.HorarioObj.Descripcion), grupo.HorarioObj.Inicio, grupo.HorarioObj.Fin);
            }
        } 
        private void btnAddGrupo_Click(object sender, EventArgs e)
        {
            frmAgregarGrupo add = new frmAgregarGrupo();
            add.RefreshAll = ReloadGrupos;
            add.ShowDialog();
        } 
        private void btnAddPlan_Click(object sender, EventArgs e)
        {
            frmAgregarPlan addplan = new frmAgregarPlan();
            addplan.RefreshAll = ReloadPlanes;
            addplan.ShowDialog();
        }

        private void btnAddCuota_Click(object sender, EventArgs e)
        {
            frmAgregarCuota addCuota = new frmAgregarCuota();
            addCuota.RefreshAll = ReloadCuotas;
            addCuota.ShowDialog();
        }

        private void btnAddCarrera_Click(object sender, EventArgs e)
        {
            frmAgregarCarrera addCarrera = new frmAgregarCarrera();
            addCarrera.RefreshAll = ReloadCarreras;
            addCarrera.ShowDialog();
        }

        private void frmRegistroAlumno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
