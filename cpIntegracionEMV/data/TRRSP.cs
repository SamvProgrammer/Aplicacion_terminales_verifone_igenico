using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpIntegracionEMV.data
{
    public static class TRRSP
    {
        public static String auth { get; set; }
        public static String cc_expmonth { get; set; }
        public static String cc_expyear { get; set; }
        public static String cc_name { get; set; }
        public static String cc_number { get; set; }
        public static String cc_type { get; set; }
        public static String cc_typeTemp { get; set; }
        public static String cd_error { get; set; }
        public static String cd_response { get; set; }
        public static String date { get; set; }
        public static String nb_street { get; set; }
        public static String nb_company { get; set; }
        public static String nb_error { get; set; }
        public static String nb_merchant { get; set; }
        public static String tp_operation { get; set; }
        public static String response { get; set; }
        public static String nb_response { get; set; }
        public static String friendly_response { get; set; }
        public static String foliocpagos { get; set; }
        public static String reference { get; set; }
        public static String time { get; set; }
        public static String amount { get; set; }


        public static String Cc_TokenB5 { get; set; }
        public static String Cc_TokenB6 { get; set; }
        public static String Cc_TokenBJ { get; set; }

        public static String emv_key_date { get; set; }
        public static String icc_csn { get; set; }
        public static String icc_atc { get; set; }
        public static String icc_arpc { get; set; }
        public static String icc_issuer_script { get; set; }
        public static String authorized_amount { get; set; }
        public static String account_balance_1 { get; set; }
        public static String arqc { get; set; }
        public static String appid { get; set; }
        public static String appidlabel { get; set; }
        public static String dcc_info { get; set; }
        public static String dcc_amount { get; set; }
        public static String rate { get; set; }
        public static String exponent_rate { get; set; }
        public static String cc_cdCurrency { get; set; }
        public static String cc_nbCurrency { get; set; }
        public static String cc_nbCurrencyCode { get; set; }
        public static String afil_nbCurrencyCode { get; set; }
        public static String cc_nbSimboloCurrency { get; set; }
        public static String nu_markup { get; set; }
        public static String cd_status { get; set; }
        public static String nb_status { get; set; }
        public static String saldo { get; set; }
        public static String voucher { get; set; }
        public static String voucher_cliente { get; set; }
        public static String voucher_comercio { get; set; }
        public static String esImprimibleVoucher { get; set; }
        public static String esTransaccionQPS { get; set; }
        //DUKPT INIT
        public static String nb_ksn { get; set; }
        public static String nb_ipek { get; set; }
        public static String nb_kcv { get; set; }
        //Factura electronica
        public static String Fe_txLeyenda { get; set; }
        public static String Fe_cdResponse { get; set; }
        public static String Fe_nbResponse { get; set; }
        //xml
        public static String xml { get; set; }

        //TAE
        public static String reciboTAE { get; set; }
    }
}
