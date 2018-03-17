using cpIntegracionEMV.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cpIntegracionEMV.UI
{
    public partial class frmAvisoPinPad : Form
    {
        public frmAvisoPinPad()
        {
            InitializeComponent();
        }

        private void cmdCerrar_Click(object sender, EventArgs e)
        {
            if(chkConf.Checked)
                utilidadesMIT.GuardaParametrosMIT("ShwMsgPinPad", "1");
            else
                utilidadesMIT.GuardaParametrosMIT("ShwMsgPinPad", "0");

            this.Close();
        }

        private void frmAvisoPinPad_Load(object sender, EventArgs e)
        {
            lblAviso.Text = "El proceso de actualización no se completó correctamente. \r\n" + "Favor de llamar a su centro de atención.";
        }
    }
}
