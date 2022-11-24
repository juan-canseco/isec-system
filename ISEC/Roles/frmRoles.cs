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
using ISEC.API.Roles;
using ISEC.API;
using ISEC.Modelos;

namespace ISEC.Roles
{
    public partial class frmRoles : Form
    {
        RolLocalRepository rolLocalRepository = new RolLocalRepository();
        public ISEC.frmMenu menu = null;
        //UsuarioViewModel usVm = null;
        IRolService sRoles;
        public frmRoles(frmMenu _frmMenu, UsuarioViewModel usuarioViewModel)
        {
            InitializeComponent();
            sRoles = Service.Adapter.Create<IRolService>();
            this.menu = _frmMenu;
            Functions.SetRoles(usuarioViewModel, btnAdd, gvRoles);
            Functions.Sync(menu,rolLocalRepository.GetAllNoSync(), new List<Action> { SyncAll, Reload }); 
        } 
        public void SyncAll()
        {
            var response = sRoles.All();
            var data = response.Data;
            var noSync = rolLocalRepository.GetAllNoSync();
            var responseNoSync = sRoles.sync(noSync);
            var dataNoSync = responseNoSync.Data;
            rolLocalRepository.UpdateSync(dataNoSync);
        }
        public void Reload()
        {
            gvRoles.DataSource = null;
            gvRoles.DataSource = rolLocalRepository.GetAll();
            gvRoles.Columns["Id"].Visible = false;
            gvRoles.Columns["Sync"].Visible = false;
            gvRoles.Columns["Activo"].Visible = false;

        } 
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAgregarRol ADD = new frmAgregarRol();
            ADD.RefreshAll = Reload;
            ADD.ShowDialog();
        }

        private void gvRoles_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                var data = gvRoles.Rows[e.RowIndex].DataBoundItem as RolLocal;
                if (data != null)
                {
                    frmEditarRol acc = new frmEditarRol(data);
                    acc.ShowDialog();
                }
            }
        }
        private void btnAsignar_Click(object sender, EventArgs e)
        { 
            frmAsignarRol acc = new frmAsignarRol(null);
            acc.ShowDialog();
        } 
        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            var data = rolLocalRepository.GetAll();
            if (!string.IsNullOrEmpty(txtFiltro.Text))
            {
                string texto = txtFiltro.Text.ToUpper();
                data = data.FindAll(s => s.Descripcion.ToString().ToUpper().Contains(texto));
                gvRoles.DataSource = data;
            }
            else
            {
                gvRoles.DataSource = rolLocalRepository.GetAll();
            }
        }
    }
}
