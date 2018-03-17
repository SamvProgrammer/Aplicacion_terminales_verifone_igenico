namespace cpIntegracionEMV.UI
{
    partial class frmVtaSrvSeleccionar
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
            this.fraCategoria = new System.Windows.Forms.Panel();
            this.CboCategoria = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fraProducto = new System.Windows.Forms.Panel();
            this.CmdAceptar = new System.Windows.Forms.Button();
            this.CboProductos = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fraCategoria.SuspendLayout();
            this.fraProducto.SuspendLayout();
            this.SuspendLayout();
            // 
            // fraCategoria
            // 
            this.fraCategoria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fraCategoria.Controls.Add(this.CboCategoria);
            this.fraCategoria.Controls.Add(this.label1);
            this.fraCategoria.Location = new System.Drawing.Point(16, 16);
            this.fraCategoria.Name = "fraCategoria";
            this.fraCategoria.Size = new System.Drawing.Size(368, 72);
            this.fraCategoria.TabIndex = 0;
            // 
            // CboCategoria
            // 
            this.CboCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboCategoria.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboCategoria.FormattingEnabled = true;
            this.CboCategoria.Location = new System.Drawing.Point(8, 32);
            this.CboCategoria.Name = "CboCategoria";
            this.CboCategoria.Size = new System.Drawing.Size(344, 24);
            this.CboCategoria.TabIndex = 1;
            this.CboCategoria.SelectedIndexChanged += new System.EventHandler(this.CboCategoria_SelectedIndexChanged);
            this.CboCategoria.Click += new System.EventHandler(this.CboCategoria_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selecciona la categoría";
            // 
            // fraProducto
            // 
            this.fraProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fraProducto.Controls.Add(this.CmdAceptar);
            this.fraProducto.Controls.Add(this.CboProductos);
            this.fraProducto.Controls.Add(this.label2);
            this.fraProducto.Location = new System.Drawing.Point(16, 104);
            this.fraProducto.Name = "fraProducto";
            this.fraProducto.Size = new System.Drawing.Size(368, 100);
            this.fraProducto.TabIndex = 1;
            this.fraProducto.Visible = false;
            // 
            // CmdAceptar
            // 
            this.CmdAceptar.Location = new System.Drawing.Point(280, 64);
            this.CmdAceptar.Name = "CmdAceptar";
            this.CmdAceptar.Size = new System.Drawing.Size(75, 23);
            this.CmdAceptar.TabIndex = 4;
            this.CmdAceptar.Text = "Aceptar";
            this.CmdAceptar.UseVisualStyleBackColor = true;
            this.CmdAceptar.Click += new System.EventHandler(this.CmdAceptar_Click);
            // 
            // CboProductos
            // 
            this.CboProductos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboProductos.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboProductos.FormattingEnabled = true;
            this.CboProductos.Location = new System.Drawing.Point(8, 32);
            this.CboProductos.Name = "CboProductos";
            this.CboProductos.Size = new System.Drawing.Size(344, 24);
            this.CboProductos.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Selecciona el producto";
            // 
            // frmVtaSrvSeleccionar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(402, 222);
            this.ControlBox = false;
            this.Controls.Add(this.fraProducto);
            this.Controls.Add(this.fraCategoria);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmVtaSrvSeleccionar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Productos";
            this.Load += new System.EventHandler(this.frmVtaSrvSeleccionar_Load);
            this.fraCategoria.ResumeLayout(false);
            this.fraCategoria.PerformLayout();
            this.fraProducto.ResumeLayout(false);
            this.fraProducto.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel fraCategoria;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CboCategoria;
        private System.Windows.Forms.Panel fraProducto;
        private System.Windows.Forms.ComboBox CboProductos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CmdAceptar;
    }
}