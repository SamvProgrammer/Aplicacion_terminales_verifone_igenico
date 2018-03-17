using cpIntegracionEMV.data;
using cpIntegracionEMV.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cpIntegracionEMV.UI
{
    public partial class frmActualizando : Form
    {
        private string filePath;
        private string fileName;
        private static bool eventHandled;

        public frmActualizando()
        {
            InitializeComponent();
        }

        public frmActualizando(string path, string bat)
        {
            InitializeComponent();
            filePath = path;
            fileName = bat;
            eventHandled = false;
        }

        private void frmActualizando_Load(object sender, EventArgs e)
        {
            
            label1.Text = "Actualizando terminal... \r\n ¡Se recomienda no interrumpir!";
            this.Refresh();

        }

        private void frmActualizando_Shown(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "Actualizando terminal... \r\n ¡Se recomienda no interrumpir!";
                this.Refresh();

                Process myProcess = new Process();
                myProcess.StartInfo.FileName = filePath + fileName;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.UseShellExecute = false; 
                myProcess.EnableRaisingEvents = true;
                myProcess.Exited += new EventHandler(myProcess_Exited);
                myProcess.Start();


                while (!eventHandled)
                {
                    this.Refresh();
                }

                TypeUsuario.isUpdate = true;
                this.Close();

            }
            catch (Exception ex)
            {
                if (utilidadesMIT.ExisteCarpeta(Info.PathExe + "\\Load"))
                    Directory.Delete(Info.PathExe + "\\Load", true);

                MITLog.PrintLn(ex.Message);
                MessageBox.Show("No se pudo completar el proceso de actualización." + "\r\n" + "Es necesario ejecutar el programa con permisos de administrador", "Centro de Pagos - Actualización firmware", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TypeUsuario.isUpdate = false;
                this.Close();
            }

        }

        private static void myProcess_Exited(object sender, System.EventArgs e)
        {
            eventHandled = true;
        }

    }
}
