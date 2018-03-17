using cpIntegracionEMV.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cpIntegracionEMV.util;

namespace cpIntegracionEMV.UI
{
    public partial class frmCsvAMEX : Form
    {

        public frmCsvAMEX()
        {
            InitializeComponent();
        }

        private void frmCsvAMEX_Load(object sender, EventArgs e)
        {
            txtCsv.Text = "";
            txtCsv.Focus();
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void cmdBorrar_Click(object sender, EventArgs e)
        {
            txtCsv.Text = "";
        }

        private void CmdAceptar_Click(object sender, EventArgs e)
        {
            if (txtCsv.Text.Equals(""))
            {
                MessageBox.Show("Falta indicar el cvv", "Centro de Pagos");
                return;
            }

            if (txtCsv.Text.Length < 4)
            {
                MessageBox.Show("El cvv debe ser de 4 caracteres", "Centro de Pagos");
                return;
            }

            TRINP.cvvcsc = txtCsv.Text;
            this.Close();

        }

        private void cmdCero_Click(object sender, EventArgs e)
        {
            if (txtCsv.Text.Length <= 3)
                txtCsv.Text = txtCsv.Text + "0";
        }

        private void cmdUno_Click(object sender, EventArgs e)
        {
            if (txtCsv.Text.Length <= 3)
                txtCsv.Text = txtCsv.Text + "1";
        }

        private void cmdDos_Click(object sender, EventArgs e)
        {
            if (txtCsv.Text.Length <= 3)
                txtCsv.Text = txtCsv.Text + "2";
        }

        private void cmdTres_Click(object sender, EventArgs e)
        {
            if (txtCsv.Text.Length <= 3)
                txtCsv.Text = txtCsv.Text + "3";
        }

        private void cmdCuatro_Click(object sender, EventArgs e)
        {
            if (txtCsv.Text.Length <= 3)
                txtCsv.Text = txtCsv.Text + "4";
        }

        private void cmdCinco_Click(object sender, EventArgs e)
        {
            if (txtCsv.Text.Length <= 3)
                txtCsv.Text = txtCsv.Text + "5";
        }

        private void cmdSeis_Click(object sender, EventArgs e)
        {
            if (txtCsv.Text.Length <= 3)
                txtCsv.Text = txtCsv.Text + "6";
        }

        private void cmdSiete_Click(object sender, EventArgs e)
        {
            if (txtCsv.Text.Length <= 3)
                txtCsv.Text = txtCsv.Text + "7";
        }

        private void cmdOcho_Click(object sender, EventArgs e)
        {
            if (txtCsv.Text.Length <= 3)
                txtCsv.Text = txtCsv.Text + "8";
        }

        private void cmdNueve_Click(object sender, EventArgs e)
        {
            if (txtCsv.Text.Length <= 3)
                txtCsv.Text = txtCsv.Text + "9";
        }

        private void txtCsv_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = utilidadesMIT.isNumeric(e, txtCsv.Text);
        }


    }
}
