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
    public partial class frmSync : Form
    {

        List<RolLocal2> roles = new List<RolLocal2>();
        LocalDb localDb = new LocalDb();
        SyncDb syncDb = new SyncDb();

        public frmSync()
        {
            InitializeComponent();
            Reload();
        }
        public   void Reload()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = roles;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RolLocal2 rol = new RolLocal2()
            {
                Descripcion = textBox1.Text,
                Sync = false,
                LastDate = String.Empty
            };
            roles.Add(rol);
            localDb.add(rol);
            Reload();
        }
    }

    class LocalDb { 
        public void add(RolLocal2 rol)
        {

        }

        public void UpdateSyncState(List<RolLocal2> rikes)
        {
            /// 
        }

        public List<RolLocal2> NotSync()
        {
            return null;
        }

    }

    class SyncDb 
    {
        public void add(RolLocal2 rol)
        {

        }

    }

    class MockRolApi
    {
        public void UpdateRoles(List<RolLocal2> rolesNotSync)
        {

        }
    }

    class BackgroundSync
    {
        public void Run()
        {
            var localDb = new LocalDb();
            MockRolApi api = new MockRolApi();
            var notSyncData = localDb.NotSync();
            api.UpdateRoles(notSyncData);
            localDb.UpdateSyncState(notSyncData);
        }
    }

}
