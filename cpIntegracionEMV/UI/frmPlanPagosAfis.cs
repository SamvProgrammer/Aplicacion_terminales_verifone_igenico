using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;
using cpIntegracionEMV.data;

namespace cpIntegracionEMV.UI
{
    public partial class frmPlanPagosAfis : Form
    {
        XmlNodeList nodocontado=null;
        XmlNodeList xmlNodoAfContado = null;
        XmlNodeList nodomsi=null;
        XmlNodeList xmlNodoAfMSI = null;
        XmlDocument xmlMCI = new XmlDocument();
        XmlNodeList nodoMCI = null;
        XmlNodeList xmlNodoAfMCI = null;

        String strmci = "";
        String tipoPago = "";

        //***********************************************************************************
        //VARIABLES PARA QUALITAS
        private string plazosQ;
        private string MSIQ;
        private string[] mesesQ;
        private string strAuxTemp;
        private bool hayMSIQualitas;
        private int iQualitas;
        //***********************************************************************************

        public frmPlanPagosAfis()
        {
            InitializeComponent();
        }

        public frmPlanPagosAfis(XmlNodeList _nodocontado, XmlNodeList _nodomsi, String _strmci)
        {
            nodocontado = _nodocontado;
            nodomsi = _nodomsi;
            strmci = _strmci;
            InitializeComponent();

        }

