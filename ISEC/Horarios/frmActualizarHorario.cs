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

namespace ISEC.Horarios
{
    public partial class frmActualizarHorario : Form
    {
        HorarioLocalRepository horarioLocalRepository = new HorarioLocalRepository();
        HorarioLocal horarioLocal = null;
        public Action RefreshAll { get; set; }
        public frmActualizarHorario(HorarioLocal _horarioLocal)
        {
            InitializeComponent();
            horarioLocal = _horarioLocal;
            txtDescripcion.Text = horarioLocal.Descripcion;
            chLunes.Checked = horarioLocal.Lunes > 0 ? true : false;
            chMartes.Checked = horarioLocal.Martes > 0 ? true : false;
            chMiercoles.Checked = horarioLocal.Miercoles > 0 ? true : false;
            chJueves.Checked = horarioLocal.Jueves > 0 ? true : false;
            chViernes.Checked = horarioLocal.Viernes > 0 ? true : false;
            chSabado.Checked = horarioLocal.Sabado > 0 ? true : false;
            chDomingo.Checked = horarioLocal.Domingo > 0 ? true : false;
            dtHoraInicial.Value = DateTime.Parse(horarioLocal.HoraInicial);
            dtHoraFinal.Value = DateTime.Parse(horarioLocal.HoraFinal);
            chActivo.Checked = horarioLocal.Activo >0 ? true : false; 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var horarioNew = new HorarioLocal()
                {
                    Id = horarioLocal.Id,
                    Descripcion = txtDescripcion.Text,
                    Lunes = chLunes.Checked ? 1 : 0,
                    Martes = chMartes.Checked ? 2 : 0,
                    Miercoles = chMiercoles.Checked ? 3 : 0,
                    Jueves = chJueves.Checked ? 4 : 0,
                    Viernes = chViernes.Checked ? 5 : 0,
                    Sabado = chSabado.Checked ? 6 : 0,
                    Domingo = chDomingo.Checked ? 7 : 0,
                    HoraInicial = dtHoraInicial.Value.ToString(),
                    HoraFinal = dtHoraFinal.Value.ToString()
                };
                bool updated = horarioLocalRepository.Update(horarioNew);
                if (updated)
                {
                    MessageBox.Show("Se ha actualizado horario correctamente!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se logro actualizar horario, verifique su información", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch ( Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }
    }
}
