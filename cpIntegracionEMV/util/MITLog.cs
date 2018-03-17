using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using cpIntegracionEMV.data;
using cpIntegracionEMV.com;
using cpIntegracionEMV.security;

namespace cpIntegracionEMV.util
{
    class MITLog
    {

        private static string cadenaCifrada = "";
        private static string llave = "";

        private static Boolean saveFile(String Log)
        {
            try
            {

                if(llave.Equals(""))
                {
                    if (string.IsNullOrEmpty(TypeUsuario.User))
                        llave = Info.keyLog;
                    else
                        llave = TypeUsuario.User.Substring(0, 4) + Info.keyLog + TypeUsuario.User.Substring(4);
                }
                
                cadenaCifrada = "";

                String path = Directory.GetCurrentDirectory();
                if(!Directory.Exists(path+"\\Log"))
                {
                    Directory.CreateDirectory(path+"\\Log");
                }

                StreamWriter writer = File.AppendText(path + "\\Log" + "\\dll" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".log");
                cadenaCifrada = RC4.Encrypt(Log,llave);
                writer.WriteLine(cadenaCifrada);
                writer.WriteLine("--------------------------------------------");
                writer.Close();
                return true;
            }
            catch(IOException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public static Boolean PrintLn(String Log)
        {
            try
            {
                BeanCom bcom = new BeanCom();
                if (!Log.Equals(""))
                {
                    Log = Log.Replace("" + bcom.getNull(), "x00"); //Replace null
                    Log = "MITLOG " + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "_" + DateTime.Now.ToLongTimeString().Replace(":", "").Substring(0, 6) + ": " + Log;
                    
                    if (Info.LOGS_CONSOLE)
                        Console.WriteLine(Log);

                    if (Info.LOGS_FILE)
                        saveFile(Log);

                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