        private void frmPlanPagosAfis_Load(object sender, EventArgs e)
        {
            btnAtras.Enabled = false;
            gbAfiliacion.Enabled = false;
            gbTipopago.Enabled = true;
            gbAfiliacion.Hide();

            this.Size = new Size(245, 288);
            gbAfiliacion.Location = gbTipopago.Location;

            //Contado
            xmlNodoAfContado = ((XmlElement)nodocontado[0]).GetElementsByTagName("af");

            //MSI
            xmlNodoAfMSI = ((XmlElement)nodomsi[0]).GetElementsByTagName("af");

            //Activo los checkbox
            if(!TRINP_Qualitas.isQualitas) //si no eres un usuario qualitas
            {
                rbContado.Enabled = false;
                rbMSI.Enabled = false;
                rbMCI.Enabled = false;

                //Contado
                if (xmlNodoAfContado.Count > 0)
                    rbContado.Enabled = true;

                //MSI
                if (xmlNodoAfMSI.Count > 0)
                    rbMSI.Enabled = true;

                //MCI
                if (!strmci.Equals(""))
                    rbMCI.Enabled = true;

            }
            else
            {
                rbContado.Enabled = false;
                rbMSI.Enabled = false;
                rbMCI.Enabled = false;

                //usuario qualitas
                if (TRINP_Qualitas.tipocobro == null || TRINP_Qualitas.tipocobro.Equals("") || TRINP_Qualitas.tipocobro.ToUpper().Equals("OTROS"))
                {
                    //Contado
                    if (xmlNodoAfContado.Count > 0)
                        rbContado.Enabled = true;
                    
                    //MSI
                    if (TRINP_Qualitas.ActivaMSI && xmlNodoAfMSI.Count > 0)
                        rbMSI.Enabled = true;
                }
                else
                {
                    if(!string.IsNullOrEmpty(TRINP_Qualitas.TipoPagosContado) &&
                        TRINP_Qualitas.TipoPagosContado.ToUpper().Equals("CONTADO"))
                    {    
                         if (xmlNodoAfContado.Count > 0)
                             rbContado.Enabled = true;
                    }

                    if (!string.IsNullOrEmpty(TRINP_Qualitas.TipoPagosMSI) &&
                        TRINP_Qualitas.TipoPagosMSI.ToUpper().Equals("MSI"))
                    {
                        plazosQ = TRINP_Qualitas.TipoPagosMSIPlan.Replace("</meses>", "");
                        plazosQ = plazosQ.Replace("<meses>", "$");
                        mesesQ = plazosQ.Split('$');
                        hayMSIQualitas = false;
                        MSIQ = TRINP.afmsi;

                        for(int iQualitas =0; iQualitas < mesesQ.Length; iQualitas++)
                        {
                            if (MSIQ.Contains(" " + mesesQ[iQualitas].Trim() + "M ") 
                                || MSIQ.Contains(" " + mesesQ[iQualitas].Trim() + "M")
                                || MSIQ.Contains(" " + mesesQ[iQualitas].Trim() + "M<"))
                            {
                                hayMSIQualitas = true;
                                break;
                            }
                        }

                        if(hayMSIQualitas)
                            rbMSI.Enabled = true;

                    }    
                    else
                    {
                        rbMSI.Enabled = false;
                        rbMSI.Checked = false;
                    }

                }

            } //Fin qualitas

            rbContado.Checked = true;

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int count, countmci;
            //XmlNodeList xmlNodoafcontado = null;
            //XmlNodeList xmlNodoafMSI = null;
            String aux="", pmci="";            

            //Pantalla de selección de tipo de pago. 1er Pagina
            if (gbTipopago.Enabled == true)
            {
                //Clear List
                lstAfiliacion.Items.Clear();

                if (rbMSI.Checked == true)
                {
                    tipoPago = "msi";
                    if (nodomsi != null)
                    {

                        if (!TRINP_Qualitas.isQualitas)
                        {
                            for (count = 0; count < xmlNodoAfMSI.Count; count++)
                            {
                                lstAfiliacion.Items.Add(xmlNodoAfMSI.Item(count).ChildNodes[0].InnerText);
                            }
                        }
                        else //eres qualitas
                        {
                            if (TRINP_Qualitas.tipocobro != null)
                            {
                                //ni deducible ni póliza
                                if (TRINP_Qualitas.tipocobro.Equals("") || TRINP_Qualitas.tipocobro.ToUpper().Equals("OTROS"))
                                {
                                    for (count = 0; count < xmlNodoAfMSI.Count; count++)
                                        lstAfiliacion.Items.Add(xmlNodoAfMSI.Item(count).ChildNodes[0].InnerText);
                                }
                                else
                                {
                                       plazosQ = TRINP_Qualitas.TipoPagosMSIPlan.Replace("</meses>", "");
                                       plazosQ = plazosQ.Replace("<meses>", "$");
                                       mesesQ = plazosQ.Split('$');

                                       for (count = 0; count < xmlNodoAfMSI.Count; count++)
                                       {
                                           MSIQ = xmlNodoAfMSI.Item(count).ChildNodes[0].InnerText;

                                           for (iQualitas = 0; iQualitas < mesesQ.Length; iQualitas++)
                                           {
                                               strAuxTemp = " " + mesesQ[iQualitas].Trim() + "M ";

                                               if (!strAuxTemp.Trim().Equals("M"))
                                               {
                                                   if (MSIQ.Contains(" " + mesesQ[iQualitas].Trim() + "M ")
                                                       || MSIQ.Contains(" " + mesesQ[iQualitas].Trim() + "M")
                                                       || MSIQ.Contains("AMEX " + mesesQ[iQualitas].Trim() + "M ")
                                                       || MSIQ.Contains("AMEX " + mesesQ[iQualitas].Trim() + "M"))
                                                   {
                                                       lstAfiliacion.Items.Add(xmlNodoAfMSI.Item(count).ChildNodes[0].InnerText);
                                                   }

                                               }
                                           }
                                       }

                                }

                            }

                        } //fin no eres qualitas

                    }
                }
                else if (rbMCI.Checked == true)
                {
                    tipoPago = "mci";
                    
                    aux = strmci;
                    while (aux.IndexOf(',')>0)
                    {
                        lstAfiliacion.Items.Add(aux.Substring(0, aux.IndexOf(',')));
                        aux = aux.Substring(aux.IndexOf(',') + 1,aux.Length-(aux.IndexOf(',')+1));
                    }
                    lstAfiliacion.Items.Add(aux);
                }
                else if (rbContado.Checked == true)
                {
                    tipoPago = "contado";
                    if (nodocontado != null)
                    {
                        //xmlNodoafcontado = ((XmlElement)nodocontado[0]).GetElementsByTagName("af");
                        for (count = 0; count < xmlNodoAfContado.Count; count++)
                        {
                            lstAfiliacion.Items.Add(xmlNodoAfContado.Item(count).ChildNodes[0].InnerText);
                        }
                    }
                }
                
                //Focus, first item.
                if (lstAfiliacion.Items.Count > 0)
                    lstAfiliacion.SelectedIndex = 0;

                gbTipopago.Enabled = false;
                gbTipopago.Hide();

                //Solo una afiliacion
                if (lstAfiliacion.Items.Count == 1 && !tipoPago.Equals("mci") && !TRINP_Qualitas.isQualitas)
                {
                    if (tipoPago.Equals("msi"))
                    {
                        TRINP.Tx_Merchant = xmlNodoAfMSI.Item(lstAfiliacion.SelectedIndex).ChildNodes[1].InnerText;
                        this.Close();
                    }

                    if (tipoPago.Equals("contado"))
                    {
                        TRINP.Tx_Merchant = xmlNodoAfContado.Item(lstAfiliacion.SelectedIndex).ChildNodes[1].InnerText;
                        this.Close();
                    } 

                }
                else
                {
                    gbAfiliacion.Enabled = true;
                    gbAfiliacion.Show();
                }

                btnAtras.Enabled = true;
            }
            //2da Pagina
            else if (gbAfiliacion.Enabled == true)
            {

                if (lstAfiliacion.SelectedIndex >= 0)
                {
                    if (tipoPago.Equals("msi"))
                    {
                        //xmlNodoafMSI = ((XmlElement)nodomsi[0]).GetElementsByTagName("af");
                        TRINP.Tx_Merchant = xmlNodoAfMSI.Item(lstAfiliacion.SelectedIndex).ChildNodes[1].InnerText;

                        if (TRINP_Qualitas.isQualitas)
                        {
                            string descMerchant = xmlNodoAfMSI.Item(lstAfiliacion.SelectedIndex).ChildNodes[0].InnerText;
                            TRINP_Qualitas.Financiamiento = "S";

                            plazosQ = TRINP_Qualitas.TipoPagosMSIPlan.Replace("</meses>", "");
                            plazosQ = plazosQ.Replace("<meses>", "$");
                            mesesQ = plazosQ.Split('$');
                            
                            for (iQualitas = 0; iQualitas < mesesQ.Length; iQualitas++)
                            {
                                strAuxTemp = " " + mesesQ[iQualitas].Trim() + "M ";

                                if (!strAuxTemp.Trim().Equals("M"))
                                {
                                    if (descMerchant.Contains(" " + mesesQ[iQualitas].Trim() + "M ")
                                     || descMerchant.Contains(" " + mesesQ[iQualitas].Trim() + "M"))
                                    {
                                        strAuxTemp = strAuxTemp.Replace(" ", "");
                                        strAuxTemp = strAuxTemp.Replace("M", "");
                                        TRINP_Qualitas.Tipofinanciamiento = strAuxTemp;
                                    }
                                }

                            }
                        }

                        this.Close();
                    }
                    else if (tipoPago.Equals("mci"))
                    {
                        aux = strmci;
                        count = 0;
                        while (aux.IndexOf(',') > 0)
                        {
                            pmci = aux.Substring(0, aux.IndexOf(','));
                            aux = aux.Substring(aux.IndexOf(',') + 1, aux.Length - (aux.IndexOf(',') + 1));
                            if (lstAfiliacion.SelectedIndex == count) 
                            {
                                break;
                            }
                            else
                            {
                                count++;
                            }
                        }
                        TRINP.plazoMCI = pmci;
                        if (nodocontado != null)
                        {
                            //Clear List
                            lstAfiliacion.Items.Clear();

                            //xmlNodoafcontado = ((XmlElement)nodocontado[0]).GetElementsByTagName("af");
                            for (count = 0; count < xmlNodoAfContado.Count; count++)
                            {
                                //Es afliliacion contado con mci activo.
                                if (xmlNodoAfContado.Item(count).ChildNodes[5].InnerText.Equals("1"))
                                {
                                    lstAfiliacion.Items.Add(xmlNodoAfContado.Item(count).ChildNodes[0].InnerText);
                                }
                            }
                        }
                        tipoPago = "mciplazo";
                        gbTipopago.Enabled = false;
                        gbTipopago.Hide();

                        //Focus, first item.
                        if (lstAfiliacion.Items.Count > 0)
                            lstAfiliacion.SelectedIndex = 0;

                        //Solo una afiliacion
                        if (lstAfiliacion.Items.Count == 1)
                        {
                            TRINP.Tx_Merchant = xmlNodoAfContado.Item(lstAfiliacion.SelectedIndex).ChildNodes[1].InnerText;
                            this.Close();
                        }
                        else
                        {
                            gbAfiliacion.Enabled = true;
                            gbAfiliacion.Show();
                        }

                       
                        btnAtras.Enabled = true;
                    }
                    else if (tipoPago.Equals("mciplazo"))
                    {
                        //Despues de seleccionar MCI->Plazo->afiliacion contado.
                        if (nodocontado != null)
                        {
                            //xmlNodoafcontado = ((XmlElement)nodocontado[0]).GetElementsByTagName("af");
                            countmci = 0;
                            for (count = 0; count < xmlNodoAfContado.Count; count++)
                            {
                                if (xmlNodoAfContado.Item(count).ChildNodes[5].InnerText.Equals("1"))
                                {
                                    //lstAfiliacion.Items.Add(xmlNodoafcontado.Item(count).ChildNodes[0].InnerText);
                                    if (count == lstAfiliacion.SelectedIndex)
                                    {
                                        TRINP.Tx_Merchant = xmlNodoAfContado.Item(count).ChildNodes[1].InnerText;
                                        break;
                                    }
                                    countmci++;
                                }
                            }
                        }
                        this.Close();
                    }
                    else if (tipoPago.Equals("contado"))
                    {
                        //xmlNodoafcontado = ((XmlElement)nodocontado[0]).GetElementsByTagName("af");
                        TRINP.Tx_Merchant = xmlNodoAfContado.Item(lstAfiliacion.SelectedIndex).ChildNodes[1].InnerText;

                        if (TRINP_Qualitas.isQualitas)
                        {
                            TRINP_Qualitas.Financiamiento = "N";
                            TRINP_Qualitas.Tipofinanciamiento = "0";
                        }

                        this.Close();
                    }                    
                }
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            gbTipopago.Enabled = true;
            gbTipopago.Show();
            gbAfiliacion.Enabled = false;
            gbAfiliacion.Hide();
            btnAtras.Enabled = false;
        }
    }
}
