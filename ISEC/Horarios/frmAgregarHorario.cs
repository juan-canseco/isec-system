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
using DataAccess.Local;
namespace ISEC
{
    public partial class frmAgregarHorario : Form
    {
        HorarioLocalRepository horarioLocalRepository = new HorarioLocalRepository();
        public Action RefreshAll { get; set; }
        public frmAgregarHorario()
        {
            InitializeComponent();
            txtDescripcion.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool inserted = horarioLocalRepository.Add(new HorarioLocal()
                {
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
                });
                if (inserted)
                {
                    MessageBox.Show("Se ha ingresado horario correctamente!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error al intentar registrar horario", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
