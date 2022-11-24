using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISEC.DbLocal;
using DataAccess.Local;
using ISEC.DbLocal.Repositorios;
using ISEC.Modelos;
using MySql.Data.MySqlClient;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ISEC.Recibos;
using System.Data.OleDb;
using System.Configuration;
using ISEC.API;
using ISEC.API.Users;
using ISEC.API.RolUsuario;
using ISEC.API.Roles;
using ISEC.API.Permisos;

namespace ISEC
{
    public partial class Form1 : Form
    {
        UsuarioLocalRepository usuarioLocalRepository = new UsuarioLocalRepository();
        CobranzaLocalRepository cobranzaLocalRepository = new CobranzaLocalRepository();
        CobranzaDetalleLocalRepository cobranzaDetalleLocalRepository = new CobranzaDetalleLocalRepository();
        RolUsuarioLocalRepository rolUsuarioRepository = new RolUsuarioLocalRepository();
        AccesoUsuarioLocalRepository accesoUsuarioLocalRepository = new AccesoUsuarioLocalRepository();
        RolLocalRepository rolLocalRepo = new RolLocalRepository();
        string dir = @"C:\isec";
        IUserService sUsers;
        IUserRolService suserRoleServie;
        IRolService srolService;
        IPermisoService sPermisoService;
        public Form1()
        {
            InitializeComponent();
            sUsers = Service.Adapter.Create<IUserService>();
            suserRoleServie = Service.Adapter.Create<IUserRolService>();
            srolService = Service.Adapter.Create<IRolService>();
            sPermisoService = Service.Adapter.Create<IPermisoService>();
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                //File.Copy("dbisec.sqlite.db", Path.Combine(dir));
            }
            if (NetWork.IsConnected)
            {
                Sync();
            }
            else
            {
                MessageBox.Show("Es necesario conexion a internet para poder sincronizar información",
                    "Sin conexión a internet", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void Sync()
        {
            if (NetWork.IsConnected)
            {

                //Usuarios no sincronizados (Agregados en server mas no en local)
                var noSyncServer = sUsers.All().Data.Where(s => s.Sync == 0).ToList();
                List<int> dataNoSync = new List<int>();
                if (noSyncServer.Count > 0)
                {
                    //Agregar nuevos usuarios creados en server a la db local
                    var converttoLocal = usuarioLocalRepository.ConvertServerUsersToLocal(noSyncServer);
                    foreach (var us in converttoLocal)
                    {
                        usuarioLocalRepository.Add(us);
                        dataNoSync.Add(us.Id);
                    }
                    //Sincronizar status en local una vez que se agregue
                    usuarioLocalRepository.UpdateSync(dataNoSync);
                }
                else
                {
                    //actualizar usuarios del server al local por si se actualizo
                    var all = sUsers.All().Data;
                    if (all.Count > 0)
                    {
                        var converttoLocal = usuarioLocalRepository.ConvertServerUsersToLocal(all);
                        foreach (var us in converttoLocal)
                        {
                            usuarioLocalRepository.Update(us);
                            dataNoSync.Add(us.Id);
                        }
                        usuarioLocalRepository.UpdateSync(dataNoSync);
                    }
                }


                //ROLES no sincronizados  (agregados en server mas no en local)
                var noSyncRolesServer = srolService.All().Data.Where(s => s.Sync == 0).ToList();
                List<int> dataNoSyncRoles = new List<int>();
                if (noSyncRolesServer.Count > 0)
                {
                    //convertir objetos de server to local 
                    var convetedLocales = rolLocalRepo.ConvertServerRolToLocal(noSyncRolesServer);
                    //agregar todos los registros nuevos en server al local
                    foreach (var convetedLocale in convetedLocales)
                    {
                        rolLocalRepo.Add(convetedLocale);
                        dataNoSyncRoles.Add(convetedLocale.Id);
                    }
                    //sincronizar datos en local una vez que se agreguen
                    rolLocalRepo.UpdateSync(dataNoSyncRoles);
                }
                else
                {
                    //actualizar de server a local por si se modifico
                    var all = srolService.All().Data;
                    if (all.Count > 0)
                    {
                        var converted = rolLocalRepo.ConvertServerRolToLocal(all);
                        foreach (var s in converted)
                        {
                            rolLocalRepo.Update(s);
                            dataNoSyncRoles.Add(s.Id);
                        }
                        rolLocalRepo.UpdateSync(dataNoSyncRoles);
                    }


                }


                //ROLUSUARIO no sincronizados en local pero agregados en server
                var noSyncRolUsuario = suserRoleServie.all().Data.Where(s => s.Sync == 0).ToList();
                List<int> dataNoSyncRolesUsuario = new List<int>();
                if (noSyncRolUsuario.Count > 0)
                {
                    var convertes = rolUsuarioRepository.ConvertRolUsuariosToLocal(noSyncRolUsuario);
                    //agregar todos los registros nuevos en server al local
                    foreach (var converte in convertes)
                    {
                        rolUsuarioRepository.Add(converte);
                        dataNoSyncRolesUsuario.Add(converte.Id);
                    }
                    rolUsuarioRepository.UpdateSync(dataNoSyncRolesUsuario);
                }
                else
                {
                    //actualizar de server a local por si se modifico 
                    var all = suserRoleServie.all().Data;
                    if (all.Count > 0)
                    {
                        var convertes = rolUsuarioRepository.ConvertRolUsuariosToLocal(all);
                        foreach (var converte in convertes)
                        {
                            rolUsuarioRepository.Update(converte);
                            dataNoSyncRolesUsuario.Add(converte.Id);
                        }
                        rolUsuarioRepository.UpdateSync(dataNoSyncRolesUsuario);
                    }
                }

                //PERMISOS NO SINCRONIZADOS EN LOCAL PERO AGREGADOS EN SERVER 
                var noSyncPermisos = sPermisoService.all().Data.ToList();
                List<int> dataNoSyncPermisos = new List<int>();
                //si hay permisos en server
                if (noSyncPermisos.Count > 0)
                {
                    //agregar todos los registros nuevos del server a local
                    var converters = accesoUsuarioLocalRepository.ConvertPermisosToLocal(noSyncPermisos);
                    foreach (var converter in converters)
                    {

                        accesoUsuarioLocalRepository.Add(converter);
                        dataNoSyncPermisos.Add(converter.Id);
                    }
                    accesoUsuarioLocalRepository.UpdateSync(dataNoSyncPermisos);
                }
                else
                {
                    //actualizar de server a local por si se modifico
                    var all = sPermisoService.all().Data;
                    if (all.Count > 0)
                    {
                        var converters = accesoUsuarioLocalRepository.ConvertPermisosToLocal(all);
                        foreach (var c in converters)
                        {
                            accesoUsuarioLocalRepository.Update(c);
                            dataNoSyncPermisos.Add(c.Id);
                        }
                        accesoUsuarioLocalRepository.UpdateSync(dataNoSyncPermisos);
                    }
                }

            }
            else
            {
                MessageBox.Show("Es necesario conexion a internet para poder sincronizar información",
                   "Sin conexión a internet", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void Acceso()
        {
            btnAcceso.Enabled = false;
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {

                UsuarioLocal user = usuarioLocalRepository.Login(txtUsername.Text, txtPassword.Text);
                if (user != null)
                {
                    //Si no tiene roles asignados
                    if (rolUsuarioRepository.CountByUser(user.Id) == 0)
                    {
                        MessageBox.Show($"El usuario {user.Username} no tiene ningun rol asignado", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnAcceso.Enabled = true;
                        return;
                    }
                    else
                    {
                        //si existe rol asignado pero si no tiene permisos 
                        var rolAsignado = rolUsuarioRepository.GetAllByRolId(user.Id)[0];
                        if (accesoUsuarioLocalRepository.countByRolId(rolAsignado.RolId) > 0)
                        {
                            UserSession.Instancia.Usuario = user;
                            btnAcceso.Enabled = true;
                            this.Hide();
                            if (cobranzaLocalRepository.Exists() <= 0)
                            {
                                frmIniciarCobranza frmIniciarCobranza = new frmIniciarCobranza();
                                frmIniciarCobranza.ShowDialog();
                            }
                            else
                            {
                                frmMenu frmMain = new frmMenu(user);
                                frmMain.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show($"El usuario {user.Username} cuenta con rol {rolAsignado.Rol} asignado pero no tiene permiso a ningun modulo", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            btnAcceso.Enabled = true;
                            return;
                        }

                    }


                }
                else
                {
                    btnAcceso.Enabled = true;
                    MessageBox.Show("Usuario y/o contraseña incorrectos, por favor verifique su información", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                btnAcceso.Enabled = true;
                MessageBox.Show("Usuario y/o contraseña son necesarios para accesar al sistema", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnAcceso_Click(object sender, EventArgs e)
        {

            if (usuarioLocalRepository.GetAll().Count > 0)
            {
                Acceso();
            }
            else
            {
                if (NetWork.IsConnected)
                {
                    Acceso();
                }

            }


        }
        frmSpinner load = null;
        public void ShowSpinner()
        {
            load = new frmSpinner();
            load.Show();
        }
        public void HideSpinner()
        {
            if (load != null)
            {
                load.Hide();
            }
        }
        private async void btnSync_Click(object sender, EventArgs e)
        {
            ShowSpinner();
            Task oTask = new Task(Sync);
            oTask.Start();
            await oTask;
            HideSpinner();
        }
    }
}
