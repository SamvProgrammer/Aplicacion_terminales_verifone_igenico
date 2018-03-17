namespace cpIntegracionEMV.UI
{
    partial class frmDownload
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
            this.progressBarDwnld = new System.Windows.Forms.ProgressBar();
            this.ldwnld = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarDwnld
            // 
            this.progressBarDwnld.Location = new System.Drawing.Point(16, 40);
            this.progressBarDwnld.Name = "progressBarDwnld";
            this.progressBarDwnld.Size = new System.Drawing.Size(383, 23);
            this.progressBarDwnld.TabIndex = 0;
            // 
            // ldwnld
            // 
            this.ldwnld.AutoSize = true;
            this.ldwnld.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ldwnld.Location = new System.Drawing.Point(12, 9);
            this.ldwnld.Name = "ldwnld";
            this.ldwnld.Size = new System.Drawing.Size(385, 20);
            this.ldwnld.TabIndex = 1;
            this.ldwnld.Text = "Descargando configuración, espere por favor...";
            // 
            // frmDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 75);
            this.ControlBox = false;
            this.Controls.Add(this.ldwnld);
            this.Controls.Add(this.progressBarDwnld);
            this.Name = "frmDownload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Centro de pagos";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.frmDownload_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarDwnld;
        private System.Windows.Forms.Label ldwnld;
    }
}