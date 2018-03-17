namespace cpIntegracionEMV.UI
{
    partial class frmConversorDCC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConversorDCC));
            this.Frame1 = new System.Windows.Forms.GroupBox();
            this.lblMonedaCambio = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMontoConvertir = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTipCam = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboxTipoMoneda = new System.Windows.Forms.ComboBox();
            this.cboxAfiliaciones = new System.Windows.Forms.ComboBox();
            this.lblLeyenda = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdMonedas = new System.Windows.Forms.Button();
            this.Frame2 = new System.Windows.Forms.GroupBox();
            this.panelMonedas = new System.Windows.Forms.Panel();
            this.cmdRegresar = new System.Windows.Forms.Button();
            this.Frame1.SuspendLayout();
            this.Frame2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Frame1
            // 
            this.Frame1.Controls.Add(this.lblMonedaCambio);
            this.Frame1.Controls.Add(this.lblResultado);
            this.Frame1.Controls.Add(this.label6);
            this.Frame1.Controls.Add(this.txtMontoConvertir);
            this.Frame1.Controls.Add(this.label5);
            this.Frame1.Controls.Add(this.label4);
            this.Frame1.Controls.Add(this.label3);
            this.Frame1.Controls.Add(this.lblTipCam);
            this.Frame1.Controls.Add(this.label2);
            this.Frame1.Controls.Add(this.cmboxTipoMoneda);
            this.Frame1.Controls.Add(this.cboxAfiliaciones);
            this.Frame1.Controls.Add(this.lblLeyenda);
            this.Frame1.Controls.Add(this.label1);
            this.Frame1.Controls.Add(this.cmdMonedas);
            this.Frame1.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame1.Location = new System.Drawing.Point(16, 8);
            this.Frame1.Name = "Frame1";
            this.Frame1.Size = new System.Drawing.Size(285, 345);
            this.Frame1.TabIndex = 0;
            this.Frame1.TabStop = false;
            this.Frame1.Text = "Centro de Pagos";
            // 
            // lblMonedaCambio
            // 
            this.lblMonedaCambio.Font = new System.Drawing.Font("Trebuchet MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonedaCambio.Location = new System.Drawing.Point(176, 208);
            this.lblMonedaCambio.Name = "lblMonedaCambio";
            this.lblMonedaCambio.Size = new System.Drawing.Size(56, 24);
            this.lblMonedaCambio.TabIndex = 13;
            this.lblMonedaCambio.Text = ".";
            // 
            // lblResultado
            // 
            this.lblResultado.Font = new System.Drawing.Font("Trebuchet MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultado.Location = new System.Drawing.Point(40, 208);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(128, 24);
            this.lblResultado.TabIndex = 12;
            this.lblResultado.Text = ".";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(176, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 24);
            this.label6.TabIndex = 11;
            this.label6.Text = "MXN";
            this.label6.Visible = false;
            // 
            // txtMontoConvertir
            // 
            this.txtMontoConvertir.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMontoConvertir.Location = new System.Drawing.Point(32, 176);
            this.txtMontoConvertir.MaxLength = 10;
            this.txtMontoConvertir.Name = "txtMontoConvertir";
            this.txtMontoConvertir.Size = new System.Drawing.Size(136, 21);
            this.txtMontoConvertir.TabIndex = 10;
            this.txtMontoConvertir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontoConvertir_KeyPress);
            this.txtMontoConvertir.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMontoConvertir_KeyUp);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "A";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "$";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Cantidad a convertir";
            // 
            // lblTipCam
            // 
            this.lblTipCam.Font = new System.Drawing.Font("Trebuchet MS", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipCam.Location = new System.Drawing.Point(176, 96);
            this.lblTipCam.Name = "lblTipCam";
            this.lblTipCam.Size = new System.Drawing.Size(96, 32);
            this.lblTipCam.TabIndex = 6;
            this.lblTipCam.Text = "moneda";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(176, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tipo de cambio:";
            // 
            // cmboxTipoMoneda
            // 
            this.cmboxTipoMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxTipoMoneda.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboxTipoMoneda.FormattingEnabled = true;
            this.cmboxTipoMoneda.Items.AddRange(new object[] {
            "-- Seleccione Moneda --"});
            this.cmboxTipoMoneda.Location = new System.Drawing.Point(16, 96);
            this.cmboxTipoMoneda.Name = "cmboxTipoMoneda";
            this.cmboxTipoMoneda.Size = new System.Drawing.Size(152, 26);
            this.cmboxTipoMoneda.TabIndex = 4;
            this.cmboxTipoMoneda.SelectedIndexChanged += new System.EventHandler(this.cmboxTipoMoneda_SelectedIndexChanged);
            // 
            // cboxAfiliaciones
            // 
            this.cboxAfiliaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxAfiliaciones.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxAfiliaciones.FormattingEnabled = true;
            this.cboxAfiliaciones.Location = new System.Drawing.Point(16, 35);
            this.cboxAfiliaciones.Name = "cboxAfiliaciones";
            this.cboxAfiliaciones.Size = new System.Drawing.Size(256, 26);
            this.cboxAfiliaciones.TabIndex = 3;
            this.cboxAfiliaciones.SelectedIndexChanged += new System.EventHandler(this.cboxAfiliaciones_SelectedIndexChanged);
            // 
            // lblLeyenda
            // 
            this.lblLeyenda.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeyenda.Location = new System.Drawing.Point(8, 272);
            this.lblLeyenda.Name = "lblLeyenda";
            this.lblLeyenda.Size = new System.Drawing.Size(264, 25);
            this.lblLeyenda.TabIndex = 2;
            this.lblLeyenda.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fecha Actualizacion";
            // 
            // cmdMonedas
            // 
            this.cmdMonedas.Enabled = false;
            this.cmdMonedas.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMonedas.Location = new System.Drawing.Point(195, 304);
            this.cmdMonedas.Name = "cmdMonedas";
            this.cmdMonedas.Size = new System.Drawing.Size(80, 32);
            this.cmdMonedas.TabIndex = 0;
            this.cmdMonedas.Text = "Monedas";
            this.cmdMonedas.UseVisualStyleBackColor = true;
            this.cmdMonedas.Click += new System.EventHandler(this.cmdMonedas_Click);
            // 
            // Frame2
            // 
            this.Frame2.Controls.Add(this.panelMonedas);
            this.Frame2.Controls.Add(this.cmdRegresar);
            this.Frame2.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frame2.Location = new System.Drawing.Point(328, 8);
            this.Frame2.Name = "Frame2";
            this.Frame2.Size = new System.Drawing.Size(285, 345);
            this.Frame2.TabIndex = 1;
            this.Frame2.TabStop = false;
            this.Frame2.Text = "Centro de Pagos";
            this.Frame2.Visible = false;
            // 
            // panelMonedas
            // 
            this.panelMonedas.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelMonedas.Location = new System.Drawing.Point(8, 32);
            this.panelMonedas.Name = "panelMonedas";
            this.panelMonedas.Size = new System.Drawing.Size(264, 264);
            this.panelMonedas.TabIndex = 1;
            // 
            // cmdRegresar
            // 
            this.cmdRegresar.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRegresar.Location = new System.Drawing.Point(195, 304);
            this.cmdRegresar.Name = "cmdRegresar";
            this.cmdRegresar.Size = new System.Drawing.Size(80, 32);
            this.cmdRegresar.TabIndex = 0;
            this.cmdRegresar.Text = "Regresar";
            this.cmdRegresar.UseVisualStyleBackColor = true;
            this.cmdRegresar.Click += new System.EventHandler(this.cmdRegresar_Click);
            // 
            // frmConversorDCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(667, 370);
            this.Controls.Add(this.Frame2);
            this.Controls.Add(this.Frame1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConversorDCC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Conversor DCC";
            this.Load += new System.EventHandler(this.frmConversorDCC_Load);
            this.Frame1.ResumeLayout(false);
            this.Frame1.PerformLayout();
            this.Frame2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Frame1;
        private System.Windows.Forms.GroupBox Frame2;
        private System.Windows.Forms.Button cmdMonedas;
        private System.Windows.Forms.Button cmdRegresar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLeyenda;
        private System.Windows.Forms.ComboBox cboxAfiliaciones;
        private System.Windows.Forms.ComboBox cmboxTipoMoneda;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTipCam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMontoConvertir;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Label lblMonedaCambio;
        private System.Windows.Forms.Panel panelMonedas;

    }
}