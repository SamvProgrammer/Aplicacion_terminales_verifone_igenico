using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using cpIntegracionEMV.UI;

namespace cpIntegracionEMV.util
{
    public static class ClsFirmaPanel
    {
        //Formato Hexadecimal de la imagen
        public static string TextoHEXFirmaPanel { get; set; }

        //Almacena los errores que se puedan presentar
        public static string Error { get; set; }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);


        /// <summary>
        /// Detecta si la pantalla donde se ejecuta el programa es Touch
        /// </summary>
        /// <returns></returns>
        public static bool EsTouch()
        {

            foreach (TabletDevice tabletDevice in Tablet.TabletDevices)
            {
                //Only detect if it is a touch Screen not how many touches (i.e. Single touch or Multi-touch)
                if (tabletDevice.Type == TabletDeviceType.Touch)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Función para obtener la firma en Panel a través de un equipo Touch
        /// </summary>
        public static void ObtieneFirmaPanel(string textoMarcaAgua)
        {

            frmFirmaPanel frmFirma = new frmFirmaPanel(textoMarcaAgua);
            frmFirma.ShowDialog();

            if (frmFirma.ErrorFrm.Equals(""))
                TextoHEXFirmaPanel = frmFirma.StrHexadecimal;
            else
                Error = frmFirma.ErrorFrm;

        }


        //Codigo del cupon seleccionado
        public static int CodigoCupon { get; set; }

        //Almacena la respuesta
        public static string RespuestaFormCupon { get; set; }

        /// <summary>
        /// Función para obtener el form de Cupones
        /// </summary>
        public static void ObtieneCupones(string xml)
        {

            frmCupones frmCupon = new frmCupones(xml);
            frmCupon.ShowDialog();
            CodigoCupon = frmCupon.NumeroCupon;
            RespuestaFormCupon = frmCupon.Respuesta;
        }
    }
}
