namespace cpIntegracionEMV.UI
{
    partial class frmAvisoPinPad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAvisoPinPad));
            this.cmdCerrar = new System.Windows.Forms.Button();
            this.chkConf = new System.Windows.Forms.CheckBox();
            this.lblAviso = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdCerrar
            // 
            this.cmdCerrar.Location = new System.Drawing.Point(368, 104);
            this.cmdCerrar.Name = "cmdCerrar";
            this.cmdCerrar.Size = new System.Drawing.Size(80, 32);
            this.cmdCerrar.TabIndex = 0;
            this.cmdCerrar.Text = "Cerrar";
            this.cmdCerrar.UseVisualStyleBackColor = true;
            this.cmdCerrar.Click += new System.EventHandler(this.cmdCerrar_Click);
            // 
            // chkConf
            // 
            this.chkConf.AutoSize = true;
            this.chkConf.Location = new System.Drawing.Point(16, 112);
            this.chkConf.Name = "chkConf";
            this.chkConf.Size = new System.Drawing.Size(211, 22);
            this.chkConf.TabIndex = 1;
            this.chkConf.Text = "No volver a mostrar este mensaje";
            this.chkConf.UseVisualStyleBackColor = true;
            // 
            // lblAviso
            // 
            this.lblAviso.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAviso.Location = new System.Drawing.Point(16, 16);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(432, 88);
            this.lblAviso.TabIndex = 2;
            this.lblAviso.Text = "El proceso de actualización no se completo correctamente. Favor de llamar a su";
            // 
            // frmAvisoPinPad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(465, 145);
            this.Controls.Add(this.lblAviso);
            this.Controls.Add(this.chkConf);
            this.Controls.Add(this.cmdCerrar);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAvisoPinPad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Centro de Pagos - Aviso";
            this.Load += new System.EventHandler(this.frmAvisoPinPad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCerrar;
        private System.Windows.Forms.CheckBox chkConf;
        private System.Windows.Forms.Label lblAviso;
    }
}