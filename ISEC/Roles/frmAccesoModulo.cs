using DataAccess.Local;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO; 

namespace ISEC.Roles
{
    public partial class frmAccesoModulo : Form
    {
        List<AccesoViewModel> accesos = null;
        public Action <List<AccesoViewModel>> RefreshAll { get; set; }
        ModuloLocal modulo = null; 
        public frmAccesoModulo(ModuloLocal mod)
        {
            InitializeComponent();
            accesos = new List<AccesoViewModel>(); 
            if (mod != null)
            {
                modulo = mod;
                lblModulo.Text = String.Format("Modulo: {0}", modulo.Nombre);
            }
        }

        private void btnAddModulo_Click(object sender, EventArgs e)
        {
            accesos.Add(new AccesoViewModel() { ModuloId = modulo.Id, Acceso = chAcceso.Checked, Lectura = chLectura.Checked, Escritura = chEscritura.Checked, Nombre = modulo.Nombre });
            RefreshAll(accesos); 
            this.Close();
        }
    }
}
