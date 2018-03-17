using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cpIntegracionEMV.data;

namespace cpIntegracionEMV.UI
{
    public partial class frmMoneda : Form
    {
        public frmMoneda()
        {
            InitializeComponent();
        }

        private void rBmxn_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rBusd_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void bAceptar_Click(object sender, EventArgs e)
        {
            if (rBmxn.Checked == true)
            {
                TRINP.Tx_Currency = "MXN";
            }
            else if(rBusd.Checked == true)
            {
                TRINP.Tx_Currency = "USD";
            }
            this.Close();
        }
    }
}
