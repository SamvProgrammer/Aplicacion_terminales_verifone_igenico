namespace cpIntegracionEMV.UI
{
    partial class frmDCC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDCC));
            this.lblDCC = new System.Windows.Forms.Label();
            this.groupBoxOriginal = new System.Windows.Forms.GroupBox();
            this.btnOrig = new System.Windows.Forms.Button();
            this.listViewOrig = new System.Windows.Forms.ListView();
            this.groupBoxCardholder = new System.Windows.Forms.GroupBox();
            this.btnCardholder = new System.Windows.Forms.Button();
            this.listViewCardholder = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxOriginal.SuspendLayout();
            this.groupBoxCardholder.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDCC
            // 
            this.lblDCC.AutoSize = true;
            this.lblDCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDCC.Location = new System.Drawing.Point(24, 19);
            this.lblDCC.Name = "lblDCC";
            this.lblDCC.Size = new System.Drawing.Size(424, 24);
            this.lblDCC.TabIndex = 0;
            this.lblDCC.Text = "Would you like to pay in your own currency?";
            // 
            // groupBoxOriginal
            // 
            this.groupBoxOriginal.Controls.Add(this.btnOrig);
            this.groupBoxOriginal.Controls.Add(this.listViewOrig);
            this.groupBoxOriginal.Location = new System.Drawing.Point(17, 71);
            this.groupBoxOriginal.Name = "groupBoxOriginal";
            this.groupBoxOriginal.Size = new System.Drawing.Size(223, 185);
            this.groupBoxOriginal.TabIndex = 1;
            this.groupBoxOriginal.TabStop = false;
            this.groupBoxOriginal.Text = "Original transaction amount";
            // 
            // btnOrig
            // 
            this.btnOrig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrig.Location = new System.Drawing.Point(112, 144);
            this.btnOrig.Name = "btnOrig";
            this.btnOrig.Size = new System.Drawing.Size(98, 30);
            this.btnOrig.TabIndex = 1;
            this.btnOrig.Text = "Accept MXN";
            this.btnOrig.UseVisualStyleBackColor = true;
            this.btnOrig.Click += new System.EventHandler(this.btnOrig_Click);
            // 
            // listViewOrig
            // 
            this.listViewOrig.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewOrig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewOrig.Location = new System.Drawing.Point(11, 23);
            this.listViewOrig.Name = "listViewOrig";
            this.listViewOrig.Size = new System.Drawing.Size(200, 113);
            this.listViewOrig.TabIndex = 0;
            this.listViewOrig.UseCompatibleStateImageBehavior = false;
            this.listViewOrig.View = System.Windows.Forms.View.List;
            this.listViewOrig.SelectedIndexChanged += new System.EventHandler(this.listViewOrig_SelectedIndexChanged);
            // 
            // groupBoxCardholder
            // 
            this.groupBoxCardholder.Controls.Add(this.btnCardholder);
            this.groupBoxCardholder.Controls.Add(this.listViewCardholder);
            this.groupBoxCardholder.Location = new System.Drawing.Point(257, 70);
            this.groupBoxCardholder.Name = "groupBoxCardholder";
            this.groupBoxCardholder.Size = new System.Drawing.Size(223, 186);
            this.groupBoxCardholder.TabIndex = 2;
            this.groupBoxCardholder.TabStop = false;
            this.groupBoxCardholder.Text = "Cardholder transaction amount";
            // 
            // btnCardholder
            // 
            this.btnCardholder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCardholder.Location = new System.Drawing.Point(112, 144);
            this.btnCardholder.Name = "btnCardholder";
            this.btnCardholder.Size = new System.Drawing.Size(98, 30);
            this.btnCardholder.TabIndex = 1;
            this.btnCardholder.Text = "Accept DCC";
            this.btnCardholder.UseVisualStyleBackColor = true;
            this.btnCardholder.Click += new System.EventHandler(this.btnCardholder_Click);
            // 
            // listViewCardholder
            // 
            this.listViewCardholder.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewCardholder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewCardholder.Location = new System.Drawing.Point(10, 23);
            this.listViewCardholder.Name = "listViewCardholder";
            this.listViewCardholder.Size = new System.Drawing.Size(200, 113);
            this.listViewCardholder.TabIndex = 0;
            this.listViewCardholder.UseCompatibleStateImageBehavior = false;
            this.listViewCardholder.View = System.Windows.Forms.View.List;
            // 
            // frmDCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(503, 273);
            this.Controls.Add(this.groupBoxCardholder);
            this.Controls.Add(this.groupBoxOriginal);
            this.Controls.Add(this.lblDCC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDCC";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DCC";
            this.Load += new System.EventHandler(this.frmDCC_Load);
            this.groupBoxOriginal.ResumeLayout(false);
            this.groupBoxCardholder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDCC;
        private System.Windows.Forms.GroupBox groupBoxOriginal;
        private System.Windows.Forms.GroupBox groupBoxCardholder;
        private System.Windows.Forms.Button btnOrig;
        private System.Windows.Forms.ListView listViewOrig;
        private System.Windows.Forms.Button btnCardholder;
        private System.Windows.Forms.ListView listViewCardholder;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}