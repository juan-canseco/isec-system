using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
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
    public partial class frmAsignarRol : Form
    {
        RolLocal rolLocal = null;
        UsuarioLocalRepository usuarioLocalRepository = new UsuarioLocalRepository();
        RolUsuarioLocalRepository rolUsuarioRepository = new RolUsuarioLocalRepository();
        RolLocalRepository rolLocalRepository = new RolLocalRepository();
        ModuloLocalRepository moduloLocalRepository = new ModuloLocalRepository();
        List<AccesoViewModel> accesos = new List<AccesoViewModel>();
        AccesoUsuarioLocalRepository accesoUsuarioLocalRepository = new AccesoUsuarioLocalRepository();
        RolUsuarioLocal rolUS = null;
        public frmAsignarRol(RolLocal rol)
        {
            InitializeComponent();
            if (rol != null)
            {
                rolLocal = rol;
            }
            Reload();
            GetRoles();
            GetUsers();
            ReloadModulos();
        }
        public void ReloadModulos()
        {
            if (moduloLocalRepository.GetAll().Count > 0)
            {
                var lst = ((CheckedListBox)chModulos);
                lst.Items.Add("seleccione rol");
                lst.SetItemCheckState(0, CheckState.Indeterminate);
                foreach (var item in moduloLocalRepository.GetAll())
                {
                    lst.Items.Add(item);
                    lst.DisplayMember = "Nombre";
                    lst.ValueMember = "Id";
                }  
                //lst.DataSource = moduloLocalRepository.GetAll();
                //lst.DisplayMember = "Nombre";
                //lst.ValueMember = "Id";
            }
        }
        public void GetRoles()
        {
            cbRoles.DataSource = null;
            cbRoles.DataSource = rolLocalRepository.GetAll();
            cbRoles.ValueMember = "Id";
            cbRoles.DisplayMember = "Descripcion";
        }
        public void GetUsers()
        {
            var users = usuarioLocalRepository.GetAll();
            cbUsuario.DataSource = users;
            cbUsuario.ValueMember = "Id";
            cbUsuario.DisplayMember = "Nombre";
        }
        public void Reload()
        {
            gvAccesos.DataSource = null;
            gvAccesos.DataSource = rolUsuarioRepository.GetAll();
            gvAccesos.Columns["Id"].Visible = false;
            gvAccesos.Columns["RolId"].Visible = false;
            gvAccesos.Columns["UsuarioId"].Visible = false;
        }


        private void btnAsignar_Click_1(object sender, EventArgs e)
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

        private void gvAccesos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var rolus = gvAccesos.Rows[e.RowIndex].DataBoundItem as RolUsuarioLocal;
            if (rolus != null)
            {
                rolUS = rolus;
                chModulos.Enabled = true;
                btnAddModulo.Enabled = true;
                cbRoles.Enabled = false;
                cbUsuario.Enabled = false;
                btnAsignar.Enabled = false;
                btnSave.Enabled = true;

            }
        }
        private void chModulos_MouseClick(object sender, MouseEventArgs e)
        {
            var selected = chModulos.SelectedItem as ModuloLocal;
            if (selected != null)
            {
                gvPermisos.Enabled = true;
                //MessageBox.Show(rolUS.RolId.ToString());
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
                        Nombre = rolLocalRepository.Get(rolUS.RolId).Descripcion
                    };
                    accesos.Add(acceso);
                    chModulos.Items.Remove(selected);
                    gvPermisos.DataSource = null;
                    gvPermisos.DataSource = accesos;
                }
           
               
                //chModulos.SelectedItems.Clear();
                //chModulos.ClearSelected();
               
            }

        }

        private void chModulos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0) e.NewValue = e.CurrentValue;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gvPermisos.Rows.Count > 0)
            {
                if (accesos.Count > 0)
                {
                    foreach (var acc in accesos)
                    {
                        accesoUsuarioLocalRepository.Add(new AccesoUsuarioLocal()
                        {
                            Acceso = acc.Acceso,
                            Lectura = acc.Lectura,
                            Escritura = acc.Escritura,
                            ModuloId = acc.ModuloId,
                            RolId = rolUS.RolId
                        });
                    }
                    chModulos.Enabled = false;
                    btnAddModulo.Enabled = false;
                    cbRoles.Enabled = true;
                    cbUsuario.Enabled = true;
                    btnAsignar.Enabled = true;
                    btnSave.Enabled = false;
                    ReloadModulos();
                    gvPermisos.DataSource = null;

                    MessageBox.Show("Se han guardado exitosamente los permisos", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else
            {
                MessageBox.Show("Es necesario tener permisos para por lo menos 1 modulo", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddRol_Click(object sender, EventArgs e)
        {
            frmAgregarRol rol = new frmAgregarRol();
            rol.RefreshAll = GetRoles;
            rol.ShowDialog();
        }
    }
}
