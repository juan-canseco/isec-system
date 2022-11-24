using ISEC.DbLocal;
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
using ISEC.API.Users;
using ISEC.API;
using ISEC.Modelos;

namespace ISEC
{
    public partial class frmActualizarUsuario : Form
    {
        UsuarioLocal usuarioLocal = null;
        UsuarioLocalRepository usuarioLocalRepository = new UsuarioLocalRepository();
        PuestoLocalRepository puestoLocalRepository = new PuestoLocalRepository();
        public Action RefreshAll { get; set; }
        IUserService userService;
        public frmActualizarUsuario(UsuarioLocal _usuarioLocal)
        {
            InitializeComponent();
            userService = Service.Adapter.Create<IUserService>();
            usuarioLocal = _usuarioLocal; 
            txtNombre.Text = usuarioLocal.Nombre;
            txtUsuario.Text = usuarioLocal.Username;
            txtPassword.Text = usuarioLocal.Password;
            GenericService.Load(cbPuesto, puestoLocalRepository.GetAll(), "Id", "Descripcion");
            var selected = puestoLocalRepository.GetAll().Where(s => s.Id == usuarioLocal.puestoid).FirstOrDefault();
            cbPuesto.SelectedValue =  selected.Id;
        }
         
        private void frmActualizarUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
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
            var puesto = cbPuesto.SelectedItem as PuestoLocal;
            UsuarioLocal local = new UsuarioLocal()
            {
                Id = usuarioLocal.Id,
                Nombre = txtNombre.Text,
                Username = txtUsuario.Text,
                Password = txtPassword.Text,
                puestoid = puesto.Id,
                Sync = usuarioLocal.Sync,
                Activo = usuarioLocal.Activo
            };
            var updated = usuarioLocalRepository.Update(local);
            if (updated)
            {
                if (NetWork.IsConnected)
                { 
                    if (local != null)
                    {
                        var response = userService.syncUpdated(local);
                        var result = response.Data;
                        usuarioLocalRepository.UpdateSync(new List<int>() { local.Id });
                    }
                }
                UserSession.Instancia.IsUpdate = true;
                MessageBox.Show("Se actualizo correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAll();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
