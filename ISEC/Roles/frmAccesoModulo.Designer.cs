namespace ISEC.Roles
{
    partial class frmAccesoModulo
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
            this.btnAddModulo = new System.Windows.Forms.Button();
            this.lblModulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chLectura
            // 
            this.chLectura.AutoSize = true;
            this.chLectura.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chLectura.Location = new System.Drawing.Point(12, 160);
            this.chLectura.Name = "chLectura";
            this.chLectura.Size = new System.Drawing.Size(91, 28);
            this.chLectura.TabIndex = 11;
            this.chLectura.Text = "Lectura";
            this.chLectura.UseVisualStyleBackColor = true;
            // 
            // chEscritura
            // 
            this.chEscritura.AutoSize = true;
            this.chEscritura.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chEscritura.Location = new System.Drawing.Point(12, 210);
            this.chEscritura.Name = "chEscritura";
            this.chEscritura.Size = new System.Drawing.Size(102, 28);
            this.chEscritura.TabIndex = 10;
            this.chEscritura.Text = "Escritura";
            this.chEscritura.UseVisualStyleBackColor = true;
            // 
            // chAcceso
            // 
            this.chAcceso.AutoSize = true;
            this.chAcceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chAcceso.Location = new System.Drawing.Point(12, 99);
            this.chAcceso.Name = "chAcceso";
            this.chAcceso.Size = new System.Drawing.Size(93, 28);
            this.chAcceso.TabIndex = 9;
            this.chAcceso.Text = "Acceso";
            this.chAcceso.UseVisualStyleBackColor = true;
            // 
            // btnAddModulo
            // 
            this.btnAddModulo.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnAddModulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddModulo.Location = new System.Drawing.Point(158, 160);
            this.btnAddModulo.Name = "btnAddModulo";
            this.btnAddModulo.Size = new System.Drawing.Size(121, 78);
            this.btnAddModulo.TabIndex = 8;
            this.btnAddModulo.Text = "Agregar";
            this.btnAddModulo.UseVisualStyleBackColor = false;
            this.btnAddModulo.Click += new System.EventHandler(this.btnAddModulo_Click);
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = true;
            this.lblModulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModulo.Location = new System.Drawing.Point(109, 30);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Size = new System.Drawing.Size(0, 20);
            this.lblModulo.TabIndex = 12;
            // 
            // frmAccesoModulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 254);
            this.Controls.Add(this.lblModulo);
            this.Controls.Add(this.chLectura);
            this.Controls.Add(this.chEscritura);
            this.Controls.Add(this.chAcceso);
            this.Controls.Add(this.btnAddModulo);
            this.Name = "frmAccesoModulo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAccesoModulo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chLectura;
        private System.Windows.Forms.CheckBox chEscritura;
        private System.Windows.Forms.CheckBox chAcceso;
        private System.Windows.Forms.Button btnAddModulo;
        private System.Windows.Forms.Label lblModulo;
    }
}