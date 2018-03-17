using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpIntegracionEMV.data
{
    public class Clear
    {
        public void ClearInfo()
        {
            Info.marca = "";
            Info.model = "";
            Info.SerialNumber = "";
            Info.version = "";
            Info.EMVFULL = "";
            Info.Printer = "";
            Info.LoadKeys = "";
            Info.KeysVersion = "";
            Info.Kiosco = "";
            Info.Dukpt = "";
            Info.DukptKey = "";
            Info.PanelSign = "";
            Info.Contactless = "0";
        }
        private void ClearTXN_TRINP()
        {
            TRINP.url = "";
            TRINP.contexto = "";
            TRINP.pwdusuario = "";
            TRINP.licencia = "";
            TRINP.huella = "";
            TRINP.dllversion = "";
            TRINP.canal = "";
            TRINP.P81MSG = "";
        
            //TXN
            TRINP.TRX_TYPE = 0;
            TRINP.Command = "";
            TRINP.DisplayTxt = "";
            TRINP.Date = "";
            TRINP.Time = "";
            TRINP.panmask = "";
            TRINP.expdate = "";
            TRINP.expmonth = "";
            TRINP.expyear = "";
            TRINP.cvvcsc = "";
            //TRINP.isAMEX = false;
            TRINP.useGetMerchant = false;
            TRINP.Tx_Add_Amount = "";
            TRINP.Bs_Branch = "";
            TRINP.GoOnline = "";
            TRINP.Key = "";
            TRINP.TimeOut = "";
            TRINP.RspType = "";
            TRINP.DsError = "";
            TRINP.afcont = "";   //Afiliaciones contado
            TRINP.afmsi  = "";   //Afiliaciones Meses sin intereses
            TRINP.afmci  = "";   //Afiliaciones Meses con intereses.
            TRINP.plazoMCI = "";
            TRINP.xmlRsp = "";
            TRINP.fallback = "";
            TRINP.NoEMVCard = "0";
            TRINP.foliocpagos = "";
            TRINP.DukptInit = false;
            TRINP.contactless = "0";
            TRINP.tx_boleto = "";
            TRINP.fh_salida = "";
            TRINP.fh_retorno = "";
            TRINP.avs_address = "";
            TRINP.avs_municipality = "";
            TRINP.avs_city = "";
            TRINP.avs_state = "";
            TRINP.avs_zip = "";
            TRINP.avs_district = "";
            TRINP.Tx_Tip = "";
            //Business
            TRINP.id_company = "";
            TRINP.id_branch = "";
            TRINP.Bs_Country = "";
            TRINP.Bs_User = "";
            TRINP.Bs_Pwd = "";
            //Transaction
            TRINP.Tx_Merchant = "";
            TRINP.Tx_Reference = "";
            TRINP.Tx_OperationType = "";
            //-->Credit card
            TRINP.Tx_Amount = "";
            TRINP.Tx_AmountBase = "";
            TRINP.Tx_Currency = "";
            TRINP.Tx_CurrencyCode = "";
            TRINP.Bs_UsrTransaction = "";
            TRINP.emv = "";
            TRINP.version = "";
            TRINP.serielector = "";
            TRINP.version_terminal = "";
            TRINP.modelo_terminal = "";
            //-->Terminal
            TRINP.tp_resp = "";
            //credit card
            TRINP.Crypto = "";
            TRINP.Cc_Type = "";
            TRINP.tracks = "";
            TRINP.cc_number = "";
            TRINP.chip = "";
            TRINP.tags = "";
            TRINP.CHName = "";
            TRINP.chipnameenc = "";
            TRINP.pin = "";
            TRINP.pose = "";
            TRINP.arqc = "";
            TRINP.aid = "";
            TRINP.applabel = "";
            //Terminal
            TRINP.printer = "";
            TRINP.display = "";
            TRINP.is_mobile = "";
            //dcc
            TRINP.dcc_status = "";
            TRINP.dcc_type = "";
            TRINP.dcc_process = false;
            //Cancelacion
            TRINP.Tx_OperationNumber = "";
            TRINP.Tx_Auth = "";

            //flags graphics
            TRINP.HidePopUpMerchant = false;

            //pinpad
            TRINP.chkPp_CdError = "";
            TRINP.chkPp_XmlError = "";
            TRINP.cancelop = false;
            TRINP.thread = false;

            //firma en pinpad
            TRINP.FirmaPinPad = "";
            TRINP.NumeroBloquesFPP = "";
            TRINP.FirmaPinPadByte = null;
            TRINP.BRecBuf = null;
            TRINP.contadorBytes  = 0;
            TRINP.strMailFirma = "";

            //Travel
            TRINP.isEmpRp3 = false;
            TRINP.numEmpresaRp3 = "";

            //TAE
            TRINP.TAEidCategoria = "";
            TRINP.TAEidProducto = "";
            TRINP.TAEidProveedor = "";
            TRINP.TAEAmount = "";
            TRINP.TAENumTel = "";
            TRINP.TAEisEfectivo = false;

            //Cupones
            TRINP.phone = "";
            
        }

        private void ClearTXN_TRRSP()
        {
            TRRSP.auth ="";
            TRRSP.cc_expmonth ="";
            TRRSP.cc_expyear ="";
            TRRSP.cc_name ="";
            TRRSP.cc_number ="";
            TRRSP.cc_type ="";
            TRRSP.cd_error ="";
            TRRSP.cd_response ="";
            TRRSP.date ="";
            TRRSP.nb_street ="";
            TRRSP.nb_company ="";
            TRRSP.nb_error ="";
            TRRSP.nb_merchant ="";
            TRRSP.tp_operation ="";
            TRRSP.response ="";
            TRRSP.nb_response ="";
            TRRSP.friendly_response ="";
            TRRSP.foliocpagos ="";
            TRRSP.reference ="";
            TRRSP.time ="";
            TRRSP.amount ="";

            TRRSP.Cc_TokenB5 = "";
            TRRSP.Cc_TokenB6 = "";
            TRRSP.Cc_TokenBJ = "";

            TRRSP.emv_key_date ="";
            TRRSP.icc_csn ="";
            TRRSP.icc_atc ="";
            TRRSP.icc_arpc ="";
            TRRSP.icc_issuer_script ="";
            TRRSP.authorized_amount ="";
            TRRSP.account_balance_1 ="";
            TRRSP.arqc ="";
            TRRSP.appid ="";
            TRRSP.appidlabel ="";
            TRRSP.dcc_info ="";
            TRRSP.dcc_amount = "";
            TRRSP.rate ="";
            TRRSP.exponent_rate ="";
            TRRSP.cc_cdCurrency ="";
            TRRSP.cc_nbCurrency ="";
            TRRSP.cc_nbCurrencyCode ="";
            TRRSP.afil_nbCurrencyCode ="";
            TRRSP.cc_nbSimboloCurrency ="";
            TRRSP.nu_markup ="";
            TRRSP.cd_status ="";
            TRRSP.nb_status ="";
            TRRSP.saldo ="";
            TRRSP.voucher = "";
            TRRSP.voucher_cliente ="";
            TRRSP.voucher_comercio ="";
            TRRSP.esImprimibleVoucher ="";
            TRRSP.esTransaccionQPS = "";
            TRRSP.nb_ksn="";
            TRRSP.nb_kcv="";
            TRRSP.nb_ipek="";
            TRRSP.Fe_txLeyenda = "";

            //TAE
            TRRSP.reciboTAE = "";
            

        }
        public void ClearTXN()
        {
            ClearTXN_TRINP();
            ClearTXN_TRRSP();
        }
    }
}
