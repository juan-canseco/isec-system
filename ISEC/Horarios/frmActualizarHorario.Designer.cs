namespace ISEC.Horarios
{
    partial class frmActualizarHorario
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
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtHoraFinal = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtHoraInicial = new System.Windows.Forms.DateTimePicker();
            this.chDomingo = new System.Windows.Forms.CheckBox();
            this.chSabado = new System.Windows.Forms.CheckBox();
            this.chViernes = new System.Windows.Forms.CheckBox();
            this.chJueves = new System.Windows.Forms.CheckBox();
            this.chMiercoles = new System.Windows.Forms.CheckBox();
            this.chMartes = new System.Windows.Forms.CheckBox();
            this.chLunes = new System.Windows.Forms.CheckBox();
            this.chActivo = new System.Windows.Forms.CheckBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(18, 43);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(252, 27);
            this.txtDescripcion.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "Descripción";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 343);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "Fin";
            // 
            // dtHoraFinal
            // 
            this.dtHoraFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtHoraFinal.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtHoraFinal.Location = new System.Drawing.Point(18, 378);
            this.dtHoraFinal.Name = "dtHoraFinal";
            this.dtHoraFinal.Size = new System.Drawing.Size(252, 27);
            this.dtHoraFinal.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "Inicio";
            // 
            // dtHoraInicial
            // 
            this.dtHoraInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtHoraInicial.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtHoraInicial.Location = new System.Drawing.Point(18, 310);
            this.dtHoraInicial.Name = "dtHoraInicial";
            this.dtHoraInicial.Size = new System.Drawing.Size(252, 27);
            this.dtHoraInicial.TabIndex = 22;
            // 
            // chDomingo
            // 
            this.chDomingo.AutoSize = true;
            this.chDomingo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chDomingo.Location = new System.Drawing.Point(155, 182);
            this.chDomingo.Name = "chDomingo";
            this.chDomingo.Size = new System.Drawing.Size(95, 24);
            this.chDomingo.TabIndex = 21;
            this.chDomingo.Text = "Domingo";
            this.chDomingo.UseVisualStyleBackColor = true;
            // 
            // chSabado
            // 
            this.chSabado.AutoSize = true;
            this.chSabado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chSabado.Location = new System.Drawing.Point(155, 133);
            this.chSabado.Name = "chSabado";
            this.chSabado.Size = new System.Drawing.Size(84, 24);
            this.chSabado.TabIndex = 20;
            this.chSabado.Text = "Sabado";
            this.chSabado.UseVisualStyleBackColor = true;
            // 
            // chViernes
            // 
            this.chViernes.AutoSize = true;
            this.chViernes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chViernes.Location = new System.Drawing.Point(155, 86);
            this.chViernes.Name = "chViernes";
            this.chViernes.Size = new System.Drawing.Size(85, 24);
            this.chViernes.TabIndex = 19;
            this.chViernes.Text = "Viernes";
            this.chViernes.UseVisualStyleBackColor = true;
            // 
            // chJueves
            // 
            this.chJueves.AutoSize = true;
            this.chJueves.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chJueves.Location = new System.Drawing.Point(18, 229);
            this.chJueves.Name = "chJueves";
            this.chJueves.Size = new System.Drawing.Size(81, 24);
            this.chJueves.TabIndex = 18;
            this.chJueves.Text = "Jueves";
            this.chJueves.UseVisualStyleBackColor = true;
            // 
            // chMiercoles
            // 
            this.chMiercoles.AutoSize = true;
            this.chMiercoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chMiercoles.Location = new System.Drawing.Point(18, 182);
            this.chMiercoles.Name = "chMiercoles";
            this.chMiercoles.Size = new System.Drawing.Size(101, 24);
            this.chMiercoles.TabIndex = 17;
            this.chMiercoles.Text = "Miercoles";
            this.chMiercoles.UseVisualStyleBackColor = true;
            // 
            // chMartes
            // 
            this.chMartes.AutoSize = true;
            this.chMartes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chMartes.Location = new System.Drawing.Point(18, 133);
            this.chMartes.Name = "chMartes";
            this.chMartes.Size = new System.Drawing.Size(80, 24);
            this.chMartes.TabIndex = 16;
            this.chMartes.Text = "Martes";
            this.chMartes.UseVisualStyleBackColor = true;
            // 
            // chLunes
            // 
            this.chLunes.AutoSize = true;
            this.chLunes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chLunes.Location = new System.Drawing.Point(18, 86);
            this.chLunes.Name = "chLunes";
            this.chLunes.Size = new System.Drawing.Size(74, 24);
            this.chLunes.TabIndex = 15;
            this.chLunes.Text = "Lunes";
            this.chLunes.UseVisualStyleBackColor = true;
            // 
            // chActivo
            // 
            this.chActivo.AutoSize = true;
            this.chActivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chActivo.Location = new System.Drawing.Point(18, 421);
            this.chActivo.Name = "chActivo";
            this.chActivo.Size = new System.Drawing.Size(74, 24);
            this.chActivo.TabIndex = 30;
            this.chActivo.Text = "Activo";
            this.chActivo.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.IndianRed;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(155, 459);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnCancelar.Size = new System.Drawing.Size(115, 42);
            this.btnCancelar.TabIndex = 27;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActualizar.Location = new System.Drawing.Point(18, 459);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnActualizar.Size = new System.Drawing.Size(120, 42);
            this.btnActualizar.TabIndex = 26;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // frmActualizarHorario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 513);
            this.Controls.Add(this.chActivo);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtHoraFinal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtHoraInicial);
            this.Controls.Add(this.chDomingo);
            this.Controls.Add(this.chSabado);
            this.Controls.Add(this.chViernes);
            this.Controls.Add(this.chJueves);
            this.Controls.Add(this.chMiercoles);
            this.Controls.Add(this.chMartes);
            this.Controls.Add(this.chLunes);
            this.Name = "frmActualizarHorario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MODIFICAR HORARIO";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtHoraFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtHoraInicial;
        private System.Windows.Forms.CheckBox chDomingo;
        private System.Windows.Forms.CheckBox chSabado;
        private System.Windows.Forms.CheckBox chViernes;
        private System.Windows.Forms.CheckBox chJueves;
        private System.Windows.Forms.CheckBox chMiercoles;
        private System.Windows.Forms.CheckBox chMartes;
        private System.Windows.Forms.CheckBox chLunes;
        private System.Windows.Forms.CheckBox chActivo;
    }
}