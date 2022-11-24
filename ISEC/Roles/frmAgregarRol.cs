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
    public partial class frmAgregarRol : Form
    {
        AccesoUsuarioLocalRepository accesoUsuarioLocalRepository = new AccesoUsuarioLocalRepository();
        ModuloLocalRepository moduloLocalRepository = new ModuloLocalRepository();
        RolLocalRepository rolLocalRepository = new RolLocalRepository();
        List<AccesoViewModel> accesos = new List<AccesoViewModel>();
        public Action RefreshAll { get; set; }
        public frmAgregarRol()
        {
            InitializeComponent(); 
            ReloadModulos();

        }
        public void ReloadModulos()
        {
            //if (moduloLocalRepository.GetAll().Count > 0)
            //{
            //    var lst = ((ListBox)chModulos);
            //    lst.DataSource = moduloLocalRepository.GetAll();
            //    lst.DisplayMember = "Nombre";
            //    lst.ValueMember = "Id";
            //}
        }
        private void btnRemoveRol_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        public void test()
        {
            var rolViewModels = new List<RolViewModel>();
            RolViewModel rol = new RolViewModel();
            rol.Modulos = new List<ModuloLocal>();
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Es necesario un nombre");
                return;
            }
            //if (chModulos.CheckedItems.Count == 0)
            //{
            //    MessageBox.Show("Es necesario un Modulos");
            //    return;
            //}
            rol.Nombre = txtNombre.Text;
            if (accesos.Count > 0)
            {
                foreach (var acc in accesos)
                {
                    ModuloLocal m = new ModuloLocal();
                    m.Id = acc.ModuloId;
                    m.Nombre = acc.Nombre;
                    rol.Modulos.Add(m);

                    rol.Acceso = acc.Acceso;
                    rol.Lectura = acc.Lectura;
                    rol.Escritura = acc.Escritura;
                }
            }
            //if (chModulos.CheckedItems.Count > 0)
            //{
            //    foreach (var index in chModulos.CheckedItems)
            //    {
            //        var modulo = (ModuloLocal)index;
            //        rol.Modulos.Add(modulo);
            //    }
            //}
            else
            {
                MessageBox.Show("No tendra acceso a ningun modulo, por lo menos seleccione uno", "AVISO!");
            }

            //rol.Acceso = chAcceso.Checked;
            //rol.Lectura = chLectura.Checked;
            //rol.Escritura = chEscritura.Checked;

            //Agregar rol
            if (!rolLocalRepository.Exists(rol.Nombre))
            {
                RolLocal rolLocal = new RolLocal();
                rolLocal.Descripcion = rol.Nombre;
                rolLocalRepository.Add(rolLocal);
                var lastRol = rolLocalRepository.GetLast();
                //Agregar acceso usuario 
                if (rol.Modulos.Count > 0)
                {
                    foreach (var mod in rol.Modulos)
                    {
                        AccesoUsuarioLocal accesoUsuarioLocal = new AccesoUsuarioLocal();
                        accesoUsuarioLocal.RolId = lastRol;
                        accesoUsuarioLocal.ModuloId = mod.Id;
                        accesoUsuarioLocal.Acceso = rol.Acceso;
                        accesoUsuarioLocal.Lectura = rol.Lectura;
                        accesoUsuarioLocal.Escritura = rol.Escritura;
                        accesoUsuarioLocalRepository.Add(accesoUsuarioLocal);
                    }
                }
            }
            //Reload();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Es necesario un nombre", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!rolLocalRepository.Exists(txtNombre.Text))
            {
                rolLocalRepository.Add(new RolLocal() { Descripcion = txtNombre.Text });
                RefreshAll();
            }
        }
    }
}
