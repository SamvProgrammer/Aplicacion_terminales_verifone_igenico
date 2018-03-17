namespace cpIntegracionEMV.UI
{
    partial class frmMoneda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMoneda));
            this.rBmxn = new System.Windows.Forms.RadioButton();
            this.rBusd = new System.Windows.Forms.RadioButton();
            this.bAceptar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // rBmxn
            // 
            this.rBmxn.AutoSize = true;
            this.rBmxn.Location = new System.Drawing.Point(88, 32);
            this.rBmxn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rBmxn.Name = "rBmxn";
            this.rBmxn.Size = new System.Drawing.Size(51, 22);
            this.rBmxn.TabIndex = 0;
            this.rBmxn.TabStop = true;
            this.rBmxn.Text = "MXN";
            this.rBmxn.UseVisualStyleBackColor = true;
            this.rBmxn.CheckedChanged += new System.EventHandler(this.rBmxn_CheckedChanged);
            // 
            // rBusd
            // 
            this.rBusd.AutoSize = true;
            this.rBusd.Location = new System.Drawing.Point(88, 96);
            this.rBusd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rBusd.Name = "rBusd";
            this.rBusd.Size = new System.Drawing.Size(47, 22);
            this.rBusd.TabIndex = 1;
            this.rBusd.TabStop = true;
            this.rBusd.Text = "USD";
            this.rBusd.UseVisualStyleBackColor = true;
            this.rBusd.CheckedChanged += new System.EventHandler(this.rBusd_CheckedChanged);
            // 
            // bAceptar
            // 
            this.bAceptar.Location = new System.Drawing.Point(168, 64);
            this.bAceptar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.bAceptar.Name = "bAceptar";
            this.bAceptar.Size = new System.Drawing.Size(76, 35);
            this.bAceptar.TabIndex = 2;
            this.bAceptar.Text = "Aceptar";
            this.bAceptar.UseVisualStyleBackColor = true;
            this.bAceptar.Click += new System.EventHandler(this.bAceptar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // frmMoneda
            // 
            this.AcceptButton = this.bAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(265, 153);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bAceptar);
            this.Controls.Add(this.rBusd);
            this.Controls.Add(this.rBmxn);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMoneda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seleccione la moneda";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rBmxn;
        private System.Windows.Forms.RadioButton rBusd;
        private System.Windows.Forms.Button bAceptar;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}