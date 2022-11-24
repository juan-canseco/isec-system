using ISEC.DbLocal;
using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
using ISEC.Modelos; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISEC.Observer; 
using ISEC.API.Users;
using ISEC.API;

namespace ISEC
{
    public partial class frmUsuarios : Form
    {
        UsuarioLocalRepository usuarioLocalRepository = new UsuarioLocalRepository(); 
        public ISEC.frmMenu menu = null;
        UsuarioViewModel usVm = null;
        IUserService sUsers; 
        public void SyncAll()
        {
            var response = sUsers.All();
            var data = response.Data;
            var noSync = usuarioLocalRepository.GetAllNoSync();
            var responseNoSync = sUsers.sync(noSync);
            var dataNoSync = responseNoSync.Data;
            usuarioLocalRepository.UpdateSync(dataNoSync);
        }
        public frmUsuarios(frmMenu _frmMenu, UsuarioViewModel usuarioViewModel)
        {
            InitializeComponent(); 
            sUsers = Service.Adapter.Create<IUserService>();
            this.menu = _frmMenu;
            Functions.SetRoles(usuarioViewModel, btnAgregar, dtUsers);
            this.menu.flpButtons.Controls.Add(btnAgregar);
            Functions.Sync(menu, usuarioLocalRepository.GetAllNoSync(), new List<Action> { SyncAll, Reload }); 
        }
        public void Reload()
        {
            dtUsers.DataSource = null;
            dtUsers.DataSource = usuarioLocalRepository.GetAll();
            dtUsers.Columns["Id"].Visible = false;
            dtUsers.Columns["puestoid"].Visible = false;
            dtUsers.Columns["Sync"].Visible = false; 
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(usuarioLocalRepository.Upload(),"AVISO!",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }


        private void dtUsers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void bgUsuarios_DoWork(object sender, DoWorkEventArgs e)
        {

        }
        private void bgUsuarios_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        private void bgUsuarios_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarUsuario ADD = new frmAgregarUsuario();
            ADD.RefreshAll = Reload;
            ADD.ShowDialog();
        }

        private void dtUsers_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            var userSelected = dtUsers.Rows[dtUsers.CurrentCell.RowIndex].DataBoundItem as UsuarioLocal;
            frmActualizarUsuario FRMau = new frmActualizarUsuario(userSelected);
            FRMau.RefreshAll = Reload;
            FRMau.ShowDialog();
        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            var data = usuarioLocalRepository.GetAll();
            if (!string.IsNullOrEmpty(txtFiltro.Text))
            {
                string texto = txtFiltro.Text.ToUpper();
                data = data.FindAll(s => s.Nombre.ToUpper().Contains(texto) || s.Password.ToUpper().Contains(texto)
                || s.Username.ToUpper().Contains(texto) || s.puestoid.ToString().ToUpper().Contains(texto));
                dtUsers.DataSource = data;
            }
            else
            {
                dtUsers.DataSource = usuarioLocalRepository.GetAll();
            }
        }

        private void dtUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)// posicion de la columna en el datagridview
            {
                //cambia el valor real y se le asignan *****
                //e.Value = new string('*', e.Value.ToString().Length);
            }
        }
    }
}
