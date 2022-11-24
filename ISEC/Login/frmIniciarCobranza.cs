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
    public partial class frmIniciarCobranza : Form
    {
        UsuarioLocalRepository usuarioLocalRepository = new UsuarioLocalRepository();
        CobranzaLocalRepository cobranzaLocalRepository = new CobranzaLocalRepository();
        UsuarioLocal user = null;
        public frmIniciarCobranza()
        {
            InitializeComponent();
            if (cobranzaLocalRepository.GetLastFolio() > 0)
            {
                txtReciboInicial.Text = (cobranzaLocalRepository.GetLastFolio()+1).ToString();
            }
            else
            {
                txtReciboInicial.Text = "86000";
            }
            int defaultt = 0;
            txtFondoInicial.Text = defaultt.ToString("D2");
            cbUsuarios.DataSource = usuarioLocalRepository.GetAll();
            cbUsuarios.ValueMember = "Username";
            cbUsuarios.DisplayMember = "Nombre";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.ShowDialog();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            user = cbUsuarios.SelectedItem as UsuarioLocal;
            if (!string.IsNullOrEmpty(txtClave.Text))
            {
                if (txtClave.Text == user.Password)
                {

                    bool inserted = cobranzaLocalRepository.Add(new CobranzaLocal()
                    {
                        Username = user.Username,
                        Folio = txtReciboInicial.Text,
                        FondoInicial = decimal.Parse(txtFondoInicial.Text),
                        FechaInicial = DateTime.Now.ToString(),
                        FechaFinal = null,
                        Sobrante = 0,
                        Faltante = 0,
                        Ingreso = 0,
                        Egreso = 0,
                        Estado = 1,
                        SaldoCaja = decimal.Parse(txtFondoInicial.Text)
                    });
                    if (inserted)
                    {
                        this.Hide();
                        MessageBox.Show("Se ha iniciado cobranza con éxito", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmMenu frmMain = new frmMenu(UserSession.Instancia.Usuario);
                        frmMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("No se logro iniciar cobranza", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("La clave de acceso no coincide, por favor ingrese la correcta", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Es necesario ingresar la clave de la secretaria seleccionada", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtClave.Focus();
            }
        }

        private void bgAdd_DoWork(object sender, DoWorkEventArgs e)
        {
           
        } 
        private void bgAdd_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }  
        private void bgAdd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("termino bg");
        }
    }
}
