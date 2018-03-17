using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cpIntegracionEMV.util;
using System.IO;

namespace cpIntegracionEMV.data
{
    public static class Info
    {
        //Login
        //public static String cdusuario { get; set; }
        //C56 Information
        public static String marca { get; set; }
        public static String model { get; set; }
        public static String SerialNumber { get; set; }
        public static String version { get; set; }
        public static String EMVFULL { get; set; }
        public static String Printer { get; set; }
        public static String LoadKeys { get; set; }
        public static String KeysVersion { get; set; }
        public static String Kiosco { get; set; }
        public static String Dukpt { get; set; }
        public static String DukptKey { get; set; }
        public static String PanelSign { get; set; }
        public static String Contactless { get; set; }
        public static bool TieneCargaCTLS { get; set; }
        public static String forceonline { get; set; }
        public static String cipherdukptkey { get; set; }
        public static String kcv { get; set; }
        public static String ksn { get; set; }
        public static String ipek { get; set; }
        public static String IsTerminal { get; set; }
        public static String ErrorPP { get; set; }
        public static String COM { get; set; }
        public static String timeout { get; set; }

        //Magtek
        public static bool useMagtek { get; set; }


        //Crypto Information.
        public const String RC4Key = "KEY CREDIT CARD KEY";
        public const String RC4_FIRMAPANEL = "KEY INTERNAL MITPV APP KEY";
        public const String RC4_AEROPAY = "KEY AEROPCPAY KEY";

        public const String TRIPLEDES_KEY = "AlS0pIB5lm5h1L1EyqTonAJUtKSAeZZu";
        //public const String TRIPLEDES_KEY = "#<?V>E=ZqRmW*:9%rH7";

        //Llave Log
        public const String keyLog = "$M1T3C$";

        //Front Version.
        public static String super_version = utilidadesMIT.GetVersionApp();

        //dll version
        public static string dll_version = "CP-D " + utilidadesMIT.GetVersionDLL();

        //carpeta MIT
        public static string sPathCarpetaMIT = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\MIT";
        public static string PathExe = Directory.GetCurrentDirectory();

        //Logs
        //Flag para Logs de consola
        public static Boolean LOGS_CONSOLE = true;
        //Flag para Logs de archivo
        public static Boolean LOGS_FILE { get; set; }

        //Llave dinámica
        public static string DinamicKey { get; set; }

    }
}
