namespace cpIntegracionEMV.UI
{
    partial class frmCapacidadTouch
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
            this.cmdAceptar = new System.Windows.Forms.Button();
            this.cmdCancelar = new System.Windows.Forms.Button();
            this.chkTouch = new System.Windows.Forms.CheckBox();
            this.chkMail = new System.Windows.Forms.CheckBox();
            this.imageMail = new System.Windows.Forms.PictureBox();
            this.pictureCompro = new System.Windows.Forms.PictureBox();
            this.lblComprobante = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageMail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCompro)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdAceptar
            // 
            this.cmdAceptar.Location = new System.Drawing.Point(48, 96);
            this.cmdAceptar.Name = "cmdAceptar";
            this.cmdAceptar.Size = new System.Drawing.Size(88, 32);
            this.cmdAceptar.TabIndex = 0;
            this.cmdAceptar.Text = "Aceptar";
            this.cmdAceptar.UseVisualStyleBackColor = true;
            this.cmdAceptar.Click += new System.EventHandler(this.cmdAceptar_Click);
            // 
            // cmdCancelar
            // 
            this.cmdCancelar.Location = new System.Drawing.Point(144, 96);
            this.cmdCancelar.Name = "cmdCancelar";
            this.cmdCancelar.Size = new System.Drawing.Size(88, 32);
            this.cmdCancelar.TabIndex = 1;
            this.cmdCancelar.Text = "Cancelar";
            this.cmdCancelar.UseVisualStyleBackColor = true;
            this.cmdCancelar.Click += new System.EventHandler(this.cmdCancelar_Click);
            // 
            // chkTouch
            // 
            this.chkTouch.AutoSize = true;
            this.chkTouch.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTouch.Location = new System.Drawing.Point(16, 16);
            this.chkTouch.Name = "chkTouch";
            this.chkTouch.Size = new System.Drawing.Size(62, 22);
            this.chkTouch.TabIndex = 2;
            this.chkTouch.Text = "Touch";
            this.chkTouch.UseVisualStyleBackColor = true;
            this.chkTouch.CheckedChanged += new System.EventHandler(this.chkTouch_CheckedChanged);
            // 
            // chkMail
            // 
            this.chkMail.AutoSize = true;
            this.chkMail.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMail.Location = new System.Drawing.Point(32, 48);
            this.chkMail.Name = "chkMail";
            this.chkMail.Size = new System.Drawing.Size(173, 22);
            this.chkMail.TabIndex = 3;
            this.chkMail.Text = "Comprobante Electrónico";
            this.chkMail.UseVisualStyleBackColor = true;
            // 
            // imageMail
            // 
            this.imageMail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imageMail.Image = global::cpIntegracionEMV.Properties.Resources.question;
            this.imageMail.Location = new System.Drawing.Point(208, 40);
            this.imageMail.Name = "imageMail";
            this.imageMail.Size = new System.Drawing.Size(40, 40);
            this.imageMail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageMail.TabIndex = 4;
            this.imageMail.TabStop = false;
            this.imageMail.Click += new System.EventHandler(this.imageMail_Click);
            this.imageMail.MouseHover += new System.EventHandler(this.imageMail_MouseHover);
            // 
            // pictureCompro
            // 
            this.pictureCompro.BackColor = System.Drawing.Color.LemonChiffon;
            this.pictureCompro.Location = new System.Drawing.Point(32, 8);
            this.pictureCompro.Name = "pictureCompro";
            this.pictureCompro.Size = new System.Drawing.Size(200, 40);
            this.pictureCompro.TabIndex = 5;
            this.pictureCompro.TabStop = false;
            this.pictureCompro.Visible = false;
            // 
            // lblComprobante
            // 
            this.lblComprobante.BackColor = System.Drawing.Color.LemonChiffon;
            this.lblComprobante.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComprobante.Location = new System.Drawing.Point(40, 12);
            this.lblComprobante.Name = "lblComprobante";
            this.lblComprobante.Size = new System.Drawing.Size(184, 32);
            this.lblComprobante.TabIndex = 6;
            this.lblComprobante.Text = "Marque la casilla si quiere enviar el Voucher por Correo Electrónico";
            this.lblComprobante.Visible = false;
            // 
            // frmCapacidadTouch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(277, 149);
            this.ControlBox = false;
            this.Controls.Add(this.lblComprobante);
            this.Controls.Add(this.pictureCompro);
            this.Controls.Add(this.imageMail);
            this.Controls.Add(this.chkMail);
            this.Controls.Add(this.chkTouch);
            this.Controls.Add(this.cmdCancelar);
            this.Controls.Add(this.cmdAceptar);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCapacidadTouch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configura Capacidad Touch";
            this.Load += new System.EventHandler(this.frmCapacidadTouch_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCapacidadTouch_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.imageMail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCompro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdAceptar;
        private System.Windows.Forms.Button cmdCancelar;
        private System.Windows.Forms.CheckBox chkTouch;
        private System.Windows.Forms.CheckBox chkMail;
        private System.Windows.Forms.PictureBox imageMail;
        private System.Windows.Forms.PictureBox pictureCompro;
        private System.Windows.Forms.Label lblComprobante;
    }
}