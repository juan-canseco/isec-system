using ISEC.Alumnos;
using DataAccess.Local;
using ISEC.DbLocal.Repositorios; 
using System; 
using System.Drawing; 
using System.Windows.Forms; 
namespace ISEC
{
    public partial class frmAlumnos : Form
    { 
        AlumnoLocalRepository alumnoLocalRepository = new AlumnoLocalRepository();
        public ISEC.frmMenu menu = null;
        UsuarioViewModel usVm = null;
        public frmAlumnos(frmMenu _frmMenu, UsuarioViewModel usuarioViewModel)
        {
            InitializeComponent();
            LoadTheme();
            this.menu = _frmMenu;
            if (usuarioViewModel != null)
            {
                usVm = usuarioViewModel;
                if (usVm.Acceso > 0)
                {
                    if (usVm.Escritura == 0)
                    {
                        btnAgregar.Enabled = false;
                        btnPagos.Enabled = false;
                        gvAlumnos.Enabled = false;
                    }
                    else
                    {
                        btnAgregar.Enabled = true;
                        btnPagos.Enabled = true;
                        gvAlumnos.Enabled = true;
                    }
                }
            }

        } 
        public void ReloadAlumnos()
        {
            gvAlumnos.DataSource = alumnoLocalRepository.GetAll();
            gvAlumnos.Columns["Id"].Visible = false;
            gvAlumnos.Columns["CarreraId"].Visible = false;
            gvAlumnos.Columns["EsActivo"].Visible = false;
            gvAlumnos.Columns["FkGrupo"].Visible = false;
            gvAlumnos.Columns["FkPlan"].Visible = false;
            gvAlumnos.Columns["CuotaId"].Visible = false;
  

        } 
        private void LoadTheme()
        {
            foreach (Control btn in this.Controls)
            {
                if (btn.GetType() == typeof(Button))
                {
                    Button btnn = (Button)btn;
                    btnn.BackColor = ThemeColor.PrimaryColor;
                    btnn.ForeColor = Color.White;
                    btnn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
        }
        private void frmAlumnos_Load(object sender, EventArgs e)
        {
            ReloadAlumnos();
        }  
         
        private void btnAgregar_Click(object sender, EventArgs e)
        {

            frmAccion accion = new frmAccion(false,FormType.Alumnos);
            accion.ReloadAlumnos = ReloadAlumnos;
            accion.ShowDialog();    
        }

        private void btnPagos_Click(object sender, EventArgs e)
        {
            frmAccion acc = new frmAccion(true,FormType.Alumnos);
            acc.ShowDialog();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var alumno = gvAlumnos.Rows[e.RowIndex].DataBoundItem as AlumnoLocal;
                if (alumno != null)
                {
                    frmActualizarAlumno up = new frmActualizarAlumno(alumno);
                    up.RefreshAll = ReloadAlumnos;
                    up.ShowDialog();
                }
            } 
        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            var data = alumnoLocalRepository.GetAll();
            if (!string.IsNullOrEmpty(txtFiltro.Text))
            {
                string texto = txtFiltro.Text.ToUpper();
                data = data.FindAll(s => s.Nombre.ToUpper().Contains(texto) || s.Credencial.ToUpper().Contains(texto)
                || s.Direccion.ToUpper().Contains(texto) || s.Telefono.ToString().ToUpper().Contains(texto));
                gvAlumnos.DataSource = data;
            }
            else
            {
                gvAlumnos.DataSource = alumnoLocalRepository.GetAll();
            }
        }
    }
}
