using DataAccess.Local;
using ISEC.Arqueo;
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

namespace ISEC
{
    public partial class frmAccion : Form
    {
        UsuarioLocalRepository usuarioLocalRepository = new UsuarioLocalRepository();
        public Action ReloadAlumnos { get; set; }
        bool isAnotherPago;
        FormType formType;
        frmMenu menu;
        public frmAccion(bool _isAnotherPago,FormType type,frmMenu _menu=null)
        {
            InitializeComponent();
            isAnotherPago = _isAnotherPago;
            formType = type;
            menu = _menu;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    var user = usuarioLocalRepository.LoginByPassword(txtPassword.Text);
                    if (user!= null)
                    {
                        UserSession.Instancia.UsuarioAccion = user;
                        this.Hide();
                        switch (formType)
                        {
                            case FormType.Arqueo:
                                menu.OpenChildForm(new frmArqueo(),sender);
                                 break;  
                            case FormType.Alumnos:
                                if (!isAnotherPago)
                                {
                                    frmRegistroAlumno frmr = new frmRegistroAlumno() { Dock = DockStyle.Bottom };
                                    frmr.RefreshAll = ReloadAlumnos;
                                    frmr.ShowDialog();
                                }
                                else
                                {
                                    frmPago p = new frmPago(null);
                                    p.ShowDialog();
                                }
                                break;
                            default:
                                break;
                        }
                    
            
                    }
                    else
                    {
                        MessageBox.Show("Su clave ha sido incorrecta", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                    }
                }
                else
                {
                    MessageBox.Show("Es necesario ingresar su clave", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void frmAccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
            }
        }
    }
}
