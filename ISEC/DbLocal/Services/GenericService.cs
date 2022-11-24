using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISEC.DbLocal.Services
{
    public class GenericService
    {
        public static void Load(Control control, IEnumerable<object> data, string id, string name)
        {
            if (control is CheckedListBox)
            {
                if (control != null)
                {
                    var ctrl = (ListBox)control;
                    ctrl.DataSource = data;
                    ctrl.ValueMember = id;
                    ctrl.DisplayMember = name;
                    if (ctrl.SelectedIndex != -1)
                    {
                        ctrl.SelectedIndex = 0; 
                    }
                } 
            }
            else if (control is ComboBox)
            {
                var ctrl = (ComboBox)control;
                ctrl.DataSource = data;
                ctrl.ValueMember = id;
                ctrl.DisplayMember = name;
                ctrl.SelectedIndex = 0;

            }
            else if (control is DataGridView)
            {
                var dgv = (DataGridView)control;
                if (id == "b")
                {

                    if (dgv.Columns.Contains("dgvEliminarColumn") == false)
                    {
                        var deleteButton = new DataGridViewButtonColumn();
                        deleteButton.Name = "dgvEliminarColumn";
                        deleteButton.HeaderText = "";
                        deleteButton.Text = "❌";
                        deleteButton.Width = 20;
                        deleteButton.UseColumnTextForButtonValue = true;
                        dgv.Columns.Add(deleteButton);
                    }
                }
                else if (name == "edit")
                {
                    if (dgv.Columns.Contains("dgvEditarColumn") == false)
                    {
                        var deleteButton = new DataGridViewButtonColumn();
                        deleteButton.Name = "dgvEditarColumn";
                        deleteButton.HeaderText = "";
                        deleteButton.Text = "✐";
                        deleteButton.Width = 20;
                        deleteButton.UseColumnTextForButtonValue = true;
                        dgv.Columns.Add(deleteButton);
                    }
                }
                dgv.DataSource = null;
                dgv.DataSource = data;

            }

        }
    }
}
