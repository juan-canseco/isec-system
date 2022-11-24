using ISEC.Carreras;
using ISEC.Claves;
using ISEC.Cuotas;
using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
using ISEC.Grupos;
using ISEC.Modelos;
using ISEC.Planes;
using ISEC.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using ISEC.Puestos;
using ISEC.Gastos; 
using System.IO;
using ISEC.Arqueo;

namespace ISEC
{
    public partial class frmMenu : Form
    {
        //Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        UsuarioLocal usuario = null;

        RolUsuarioLocalRepository rolUsuarioLocal = new RolUsuarioLocalRepository();
        AccesoUsuarioLocalRepository accesoUsuarioLocalRepository = new AccesoUsuarioLocalRepository();
        ModuloLocalRepository moduloLocalRepository = new ModuloLocalRepository();
        List<UsuarioViewModel> modulosUser = null;
        List<UsuarioViewModel> vms = new List<UsuarioViewModel>();
        CobranzaLocalRepository cobranza = new CobranzaLocalRepository();
        private string path = @"C:\isec\";
        public Action Method { get; set; }
        public Action RefreshAll { get; set; }
        string estado = string.Empty;
        public frmMenu(UsuarioLocal _usuario)
        {
            InitializeComponent();

            if (Directory.Exists(path))
            {
                string origen = Path.Combine(path, "kardex1.sqlite.db");
                string destino = Path.Combine(path, "kardex1Sync.sqlite.db");
                if (!File.Exists(destino))
                {
                    File.Copy(origen, destino); 
                }
                else
                {
                    File.Delete(destino);
                    File.Copy(origen,destino); 
                }
            }


            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
            var isconnected = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (isconnected)
            {
                rbEstado.Text = "Online";

            }
            else
            {
                rbEstado.Text = "Offline";

            }
            random = new Random();
            usuario = _usuario;
            lblFecha.Text = $"{DateTime.Now.ToLongDateString()} Semana:{Functions.Week(DateTime.Now).ToString()}";
            //lblTitle.Text = $"Bienvenid@: {usuario.Nombre}";
            //lblTituloProgreso.Text = "sincronizando usuarios...";
            if (UserSession.Instancia.Usuario != null)
            {
                if (rolUsuarioLocal.Get(UserSession.Instancia.Usuario.Id).Count >0)
                {
                    var rol = rolUsuarioLocal.Get(UserSession.Instancia.Usuario.Id)[0];
                    var modulos = accesoUsuarioLocalRepository.GetAllByRol(rol.RolId);
                    if (modulos.Count > 0)
                    {
                        foreach (var item in modulos)
                        {
                            var mod = moduloLocalRepository.Get(item.ModuloId);
                            UsuarioViewModel vm = new UsuarioViewModel();
                            vm.ModuloId = item.ModuloId;
                            vm.RolId = item.RolId;
                            vm.Lectura = item.Lectura ? 1 : 0;
                            vm.Escritura = item.Escritura ? 1 : 0;
                            vm.Acceso = item.Acceso ? 1 : 0;
                            vm.UsuarioId = rol.UsuarioId;
                            vm.Modulo = mod.Nombre;
                            vms.Add(vm);

                        }
                    }
                }
                
                if (vms.Count > 0)
                {
                    var moduloss = vms.Select(r => r.Modulo);
                    var buttons = panelMenu.Controls.OfType<Button>().Select(ss => ss.Text);
                    var distinct = buttons.Except(moduloss);
                    foreach (var item in distinct)
                    {
                        string btnName = string.Format("btn{0}", item);
                        var btn = panelMenu.Controls[btnName];
                        //btn.Enabled = false;
                        btn.Visible = false;
                    }
                    UserSession.Instancia.Usuario.Modulos = new List<UsuarioViewModel>();
                    foreach (var v in vms)
                    {
                        UserSession.Instancia.Usuario.Modulos.Add(v);
                        string btnName = string.Format("btn{0}", v.Modulo);
                        var btn = panelMenu.Controls[btnName];
                        if (v.Acceso > 0)
                        {
                            btn.Enabled = true;
                            //btn.Visible = true;
                        }
                        else
                        {
                            btn.Enabled = false;
                            //btn.Visible = false;
                        }
                    }
                }
                lblUser.Text = $"Bienvenid@\n{UserSession.Instancia.Usuario.Nombre}";

                //SI ES MAYOR EXISTEN NUEVOS 
                int count = panelMenu.Controls.Count-1;
                if (count > 0)
                {
                    if (count > moduloLocalRepository.GetAll().Count)
                    {
                        SaveRoles();
                    }
                }
                else
                {
                    SaveRoles();
                }

            }
            else
            {
                lblUser.Text = "";
            }
            modulosUser = UserSession.Instancia.Usuario.Modulos != null ? UserSession.Instancia.Usuario.Modulos : null;

            ReloadSaldo();

        }
        public void ReloadSaldo()
        {
            var caja = cobranza.GetLastCobranza();
            if (caja != null)
            {
                lblSaldoCaja.Text = $"Saldo Caja: ${caja.SaldoCaja.ToString("n2")}";
                UserSession.Instancia.Cobranza = caja;
            }
        }
        public void SaveRoles()
        { 
            foreach (var item in panelMenu.Controls)
            {
                var btn = item as Button;
                if (btn != null)
                {
                    if (!moduloLocalRepository.Exists(btn.Text))
                    {
                        moduloLocalRepository.Add(new ModuloLocal() { Nombre = btn.Text });
                    }
                }
            }
        }

        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
            {
                estado = "Online"; 
            }
            else
            {
                estado = "Offline";
            }
        }


