using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpIntegracionEMV.data
{
    public static class TRINP
    {
        
        public static String url { get; set; }
        public static String contexto { get; set; }
        //public static String cdusuario { get; set; }
        public static String pwdusuario { get; set; }
        public static String licencia { get; set; }
        public static String huella { get; set; }
        public static String dllversion { get; set; }
        public static String canal { get; set; }
        public static String P81MSG { get; set; }
        
        //TXN
        public static Byte TRX_TYPE { get; set; }
        public static String Command { get; set; }
        public static String DisplayTxt { get; set; }
        public static String Date { get; set; }
        public static String Time { get; set; }
        public static String panmask { get; set; }
        public static String expdate { get; set; }
        public static String expmonth { get; set; }
        public static String expyear { get; set; }
        public static String cvvcsc { get; set; }
        public static Boolean isAMEX { get; set; }
        public static Boolean useGetMerchant { get; set; }
        public static String Tx_Add_Amount { get; set; }
        public static String Bs_Branch { get; set; }
        public static String GoOnline { get; set; }
        public static String Key { get; set; }
        public static String TimeOut { get; set; }
        public static String RspType { get; set; }
        public static String DsError { get; set; }
        public static String afcont { get; set; }   //Afiliaciones contado
        public static String afmsi { get; set; }    //Afiliaciones Meses sin intereses
        public static String afmci { get; set; }    //Afiliaciones Meses con intereses.
        public static String plazoMCI { get; set; }
        public static String xmlRsp { get; set; }
        public static String fallback { get; set; }
        public static String NoEMVCard { get; set; } //Tarjeta no bancaria.
        public static String ksn { get; set; }
        //public static Boolean isQualitas { get; set; }
        public static String foliocpagos { get; set; }
        public static Boolean DukptInit { get; set; }
        public static String contactless { get; set; }
        //Agencia
        public static String tx_boleto { get; set; }
        public static String fh_salida { get; set; }
        public static String fh_retorno { get; set; }
        
        //AVS MOTO
        public static String avs_address { get; set; }
        public static String avs_municipality { get; set; }
        public static String avs_city { get; set; }
        public static String avs_state { get; set; }
        public static String avs_zip { get; set; }
        public static String avs_district { get; set; }
        //Restaurante
        public static String Tx_Tip { get; set; }
        //Hotel
        public static String Tx_Room { get; set; }
        public static String cavv { get; set; }
        public static String eci { get; set; }
        public static String xid { get; set; }
        
        //Business
        public static String id_company { get; set; }
        public static String id_branch { get; set; }
        public static String Bs_Country { get; set; }
        public static String Bs_User { get; set; }
        public static String Bs_Pwd { get; set; }
        //Transaction
        public static String Tx_Merchant { get; set; }
        public static String Tx_Reference { get; set; }
        public static String Tx_OperationType { get; set; }
        //-->Credit card
        public static String Tx_Amount { get; set; }
        public static String Tx_AmountBase { get; set; }
        public static String Tx_Currency { get; set; }
        public static String Tx_CurrencyCode { get; set; }
        public static String Bs_UsrTransaction { get; set; }
        public static String emv { get; set; }
        public static String version { get; set; }
        public static String serielector { get; set; }
        public static String version_terminal { get; set; }
        public static String modelo_terminal { get; set; }
        //-->Terminal
        public static String tp_resp { get; set; }
        //credit card
        public static String Crypto { get; set; }
        public static String Cc_Type { get; set; }
        public static String tracks { get; set; }
        public static String cc_number { get; set; }
        public static String chip { get; set; }
        public static String tags { get; set; }
        public static String CHName { get; set; }
        public static String chipnameenc { get; set; }
        public static String pin { get; set; }
        public static String pose { get; set; }
        public static String arqc { get; set; }
        public static String aid { get; set; }
        public static String applabel { get; set; }
        //Terminal
        public static String printer { get; set; }
        public static String display { get; set; }
        public static String is_mobile { get; set; }
        //dcc
        public static String dcc_status { get; set; }
        public static String dcc_amount { get; set; }
        public static String dcc_type { get; set; }
        public static bool dcc_process { get; set; }
        //Cancelacion
        public static String Tx_OperationNumber { get; set; }
        public static String Tx_Auth { get; set; }
        //Recompensas
        public static String RecomAmount { get; set; }
        public static String RecomCopia { get; set; }
        public static String RecomPrtOpt { get; set; }
        public static String RecomTipoReporte { get; set; }
        public static String RecomTipoTicket { get; set; }
        //pinpad
        public static String pinpadrsp { get; set; }
        public static Boolean cancelop { get; set; }
        public static Boolean thread { get; set; }
        //flags graphics
        public static Boolean HidePopUpMerchant { get; set; }
        public static Boolean HidePopUpCurrency { get; set; }

        //Restaurant consumo
        public static string Tx_Waiter { get; set; }
        public static string Tx_Shifts { get; set; }
        public static string Tx_Table { get; set; }

        //errores en pinpad
        public static string chkPp_CdError { get; set; }
        public static string chkPp_XmlError { get; set; }

        //Firma en PinPad
        public static string NumeroBloquesFPP { get; set; }
        public static string FirmaPinPad { get; set; }
        public static byte[] FirmaPinPadByte { get; set; }
        public static byte[] BRecBuf { get; set; }
        public static int contadorBytes { get; set; }
        public static string strMailFirma { get; set; }
 
        public static String tipoPago { get; set; }    //Tipo de pago.

        //Travel
        public static bool isEmpRp3 { get; set; }
        public static string numEmpresaRp3 { get; set; }

        //TAE
        public static string TAEidCategoria { get; set; }
        public static string TAEidProducto { get; set; }
        public static string TAEidProveedor { get; set; }
        public static string TAEAmount { get; set; }
        public static string TAENumTel { get; set; }
        public static bool TAEisEfectivo { get; set; }

        //Cupones
        public static string phone { get; set; }

        //Update PinPad
        public static bool isCargaVerifone { get; set; }
        public static bool isCargaIngenico { get; set; }
        public static string xmlPinPad { get; set; }
    }
}
