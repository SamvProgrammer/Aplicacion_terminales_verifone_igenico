using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpIntegracionEMV.data
{
    public static class TypeUsuario
    {
        public static String Id_Company { get; set; }
        public static String nb_company { get; set; }
        public static String nb_user { get; set; }
        public static String nb_companystreet { get; set; }
        public static String Id_Branch { get; set; }
        public static String nb_branch { get; set; }
        public static String country { get; set; }
        public static String URL { get; set; }
        public static String URL_Instalador { get; set; }
        public static String iata { get; set; }
        public static String User { get; set; }
        public static String giro { get; set; }
        public static Boolean consumo { get; set; }
        public static String Pass { get; set; }
        public static String CadenaXML { get; set; }
        public static String RESPRODUCTOS { get; set; }
        //public static String catBanco { get; set; }
        //public static String ventaspropias { get; set; }
        public static Boolean encuesta { get; set; }
        public static String MXN { get; set; }
        public static String USD { get; set; }
        //public static Boolean IsVIP { get; set; }
        //public static Boolean IsEMVFull { get; set; }
        public static Boolean points2 { get; set; }
        public static Boolean facturaE { get; set; }
        public static Boolean emvReverso { get; set; }
        //public static String strVersion { get; set; }
        public static String rspError { get; set; }
        public static String pagomVMC { get; set; }
        public static String pagomAMEX { get; set; }
        public static String pagobVMC { get; set; }
        public static String pagobAMEX { get; set; }
        public static String pagobSIP { get; set; }
        public static String pagoavsVMC { get; set; }
        public static String pagoavsAMEX { get; set; }
        public static String pagoomVMC { get; set; }
        public static String pagoomAMEX { get; set; }
        public static String pagovtaforzadaVMC { get; set; }
        public static String pagovtaforzadaAMEX { get; set; }
        public static String type { get; set; }
        public static String ipKeyWeb { get; set; }
        public static String publicKeyRSA { get; set; }
        public static String RSAresp { get; set; }
        public static String RSAKeyData { get; set; }
        public static String RSAKeyDataMSG { get; set; }
        public static String RSAKeyDataLength { get; set; }
        //public static String RSAerror { get; set; }
        public static Boolean isUpdate { get; set; }
        public static Boolean ActivaMSI { get; set; }
        public static bool PayNoPain { get; set; }
        public static bool isRecompensas { get; set; }
        public static string dbgGetIsAgencia { get; set; } //Agencia - boletos
        public static string etiquetaReference { get; set; }

        //agencia
        public static string isAgencia { get; set; }
        public static string EmpresasAgencia { get; set; }

        public static bool MenuToken { get; set; } //token
        //EMV Parameters
        public static string st_update { get; set; }
        public static string keys_version { get; set; }
        public static string force_update { get; set; }

        //Calculadora DCC
        public static string afiliacionesDCC { get; set; }

        //Log
        public static bool SaveLogTransaction { get; set; }

        //CTLS
        public static double LimiteTrxCTLS { get; set; }

    }
}

