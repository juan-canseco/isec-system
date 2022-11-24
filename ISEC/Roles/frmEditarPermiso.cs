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

namespace ISEC.Roles
{
    public partial class frmEditarPermiso : Form
    {
        AccesoUsuarioLocalRepository accesoUsuarioLocalRepository = new AccesoUsuarioLocalRepository(); 
        AccesoUsuarioLocal permiso = null;
        public Action RefreshAll { get; set; }
        public frmEditarPermiso(AccesoUsuarioLocal _permiso)
        {
            InitializeComponent();
            if (_permiso != null)
            {
                permiso = _permiso;
                lblModulo.Text = permiso.Modulo;
                chAcceso.Checked = permiso.Acceso;
                chLectura.Checked = permiso.Lectura;
                chEscritura.Checked = permiso.Escritura;
            } 
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            bool updated = accesoUsuarioLocalRepository.Update(new AccesoUsuarioLocal()
            {
                Id = permiso.Id,
                Acceso= chAcceso.Checked,
                Lectura = chLectura.Checked,
                Escritura =chEscritura.Checked,
                ModuloId = permiso.ModuloId,
                RolId = permiso.RolId,
                Rol =permiso.Rol,
                Modulo = permiso.Modulo
            });
            if (updated)
            {
                RefreshAll();
                MessageBox.Show("Se ha actualizado con exito", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("No se logro actualizar permiso", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
