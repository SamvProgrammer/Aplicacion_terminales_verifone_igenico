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
    public partial class frmCapacidadTouch : Form
    {
        string strChkTouch;
        string strChkMail;

        public frmCapacidadTouch()
        {
            InitializeComponent();
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCapacidadTouch_Load(object sender, EventArgs e)
        {
            strChkTouch = utilidadesMIT.ObtieneParametrosMIT("CAPACIDAD_TOUCH");
            strChkMail = utilidadesMIT.ObtieneParametrosMIT("CAPACIDAD_TOUCH_MAIL");

            if (strChkTouch.Equals("0"))
                chkTouch.Checked = false;
            else
                chkTouch.Checked = true;
            
            if (strChkMail.Equals("0"))
                chkMail.Checked = false;
            else
                chkMail.Checked = true;

        }

        private void chkTouch_CheckedChanged(object sender, EventArgs e)
        {
            if(chkTouch.Checked == false)
            {
                chkMail.Checked = false;
                chkMail.Enabled = false;
            }
            else
            {
                chkMail.Enabled = true;
            }
        }

        private void imageMail_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.imageMail, "Marque la casilla si quiere enviar el Voucher por Correo Electrónico");
        }

        private void imageMail_Click(object sender, EventArgs e)
        {
            lblComprobante.Visible = true;
            pictureCompro.Visible = true;
        }

        private void frmCapacidadTouch_MouseMove(object sender, MouseEventArgs e)
        {
            lblComprobante.Visible = false;
            pictureCompro.Visible = false;
        }

        private void cmdAceptar_Click(object sender, EventArgs e)
        {
            if (chkTouch.Checked)
                strChkTouch = "1";
            else
                strChkTouch = "0";

            if (chkMail.Checked)
                strChkMail = "1";
            else
                strChkMail = "0";

            utilidadesMIT.GuardaParametrosMIT("CAPACIDAD_TOUCH", strChkTouch);
            utilidadesMIT.GuardaParametrosMIT("CAPACIDAD_TOUCH_MAIL", strChkMail);
            this.Close();
        }
    }
}
