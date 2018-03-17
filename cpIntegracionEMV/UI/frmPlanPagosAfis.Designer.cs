namespace cpIntegracionEMV.UI
{
    partial class frmPlanPagosAfis
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
            this.rbContado = new System.Windows.Forms.RadioButton();
            this.rbMSI = new System.Windows.Forms.RadioButton();
            this.rbMCI = new System.Windows.Forms.RadioButton();
            this.gbTipopago = new System.Windows.Forms.GroupBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.gbAfiliacion = new System.Windows.Forms.GroupBox();
            this.lstAfiliacion = new System.Windows.Forms.ListBox();
            this.btnAtras = new System.Windows.Forms.Button();
            this.gbTipopago.SuspendLayout();
            this.gbAfiliacion.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbContado
            // 
            this.rbContado.AutoSize = true;
            this.rbContado.Location = new System.Drawing.Point(16, 32);
            this.rbContado.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbContado.Name = "rbContado";
            this.rbContado.Size = new System.Drawing.Size(72, 22);
            this.rbContado.TabIndex = 1;
            this.rbContado.TabStop = true;
            this.rbContado.Text = "Contado";
            this.rbContado.UseVisualStyleBackColor = true;
            // 
            // rbMSI
            // 
            this.rbMSI.AutoSize = true;
            this.rbMSI.Location = new System.Drawing.Point(16, 72);
            this.rbMSI.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbMSI.Name = "rbMSI";
            this.rbMSI.Size = new System.Drawing.Size(135, 22);
            this.rbMSI.TabIndex = 2;
            this.rbMSI.TabStop = true;
            this.rbMSI.Text = "Meses Sin Intereses";
            this.rbMSI.UseVisualStyleBackColor = true;
            // 
            // rbMCI
            // 
            this.rbMCI.AutoSize = true;
            this.rbMCI.Location = new System.Drawing.Point(17, 110);
            this.rbMCI.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbMCI.Name = "rbMCI";
            this.rbMCI.Size = new System.Drawing.Size(140, 22);
            this.rbMCI.TabIndex = 3;
            this.rbMCI.TabStop = true;
            this.rbMCI.Text = "Meses Con Intereses";
            this.rbMCI.UseVisualStyleBackColor = true;
            // 
            // gbTipopago
            // 
            this.gbTipopago.Controls.Add(this.rbMSI);
            this.gbTipopago.Controls.Add(this.rbContado);
            this.gbTipopago.Controls.Add(this.rbMCI);
            this.gbTipopago.Location = new System.Drawing.Point(16, 16);
            this.gbTipopago.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbTipopago.Name = "gbTipopago";
            this.gbTipopago.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbTipopago.Size = new System.Drawing.Size(199, 168);
            this.gbTipopago.TabIndex = 0;
            this.gbTipopago.TabStop = false;
            this.gbTipopago.Text = "Seleccione tipo de pago";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(120, 192);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(92, 40);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Siguiente";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // gbAfiliacion
            // 
            this.gbAfiliacion.Controls.Add(this.lstAfiliacion);
            this.gbAfiliacion.Location = new System.Drawing.Point(224, 16);
            this.gbAfiliacion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbAfiliacion.Name = "gbAfiliacion";
            this.gbAfiliacion.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbAfiliacion.Size = new System.Drawing.Size(199, 168);
            this.gbAfiliacion.TabIndex = 6;
            this.gbAfiliacion.TabStop = false;
            this.gbAfiliacion.Text = "Seleccione Afiliación";
            // 
            // lstAfiliacion
            // 
            this.lstAfiliacion.FormattingEnabled = true;
            this.lstAfiliacion.IntegralHeight = false;
            this.lstAfiliacion.ItemHeight = 18;
            this.lstAfiliacion.Location = new System.Drawing.Point(9, 24);
            this.lstAfiliacion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstAfiliacion.Name = "lstAfiliacion";
            this.lstAfiliacion.Size = new System.Drawing.Size(182, 128);
            this.lstAfiliacion.TabIndex = 7;
            // 
            // btnAtras
            // 
            this.btnAtras.Location = new System.Drawing.Point(16, 192);
            this.btnAtras.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAtras.Name = "btnAtras";
            this.btnAtras.Size = new System.Drawing.Size(89, 40);
            this.btnAtras.TabIndex = 4;
            this.btnAtras.Text = "Atras";
            this.btnAtras.UseVisualStyleBackColor = true;
            this.btnAtras.Click += new System.EventHandler(this.btnAtras_Click);
            // 
            // frmPlanPagosAfis
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(229, 250);
            this.ControlBox = false;
            this.Controls.Add(this.btnAtras);
            this.Controls.Add(this.gbAfiliacion);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.gbTipopago);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlanPagosAfis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Plan de Pagos";
            this.Load += new System.EventHandler(this.frmPlanPagosAfis_Load);
            this.gbTipopago.ResumeLayout(false);
            this.gbTipopago.PerformLayout();
            this.gbAfiliacion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbContado;
        private System.Windows.Forms.RadioButton rbMSI;
        private System.Windows.Forms.RadioButton rbMCI;
        private System.Windows.Forms.GroupBox gbTipopago;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.GroupBox gbAfiliacion;
        private System.Windows.Forms.Button btnAtras;
        private System.Windows.Forms.ListBox lstAfiliacion;
    }
}