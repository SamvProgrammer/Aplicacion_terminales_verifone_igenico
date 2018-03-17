using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cpIntegracionEMV.txn_flow;
using cpIntegracionEMV.com;
using System.Threading;

namespace cpIntegracionEMV.UI
{
    public partial class frmDownload : Form
    {
        public frmDownload()
        {
            InitializeComponent();
        }

        private void frmDownload_Shown(object sender, EventArgs e)
        {
            
            EjecutaOperacion eo = new EjecutaOperacion();
            MITProtocol mp = new MITProtocol();
            progressBarDwnld.Visible = true;
            progressBarDwnld.Value = 0;
            String buffer = "";
            String cmdData = "";
            int count;
            int len = 0;
            try
            {
                //Parametros generales EMV
                buffer = eo.getEMVParams("2");
                if (!buffer.Trim().Equals(""))
                {
                    cmdData = buffer.Split('|')[1];
                    cmdData = cmdData.Replace("\r", "");
                    cmdData = cmdData.Replace("\n", "");
                    Thread.Sleep(500);
                    mp.sendEMVConfig("01", "02", cmdData);
                    progressBarDwnld.Value = 10;
                }
                
                //Bines
                buffer = eo.getEMVParams("3");
                if (!buffer.Trim().Equals(""))
                {
                    len = buffer.Split('|').Length;
                    for (count = 1; count < len; count++)
                    {
                        cmdData = buffer.Split('|')[count];
                        cmdData = cmdData.Replace("\r", "");
                        cmdData = cmdData.Replace("\n", "");
                        if (count == 1)
                        {
                            mp.sendEMVConfig("02", "02", cmdData);
                        }
                        else
                        {
                            mp.sendEMVConfig("02", "01", cmdData);
                        }
                        progressBarDwnld.Value = progressBarDwnld.Value + 6;
                    }
                    progressBarDwnld.Value = 30;
                }
                //Aplicaciones
                buffer = eo.getEMVParams("5");
                if (!buffer.Trim().Equals(""))
                {
                    len = buffer.Split('|').Length;
                    for (count = 1; count < len; count++)
                    {
                        cmdData = buffer.Split('|')[count];
                        cmdData = cmdData.Replace("\r", "");
                        cmdData = cmdData.Replace("\n", "");
                        if (count == 1)
                        {
                            mp.sendEMVConfig("03", "02", cmdData);
                        }
                        else
                        {
                            mp.sendEMVConfig("03", "01", cmdData);
                        }
                        progressBarDwnld.Value = progressBarDwnld.Value + 3;
                    }
                    progressBarDwnld.Value = 60;
                }
                //Llaves
                buffer = eo.getEMVParams("6");
                if (!buffer.Trim().Equals(""))
                {
                    len = buffer.Split('|').Length;
                    for (count = 1; count < len; count++)
                    {
                        cmdData = buffer.Split('|')[count];
                        cmdData = cmdData.Replace("\r", "");
                        cmdData = cmdData.Replace("\n", "");
                        if (count == 1)
                        {
                            mp.sendEMVConfig("04", "02", cmdData);
                        }
                        else
                        {
                            mp.sendEMVConfig("04", "01", cmdData);
                        }
                        progressBarDwnld.Value = progressBarDwnld.Value + 1;
                    }                    
                }

                progressBarDwnld.Value = 98;
                //Finish Emv configuration
                mp.endEMVConfig();
                progressBarDwnld.Value = 100;
                Thread.Sleep(1000);
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this.Close();
            }
        }

    }
}
