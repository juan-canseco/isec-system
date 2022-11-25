namespace ISEC.Arqueo
{
    partial class frmTemporal
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
            this.lblGastosTotales = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalEnCaja = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblGastosTotales
            // 
            this.lblGastosTotales.AutoSize = true;
            this.lblGastosTotales.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.32727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGastosTotales.Location = new System.Drawing.Point(265, 108);
            this.lblGastosTotales.Name = "lblGastosTotales";
            this.lblGastosTotales.Size = new System.Drawing.Size(27, 29);
            this.lblGastosTotales.TabIndex = 80;
            this.lblGastosTotales.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.32727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 29);
            this.label2.TabIndex = 79;
            this.label2.Text = "GASTOS TOTALES";
            // 
            // lblTotalEnCaja
            // 
            this.lblTotalEnCaja.AutoSize = true;
            this.lblTotalEnCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.32727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalEnCaja.Location = new System.Drawing.Point(265, 67);
            this.lblTotalEnCaja.Name = "lblTotalEnCaja";
            this.lblTotalEnCaja.Size = new System.Drawing.Size(27, 29);
            this.lblTotalEnCaja.TabIndex = 78;
            this.lblTotalEnCaja.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.32727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(207, 29);
            this.label6.TabIndex = 77;
            this.label6.Text = "TOTAL EN CAJA";
            // 
            // frmTemporal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblGastosTotales);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTotalEnCaja);
            this.Controls.Add(this.label6);
            this.Name = "frmTemporal";
            this.Text = "frmTemporal";
            this.Load += new System.EventHandler(this.frmTemporal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGastosTotales;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalEnCaja;
        private System.Windows.Forms.Label label6;
    }
}