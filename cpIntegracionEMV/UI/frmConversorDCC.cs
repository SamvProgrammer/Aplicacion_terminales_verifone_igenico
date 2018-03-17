using cpIntegracionEMV.data;
using cpIntegracionEMV.txn_flow;
using cpIntegracionEMV.util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace cpIntegracionEMV.UI
{
    public partial class frmConversorDCC : Form
    {
        private EjecutaOperacion ejecutaOpera = new EjecutaOperacion();
        private ArrayList lstActualMoneda = new ArrayList();
        private XmlDocument xmlDocto = new XmlDocument();
        private XmlNodeList xmlMonedas = null;
        private string numAfiliacion;
        private string sPathUserBanderas;

        double tipoCambio;
        double aux, monto;

        public frmConversorDCC()
        {
            InitializeComponent();
        }

        private void frmConversorDCC_Load(object sender, EventArgs e)
        {
            //configuracion
            this.Size = new Size(333, 408);
            this.CenterToParent();
            Frame2.Location = Frame1.Location;
            lblTipCam.Text = "";
            lblResultado.Text = "";
            lblMonedaCambio.Text = "";

            //datos
            DateTime date;
            date = DateTime.Now;
            lblLeyenda.Text = date.ToString("dddd d MMMM yyyy HH:mm:ss").ToUpper();

            cmboxTipoMoneda.SelectedIndex = 0;

           //Se agrega la carga de afiliaciones para la calculadora DCC
           if(!TypeUsuario.afiliacionesDCC.Equals(""))
                this.dbgGetCboAfiliaciones(cboxAfiliaciones, TypeUsuario.afiliacionesDCC);
            else
            {
                cboxAfiliaciones.Visible = false;
                this.getInfo();
            }
    
            txtMontoConvertir.Enabled = false;
            cmdMonedas.Enabled = false;
        }

        private void getInfo([Optional]string numAfiliacion)
        {
            string strCadena, strMonedas;
            int x = 5;
            int y = 16;
            int xMoneda = 45;
            int yMoneda = 16;
            int xRate = 85;
            int yRate = 16;
            int xDesc = 140;
            int yDesc = 16;

            strCadena = "";
            strCadena = ejecutaOpera.ConsultaXMLConversorDCC(TypeUsuario.URL, numAfiliacion);
            strCadena = strCadena.Replace("&aacute;", "á");
            strCadena = strCadena.Replace("&eacute;", "é");
            strCadena = strCadena.Replace("&iacute;", "í");
            strCadena = strCadena.Replace("&oacute;", "ó");
            strCadena = strCadena.Replace("&uacute;", "ú");
            strCadena = strCadena.Replace("&ntilde;", "ñ");
            strCadena = strCadena.Replace("&uuml;", "ü");

            strCadena = strCadena.Replace("&Aacute;", "á");
            strCadena = strCadena.Replace("&Eacute;", "é");
            strCadena = strCadena.Replace("&Iacute;", "í");
            strCadena = strCadena.Replace("&Oacute;", "ó");
            strCadena = strCadena.Replace("&Uacute;", "ú");
            strCadena = strCadena.Replace("&Ntilde;", "ñ");
            strCadena = strCadena.Replace("&Uuml;", "ü");

            strMonedas = utilidadesMIT.GetDataXML("conversor", strCadena);

            cmboxTipoMoneda.Items.Clear();
            cmboxTipoMoneda.Items.Add("-- Seleccione Moneda --");

            if (lstActualMoneda.Count > 0)
                lstActualMoneda.Clear();

            lstActualMoneda.Add("-- Seleccione Moneda --");
            this.limpiaCampos();
            
            xmlDocto.LoadXml(strCadena);
            xmlMonedas = xmlDocto.GetElementsByTagName("moneda");

            if (xmlMonedas.Count > 0)
            {
                txtMontoConvertir.Enabled = true;
                cmdMonedas.Enabled = true;

                for (int i = 0; i < xmlMonedas.Count; i++)
                {
                    cmboxTipoMoneda.Items.Add(xmlMonedas[i].ChildNodes[0].InnerText);
                    lstActualMoneda.Add(xmlMonedas[i].ChildNodes[1].InnerText);

                    //Para crear el path de las imagenes.
                    sPathUserBanderas = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\MIT\\Data\\Banderas\\";

                    if (!System.IO.Directory.Exists(sPathUserBanderas))
                        System.IO.Directory.CreateDirectory(sPathUserBanderas);

                    if (!System.IO.File.Exists(sPathUserBanderas + xmlMonedas[i].ChildNodes[2].InnerText + ".jpg"))
                        utilidadesMIT.DownloadFile(TRINP.url + "/pgs/jsp/cpagos/cargas/Picture/Banderas/" + xmlMonedas[i].ChildNodes[2].InnerText + ".jpg", sPathUserBanderas + xmlMonedas[i].ChildNodes[2].InnerText + ".jpg");

                    //se crean los objetos
                    if (System.IO.File.Exists(sPathUserBanderas + xmlMonedas[i].ChildNodes[2].InnerText + ".jpg"))
                    {
                        PictureBox pb = new PictureBox();
                        pb.Image = Image.FromFile(sPathUserBanderas + xmlMonedas[i].ChildNodes[2].InnerText + ".jpg");
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        pb.Location = new Point(x, y);
                        pb.Size = new System.Drawing.Size(28, 19);
                        y += 30;
                        panelMonedas.Controls.Add(pb);
                    }

                    //Moneda
                    Label lblMoneda = new Label();

                    if (xmlMonedas[i].ChildNodes[0].InnerText.Contains('-'))
                        lblMoneda.Text = xmlMonedas[i].ChildNodes[0].InnerText.Split('-')[0];
                    else
                        lblMoneda.Text = xmlMonedas[i].ChildNodes[0].InnerText;

                    lblMoneda.Visible = true;
                    lblMoneda.Size = new System.Drawing.Size(35, 19);
                    lblMoneda.ForeColor = Color.Blue;
                    lblMoneda.Location = new Point(xMoneda, yMoneda);
                    yMoneda += 30;
                    panelMonedas.Controls.Add(lblMoneda);

                    //rate
                    Label lblRate = new Label();
                    lblRate.Text = xmlMonedas[i].ChildNodes[1].InnerText;
                    lblRate.Visible = true;
                    lblRate.Size = new System.Drawing.Size(60, 19);
                    lblRate.Location = new Point(xRate, yRate);
                    yRate += 30;
                    panelMonedas.Controls.Add(lblRate);

                    //descripcion
                    Label lblDesc = new Label();

                    if (xmlMonedas[i].ChildNodes[0].InnerText.Contains('-'))
                        lblDesc.Text = xmlMonedas[i].ChildNodes[0].InnerText.Split('-')[1];
                    else
                        lblDesc.Text = xmlMonedas[i].ChildNodes[0].InnerText;

                    lblDesc.Size = new System.Drawing.Size(200, 19);
                    lblDesc.Visible = true;
                    lblDesc.Location = new Point(xDesc, yDesc);
                    yDesc += 30;
                    panelMonedas.Controls.Add(lblDesc);

                }
            }
            else
            {
                txtMontoConvertir.Enabled = false;
                cmdMonedas.Enabled = false;
            }

            cmboxTipoMoneda.SelectedIndex = 0;

        }

        private void cmdMonedas_Click(object sender, EventArgs e)
        {
            Frame1.Visible = false;
            Frame2.Visible = true;
        }

        private void cmdRegresar_Click(object sender, EventArgs e)
        {
            Frame2.Visible = false;
            Frame1.Visible = true;
        }

        private void dbgGetCboAfiliaciones(ComboBox cboContenedor, string catAfis)
        {
            try
            {
                string[] afil;

                cboContenedor.Items.Clear();
                cboContenedor.Items.Add("-- Seleccione Afiliación --");

                afil = catAfis.Split('|');

                for(int i = 0; i < afil.Length;i++)
                {
                    cboContenedor.Items.Add(afil[i]);
                }

                cboContenedor.SelectedIndex = 0;

            }
            catch(Exception ex)
            {
                MITLog.PrintLn("dbgGetCboAfiliaciones:--- " + ex.Message);
            }
        }

        private void cboxAfiliaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            string aux;
            this.limpiaCampos();

            this.Cursor = Cursors.WaitCursor;

            if(cboxAfiliaciones.SelectedIndex != 0)
            {
                aux = cboxAfiliaciones.Text.Trim();

                if (aux.Contains('-'))
                    aux = aux.Split('-')[0].Trim();

                numAfiliacion = aux;
                txtMontoConvertir.Enabled = true;
                cmdMonedas.Enabled = true;
            }
            else
            {
                numAfiliacion = "0000000";
                txtMontoConvertir.Enabled = false;
                cmdMonedas.Enabled = false;
            }

            cmboxTipoMoneda.Items.Clear();
            cmboxTipoMoneda.Items.Add("-- Seleccione Moneda --");
            cmboxTipoMoneda.SelectedIndex = 0;

            txtMontoConvertir.Text = "";
            lblResultado.Text = "";
            lblTipCam.Text = "";
            lblMonedaCambio.Text = "";
            this.getInfo(numAfiliacion);
            Thread.Sleep(1000);
            this.Cursor = Cursors.Default;
        }

        private void limpiaCampos()
        {
            panelMonedas.Controls.Clear();
        }

        private void cmboxTipoMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                if( cmboxTipoMoneda.SelectedIndex != 0 && !txtMontoConvertir.Text.Equals("") && !txtMontoConvertir.Text.Equals("."))
                {
                    double.TryParse(txtMontoConvertir.Text, out monto);
                    double.TryParse(lstActualMoneda[cmboxTipoMoneda.SelectedIndex].ToString(), out aux);
                    tipoCambio = monto  * aux;
                    lblMonedaCambio.Text = cmboxTipoMoneda.Text.Substring(0,3);

                    if (lblMonedaCambio.Text.ToUpper().Equals("JPY"))
                        lblResultado.Text = tipoCambio.ToString("##########");
                    else
                        lblResultado.Text = tipoCambio.ToString("##########.00");
                    
                }

                if(cmboxTipoMoneda.SelectedIndex != 0 && !txtMontoConvertir.Text.Equals(""))
                {
                    double.TryParse(lstActualMoneda[cmboxTipoMoneda.SelectedIndex].ToString(), out aux);
                    tipoCambio = 1 / aux;
                    lblTipCam.Text = tipoCambio.ToString("##########.00");
                }

                if(cmboxTipoMoneda.SelectedIndex == 0 )
                {
                    txtMontoConvertir.Text = "";
                    lblResultado.Text = "";
                    lblTipCam.Text = "";
                    lblMonedaCambio.Text = "";
                }
            }
            catch (Exception ex)
            {
                MITLog.PrintLn("cmboxTipoMoneda_SelectedIndexChanged:-- " + ex.Message);
            }
        }

        private void txtMontoConvertir_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtMontoConvertir.Text = LimpiarCampo(txtMontoConvertir.Text);
            e.Handled = utilidadesMIT.isAmount(e, txtMontoConvertir.Text);

        }

        private string LimpiarCampo(string str)
        {
            str = str.Replace("|", "");
            str = str.Replace("°", "");
            str = str.Replace("¬", "");
            str = str.Replace("!", "");
            str = str.Replace("#", "");
            str = str.Replace("$", "");
            str = str.Replace("%", "");
            str = str.Replace("&", "");
            str = str.Replace("/", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("=", "");
            str = str.Replace("?", "");
            str = str.Replace("¡", "");
            str = str.Replace("¿", "");
            str = str.Replace("*", "");
            str = str.Replace("+", "");
            str = str.Replace(",", "");
            str = str.Replace(";", "");
            str = str.Replace("_", "");
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            
            return str;
        }

        private void txtMontoConvertir_KeyUp(object sender, KeyEventArgs e)
        {
            if (cmboxTipoMoneda.SelectedIndex != 0 && !txtMontoConvertir.Text.Equals(""))
            {
                lblMonedaCambio.Text = cmboxTipoMoneda.Text.Substring(0, 3);

                double.TryParse(txtMontoConvertir.Text, out monto);
                double.TryParse(lstActualMoneda[cmboxTipoMoneda.SelectedIndex].ToString(), out aux);
                tipoCambio = monto * aux;

                if (lblMonedaCambio.Text.ToUpper().Equals("JPY"))
                    lblResultado.Text = tipoCambio.ToString("##########");
                else
                    lblResultado.Text = tipoCambio.ToString("##########.00");
            }

            if (cmboxTipoMoneda.SelectedIndex != 0)
            {
                double.TryParse(lstActualMoneda[cmboxTipoMoneda.SelectedIndex].ToString(), out aux);
                tipoCambio = 1 / aux;
                lblTipCam.Text = tipoCambio.ToString("##########.00");
            }

            if (txtMontoConvertir.Text.Equals(""))
            {
                lblResultado.Text = "";
                lblTipCam.Text = "";
                lblMonedaCambio.Text = "";
            }
        }

        
    }
}
