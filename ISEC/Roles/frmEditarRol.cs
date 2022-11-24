using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
using ISEC.DbLocal.Services;
using ISEC.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace ISEC.Roles
{
    public partial class frmEditarRol : Form
    {
        UsuarioLocalRepository usuarioLocalRepository = new UsuarioLocalRepository();
        RolUsuarioLocalRepository rolUsuarioRepository = new RolUsuarioLocalRepository();
        RolLocalRepository rolLocalRepository = new RolLocalRepository();
        ModuloLocalRepository moduloLocalRepository = new ModuloLocalRepository();
        //List<AccesoViewModel> accesos = new List<AccesoViewModel>();
        AccesoUsuarioLocalRepository accesoUsuarioLocalRepository = new AccesoUsuarioLocalRepository();
        RolLocal rolLocal = null;
        public frmEditarRol(RolLocal _rol)
        {
            InitializeComponent();
            if (_rol != null)
            {
                rolLocal = _rol;
                GenericService.Load(cbRoles, new List<RolLocal>() { rolLocal }, "Id", "Descripcion");
                GenericService.Load(cbUsuario, usuarioLocalRepository.GetAll(), "Id", "Nombre");
                Reload();
                ReloadPermisos();
                ReloadModulos();
            }
        }
        public void ReloadModulos()
        {
            var list = moduloLocalRepository.GetAll();
            var modulos = accesoUsuarioLocalRepository.GetAllByRol(rolLocal.Id).Select(s => s.ModuloId);
            var d = (from s in list join ds in modulos on s.Id equals ds select s).ToList();
            var excepts = list.Except(d).ToList();
            GenericService.Load(chModulos, excepts, "Id", "Nombre");
        }
        public void ReloadPermisos()
        {
            GenericService.Load(gvPermisos, accesoUsuarioLocalRepository.GetAllByRol(rolLocal.Id), "", "edit");
            gvPermisos.Columns["Id"].Visible = false;
            gvPermisos.Columns["RolId"].Visible = false;
            gvPermisos.Columns["ModuloId"].Visible = false;
            gvPermisos.Columns["Rol"].Visible = false;
            gvPermisos.Columns[0].Width = 50;
        }
        public void Reload()
        {
            GenericService.Load(gvAccesos, rolUsuarioRepository.GetAllByRolId(rolLocal.Id), "b", "");
            gvAccesos.Columns["Id"].Visible = false;
            gvAccesos.Columns["RolId"].Visible = false;
            gvAccesos.Columns["UsuarioId"].Visible = false;
            gvAccesos.Columns[0].Width = 50;
        }
        private void gvAccesos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var selected = senderGrid.Rows[e.RowIndex].DataBoundItem as RolUsuarioLocal;

                var dialog = MessageBox.Show(String.Format("{0}", "Esta seguro que desea eliminar el acceso?"), "AVISO!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    //delete
                    bool deleted = rolUsuarioRepository.Delete(selected.Id);
                    if (deleted)
                    {
                        MessageBox.Show("Se ha eliminado el acceso con exito ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reload();
                    }
                    return;
                }
                return;
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            var us = cbUsuario.SelectedItem as UsuarioLocal;
            var rol = cbRoles.SelectedItem as RolLocal;
            RolUsuarioLocal rolUsuarioLocal = new RolUsuarioLocal();
            rolUsuarioLocal.RolId = rol.Id;
            rolUsuarioLocal.UsuarioId = us.Id;
            var exists = rolUsuarioRepository.Exists(rolUsuarioLocal.RolId, rolUsuarioLocal.UsuarioId);
            if (!exists)
            {
                var inserted = rolUsuarioRepository.Add(rolUsuarioLocal);
                if (inserted)
                {
                    MessageBox.Show(String.Format("Se ha asignado rol {0} a {1}", rol.Descripcion, us.Nombre), "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reload();
                }
            }
            else
            {
                MessageBox.Show(String.Format("Ya existe rol {0} para {1}", rol.Descripcion, us.Nombre), "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddModulo_Click(object sender, EventArgs e)
        {
            if (chModulos.CheckedItems.Count > 0)
            {
                for (int i = 0; i < chModulos.CheckedItems.Count; i++)
                {
                    var selected = (ModuloLocal)chModulos.CheckedItems[i];
                    var acceso = new AccesoViewModel()
                    {
                        Acceso = chAcceso.Checked,
                        Lectura = chLectura.Checked,
                        Escritura = chEscritura.Checked,
                        ModuloId = selected.Id,
                        Nombre = rolLocalRepository.Get(rolLocal.Id).Descripcion
                    };
                    accesoUsuarioLocalRepository.Add(new AccesoUsuarioLocal()
                    {
                        Acceso = acceso.Acceso,
                        Lectura = acceso.Lectura
                    ,
                        Escritura = acceso.Escritura,
                        ModuloId = acceso.ModuloId,
                        RolId = rolLocal.Id
                    });
                    ReloadModulos();
                    ReloadPermisos();
                }
            }
            else
            {
                MessageBox.Show("No existen modulos disponibles","AVISO!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void gvPermisos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                var selected = senderGrid.Rows[e.RowIndex].DataBoundItem as AccesoUsuarioLocal; 
                //edit
                frmEditarPermiso p = new frmEditarPermiso(selected);
                p.RefreshAll = ReloadPermisos;
                p.ShowDialog(); 
            }
        }
    }
}
