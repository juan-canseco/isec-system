using ISEC.DbLocal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess.Local;
using ISEC.DbLocal.Repositorios; 
using ISEC.API.Users;
using ISEC.API;
using ISEC.Modelos;

namespace ISEC
{
    public partial class frmAgregarUsuario : Form
    {
        PuestoLocalRepository puestoLocalRepository = new PuestoLocalRepository();
        UsuarioLocalRepository usuarioLocalRepository = new UsuarioLocalRepository();
        public Action RefreshAll { get; set; }
        IUserService sUsers;

        public frmAgregarUsuario()
        {
            InitializeComponent(); 
            GetPuestos();
            sUsers = Service.Adapter.Create<IUserService>();

        }
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            var puesto = cbPuesto.SelectedItem as PuestoLocal;
            bool inserted = usuarioLocalRepository.Add(
                new UsuarioLocal()
                {
                    Nombre = txtNombre.Text,
                    Username = txtUsuario.Text,
                    Password = txtPassword.Text,
                    puestoid = puesto.Id
                }
                );
            if (inserted)
            {
                if (NetWork.IsConnected)
                {
                    var last = usuarioLocalRepository.GetLast();
                    if (last != null)
                    {
                        var response =sUsers.syncInserted(last);
                        var result = response.Data; 
                        usuarioLocalRepository.UpdateSync(new List<int>() { last.Id}); 
                    }
                }
              
                MessageBox.Show("Se agrego correctamente!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshAll();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetPuestos()
        {
            cbPuesto.DataSource = null;
            cbPuesto.DataSource = puestoLocalRepository.GetAll();
            cbPuesto.ValueMember = "Id";
            cbPuesto.DisplayMember = "Descripcion";
        }
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmAgregarUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
            }
        }
    }
}
