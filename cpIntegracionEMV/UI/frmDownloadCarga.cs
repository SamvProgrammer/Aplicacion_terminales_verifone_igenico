using cpIntegracionEMV.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Timers;

namespace cpIntegracionEMV.UI
{
    public partial class frmDownloadCarga : Form
    {

        public string URLDownload { get; set; }
        public string Path { get; set; }
        public string model { get; set; }
        public string PathDestino { get; set; }

        public frmDownloadCarga()
        {
            InitializeComponent();
        }

        private void frmDownloadCarga_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Refresh();

                //utilidadesMIT.DownloadFile(URLDownload + Path, PathDestino);

                //this.Close();

                //System.Timers.Timer aTimer;
                //aTimer = new System.Timers.Timer();
                ////aTimer.Interval = 2000;
                //aTimer.Elapsed += OnTimedEvent;
                //aTimer.AutoReset = false;
                //aTimer.Enabled = true;

                //aTimer.Start();
                ////this.Close();


                Thread oThread = new Thread(new ThreadStart(descargaFile));
                oThread.Start();
                while (oThread.IsAlive)
                { 
                    this.Refresh();
                };

                oThread.Abort();
                oThread.Join();
                this.Close();

            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
            }

        }

        private void descargaFile()
        {
            utilidadesMIT.DownloadFile(URLDownload + Path, PathDestino);
        }

        //private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        //{
        //    utilidadesMIT.DownloadFile(URLDownload + Path, PathDestino);
        //   // this.Close();
        //}


    }
}
