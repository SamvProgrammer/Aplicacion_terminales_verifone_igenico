namespace cpIntegracionEMV.UI
{
    partial class frmFirmaenPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFirmaenPanel));
            this.Frame1 = new System.Windows.Forms.Panel();
            this.cmdCancelar = new System.Windows.Forms.Button();
            this.cmdAceptar = new System.Windows.Forms.Button();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.lblMail = new System.Windows.Forms.Label();
            this.Frame2 = new System.Windows.Forms.Panel();
            this.cmdSalir = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.Frame1.SuspendLayout();
            this.Frame2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Frame1
            // 
            this.Frame1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Frame1.Controls.Add(this.cmdCancelar);
            this.Frame1.Controls.Add(this.cmdAceptar);
            this.Frame1.Controls.Add(this.txtMail);
            this.Frame1.Controls.Add(this.lblMail);
            this.Frame1.Location = new System.Drawing.Point(16, 16);
            this.Frame1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Frame1.Name = "Frame1";
            this.Frame1.Size = new System.Drawing.Size(405, 120);
            this.Frame1.TabIndex = 0;
            // 
            // cmdCancelar
            // 
            this.cmdCancelar.Location = new System.Drawing.Point(200, 72);
            this.cmdCancelar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdCancelar.Name = "cmdCancelar";
            this.cmdCancelar.Size = new System.Drawing.Size(80, 30);
            this.cmdCancelar.TabIndex = 3;
            this.cmdCancelar.Text = "Cancelar";
            this.cmdCancelar.UseVisualStyleBackColor = true;
            this.cmdCancelar.Click += new System.EventHandler(this.cmdCancelar_Click);
            // 
            // cmdAceptar
            // 
            this.cmdAceptar.Location = new System.Drawing.Point(112, 72);
            this.cmdAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdAceptar.Name = "cmdAceptar";
            this.cmdAceptar.Size = new System.Drawing.Size(80, 30);
            this.cmdAceptar.TabIndex = 2;
            this.cmdAceptar.Text = "Aceptar";
            this.cmdAceptar.UseVisualStyleBackColor = true;
            this.cmdAceptar.Click += new System.EventHandler(this.cmdAceptar_Click);
            // 
            // txtMail
            // 
            this.txtMail.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtMail.Location = new System.Drawing.Point(80, 39);
            this.txtMail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(232, 20);
            this.txtMail.TabIndex = 1;
            // 
            // lblMail
            // 
            this.lblMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMail.ForeColor = System.Drawing.Color.Red;
            this.lblMail.Location = new System.Drawing.Point(80, 10);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(240, 30);
            this.lblMail.TabIndex = 0;
            this.lblMail.Text = "Introduzca el correo electrónico";
            // 
            // Frame2
            // 
            this.Frame2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Frame2.Controls.Add(this.cmdSalir);
            this.Frame2.Controls.Add(this.lblMensaje);
            this.Frame2.Location = new System.Drawing.Point(16, 144);
            this.Frame2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Frame2.Name = "Frame2";
            this.Frame2.Size = new System.Drawing.Size(405, 65);
            this.Frame2.TabIndex = 1;
            // 
            // cmdSalir
            // 
            this.cmdSalir.Location = new System.Drawing.Point(312, 20);
            this.cmdSalir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmdSalir.Name = "cmdSalir";
            this.cmdSalir.Size = new System.Drawing.Size(80, 30);
            this.cmdSalir.TabIndex = 1;
            this.cmdSalir.Text = "Salir";
            this.cmdSalir.UseVisualStyleBackColor = true;
            this.cmdSalir.Visible = false;
            this.cmdSalir.Click += new System.EventHandler(this.cmdSalir_Click);
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.ForeColor = System.Drawing.Color.Blue;
            this.lblMensaje.Location = new System.Drawing.Point(16, 10);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(376, 49);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "Indica el correo electrónico al que se desea enviar Voucher Cliente. Haz clic en " +
    "el botón \"Aceptar\"";
            // 
            // frmFirmaenPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(440, 228);
            this.Controls.Add(this.Frame2);
            this.Controls.Add(this.Frame1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFirmaenPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Envío Copia Cliente";
            this.Load += new System.EventHandler(this.frmFirmaenPanel_Load);
            this.Frame1.ResumeLayout(false);
            this.Frame1.PerformLayout();
            this.Frame2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Frame1;
        private System.Windows.Forms.Panel Frame2;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Button cmdCancelar;
        private System.Windows.Forms.Button cmdAceptar;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button cmdSalir;
    }
}