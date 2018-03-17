using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using cpIntegracionEMV.data;

namespace cpIntegracionEMV.UI
{
    public partial class frmFirmaenPanel : Form
    {
         //0 ok, 1 error, -1 inicio
        public string strTipoRespuesta { get; set;}
        //public string strMailFirma { get; set; }
        private const string NOMBRE_GENERAL = "frmFirmaenPanel";

        public frmFirmaenPanel()
        {
            InitializeComponent();
        }

        private void frmFirmaenPanel_Load(object sender, EventArgs e)
        {
            if( strTipoRespuesta == "-2" )
                MessageBox.Show("Ingresa Correo Electrónico.", NOMBRE_GENERAL, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (strTipoRespuesta == "0")
            {
                lblMail.Visible = false;
                lblMensaje.Text = "Voucher Cliente envíado " + "\r\n" + "Exitosamente.";
                cmdAceptar.Enabled = false;
                cmdCancelar.Enabled = false;
                txtMail.Enabled = false;

                //'se reajustan unos controles del form
                cmdSalir.Visible = true;
                Frame1.Visible = false;
                Frame2.Location = new Point(16, 16);
                this.Size = new Size(456, 130);
            }

            if (strTipoRespuesta == "-1" || strTipoRespuesta == "-2")
            {
                lblMail.Visible = true;
                cmdAceptar.Enabled = true;
                cmdCancelar.Enabled = true;
                txtMail.Text = "";
            }
    
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            TRINP.strMailFirma = "";
            this.Close();

        }


        private Boolean ValidaEmail(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void cmdAceptar_Click(object sender, EventArgs e)
        {
            //valida que se haya introducido un mail
            if(txtMail.Text.Trim().Equals(""))
            {
                MessageBox.Show("Introduzca el correo electrónico", NOMBRE_GENERAL, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMail.Focus();
                return;
            }

            //valida si es un mail válido
            if( !ValidaEmail(txtMail.Text) )
            {
                MessageBox.Show("Correo electrónico no válido", NOMBRE_GENERAL, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMail.Focus();
                return;
            }

            TRINP.strMailFirma = txtMail.Text;
            this.Close();

        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
