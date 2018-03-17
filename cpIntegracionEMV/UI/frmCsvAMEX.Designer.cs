namespace cpIntegracionEMV.UI
{
    partial class frmCsvAMEX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCsvAMEX));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCsv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCero = new System.Windows.Forms.Button();
            this.cmdUno = new System.Windows.Forms.Button();
            this.cmdDos = new System.Windows.Forms.Button();
            this.cmdTres = new System.Windows.Forms.Button();
            this.cmdCuatro = new System.Windows.Forms.Button();
            this.cmdNueve = new System.Windows.Forms.Button();
            this.cmdOcho = new System.Windows.Forms.Button();
            this.cmdSiete = new System.Windows.Forms.Button();
            this.cmdSeis = new System.Windows.Forms.Button();
            this.cmdCinco = new System.Windows.Forms.Button();
            this.CmdAceptar = new System.Windows.Forms.Button();
            this.cmdBorrar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtCsv);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(16, 16);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 64);
            this.panel1.TabIndex = 0;
            // 
            // txtCsv
            // 
            this.txtCsv.Location = new System.Drawing.Point(124, 20);
            this.txtCsv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCsv.MaxLength = 4;
            this.txtCsv.Name = "txtCsv";
            this.txtCsv.PasswordChar = '*';
            this.txtCsv.Size = new System.Drawing.Size(116, 21);
            this.txtCsv.TabIndex = 0;
            this.txtCsv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCsv_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Introduzca cvv:";
            // 
            // cmdCero
            // 
            this.cmdCero.Location = new System.Drawing.Point(16, 96);
            this.cmdCero.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCero.Name = "cmdCero";
            this.cmdCero.Size = new System.Drawing.Size(41, 42);
            this.cmdCero.TabIndex = 1;
            this.cmdCero.Text = "0";
            this.cmdCero.UseVisualStyleBackColor = true;
            this.cmdCero.Click += new System.EventHandler(this.cmdCero_Click);
            // 
            // cmdUno
            // 
            this.cmdUno.Location = new System.Drawing.Point(70, 96);
            this.cmdUno.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdUno.Name = "cmdUno";
            this.cmdUno.Size = new System.Drawing.Size(41, 42);
            this.cmdUno.TabIndex = 2;
            this.cmdUno.Text = "1";
            this.cmdUno.UseVisualStyleBackColor = true;
            this.cmdUno.Click += new System.EventHandler(this.cmdUno_Click);
            // 
            // cmdDos
            // 
            this.cmdDos.Location = new System.Drawing.Point(125, 96);
            this.cmdDos.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdDos.Name = "cmdDos";
            this.cmdDos.Size = new System.Drawing.Size(41, 42);
            this.cmdDos.TabIndex = 3;
            this.cmdDos.Text = "2";
            this.cmdDos.UseVisualStyleBackColor = true;
            this.cmdDos.Click += new System.EventHandler(this.cmdDos_Click);
            // 
            // cmdTres
            // 
            this.cmdTres.Location = new System.Drawing.Point(179, 96);
            this.cmdTres.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdTres.Name = "cmdTres";
            this.cmdTres.Size = new System.Drawing.Size(41, 42);
            this.cmdTres.TabIndex = 4;
            this.cmdTres.Text = "3";
            this.cmdTres.UseVisualStyleBackColor = true;
            this.cmdTres.Click += new System.EventHandler(this.cmdTres_Click);
            // 
            // cmdCuatro
            // 
            this.cmdCuatro.Location = new System.Drawing.Point(232, 96);
            this.cmdCuatro.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCuatro.Name = "cmdCuatro";
            this.cmdCuatro.Size = new System.Drawing.Size(41, 42);
            this.cmdCuatro.TabIndex = 5;
            this.cmdCuatro.Text = "4";
            this.cmdCuatro.UseVisualStyleBackColor = true;
            this.cmdCuatro.Click += new System.EventHandler(this.cmdCuatro_Click);
            // 
            // cmdNueve
            // 
            this.cmdNueve.Location = new System.Drawing.Point(232, 145);
            this.cmdNueve.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdNueve.Name = "cmdNueve";
            this.cmdNueve.Size = new System.Drawing.Size(41, 42);
            this.cmdNueve.TabIndex = 10;
            this.cmdNueve.Text = "9";
            this.cmdNueve.UseVisualStyleBackColor = true;
            this.cmdNueve.Click += new System.EventHandler(this.cmdNueve_Click);
            // 
            // cmdOcho
            // 
            this.cmdOcho.Location = new System.Drawing.Point(179, 145);
            this.cmdOcho.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdOcho.Name = "cmdOcho";
            this.cmdOcho.Size = new System.Drawing.Size(41, 42);
            this.cmdOcho.TabIndex = 9;
            this.cmdOcho.Text = "8";
            this.cmdOcho.UseVisualStyleBackColor = true;
            this.cmdOcho.Click += new System.EventHandler(this.cmdOcho_Click);
            // 
            // cmdSiete
            // 
            this.cmdSiete.Location = new System.Drawing.Point(125, 145);
            this.cmdSiete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdSiete.Name = "cmdSiete";
            this.cmdSiete.Size = new System.Drawing.Size(41, 42);
            this.cmdSiete.TabIndex = 8;
            this.cmdSiete.Text = "7";
            this.cmdSiete.UseVisualStyleBackColor = true;
            this.cmdSiete.Click += new System.EventHandler(this.cmdSiete_Click);
            // 
            // cmdSeis
            // 
            this.cmdSeis.Location = new System.Drawing.Point(70, 145);
            this.cmdSeis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdSeis.Name = "cmdSeis";
            this.cmdSeis.Size = new System.Drawing.Size(41, 42);
            this.cmdSeis.TabIndex = 7;
            this.cmdSeis.Text = "6";
            this.cmdSeis.UseVisualStyleBackColor = true;
            this.cmdSeis.Click += new System.EventHandler(this.cmdSeis_Click);
            // 
            // cmdCinco
            // 
            this.cmdCinco.Location = new System.Drawing.Point(16, 145);
            this.cmdCinco.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCinco.Name = "cmdCinco";
            this.cmdCinco.Size = new System.Drawing.Size(41, 42);
            this.cmdCinco.TabIndex = 6;
            this.cmdCinco.Text = "5";
            this.cmdCinco.UseVisualStyleBackColor = true;
            this.cmdCinco.Click += new System.EventHandler(this.cmdCinco_Click);
            // 
            // CmdAceptar
            // 
            this.CmdAceptar.Location = new System.Drawing.Point(15, 198);
            this.CmdAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CmdAceptar.Name = "CmdAceptar";
            this.CmdAceptar.Size = new System.Drawing.Size(94, 36);
            this.CmdAceptar.TabIndex = 11;
            this.CmdAceptar.Text = "Aceptar";
            this.CmdAceptar.UseVisualStyleBackColor = true;
            this.CmdAceptar.Click += new System.EventHandler(this.CmdAceptar_Click);
            // 
            // cmdBorrar
            // 
            this.cmdBorrar.Location = new System.Drawing.Point(179, 198);
            this.cmdBorrar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdBorrar.Name = "cmdBorrar";
            this.cmdBorrar.Size = new System.Drawing.Size(94, 36);
            this.cmdBorrar.TabIndex = 12;
            this.cmdBorrar.Text = "Borrar";
            this.cmdBorrar.UseVisualStyleBackColor = true;
            this.cmdBorrar.Click += new System.EventHandler(this.cmdBorrar_Click);
            // 
            // frmCsvAMEX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(295, 256);
            this.Controls.Add(this.cmdBorrar);
            this.Controls.Add(this.CmdAceptar);
            this.Controls.Add(this.cmdNueve);
            this.Controls.Add(this.cmdOcho);
            this.Controls.Add(this.cmdSiete);
            this.Controls.Add(this.cmdSeis);
            this.Controls.Add(this.cmdCinco);
            this.Controls.Add(this.cmdCuatro);
            this.Controls.Add(this.cmdTres);
            this.Controls.Add(this.cmdDos);
            this.Controls.Add(this.cmdUno);
            this.Controls.Add(this.cmdCero);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCsvAMEX";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Introduzca el cvv de su tarjeta AMEX";
            this.Load += new System.EventHandler(this.frmCsvAMEX_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCsv;
        private System.Windows.Forms.Button cmdCero;
        private System.Windows.Forms.Button cmdUno;
        private System.Windows.Forms.Button cmdDos;
        private System.Windows.Forms.Button cmdTres;
        private System.Windows.Forms.Button cmdCuatro;
        private System.Windows.Forms.Button cmdNueve;
        private System.Windows.Forms.Button cmdOcho;
        private System.Windows.Forms.Button cmdSiete;
        private System.Windows.Forms.Button cmdSeis;
        private System.Windows.Forms.Button cmdCinco;
        private System.Windows.Forms.Button CmdAceptar;
        private System.Windows.Forms.Button cmdBorrar;
    }
}