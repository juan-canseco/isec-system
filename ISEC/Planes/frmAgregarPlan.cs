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

namespace ISEC.Planes
{
    public partial class frmAgregarPlan : Form
    {
        PlanLocalRepository planLocalRepository = new PlanLocalRepository();
        public Action RefreshAll { get; set; }
        public frmAgregarPlan()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show($"Ingrese una descripción", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } 
            try
            {
                PlanLocal plan = new PlanLocal()
                {
                    Descripcion = txtDescripcion.Text,
                    Fecha = DateTime.Now.ToString()
                };
                bool inserted = planLocalRepository.Add(plan);
                if (inserted)
                {
                    MessageBox.Show($"Se ha registrado plan {plan.Descripcion} correctamente!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"No se logro registrar plan", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
