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
    public partial class frmActualizarPlan : Form
    {
        PlanLocalRepository planLocalRepository = new PlanLocalRepository(); 
        public Action RefreshAll { get; set; }
        PlanLocal plan = null;
        public frmActualizarPlan(PlanLocal _plan)
        {
            InitializeComponent(); 
            this.plan = _plan;
            txtDescripcion.Text = this.plan.Descripcion;
            chActivo.Checked = this.plan.EsActivo > 0 ? true : false; 
        }  
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    MessageBox.Show($"Ingrese una descripción", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                PlanLocal newPlan = new PlanLocal()
                {
                    Id= this.plan.Id,
                    Descripcion = txtDescripcion.Text,
                    Fecha = DateTime.Now.ToString(),
                    EsActivo = chActivo.Checked ? 1 : 0
                };
                bool updated = planLocalRepository.Update(newPlan);
                if (updated)
                {
                    MessageBox.Show($"Se modificó plan exitosamente!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"No se logro modificar plan, por favor revise su información", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message}", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            } 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
