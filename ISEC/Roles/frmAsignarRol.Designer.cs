namespace ISEC.Roles
{
    partial class frmAsignarRol
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbUsuario = new System.Windows.Forms.ComboBox();
            this.gvAccesos = new System.Windows.Forms.DataGridView();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.cbRoles = new System.Windows.Forms.ComboBox();
            this.chModulos = new System.Windows.Forms.CheckedListBox();
            this.chLectura = new System.Windows.Forms.CheckBox();
            this.chEscritura = new System.Windows.Forms.CheckBox();
            this.chAcceso = new System.Windows.Forms.CheckBox();
            this.btnAddModulo = new System.Windows.Forms.Button();
            this.gvPermisos = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddRol = new System.Windows.Forms.Button();
            this.btnAddUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvAccesos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPermisos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Rol:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Usuario:";
            // 
            // cbUsuario
            // 
            this.cbUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUsuario.FormattingEnabled = true;
            this.cbUsuario.Location = new System.Drawing.Point(90, 66);
            this.cbUsuario.Name = "cbUsuario";
            this.cbUsuario.Size = new System.Drawing.Size(419, 28);
            this.cbUsuario.TabIndex = 14;
            // 
            // gvAccesos
            // 
            this.gvAccesos.AllowUserToAddRows = false;
            this.gvAccesos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Beige;
            this.gvAccesos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvAccesos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvAccesos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvAccesos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvAccesos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvAccesos.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvAccesos.Location = new System.Drawing.Point(16, 232);
            this.gvAccesos.Name = "gvAccesos";
            this.gvAccesos.ReadOnly = true;
            this.gvAccesos.RowHeadersVisible = false;
            this.gvAccesos.RowHeadersWidth = 47;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.gvAccesos.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvAccesos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvAccesos.Size = new System.Drawing.Size(532, 236);
            this.gvAccesos.TabIndex = 30;
            this.gvAccesos.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvAccesos_CellMouseClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.IndianRed;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancelar.Location = new System.Drawing.Point(12, 474);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(121, 35);
            this.btnCancelar.TabIndex = 32;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnAsignar
            // 
            this.btnAsignar.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnAsignar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignar.Location = new System.Drawing.Point(427, 111);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(121, 35);
            this.btnAsignar.TabIndex = 31;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = false;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click_1);
            // 
            // cbRoles
            // 
            this.cbRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRoles.FormattingEnabled = true;
            this.cbRoles.Location = new System.Drawing.Point(90, 12);
            this.cbRoles.Name = "cbRoles";
            this.cbRoles.Size = new System.Drawing.Size(419, 28);
            this.cbRoles.TabIndex = 33;
            // 
            // chModulos
            // 
            this.chModulos.CheckOnClick = true;
            this.chModulos.Enabled = false;
            this.chModulos.FormattingEnabled = true;
            this.chModulos.Location = new System.Drawing.Point(568, 12);
            this.chModulos.MultiColumn = true;
            this.chModulos.Name = "chModulos";
            this.chModulos.Size = new System.Drawing.Size(477, 154);
            this.chModulos.TabIndex = 34;
            this.chModulos.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chModulos_ItemCheck);
            this.chModulos.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chModulos_MouseClick);
            // 
            // chLectura
            // 
            this.chLectura.AutoSize = true;
            this.chLectura.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chLectura.Location = new System.Drawing.Point(1051, 46);
            this.chLectura.Name = "chLectura";
            this.chLectura.Size = new System.Drawing.Size(91, 28);
            this.chLectura.TabIndex = 38;
            this.chLectura.Text = "Lectura";
            this.chLectura.UseVisualStyleBackColor = true;
            // 
            // chEscritura
            // 
            this.chEscritura.AutoSize = true;
            this.chEscritura.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chEscritura.Location = new System.Drawing.Point(1051, 80);
            this.chEscritura.Name = "chEscritura";
            this.chEscritura.Size = new System.Drawing.Size(102, 28);
            this.chEscritura.TabIndex = 37;
            this.chEscritura.Text = "Escritura";
            this.chEscritura.UseVisualStyleBackColor = true;
            // 
            // chAcceso
            // 
            this.chAcceso.AutoSize = true;
            this.chAcceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chAcceso.Location = new System.Drawing.Point(1051, 12);
            this.chAcceso.Name = "chAcceso";
            this.chAcceso.Size = new System.Drawing.Size(93, 28);
            this.chAcceso.TabIndex = 36;
            this.chAcceso.Text = "Acceso";
            this.chAcceso.UseVisualStyleBackColor = true;
            // 
            // btnAddModulo
            // 
            this.btnAddModulo.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnAddModulo.Enabled = false;
            this.btnAddModulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddModulo.Location = new System.Drawing.Point(1051, 123);
            this.btnAddModulo.Name = "btnAddModulo";
            this.btnAddModulo.Size = new System.Drawing.Size(93, 43);
            this.btnAddModulo.TabIndex = 35;
            this.btnAddModulo.Text = "Agregar";
            this.btnAddModulo.UseVisualStyleBackColor = false;
            this.btnAddModulo.Click += new System.EventHandler(this.btnAddModulo_Click);
            // 
            // gvPermisos
            // 
            this.gvPermisos.AllowUserToAddRows = false;
            this.gvPermisos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Beige;
            this.gvPermisos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvPermisos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPermisos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvPermisos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gvPermisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvPermisos.DefaultCellStyle = dataGridViewCellStyle7;
            this.gvPermisos.Enabled = false;
            this.gvPermisos.Location = new System.Drawing.Point(568, 232);
            this.gvPermisos.Name = "gvPermisos";
            this.gvPermisos.ReadOnly = true;
            this.gvPermisos.RowHeadersVisible = false;
            this.gvPermisos.RowHeadersWidth = 47;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            this.gvPermisos.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.gvPermisos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvPermisos.Size = new System.Drawing.Size(574, 236);
            this.gvPermisos.TabIndex = 39;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(564, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 40;
            this.label3.Text = "Permisos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 41;
            this.label4.Text = "Roles-Usuarios";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSave.Location = new System.Drawing.Point(1021, 483);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 35);
            this.btnSave.TabIndex = 42;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddRol
            // 
            this.btnAddRol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.btnAddRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRol.ForeColor = System.Drawing.Color.White;
            this.btnAddRol.Location = new System.Drawing.Point(515, 12);
            this.btnAddRol.Name = "btnAddRol";
            this.btnAddRol.Size = new System.Drawing.Size(33, 28);
            this.btnAddRol.TabIndex = 43;
            this.btnAddRol.Text = "+";
            this.btnAddRol.UseVisualStyleBackColor = false;
            this.btnAddRol.Click += new System.EventHandler(this.btnAddRol_Click);
            // 
            // btnAddUser
            // 
            this.btnAddUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.btnAddUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddUser.ForeColor = System.Drawing.Color.White;
            this.btnAddUser.Location = new System.Drawing.Point(515, 66);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(33, 28);
            this.btnAddUser.TabIndex = 44;
            this.btnAddUser.Text = "+";
            this.btnAddUser.UseVisualStyleBackColor = false;
            // 
            // frmAsignarRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 541);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.btnAddRol);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.gvPermisos);
            this.Controls.Add(this.chLectura);
            this.Controls.Add(this.chEscritura);
            this.Controls.Add(this.chAcceso);
            this.Controls.Add(this.btnAddModulo);
            this.Controls.Add(this.chModulos);
            this.Controls.Add(this.cbRoles);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.gvAccesos);
            this.Controls.Add(this.cbUsuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmAsignarRol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar Rol";
            ((System.ComponentModel.ISupportInitialize)(this.gvAccesos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPermisos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbUsuario;
        private System.Windows.Forms.DataGridView gvAccesos;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.ComboBox cbRoles;
        private System.Windows.Forms.CheckedListBox chModulos;
        private System.Windows.Forms.CheckBox chLectura;
        private System.Windows.Forms.CheckBox chEscritura;
        private System.Windows.Forms.CheckBox chAcceso;
        private System.Windows.Forms.Button btnAddModulo;
        private System.Windows.Forms.DataGridView gvPermisos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAddRol;
        private System.Windows.Forms.Button btnAddUser;
    }
}