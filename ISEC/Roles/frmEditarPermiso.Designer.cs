namespace ISEC.Roles
{
    partial class frmEditarPermiso
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
            this.chLectura = new System.Windows.Forms.CheckBox();
            this.chEscritura = new System.Windows.Forms.CheckBox();
            this.chAcceso = new System.Windows.Forms.CheckBox();
            this.btnEditar = new System.Windows.Forms.Button();
            this.lblModulo = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chLectura
            // 
            this.chLectura.AutoSize = true;
            this.chLectura.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.74545F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chLectura.Location = new System.Drawing.Point(21, 119);
            this.chLectura.Name = "chLectura";
            this.chLectura.Size = new System.Drawing.Size(103, 29);
            this.chLectura.TabIndex = 60;
            this.chLectura.Text = "Lectura";
            this.chLectura.UseVisualStyleBackColor = true;
            // 
            // chEscritura
            // 
            this.chEscritura.AutoSize = true;
            this.chEscritura.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.74545F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chEscritura.Location = new System.Drawing.Point(21, 163);
            this.chEscritura.Name = "chEscritura";
            this.chEscritura.Size = new System.Drawing.Size(116, 29);
            this.chEscritura.TabIndex = 59;
            this.chEscritura.Text = "Escritura";
            this.chEscritura.UseVisualStyleBackColor = true;
            // 
            // chAcceso
            // 
            this.chAcceso.AutoSize = true;
            this.chAcceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.74545F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chAcceso.Location = new System.Drawing.Point(21, 73);
            this.chAcceso.Name = "chAcceso";
            this.chAcceso.Size = new System.Drawing.Size(102, 29);
            this.chAcceso.TabIndex = 58;
            this.chAcceso.Text = "Acceso";
            this.chAcceso.UseVisualStyleBackColor = true;
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.Location = new System.Drawing.Point(295, 214);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(102, 43);
            this.btnEditar.TabIndex = 57;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = true;
            this.lblModulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.74545F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModulo.Location = new System.Drawing.Point(148, 25);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Size = new System.Drawing.Size(70, 25);
            this.lblModulo.TabIndex = 61;
            this.lblModulo.Text = "label1";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.IndianRed;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancelar.Location = new System.Drawing.Point(172, 214);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(102, 43);
            this.btnCancelar.TabIndex = 62;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmEditarPermiso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 269);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lblModulo);
            this.Controls.Add(this.chLectura);
            this.Controls.Add(this.chEscritura);
            this.Controls.Add(this.chAcceso);
            this.Controls.Add(this.btnEditar);
            this.Name = "frmEditarPermiso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editar permiso";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chLectura;
        private System.Windows.Forms.CheckBox chEscritura;
        private System.Windows.Forms.CheckBox chAcceso;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.Button btnCancelar;
    }
}