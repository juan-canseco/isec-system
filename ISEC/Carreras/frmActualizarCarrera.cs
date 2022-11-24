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

namespace ISEC.Carreras
{
    public partial class frmActualizarCarrera : Form
    {
        CarreraLocalRepository carreraLocalRepository = new CarreraLocalRepository();
        public Action RefreshAll { get; set; }
        CarreraLocal carrera = null;
        public frmActualizarCarrera(CarreraLocal _carrera)
        {
            InitializeComponent();
            carrera = _carrera;
            txtDescripcion.Text = carrera.Nombre;
            chActivo.Checked = carrera.Activa;
        } 
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    MessageBox.Show("Ingrese una descripción", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CarreraLocal carreraLocal = new CarreraLocal()
                {
                    Id = carrera.Id,
                    Nombre = txtDescripcion.Text,
                    Activa = chActivo.Checked 
                };
                bool inserted = carreraLocalRepository.Update(carreraLocal);
                if (inserted)
                {
                    MessageBox.Show("Se ha modificado carrera exitosamente!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message}", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