        //Methods
        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                } 
            }
        }
        public void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            if (this != childForm)
            {
                panelDesktop.Controls.Add(childForm);
                panelDesktop.Tag = childForm;
            }
          
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;

        }
        private void DisableButton()
        {
            foreach (Control previoudBtn in panelMenu.Controls)
            {
                if (previoudBtn.GetType() == typeof(Button))
                {
                    previoudBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previoudBtn.ForeColor = Color.Gainsboro;
                    previoudBtn.Font = new System.Drawing.Font("Microsoft Sans Serif",13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void timerHora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
        private void bgGlobal_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 10; i++)
            {
                if (bgGlobal.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                Thread.Sleep(100);
                bgGlobal.ReportProgress(i * 10);
            }
            Method();
        }
        private void bgGlobal_ProgressChanged(object sender, ProgressChangedEventArgs e)
        { 
            lblTituloProgreso.Text = "por favor espere...";
            progressBarProceso.Value = e.ProgressPercentage; 
        }
        private void bgGlobal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                lblTituloProgreso.Text = "El proceso se ha cancelado";
            }
            else if (e.Error != null)
            {
                lblTituloProgreso.Text = $"Error:{e.Error.Message}";
            }
            else
            {
                lblTituloProgreso.Text = $"Se han sincronizado correctamente!";
                RefreshAll();
                progressBarProceso.Value = 0;
            }
        }

        private void btnInscripciones_Click(object sender, EventArgs e)
        {
            UsuarioViewModel vm = new UsuarioViewModel();
            if (modulosUser != null)
            {
                vm = modulosUser.Where(s => s.Modulo == "Inscripción").FirstOrDefault();
            }
            OpenChildForm(new frmAlumnos(this,vm), sender);
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            UsuarioViewModel vm = new UsuarioViewModel();
            if (modulosUser != null)
            {
                vm = modulosUser.Where(s => s.Modulo == "Usuarios").FirstOrDefault();
            }
            frmUsuarios usfrm = new frmUsuarios(this, vm);
            OpenChildForm(usfrm, sender);
        }
        private void btnHorarios_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHorario(), sender);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }
        private void btnGrupos_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmGrupos(), sender);
        }
        private void btnPlanes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmPlanes(), sender);
        }
        private void tbnCuotas_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmCuotas(), sender);
        }

        private void btnCarreras_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmCarreras(), sender);
        }

        private void btnClaves_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmClaves(), sender);
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            UsuarioViewModel vm = new UsuarioViewModel();
            if (modulosUser != null)
            {
                vm = modulosUser.Where(s => s.Modulo == "Roles").FirstOrDefault();
            }
            OpenChildForm(new frmRoles(this,vm), sender);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            UserSession.Instancia.Usuario = null;
            this.Close();
            Form1 login = new Form1();
            login.Show();
        }

        private void btnPuestos_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmPuestos(), sender); 
        }

        private void btnGastos_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmGastos(this), sender);
        } 
        private void btnArqueo_Click(object sender, EventArgs e)
        { 
            OpenChildForm(new frmArqueos(), sender);
        } 
        private void btnHome_Click(object sender, EventArgs e)
        { 
            OpenChildForm(this,sender);
        }
    }
}
