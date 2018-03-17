using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml;
using cpIntegracionEMV.com;
using cpIntegracionEMV.data;
using cpIntegracionEMV.security;
using cpIntegracionEMV.UI;
using cpIntegracionEMV.util;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace cpIntegracionEMV.txn_flow
{
    
    public class EjecutaOperacion
    {
        WS ws = new WS();
        bitmapXML bm = new bitmapXML();
        BeanCom bc = new BeanCom();
        MITProtocol mp = new MITProtocol();
        Clear clear = new Clear();
        crypto cr = new crypto();

        //imagen Firma en panel
        Imagen objImage = new Imagen();

        private bool parseUser(String xmlResponse, String strUsr, String strPwd)
        {
            try
            {
                TypeUsuario.CadenaXML = xmlResponse;
                if (bm.getDataXML("response", xmlResponse).Trim().ToLower().Equals("true"))
                {
                    //Se crean las carpetas para guardas la info
                    if (!utilidadesMIT.ExisteCarpeta(Info.sPathCarpetaMIT))
                    {
                        utilidadesMIT.CreaCarpeta(Info.sPathCarpetaMIT);
                        utilidadesMIT.CreaCarpeta(Info.sPathCarpetaMIT + "\\Data");
                        utilidadesMIT.CreaCarpeta(Info.sPathCarpetaMIT + "\\Load");
                        utilidadesMIT.CreaCarpeta(Info.sPathCarpetaMIT + "\\Log");
                    }

                    //**************************************************************************************************
                    //Se agrega para validar el archivo de bines
                    if ((bm.getDataXML("actualiza_archivo_bines", xmlResponse).Equals("1")) || !System.IO.File.Exists(Info.sPathCarpetaMIT + "\\Data\\numeros.txt"))
                    {
                        utilidadesMIT.DownloadFile(TRINP.url + "/pgs/jsp/cpagos/numeros.txt", Info.sPathCarpetaMIT + "\\Data\\numeros.txt");
                        utilidadesMIT.EncriptaBines(Info.sPathCarpetaMIT + "\\Data\\numeros.txt");
                    }

                    //**************************************************************************************************
                    //ACTUALIZA LOS BINES PARA LAS TARJETAS SANTANDER
                     //If (getDataXML("st_recompensas_sant", TypeUsuario.CadenaXML) = "1") Then
                     if (!System.IO.File.Exists(Info.sPathCarpetaMIT + "\\Data\\numsant.txt"))
                     {
                         utilidadesMIT.DownloadFile(TRINP.url + "/pgs/jsp/cpagos/numsant.txt", Info.sPathCarpetaMIT + "\\Data\\numsant.txt");
                         utilidadesMIT.EncriptaBines(Info.sPathCarpetaMIT + "\\Data\\numsant.txt");
                     }
                    //End If
                    
                    //**************************************************************************************************
                    //Se agrega para validar el archivo de bines AMEX
                    if( (bm.getDataXML("actualiza_archivo_bines", xmlResponse).Equals("1")) || !System.IO.File.Exists(Info.sPathCarpetaMIT + "\\Data\\amex.txt") )
                    {
                        utilidadesMIT.DownloadFile(TRINP.url +  "/pgs/jsp/cpagos/amex.txt", Info.sPathCarpetaMIT + "\\Data\\amex.txt");
                        utilidadesMIT.EncriptaBines(Info.sPathCarpetaMIT + "\\Data\\amex.txt");
                    }
   
                    TypeUsuario.Id_Company = bm.getDataXML("id_company", xmlResponse);
                    TypeUsuario.nb_company = bm.getDataXML("nb_company", xmlResponse);
                    TypeUsuario.nb_user = bm.getDataXML("nb_user", xmlResponse);
                    TypeUsuario.nb_companystreet = bm.getDataXML("nb_companystreet", xmlResponse);
                    TypeUsuario.Id_Branch = bm.getDataXML("id_branch", xmlResponse);
                    TypeUsuario.nb_branch = bm.getDataXML("nb_branch", xmlResponse);
                    TypeUsuario.country = bm.getDataXML("country", xmlResponse);
                    //TypeUsuario.URL = bm.getDataXML("url", xmlResponse);
                    TypeUsuario.iata = bm.getDataXML("", xmlResponse);
                    TypeUsuario.User = strUsr;
                    TypeUsuario.giro = bm.getDataXML("cd_giro", xmlResponse);
                    if (bm.getDataXML("st_mesa", xmlResponse) == "1")
                    {
                        TypeUsuario.consumo = true;
                    }
                    else
                    {
                        TypeUsuario.consumo = false;
                    }
                    TypeUsuario.Pass = strPwd;
                    //TypeUsuario.CadenaXML = xmlResponse;
                    TypeUsuario.RESPRODUCTOS = bm.getDataXML("RESPRODUCTOS", xmlResponse);
                    //TypeUsuario.catBanco = bm.getDataXML("", xmlResponse);
                    //TypeUsuario.ventaspropias = bm.getDataXML("", xmlResponse);
                    if (bm.getDataXML("encuesta", xmlResponse) == "1")
                    {
                        TypeUsuario.encuesta = true;
                    }
                    else
                    {
                        TypeUsuario.encuesta = false;
                    }
                    TypeUsuario.MXN = bm.getDataXML("MXN", xmlResponse);
                    TypeUsuario.USD = bm.getDataXML("USDLS", xmlResponse);
                    //TypeUsuario.IsVIP = bm.getDataXML("", xmlResponse);
                    //TypeUsuario.IsEMVFull = bm.getDataXML("", xmlResponse);
                    if (bm.getDataXML("points2", xmlResponse) == "1")
                    {
                        TypeUsuario.points2 = true;
                    }
                    else
                    {
                        TypeUsuario.points2 = false;
                    }
                    if (bm.getDataXML("facturaElectronica", xmlResponse) == "1")
                    {
                        TypeUsuario.facturaE = true;
                    }
                    else
                    {
                        TypeUsuario.facturaE = false;
                    }
                    if (bm.getDataXML("emvReverso", xmlResponse) == "1")
                    {
                        TypeUsuario.emvReverso = true;
                    }
                    else
                    {
                        TypeUsuario.emvReverso = false;
                    }
                    //TypeUsuario.strVersion = bm.getDataXML("", xmlResponse);
                    TypeUsuario.rspError = bm.getDataXML("", xmlResponse);
                    TypeUsuario.pagomVMC = bm.getDataXML("tipopagomVMC", xmlResponse);
                    TypeUsuario.pagomAMEX = bm.getDataXML("tipopagomAMEX", xmlResponse);
                    TypeUsuario.pagobVMC = bm.getDataXML("tipopagobVMC", xmlResponse);
                    TypeUsuario.pagobAMEX = bm.getDataXML("tipopagobAMEX", xmlResponse);
                    TypeUsuario.pagobSIP = bm.getDataXML("tipopagobSIP", xmlResponse);
                    TypeUsuario.pagoavsVMC = bm.getDataXML("tipopagoavsVMC", xmlResponse);
                    TypeUsuario.pagoavsAMEX = bm.getDataXML("tipopagoavsAMEX", xmlResponse);
                    TypeUsuario.pagoomVMC = bm.getDataXML("tipopagoomVMC", xmlResponse);
                    TypeUsuario.pagoomAMEX = bm.getDataXML("tipopagoomAMEX", xmlResponse);
                    TypeUsuario.pagovtaforzadaVMC = bm.getDataXML("tipopagovtaforzadaVMC", xmlResponse);
                    TypeUsuario.pagovtaforzadaAMEX = bm.getDataXML("tipopagovtaforzadaAMEX", xmlResponse);
                    TypeUsuario.type = bm.getDataXML("tpRespuestaTerminal", xmlResponse);
                    if (TypeUsuario.type == "")
                    {
                        TypeUsuario.type = "TAGS";
                    }

                    TypeUsuario.isUpdate = true;
                    this.UpdatePinPad();

                    //******************************************************************************
                    //CUPONES MIT
                    if (bm.getDataXML("activa_cupones", xmlResponse) == "1")
                        TypeUsuario.PayNoPain = true;
                    else
                        TypeUsuario.PayNoPain = false;

                    //******************************************************************************
                    //LIMITE PARA UNA TRX POR CTLS
                    if (utilidadesMIT.GetDataXML("nu_floor_limit_ctls", TypeUsuario.CadenaXML).Equals("") || utilidadesMIT.GetDataXML("nu_floor_limit_ctls", TypeUsuario.CadenaXML).Equals(" "))
                        TypeUsuario.LimiteTrxCTLS = 0;
                    else
                    {   double aux;
                        double.TryParse(utilidadesMIT.GetDataXML("nu_floor_limit_ctls", TypeUsuario.CadenaXML), out aux);
                        TypeUsuario.LimiteTrxCTLS = aux;
                    }
                    
                    //Se agrega para validar las afiliaciones a utilizar en la calculadora DCC
                    TypeUsuario.afiliacionesDCC = bm.getDataXML("dcc_af", xmlResponse);
                    
                    //Se pasa arriba para utilizarlo en PcPay tambien
                    TypeUsuario.User = strUsr;

                    //******************************************************************************
                    //Activa Recompensas
                    if (bm.getDataXML("st_recompensas_sant", xmlResponse) == "1")
                        TypeUsuario.isRecompensas = true;
                    else
                        TypeUsuario.isRecompensas = false;
                    
                    //pendiente
                    TypeUsuario.dbgGetIsAgencia = "0";

                    //etiquetareferencia
                    TypeUsuario.etiquetaReference = bm.getDataXML("reference", xmlResponse);

                    //****************************************************************************************************
                    //TOKENIZACION
                    if (bm.getDataXML("st_tokenizacion", xmlResponse) == "1")
                        TypeUsuario.MenuToken = true;
                    else
                        TypeUsuario.MenuToken = false;
                    
                    //Parametros EMV
                    TypeUsuario.st_update = bm.getDataXML("st_update", xmlResponse);
                    TypeUsuario.keys_version = bm.getDataXML("keys_version", xmlResponse);
                    TypeUsuario.force_update = bm.getDataXML("force_update", xmlResponse);
                    updateEMVParams();

                    //Agencia
                    TypeUsuario.EmpresasAgencia = bm.getDataXML("catempresas", xmlResponse);

                    //Se Activa el log
                    if (utilidadesMIT.GetDataXML("log", TypeUsuario.CadenaXML).Equals("1"))
                        TypeUsuario.SaveLogTransaction = true;
                    else
                        TypeUsuario.SaveLogTransaction = false;

                    this.EnabledLog();

                }
                else
                {
                    TypeUsuario.rspError = bm.getDataXML("error", xmlResponse);
                    if (TypeUsuario.rspError.Equals(""))
                    {
                        TypeUsuario.rspError = "Servicio no disponible";
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
                return false;
            }

            return true;
        }
        //Login.
        public bool pcpayLogin8(String strUsr, String strPwd)
        {
            String xmlResponse = "";    
            //Connect port and Get device information.
            /*if(!mp.connect())
            {
                return false;
            }*/

            mp.about();

            // Get Login Information
            TRINP.canal = "1";
            TRINP.url = TypeUsuario.URL;
            TypeUsuario.User = strUsr;
            TRINP.pwdusuario = RC4.Encrypt(strPwd, Info.RC4Key);
            //TRINP.licencia = "0012CF4300201853A753";
            //TRINP.licencia = "";
            //TRINP.huella = "D41D8CD98F00B204E9800998ECF8427E";
            //TRINP.huella = "";
            TRINP.dllversion = utilidadesMIT.GetVersionDLL();
            
            //build XML to send
            try
            {
                TRINP.contexto = "/pgs/pcpayLogin8";
                //Build XML
                String xml = "cdUsuario=" + TypeUsuario.User;
                xml += "&pwdUsuario=" + TRINP.pwdusuario;
                //xml += "&licencia=" + TRINP.licencia;
                //xml += "&huella=" + TRINP.huella;
                xml += "&crypto=";
                xml += "&version=" + TRINP.dllversion;
                xml += "&serieLector=" + TRINP.serielector;
                xml += "&canal=" + TRINP.canal;

                //Arma mensaje XML Request
                //bm.ArmaXML(); //NA
                
                //Send and Request Message
                xmlResponse = ws.SendWS(TRINP.url + "" + TRINP.contexto, xml);
                if (xmlResponse != "")
                {
                    return parseUser(xmlResponse, strUsr, strPwd);                    
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                MITLog.PrintLn(ex.Message);
                return false;
            }
        }
        //Set response values
        private void setResponse(String xmlResponse)
        {
            //xml Backup
            TRRSP.xml = xmlResponse;
            //TXN
            TRRSP.auth = bm.getDataXML("auth", xmlResponse);
            TRRSP.cc_expmonth = bm.getDataXML("cc_expmonth", xmlResponse);
            TRRSP.cc_expyear = bm.getDataXML("cc_expyear", xmlResponse);
            TRRSP.cc_name = bm.getDataXML("cc_name", xmlResponse);
            TRRSP.cc_number = bm.getDataXML("cc_number", xmlResponse);
            TRRSP.cc_type = bm.getDataXML("cc_type", xmlResponse);
            TRRSP.cd_error = bm.getDataXML("cd_error", xmlResponse);
            TRRSP.cd_response = bm.getDataXML("cd_response", xmlResponse);
            TRRSP.date = bm.getDataXML("date", xmlResponse);
            TRRSP.nb_street = bm.getDataXML("nb_street", xmlResponse);
            TRRSP.nb_company = bm.getDataXML("nb_company", xmlResponse);
            TRRSP.nb_error = bm.getDataXML("nb_error", xmlResponse);
            TRRSP.nb_merchant = bm.getDataXML("nb_merchant", xmlResponse);
            TRRSP.tp_operation = bm.getDataXML("tp_operation", xmlResponse);
            TRRSP.response = bm.getDataXML("response", xmlResponse);
            TRRSP.nb_response = bm.getDataXML("nb_response", xmlResponse);
            TRRSP.friendly_response = bm.getDataXML("friendly_response", xmlResponse);
            TRRSP.foliocpagos = bm.getDataXML("foliocpagos", xmlResponse);  //trx.OperationNumber
            TRRSP.reference = bm.getDataXML("reference", xmlResponse);
            TRRSP.time = bm.getDataXML("time", xmlResponse);
            TRRSP.amount = bm.getDataXML("amount", xmlResponse);

            TRRSP.Cc_TokenB5 = bm.getDataXML("tokenb5", xmlResponse);
            TRRSP.Cc_TokenB6 = bm.getDataXML("tokenb6", xmlResponse);
            TRRSP.Cc_TokenBJ = bm.getDataXML("tokenbj", xmlResponse);

            TRRSP.emv_key_date = bm.getDataXML("emv_key_date", xmlResponse);
            TRRSP.icc_csn = bm.getDataXML("icc_csn", xmlResponse);
            TRRSP.icc_atc = bm.getDataXML("icc_atc", xmlResponse);
            TRRSP.icc_arpc = bm.getDataXML("icc_arpc", xmlResponse);
            TRRSP.icc_issuer_script = bm.getDataXML("icc_issuer_script", xmlResponse);
            TRRSP.authorized_amount = bm.getDataXML("authorized_amount", xmlResponse);
            TRRSP.account_balance_1 = bm.getDataXML("account_balance_1", xmlResponse);
            TRRSP.arqc = bm.getDataXML("arqc", xmlResponse);
            TRRSP.appid = bm.getDataXML("appid", xmlResponse);
            TRRSP.appidlabel = bm.getDataXML("appidlabel", xmlResponse);
            //DCC
            TRRSP.dcc_info = bm.getDataXML("dcc_info", xmlResponse);
            TRRSP.dcc_amount = bm.getDataXML("amount", TRRSP.dcc_info);
            TRRSP.rate = bm.getDataXML("rate", TRRSP.dcc_info);
            TRRSP.exponent_rate = bm.getDataXML("exponent_rate", TRRSP.dcc_info);
            TRRSP.cc_cdCurrency = bm.getDataXML("cc_cdCurrency", TRRSP.dcc_info);
            TRRSP.cc_nbCurrency = bm.getDataXML("cc_nbCurrency", TRRSP.dcc_info);
            TRRSP.cc_nbCurrencyCode = bm.getDataXML("cc_nbCurrencyCode", TRRSP.dcc_info);
            TRRSP.afil_nbCurrencyCode = bm.getDataXML("afil_nbCurrencyCode", TRRSP.dcc_info);
            TRRSP.cc_nbSimboloCurrency = bm.getDataXML("cc_nbSimboloCurrency", TRRSP.dcc_info);
            TRRSP.nu_markup = bm.getDataXML("nu_markup", TRRSP.dcc_info);
            TRRSP.cd_status = bm.getDataXML("cd_status", xmlResponse);
            TRRSP.nb_status = bm.getDataXML("nb_status", xmlResponse);
            TRRSP.saldo = bm.getDataXML("saldo", xmlResponse);
            
           
            TRRSP.voucher_cliente = utilidadesMIT.VerificaVoucher(bm.getDataXML("voucher_cliente", xmlResponse),"", TRRSP.foliocpagos);
            TRRSP.voucher_comercio = utilidadesMIT.VerificaVoucher(bm.getDataXML("voucher_comercio", xmlResponse), "", TRRSP.foliocpagos);

            if (!TRRSP.voucher_comercio.Equals(""))
                TRRSP.voucher = "<voucher_comercio>" + TRRSP.voucher_comercio + "</voucher_comercio>" + "<voucher_cliente>" + TRRSP.voucher_cliente + "</voucher_cliente> ";
            else
                TRRSP.voucher = utilidadesMIT.VerificaVoucher(bm.getDataXML("voucher", xmlResponse), "", TRRSP.foliocpagos);

            TRRSP.voucher_cliente = utilidadesMIT.RevisaVoucher(TRRSP.voucher_cliente);
            TRRSP.voucher_comercio = utilidadesMIT.RevisaVoucher(TRRSP.voucher_comercio);

            TRRSP.esImprimibleVoucher = bm.getDataXML("st_voucher", xmlResponse);
            TRRSP.esTransaccionQPS = bm.getDataXML("st_qps", xmlResponse);

            //DUKPT INIT
            TRRSP.nb_ksn = bm.getDataXML("nb_ksn", xmlResponse);
            TRRSP.nb_ipek = bm.getDataXML("nb_ipek", xmlResponse);
            TRRSP.nb_kcv = bm.getDataXML("nb_kcv", xmlResponse);

            //Set dinamic values.
            if (TRRSP.cd_status.Equals("2"))
            {
                TRRSP.response = "approved";
                TRRSP.cd_response = "00";
            }
            else if (TRRSP.cd_status.Equals("1"))
            {
                TRRSP.response = "denied";
                TRRSP.cd_response = "01";
            }
            else if (TRRSP.cd_status.Equals("0"))
            {
                TRRSP.response = "error";
                TRRSP.cd_response = "01";
            }

            //Si no tiene respuesta de pagos
            if (TRRSP.cd_response.Equals(""))
                TRRSP.cd_response = "01";

            //respuesta aprobada de pagos
            if (TRRSP.cd_response.Equals("0C"))
                TRRSP.cd_response = "00";

            //Factura electronica
            TRRSP.Fe_txLeyenda = bm.getDataXML("tx_leyenda", xmlResponse);
            TRRSP.Fe_cdResponse = bm.getDataXML("cd_response", xmlResponse);
            TRRSP.Fe_nbResponse = bm.getDataXML("nb_response", xmlResponse);
            
            //DUKPT Keys validate.
            if(TRRSP.cd_error != null)
            {
                if (TRRSP.cd_error.Trim().Equals("92") && !TRINP.DukptInit)
                {
                    TRINP.DukptInit = true; 

                    if (dukptInit())
                    {

                        setDUKPT(Info.ksn, Info.kcv, Info.ipek);
                        //xmlResponse = TRRSP.xml;
                    }
                  

                    setResponse(xmlResponse);                        
                }
            }

            //IDENTIFICA LA TARJETA (TDC - TDD)
            if (!TRRSP.cc_type.Equals(""))
                TRRSP.cc_typeTemp = TRRSP.cc_type;
            

        }

        private Boolean sendMsg([Optional] string varInicial)
        {
            String XML = "";
            String xmlResponse = "";
            String AesKey = "";
            String RsaKey = "";
            
            //Build XML Request
            XML = bm.ArmaXML(); //NA

            //AES Encrypt.
            AesKey = EncryptC.ObtieneRandomAESKey();
            MITLog.PrintLn("Key Aes:" + AesKey);
            if (!AesKey.Equals(""))
            {
                RsaKey = EncryptC.EncryptRSA(AesKey, TypeUsuario.publicKeyRSA);
                RsaKey = EncryptC.CodificaCaracteresXML(RsaKey);

                XML = EncryptC.encrypInAES128(AesKey, XML);
                XML = EncryptC.CodificaCaracteresXML(XML);

                if (string.IsNullOrEmpty(varInicial))
                    XML = "xml=<pgs><data0>" + RsaKey + "</data0><data>" + XML + "</data></pgs>";
                else
                    XML = varInicial + "=<pgs><data0>" + RsaKey + "</data0><data>" + XML + "</data></pgs>";
            }

            xmlResponse = ws.SendWS(TRINP.url + "" + TRINP.contexto, XML);

            //Is there host response?
            if (!xmlResponse.Equals(""))
            {
                //no viene cifrada la respuesta(nuevo AES)
                if (!xmlResponse.Contains("<cd_error>98</cd_error>"))
                {
                    MITLog.PrintLn("XML Response PGS:" + xmlResponse);

                    //Aes Decrypt
                    xmlResponse = EncryptC.DescodificaCaracteresXML(xmlResponse);

                    MITLog.PrintLn("XML Response decodificado:" + xmlResponse);

                    xmlResponse = EncryptC.descrypInAES128(AesKey, xmlResponse);
                    xmlResponse = utilidadesMIT.ValidaCadena(xmlResponse);
                }
                else
                { 
                    //No viene respuesta de PGS
                    //xmlResponse = xmlResponse.Replace("<cd_response/>", "<cd_response>01</cd_response>");
                }

                MITLog.PrintLn("XML Response:" + xmlResponse);
                //Save Response
                setResponse(xmlResponse);
                return true;                
            }
            return false;
        }
        private Boolean sendMsgSoap(String Action, String nbparams, String method)
        {
            //Build XML Request
            String XML = bm.ArmaXML(); //NA
            //Send message to host.
            String Result = ws.SendWSSoap(TRINP.url + TRINP.contexto, XML, Action, nbparams, method);

            if (!Result.Equals(""))
            {
                //Save Response
                setResponse(Result);
                return true;
            }
            return false;
        }


        private string sendMsgSoap(String Action, String nbparams, String method, String XML)
        {
            //Build XML Request
            //String XML = bm.ArmaXML(); //NA
            //Send message to host.
            String Result = ws.SendWSSoap(TRINP.url + TRINP.contexto, XML, Action,  method);

            if (!Result.Equals(""))
            {
                return Result;
            }
            else
                return "";
        }

        private void getTxDate()
        {
            TRINP.Date = DateTime.Now.ToShortDateString().Replace("/", "").Substring(0, 4);
            TRINP.Date = TRINP.Date + DateTime.Now.Year.ToString().Substring(2, 2);
        }
        private void getTxTime()
        {
            //TRINP.Time = DateTime.Now.ToShortTimeString().Replace(":", "");
            TRINP.Time = DateTime.Now.ToString("HHmm");
        }
        /*** Funciones de la venta ****/
        private Boolean StartTxEMVProcess(String Tx_Amount)
        {
            double valorTemp;
            double.TryParse(Tx_Amount, out valorTemp);

            TRINP.isAMEX = false;

             //Check port connection.
            if (!mp.connect())
            {
                //Not connected
                TRINP.chkPp_CdError = "No hay respuesta del lector, verifique que se encuentra conectado.";
                Info.ErrorPP = "PPE03";
                return false;
            }

            //is it DUKPT.
            if (Info.Dukpt.Equals("1"))
            {
                TRINP.Command = "C93";
            }
            else
            {
                TRINP.Command = "C71";
            }
            TRINP.DisplayTxt = "INSERTE CHIP O  DESLICE TARJETA";
            
            if (Info.Contactless != null)
            {
                if (Info.Contactless.Equals("1"))
                {
                    if (valorTemp <= TypeUsuario.LimiteTrxCTLS)
                        TRINP.DisplayTxt = "ACERQUE, INSERTE CHIP O  DESLICE TARJETA";
                }
            }
            
            getTxDate(); // get Date
            getTxTime(); //get Time            
            
            TRINP.Tx_Amount = Tx_Amount;

            TRINP.GoOnline = "1";
            TRINP.RspType = "TAGS";

            if (!TRINP.dcc_process)
            {

                if (TRINP_Qualitas.isQualitas && !string.IsNullOrEmpty(TRINP_Qualitas.PolizaMoneda))
                {
                        if (TRINP_Qualitas.PolizaMoneda.ToUpper().Equals("MXN"))
                            TRINP.Tx_Currency = "MXN";
                        else
                            TRINP.Tx_Currency = "USD";
                }
                else
                {
                    if (!TypeUsuario.CadenaXML.Equals("") && !TypeUsuario.USD.Equals("") && !TRINP.HidePopUpCurrency)
                    {
                        if (!TypeUsuario.USD.Equals("") && !TypeUsuario.MXN.Equals(""))
                        {
                            frmMoneda frmcurrency = new frmMoneda();
                            frmcurrency.ShowDialog();
                        }
                        else if (!TypeUsuario.MXN.Equals(""))
                        {
                            TRINP.Tx_Currency = "USD";
                        }
                    }
                    else
                    {
                        TRINP.Tx_Currency = "MXN";
                    }
                }

                TRINP_Qualitas.Moneda = TRINP.Tx_Currency;
                //Set currency
                this.dbgSetCurrencyType(TRINP.Tx_Currency);
                
            }

            //Send cmd to device.
            if (!mp.SendStartEMV())
            {
                setResponse(TRRSP.xml);
                Info.ErrorPP = TRRSP.nb_error;
                return false;
            }
            return true;
        }
        public Boolean StartTxEMV(String Tx_Amount)
        {
            //Clear transaction vars
            clear.ClearTXN();
            return StartTxEMVProcess(Tx_Amount);
        }
        //Venta
        public Boolean ejecutaVentadirecta(String Bs_User,
                                        String Bs_Pwd,
                                        String Bs_UsrTransaction,
                                        String Bs_Company,
                                        String Bs_Branch,
                                        String Bs_Country,
                                        String Cc_Type,
                                        String Tx_Merchant,
                                        String Tx_Reference,
                                        String Tx_OperationType,
                                        String Tx_Currency,
                                        String Cc_AMEXCvvCsc)
        {
            Boolean status=false;
            Boolean _continue;
            String RC4Key = "";
            TRINP.TRX_TYPE = Transaction.VTADIRECTA;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/cobroXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Cc_Type = Cc_Type;
            TRINP.Tx_Merchant = Tx_Merchant;
            TRINP.Tx_Reference = Tx_Reference;
            TRINP.Tx_OperationType = Tx_OperationType;
            TRINP.Tx_Currency = Tx_Currency;
            TRINP.tp_resp = "1"; //Soporta Impresion o no de Voucher
            //Transaction Key RC4
            RC4Key = RC4.Encrypt(TRINP.id_company, TRINP.id_company);
            //Encrypt password
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, RC4Key);
            TRINP.dcc_status = "I";
            
            try
            {
                //Crypto type
                if (TRINP.Command.Equals("C71") || TRINP.Command.Equals("C93"))
                {
                    TRINP.Crypto = "4";
                    if (!TRINP.Command.Equals("C93"))
                    {
                        //Encrypt Tracks
                        TRINP.tracks = RC4.Encrypt(TRINP.tracks, RC4Key);
                    }                    
                }
                else
                {
                    TRINP.Crypto = "2";
                }
                //Automatic currency set.
                if (!TypeUsuario.CadenaXML.Equals("") && Tx_OperationType.Equals("11"))
                {
                    if(bm.getDataXML("MXN", TypeUsuario.CadenaXML).IndexOf(Tx_Merchant)>0)
                    {
                        TRINP.Tx_Currency = "MXN";
                    }
                    else
                    {
                        TRINP.Tx_Currency = "USD";
                    }
                }

                if (TRINP.isAMEX)
                    TRINP.Cc_Type = "AMEX";
                else
                    TRINP.Cc_Type = "V/MC";
                
                //terminal version
                TRINP.version_terminal = Info.version;
                //terminal model
                TRINP.modelo_terminal = Info.model;
                //is there printer?
                TRINP.printer = Info.Printer;
                //is mobile
                TRINP.is_mobile = "0";
                //is there display
                TRINP.display = "1";


                if (TRINP.isAMEX)
                {
                    //Se agrega funcionalidad para pedir CVV AMEX desde la DLL.
                    //Se agrega validación para solo pedir CVV en tarjetas banda AMEX
                    if (TRINP.chip.Equals("0") && TRINP.cvvcsc.Equals("") && !TRINP.Command.Equals("C93"))
                    {
                        frmCsvAMEX formAmex = new frmCsvAMEX();
                        formAmex.ShowDialog();
                        TRINP.cvvcsc = RC4.Encrypt(Cc_AMEXCvvCsc, Info.RC4Key);
                    }

                    //Se agrega validación para no enviar el CVV en caso de que las tarjetas sean con CHIP
                    if (TRINP.chip.Equals("1") && !TRINP.cvvcsc.Equals(""))
                        TRINP.cvvcsc = "";
                }

                do
                {
                    _continue = false;

                    //CHIP
                    if (TRINP.chip.Equals("1"))
                    {
                        TRINP.emv = "3";
                        //Encrypt Chip Name
                        TRINP.CHName = RC4.Encrypt(TRINP.CHName, RC4Key);
                        TRINP.chipnameenc = "1";
                    }
                    else
                    {
                        //Swipe card
                        TRINP.emv = "2";
                    }
                    
                    //Send Message
                    if(sendMsg())
                    {
                        if (TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;
                            if(DCC_Validate())
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            //End pinpad flow.
                            if (mp.SendFinishEMV())
                            {
                                //Pinpad Response, 00-Approved, !00-Declined
                                if (!TRINP.pinpadrsp.Trim().Equals("00"))
                                {
                                    if (TRRSP.response.Equals("approved"))
                                    {
                                        //Host approved, but pinpad declined.
                                        if (TypeUsuario.emvReverso)
                                        {
                                            ejecutaReverso(Bs_Pwd);
                                        }
                                    }
                                }
                                status = true;
                            }
                        }
                    }
                } while (_continue);
            }
            catch(Exception ex)
            {
                MITLog.PrintLn(ex.Message);
            }
            return status;
        }
        //Venta MOTO
        public Boolean ejecutaVentaMOTO(String Bs_User,
                                        String Bs_Pwd,
                                        String Bs_UsrTransaction,
                                        String Bs_Company,
                                        String Bs_Branch,
                                        String Bs_Country,
                                        String Tx_Merchant,
                                        String Tx_Reference,
                                        String Tx_OperationType,
                                        String Tx_Amount,
                                        String Tx_Currency,
                                        String Cc_Type,
                                        String Cc_Name,
                                        String Cc_Number,
                                        String Cc_ExpMonth,
                                        String Cc_ExpYear,
                                        String Cc_CvvCsc)
        {
            int tpCc = 0;
            Boolean _continue;

            //Clear transaction vars
            clear.ClearTXN();

            TRINP.TRX_TYPE = Transaction.VTAMOTO;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/cobroXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Tx_Merchant = Tx_Merchant;
            TRINP.Tx_Reference = Tx_Reference;
            TRINP.Tx_OperationType = Tx_OperationType;
            TRINP.Tx_Amount = Tx_Amount;
            TRINP.Tx_Currency = Tx_Currency;
            TRINP.Cc_Type = Cc_Type;
            TRINP.CHName = RC4.Encrypt(Cc_Name, Info.RC4Key);
            TRINP.tracks = RC4.Encrypt(Cc_Number, Info.RC4Key);
            TRINP.expmonth = RC4.Encrypt(Cc_ExpMonth, Info.RC4Key);
            TRINP.expyear = RC4.Encrypt(Cc_ExpYear, Info.RC4Key);
            TRINP.cvvcsc = RC4.Encrypt(Cc_CvvCsc, Info.RC4Key);
            TRINP.Crypto = "2";
            TRINP.dcc_status = "I";

            if ((TRINP.Cc_Type == "AMEX") && (!TRINP.useGetMerchant))
            {
                TRINP.isAMEX = true;
                tpCc = 4;
            }

            if (!TRINP.isAMEX)
            {
                TRINP.Cc_Type = "V/MC";
                tpCc = 3;
            }

            if (!Cc_CvvCsc.Equals("") && ((Cc_CvvCsc.Trim().Length == tpCc) || (Cc_CvvCsc.Equals("0000"))))
            {
                do
                {
                    _continue = false;

                    if (sendMsg())
                    {

                        if (TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;
                            if (DCC_Validate(true))
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                        TRRSP.xml = xmlErrores.xmlConectionError;
                } while (_continue);
            }
            else
            {
                setResponse(xmlErrores.xmlAmexError);
                return false;
            }

            return false;
        }
        //Venta Forzada MOTO
        public Boolean ejecutaVentaForzadaMOTO(String Bs_User,
                                        String Bs_Pwd,
                                        String Bs_UsrTransaction,
                                        String Bs_Company,
                                        String Bs_Branch,
                                        String Bs_Country,
                                        String Tx_Merchant,
                                        String Tx_Reference,
                                        String Tx_OperationType,
                                        String Tx_Amount,
                                        String Tx_Currency,
                                        String Tx_Auth,
                                        String Cc_Type,
                                        String Cc_Name,
                                        String Cc_Number,
                                        String Cc_ExpMonth,
                                        String Cc_ExpYear,
                                        String Cc_CvvCsc, 
                                        [Optional]String Tx_boleto,
                                        [Optional]String fh_salida,
                                        [Optional]String fh_retorno)
        {
            int tpCc = 0;
            bool _continue;
            clear.ClearTXN();

            TRINP.TRX_TYPE = Transaction.VTAFRZDMOTO;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/VentaForzadaXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Tx_Merchant = Tx_Merchant;
            TRINP.Tx_Reference = Tx_Reference;
            TRINP.Tx_OperationType = Tx_OperationType;
            TRINP.Tx_Amount = Tx_Amount;
            TRINP.Tx_Currency = Tx_Currency;
            TRINP.Tx_Auth = Tx_Auth;
            TRINP.Cc_Type = Cc_Type;
            TRINP.CHName = RC4.Encrypt(Cc_Name, Info.RC4Key);
            TRINP.tracks = RC4.Encrypt(Cc_Number, Info.RC4Key);
            TRINP.expmonth = RC4.Encrypt(Cc_ExpMonth, Info.RC4Key);
            TRINP.expyear = RC4.Encrypt(Cc_ExpYear, Info.RC4Key);
            TRINP.cvvcsc = RC4.Encrypt(Cc_CvvCsc, Info.RC4Key);
            TRINP.tx_boleto = Tx_boleto;
            TRINP.fh_salida = fh_salida;
            TRINP.fh_retorno = fh_retorno;
            TRINP.Crypto = "2";
            
            TRINP.dcc_status = "I";

            if (TRINP.isAMEX)
            {
                TRINP.Cc_Type = "AMEX";
                tpCc = 4;
            }
            else
            {
                TRINP.Cc_Type = "V/MC";
                tpCc = 3;
            }

            if (!Cc_CvvCsc.Equals("") && ((Cc_CvvCsc.Trim().Length == tpCc) || (Cc_CvvCsc.Equals("0000"))))
            {
                do
                {
                    _continue = false;

                    if (sendMsg())
                    {

                        if (TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;
                            if (DCC_Validate(true))
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                        TRRSP.xml = xmlErrores.xmlConectionError;
                } while (_continue);
            }
            else
            {
                setResponse(xmlErrores.xmlAmexError);
                return false;
            }

            return false;
        }
        //Venta Avs MOTO
        public Boolean ejecutaVentaAvsMOTO(String Bs_User,
                                            String Bs_Pwd,
                                            String Bs_UsrTransaction,
                                            String Bs_Company,
                                            String Bs_Branch,
                                            String Bs_Country,
                                            String Tx_Merchant,
                                            String Tx_Reference,
                                            String Tx_OperationType,
                                            String Tx_Amount,
                                            String Tx_Currency,
                                            String Cc_Type,
                                            String Cc_Name,
                                            String Cc_Number,
                                            String Cc_ExpMonth,
                                            String Cc_ExpYear,
                                            String Cc_CvvCsc,
                                            String Avs_Address,
                                            String Avs_Municipality,
                                            String Avs_City,
                                            String Avs_State,
                                            String Avs_zip,
                                            String Avs_District)
        {
            int tpCc = 0;
            bool _continue;
            clear.ClearTXN();

            TRINP.TRX_TYPE = Transaction.VTAFRZDMOTO;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/cobroXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Tx_Merchant = Tx_Merchant;
            TRINP.Tx_Reference = Tx_Reference;
            TRINP.Tx_OperationType = Tx_OperationType;
            TRINP.Tx_Amount = Tx_Amount;
            TRINP.Tx_Currency = Tx_Currency;
            TRINP.Cc_Type = Cc_Type;
            TRINP.CHName = RC4.Encrypt(Cc_Name, Info.RC4Key);
            TRINP.tracks = RC4.Encrypt(Cc_Number, Info.RC4Key);
            TRINP.expmonth = RC4.Encrypt(Cc_ExpMonth, Info.RC4Key);
            TRINP.expyear = RC4.Encrypt(Cc_ExpYear, Info.RC4Key);
            TRINP.cvvcsc = RC4.Encrypt(Cc_CvvCsc, Info.RC4Key);
            TRINP.avs_address = Avs_Address;
            TRINP.avs_municipality = Avs_Municipality;
            TRINP.avs_city = Avs_City;
            TRINP.avs_state = Avs_State;
            TRINP.avs_zip = Avs_zip;
            TRINP.avs_district = Avs_District;

            TRINP.Crypto = "2";
            TRINP.dcc_status = "I";

            if (TRINP.isAMEX)
            {
                TRINP.Cc_Type = "AMEX";
                tpCc = 4;
            }
            else
            {
                TRINP.Cc_Type = "V/MC";
                tpCc = 3;
            }

            if (!Cc_CvvCsc.Equals("") && ((Cc_CvvCsc.Trim().Length == tpCc) || (Cc_CvvCsc.Equals("0000"))))
            {
                do
                {
                    _continue = false;

                    if (sendMsg())
                    {

                        if (TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;
                            if (DCC_Validate(true))
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                        TRRSP.xml = xmlErrores.xmlConectionError;
                } while (_continue);
            }
            return true;
        }
        //Cancelacion
        public Boolean ejecutaCancelacion(  String Bs_User,
                                            String Bs_Pwd,
                                            String Bs_UsrTransaction,
                                            String Bs_Company,
                                            String Bs_Branch,
                                            String Bs_Country,
                                            String Tx_Amount,
                                            String Tx_OperationNumber,
                                            String Tx_Auth
                                          )
        {
            //Clear transaction vars
            clear.ClearTXN();
            //Set Input values.
            TRINP.TRX_TYPE = Transaction.CANCELACION;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/CancelacionXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Tx_Amount = Tx_Amount;
            TRINP.Tx_OperationNumber = Tx_OperationNumber;
            TRINP.Tx_Auth = Tx_Auth;
            TRINP.Crypto = "2";

            //Send message to host.
            if (!sendMsg())
            {
                return false;
            }
            return true;
        }
        //Reverso
        private Boolean ejecutaReverso(String Bs_Pwd)
        {
            TRINP.TRX_TYPE = Transaction.REVERSO;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/services/Reverso";
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Crypto = "2";
            TRINP.Tx_OperationNumber = TRRSP.foliocpagos;
            TRINP.Tx_Auth = TRRSP.auth;
            //TRINP.version = "pcpay 9.1.0";

            //Send message soap to host
            if (!sendMsgSoap("http://reverso.ws.cobroxml.cpagos", "in0", "doReverso"))
            {
                setResponse(xmlErrores.xmlReversoFalse);
                return false;
            }
            setResponse(xmlErrores.xmlReversoTrue);
            return true;
        }
        //Venta Recompensas
        public Boolean ejecutaVtaRecompensas(String Bs_User,
                                                String Bs_Pwd,
                                                String Bs_Company,
                                                String Bs_Branch,
                                                String Bs_Country,
                                                String Tx_Reference,
                                                String Tx_Amount,
                                                String tp_operation)
        {
            //Clear transaction vars
            //clear.ClearTXN();
            //Set Input values.
            TRINP.TRX_TYPE = Transaction.RECOMPENSAS;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/recomp/ProcesaXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Tx_Reference = Tx_Reference;
            TRINP.RecomAmount = Tx_Amount;
            TRINP.Tx_OperationType = tp_operation;
            TRINP.Crypto = "2";
            TRINP.Tx_Currency = "MXN";
            //terminal version
            TRINP.version_terminal = Info.version;
            //terminal model
            TRINP.modelo_terminal = Info.model;

            if (Info.Dukpt.Equals("1"))
            {
                TRINP.cc_number = RC4.Encrypt(TRINP.tracks, Info.RC4Key);
            }
            else
            {
                //TRINP.cc_number = RC4.Encrypt(TRINP.tracks, Info.RC4Key);
                string cc_num;
                int posIni;

                cc_num = EncryptC.TresDesDescrypt(TRINP.tracks, Info.DinamicKey);
                posIni = cc_num.IndexOf(";") + 1;
                
                if(posIni > 0)
                    cc_num = cc_num.Substring(posIni, 16);

                TRINP.cc_number = RC4.Encrypt(cc_num, Info.RC4Key);

            }
            
            //Send message to host.
            if (!sendMsg())
            {
                return false;
            }

           if(tp_operation.Equals("101") && !TRRSP.cd_error.Equals("92"))
               return true;
           
               return mp.SendFinishEMV();
            
        }
        
        //Hotel
        public Boolean ejecutaCheckInEMV(String Bs_User,
                                         String Bs_Pwd,
                                         String Bs_UsrTransaction,
                                         String Bs_Company,
                                         String Bs_Branch,
                                         String Bs_Country,
                                         String Cc_Type,
                                         String Tx_Merchant,
                                         String Tx_Reference,
                                         String Tx_OperationType,
                                         String Tx_Currency,
                                         String Tx_Room,
                                         [Optional]String Cc_AMEXCvvCsc)
        {
            string RC4Key;
            Boolean _continue;

            TRINP.TRX_TYPE = Transaction.CHECKIN;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/CheckInXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Cc_Type = Cc_Type;
            TRINP.Tx_Merchant = Tx_Merchant;
            TRINP.Tx_Reference = Tx_Reference.ToUpper();
            TRINP.Tx_OperationType = Tx_OperationType;
            TRINP.Tx_Currency = Tx_Currency;
            TRINP.Tx_Room = Tx_Room;
            TRINP.cvvcsc = RC4.Encrypt(Cc_AMEXCvvCsc, Info.RC4Key);
            TRINP.Crypto = "2";
            RC4Key = RC4.Encrypt(TRINP.id_company, TRINP.id_company);
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, RC4Key);

            TRINP.dcc_status = "I";

            //terminal version
            TRINP.version_terminal = Info.version;
            //terminal model
            TRINP.modelo_terminal = Info.model;
            //is there printer?
            TRINP.printer = Info.Printer;
            //is mobile
            TRINP.is_mobile = "0";
            //is there display
            TRINP.display = "1";

            try
            {
                
                //Crypto type
                if (TRINP.Command.Equals("C71") || TRINP.Command.Equals("C93"))
                {
                    TRINP.Crypto = "4";
                    if (!TRINP.Command.Equals("C93"))
                    {
                        //Encrypt Tracks
                        TRINP.tracks = RC4.Encrypt(TRINP.tracks, RC4Key);
                    }
                }
                else
                {
                    TRINP.Crypto = "2";
                }

                if (TRINP.isAMEX)
                    TRINP.Cc_Type = "AMEX";
                else
                    TRINP.Cc_Type = "V/MC";


                if (TRINP.isAMEX)
                {
                    //Se agrega funcionalidad para pedir CVV AMEX desde la DLL.
                    //Se agrega validación para solo pedir CVV en tarjetas banda AMEX
                    if (TRINP.chip.Equals("0") && TRINP.cvvcsc.Equals("") && !TRINP.Command.Equals("C93"))
                    {
                        frmCsvAMEX formAmex = new frmCsvAMEX();
                        formAmex.ShowDialog();
                        TRINP.cvvcsc = RC4.Encrypt(Cc_AMEXCvvCsc, Info.RC4Key);
                    }

                    //Se agrega validación para no enviar el CVV en caso de que las tarjetas sean con CHIP
                    if (TRINP.chip.Equals("1") && !TRINP.cvvcsc.Equals(""))
                        TRINP.cvvcsc = "";
                }

                do
                {
                    _continue = false;

                    //CHIP
                    if (TRINP.chip.Equals("1"))
                    {
                        TRINP.emv = "3";
                        //Encrypt Chip Name
                        TRINP.CHName = RC4.Encrypt(TRINP.CHName, RC4Key);
                        TRINP.chipnameenc = "1";
                    }
                    else
                    {
                        //Swipe card
                        TRINP.emv = "2";
                    }

                    //Send Message
                    if (sendMsg())
                    {
                        if(TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;

                            if (DCC_Validate())
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            //End pinpad flow.
                            if (mp.SendFinishEMV())
                            {
                                //Pinpad Response, 00-Approved, !00-Declined
                                if (!TRINP.pinpadrsp.Trim().Equals("00"))
                                {
                                    if (TRRSP.response.Equals("approved"))
                                    {
                                        //Host approved, but pinpad declined.
                                        if (TypeUsuario.emvReverso)
                                        {
                                            ejecutaReverso(Bs_Pwd);
                                        }
                                    }
                                }
                                return true;
                            }
                        }
                    }
                } while (_continue);
            }
            catch (Exception ex)
            {
                setResponse(xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message));
            }
            return false;
        }

        //Hotel
        public Boolean ejecutaCheckInMoto(string Bs_User,
                                          string Bs_Pwd,
                                          string Bs_UsrTransaction,
                                          string Bs_Company,
                                          string Bs_Branch,
                                          string Bs_Country,
                                          string Tx_Merchant,
                                          string Tx_Reference,
                                          string Tx_OperationType,
                                          string Tx_Amount,
                                          string Tx_Currency,
                                          string Tx_Room,
                                          string Cc_Type,
                                          string Cc_Name,
                                          string Cc_Number,
                                          string Cc_ExpMonth,
                                          string Cc_ExpYear,
                                          string Cc_CvvCsc)
        {
            //Clear transaction vars
            clear.ClearTXN();

            bool _continue;
            int tpCc = 0;
            TRINP.TRX_TYPE = Transaction.CHECKINMOTO;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/CheckInXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Cc_Type = Cc_Type;
            TRINP.Tx_Merchant = Tx_Merchant;
            TRINP.Tx_Reference = Tx_Reference;
            TRINP.Tx_OperationType = Tx_OperationType;
            TRINP.Tx_Amount = Tx_Amount;
            TRINP.Tx_Currency = Tx_Currency;
            TRINP.Tx_Room = Tx_Room;
            //TRINP.Cc_Type = Cc_Type;
            TRINP.CHName = RC4.Encrypt(Cc_Name, Info.RC4Key);
            TRINP.tracks = RC4.Encrypt(Cc_Number, Info.RC4Key);
            TRINP.expmonth = RC4.Encrypt(Cc_ExpMonth, Info.RC4Key);
            TRINP.expyear = RC4.Encrypt(Cc_ExpYear, Info.RC4Key);
            TRINP.cvvcsc = RC4.Encrypt(Cc_CvvCsc, Info.RC4Key);
            TRINP.Crypto = "2";
            TRINP.dcc_status = "I";
                        
            try
            {
                if (TRINP.isAMEX)
                {
                    tpCc = 4;
                    TRINP.Cc_Type = "AMEX";
                }
                else
                {
                    tpCc = 3;
                    TRINP.Cc_Type = "V/MC";
                }

             

                if (!Cc_CvvCsc.Equals("") && ((Cc_CvvCsc.Trim().Length == tpCc) || (Cc_CvvCsc.Equals("0000"))))
                {
                    do
                    {
                        _continue = false;

                        if (sendMsg())
                        {

                            if (TRRSP.response.Equals("dcc"))
                            {
                                TRINP.dcc_process = true;
                                if (DCC_Validate(true))
                                {
                                    _continue = true;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                            TRRSP.xml = xmlErrores.xmlConectionError;
                    } while (_continue);

                }
            }
            catch (Exception ex)
            {
                setResponse(xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message));
            }
            return false;
        }
        public Boolean ejecutaVtaMOTO3DS(string Bs_User,
                                            string Bs_Pwd,
                                            string Bs_UsrTransaction,
                                            string Bs_Company,
                                            string Bs_Branch,
                                            string Bs_Country,
                                            string Tx_Merchant,
                                            string Tx_Reference,
                                            string Tx_OperationType,
                                            string Tx_Amount,
                                            string Tx_Currency,
                                            string Cc_Type,
                                            string Cc_Name,
                                            string Cc_Number,
                                            string Cc_ExpMonth,
                                            string Cc_ExpYear,
                                            string Cc_CvvCsc,
                                            string Tx_cavv,
                                            string Tx_eci,
                                            string Tx_xid)
        {
            //Clear transaction vars
            clear.ClearTXN();

            int tpCc = 0;
            bool _continue;
            TRINP.TRX_TYPE = Transaction.VTAMOTO3DS;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/cobroXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Cc_Type = Cc_Type;
            TRINP.Tx_Merchant = Tx_Merchant;
            TRINP.Tx_Reference = Tx_Reference;
            TRINP.Tx_OperationType = Tx_OperationType;
            TRINP.Tx_Amount = Tx_Amount;
            TRINP.Tx_Currency = Tx_Currency;
            TRINP.Cc_Type = Cc_Type;
            TRINP.CHName = RC4.Encrypt(Cc_Name, Info.RC4Key);
            TRINP.tracks = RC4.Encrypt(Cc_Number, Info.RC4Key);
            TRINP.expmonth = RC4.Encrypt(Cc_ExpMonth, Info.RC4Key);
            TRINP.expyear = RC4.Encrypt(Cc_ExpYear, Info.RC4Key);
            TRINP.cvvcsc = RC4.Encrypt(Cc_CvvCsc, Info.RC4Key);
            TRINP.cavv = RC4.Encrypt(Tx_cavv, Info.RC4Key);
            TRINP.eci = RC4.Encrypt(Tx_eci, Info.RC4Key);
            TRINP.xid = RC4.Encrypt(Tx_xid, Info.RC4Key);
            TRINP.Crypto = "2";
            //Transaction Key RC4
            String RC4Key = RC4.Encrypt(TRINP.id_company, TRINP.id_company);
            TRINP.dcc_status = "I";

            try
            {
                if ((TRINP.Cc_Type == "AMEX") && (!TRINP.useGetMerchant))
                {
                    TRINP.isAMEX = true;
                    tpCc = 4;
                }

                if (!TRINP.isAMEX)
                {
                    TRINP.Cc_Type = "V/MC";
                    tpCc = 3;
                }
                if (!Cc_CvvCsc.Equals("") && ((Cc_CvvCsc.Trim().Length == tpCc) || (Cc_CvvCsc.Equals("0000"))))
                {
                    do
                    {
                        _continue = false;

                        if (sendMsg())
                        {

                            if (TRRSP.response.Equals("dcc"))
                            {
                                TRINP.dcc_process = true;
                                if (DCC_Validate(true))
                                {
                                    _continue = true;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                            TRRSP.xml = xmlErrores.xmlConectionError;
                    } while (_continue);
                }
            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
            }
            return false;
        }

        public Boolean ejecutaCheckout(String Bs_User,
                                              String Bs_Pwd,
                                              String Bs_UsrTransaction,
                                              String Bs_Company,
                                              String Bs_Branch,
                                              String Bs_Country,
                                              String Tx_Amount,
                                              String Tx_OperationNumber)
        {
            //Clear transaction vars
            clear.ClearTXN();
            
            Boolean _continue;

            TRINP.TRX_TYPE = Transaction.CHECKOUT;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/CheckOutXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Tx_Amount = Tx_Amount;
            TRINP.Tx_OperationNumber = Tx_OperationNumber;
            TRINP.Crypto = "2";
            TRINP.dcc_status = "I";

            try
            {
                do
                {
                    _continue = false;

                    if (sendMsg())
                    {
                        if (TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;
                            if (DCC_Validate(true))
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                        TRRSP.xml = xmlErrores.xmlConectionError;
                } while (_continue);
            }
            catch (Exception ex)
            {
                setResponse(xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message));
            }
            return false;
        }

        public Boolean ejecutaCheckOutBoletosMOTO(String Bs_User,
                                              String Bs_Pwd,
                                              String Bs_UsrTransaction,
                                              String Bs_Company,
                                              String Bs_Branch,
                                              String Bs_Country,
                                              String Tx_Amount,
                                              String Tx_OperationNumber,
                                              String Tx_boleto,
                                              String fh_salida,
                                              String fh_retorno)
        {
            //Clear transaction vars
            clear.ClearTXN();

            TRINP.TRX_TYPE = Transaction.CHECKOUT;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/CheckOutXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Tx_Amount = Tx_Amount;
            TRINP.Tx_OperationNumber = Tx_OperationNumber;
            TRINP.tx_boleto = Tx_boleto;
            TRINP.fh_salida = fh_salida;
            TRINP.fh_retorno = fh_retorno;
            TRINP.Crypto = "2";
            //Transaction Key RC4
            String RC4Key = RC4.Encrypt(TRINP.id_company, TRINP.id_company);
            Boolean _continue;
            TRINP.dcc_status = "I";

            try
            {
                do
                {
                    _continue = false;

                    if (sendMsg())
                    {

                        if (TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;
                            if (DCC_Validate(true))
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                        TRRSP.xml = xmlErrores.xmlConectionError;
                } while (_continue);
            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
            }
            return false;
        }

        //Get merchant
        public string GetMerchant(String type, [Optional]String tp_canal, [Optional]String originante)
        {
            String xmlResponse = "";
            String xml = "";
            String Merchant = "";
            XmlDocument xmlContado = new XmlDocument();
            XmlDocument xmlMSI = new XmlDocument();
            XmlDocument xmlMCI = new XmlDocument();
            XmlNodeList xmlNodoContado = null;
            XmlNodeList xmlNodoafcontado = null;
            XmlNodeList xmlNodoMSI = null;
            XmlNodeList xmlNodoafMSI = null;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/PgsServices";

            bool limpiaAfiliacion = false;

            //Qualitas
            string plazosQ;
            string MSIQ;
            string[] mesesQ;
            string strAuxTemp;

            int numNodosContado = 0;
            int numNodosMSI = 0;

            if ((tp_canal == null) || (tp_canal.Equals("")))
            {
                tp_canal = "11";
            }

            TRINP_Qualitas.strTipoCobroQualitas = "";
            TRINP_Qualitas.strAfiliacionQualitas = "";

            
            xml = "accion=tipoPagoInfo";
            if (TRINP.panmask.Length > 6)
            {
                xml += "&cc_num=" + RC4.Encrypt(TRINP.panmask.Substring(0, 6), "MIT");
                xml += "&usuario=" + TypeUsuario.User;
                xml += "&canal=" + tp_canal;
                //Is it Moto trx?
                if (type.Trim().Equals("MOTO") || 
                    type.Trim().Equals("AVS") ||
                    type.Trim().Equals("MANUAL") ||
                    type.Trim().Equals("VFORZADA") ||
                    type.Trim().Equals("BandaMotoRP3"))
                {
                    xml += "&tp_canal=B";
                    limpiaAfiliacion = false;
                }
                else
                {
                    xml += "&tp_canal=1";
                    limpiaAfiliacion = true;
                }

                //Agencia
                if(!string.IsNullOrEmpty(originante))
                    xml += "&originante=" + originante;

                xmlResponse = ws.SendWS(TRINP.url + "" + TRINP.contexto, xml);
                if (!xmlResponse.Equals(""))
                {
                    if (bm.getDataXML("respuesta", xmlResponse).Equals("0"))
                    {
                        
                        mp.Cancel("1");

                        TRINP.DsError = bm.getDataXML("nb_respuesta", xmlResponse);
                        TRRSP.nb_error = bm.getDataXML("nb_respuesta", xmlResponse);
                        Merchant = "";
                        return Merchant;
                    }
                    else
                    {
                        //Obtener afilicaciones de contado 
                        TRINP.afcont = "<contado>" + bm.getDataXML("contado", xmlResponse) + "</contado>";
                        //Obtener afiliaciones con meses sin intereses.
                        TRINP.afmsi = "<msi>" + bm.getDataXML("msi", xmlResponse) + "</msi>";
                        //Obtener afiliaciones con meses sin intereses.
                        TRINP.afmci = bm.getDataXML("plazosmci", xmlResponse);

                        if (limpiaAfiliacion)
                        {
                            TRINP.afcont = this.dbgGetStringTpPago("contado", TRINP.afcont);
                            TRINP.afmsi = this.dbgGetStringTpPago("msi", TRINP.afmsi);
                        }
                        

                        if (TRINP.HidePopUpMerchant == true)
                        {
                            Merchant = TRINP.Tx_Merchant = TRINP.afcont + TRINP.afmsi + "<plazosmci>" + TRINP.afmci + "</plazosmci>";
                        }
                        else
                        {

                            //Hay mas de una afiliacion de contado?
                            if (!TRINP.afcont.Equals(""))
                            {
                                xmlContado.LoadXml(TRINP.afcont);
                                xmlNodoContado = xmlContado.GetElementsByTagName("contado");
                                xmlNodoafcontado = ((XmlElement)xmlNodoContado[0]).GetElementsByTagName("af");

                                numNodosContado = xmlNodoafcontado.Count;
                            }
                            //Hay mas de una afiliacion de MSI?
                            if (!TRINP.afmsi.Equals(""))
                            {

                                xmlMSI.LoadXml(TRINP.afmsi);
                                xmlNodoMSI = xmlMSI.GetElementsByTagName("msi");
                                xmlNodoafMSI = ((XmlElement)xmlNodoMSI[0]).GetElementsByTagName("af");

                                numNodosMSI = xmlNodoafMSI.Count;
                            }

                            //Se muestra el formulario
                            if (numNodosContado > 1 || (numNodosContado > 0 && numNodosMSI > 0) || !TRINP.afmci.Equals("") || numNodosMSI > 1)
                            {
                                frmPlanPagosAfis frmplan = new frmPlanPagosAfis(xmlNodoContado, xmlNodoMSI, TRINP.afmci);
                                frmplan.ShowDialog();
                                Merchant = TRINP.Tx_Merchant;
                            }
                            else if(numNodosContado == 1 && utilidadesMIT.GetDataXML("nb_currency", TRINP.afcont) == TRINP.Tx_Currency) //Solo tiene una afiliación de contado.
                            {
                                Merchant = TRINP.Tx_Merchant = utilidadesMIT.GetDataXML("merchant", TRINP.afcont);
        
                                 if(TRINP_Qualitas.isQualitas)
                                 {
                                    TRINP_Qualitas.Financiamiento = "N";
                                    TRINP_Qualitas.Tipofinanciamiento = "0";
                                 }
                            }
                            else if (numNodosMSI == 1 && utilidadesMIT.GetDataXML("nb_currency", TRINP.afcont) == TRINP.Tx_Currency) //Solo tiene una afiliación de MSI.
                            {
                                Merchant = TRINP.Tx_Merchant = utilidadesMIT.GetDataXML("merchant", TRINP.afmsi);
        
                                if(TRINP_Qualitas.isQualitas)
                                {
                                    TRINP_Qualitas.Financiamiento = "S";

                                    plazosQ = TRINP_Qualitas.TipoPagosMSIPlan.Replace("</meses>", "");
                                    plazosQ = plazosQ.Replace("<meses>", "$");
                                    mesesQ = plazosQ.Split('$');
                                    MSIQ = utilidadesMIT.GetDataXML("desc",TRINP.afmsi);
    
                                    for(int iQualitas =0; iQualitas < mesesQ.Length; iQualitas++)
                                    {
                                        strAuxTemp = " " + mesesQ[iQualitas].Trim() + "M ";

                                        if (MSIQ.Contains(" " + mesesQ[iQualitas].Trim() + "M ") 
                                            || MSIQ.Contains(" " + mesesQ[iQualitas].Trim() + "M"))
                                        {
                                            strAuxTemp = strAuxTemp.Replace(" ", "");
                                            strAuxTemp = strAuxTemp.Replace("M", "");
                                            TRINP_Qualitas.Tipofinanciamiento = strAuxTemp;
                                        }
                                    }
                                }
                            }
                            else if (TRINP.afcont == "" && TRINP.afmsi == "" && TRINP.afmci == "" && TRINP.Tx_Currency == "USD")
                            {
                                CancelOperation();
                                TRRSP.nb_error = "No se pueden realizar transacciones en dólares con tarjetas nacionales.";
                                Merchant = "";
                            }
                            else
                            {
                                //MOTO

                                if (numNodosContado == 1 ) //Solo tiene una afiliación de contado.
                                {
                                    Merchant = TRINP.Tx_Merchant = utilidadesMIT.GetDataXML("merchant", TRINP.afcont);

                                    if (TRINP_Qualitas.isQualitas)
                                    {
                                        TRINP_Qualitas.Financiamiento = "N";
                                        TRINP_Qualitas.Tipofinanciamiento = "0";
                                    }
                                }
                                else if (numNodosMSI == 1 ) //Solo tiene una afiliación de MSI.
                                {
                                    Merchant = TRINP.Tx_Merchant = utilidadesMIT.GetDataXML("merchant", TRINP.afmsi);

                                    if (TRINP_Qualitas.isQualitas)
                                    {
                                        TRINP_Qualitas.Financiamiento = "S";

                                        plazosQ = TRINP_Qualitas.TipoPagosMSIPlan.Replace("</meses>", "");
                                        plazosQ = plazosQ.Replace("<meses>", "$");
                                        mesesQ = plazosQ.Split('$');
                                        MSIQ = utilidadesMIT.GetDataXML("desc", TRINP.afmsi);

                                        for (int iQualitas = 0; iQualitas < mesesQ.Length; iQualitas++)
                                        {
                                            strAuxTemp = " " + mesesQ[iQualitas].Trim() + "M ";

                                            if (MSIQ.Contains(" " + mesesQ[iQualitas].Trim() + "M ")
                                                || MSIQ.Contains(" " + mesesQ[iQualitas].Trim() + "M"))
                                            {
                                                strAuxTemp = strAuxTemp.Replace(" ", "");
                                                strAuxTemp = strAuxTemp.Replace("M", "");
                                                TRINP_Qualitas.Tipofinanciamiento = strAuxTemp;
                                            }
                                        }
                                    }
                                }

                            
                            }

                        }
                    }
                }
                else
                {
                    TRINP.DsError = "Error al conectase al obtener los planes de pago, por favor intente de nuevo.";
                    Merchant = "";
                }
            }
            else
            {
                TRINP.DsError = "Error en tarjeta!";
                Merchant = "";
            }

            if ((Merchant.Equals("")) && bm.getDataXML("bank", xmlResponse).Equals(""))
            {
                CancelOperation();
                setResponse(xmlErrores.xmlPinPadError04);
                Merchant = "";
            }

            if( Merchant.Equals("00000"))
            {
                CancelOperation();
                setResponse(xmlErrores.xmlPinPadError11);
                Merchant = "";
            }

            TRINP.tipoPago = "";
            TRINP.isEmpRp3 = false;
                
            //QUALITAS, PARA LAS ETIQUETAS QUE SE MUUESTRAN EN EL FRAME
            if (TRINP_Qualitas.isQualitas)
            {
                if (TRINP_Qualitas.strTipoCobroQualitas.Equals(""))
                {
                    if (!TRINP.afcont.Equals(""))
                        TRINP_Qualitas.strTipoCobroQualitas = "Contado";

                    if (!TRINP.afmsi.Equals(""))
                        TRINP_Qualitas.strTipoCobroQualitas = "MSI";
                }

                if (TRINP_Qualitas.strAfiliacionQualitas.Equals(""))
                {
                    if (!TRINP.afcont.Equals(""))
                        TRINP_Qualitas.strAfiliacionQualitas = utilidadesMIT.GetDataXML("desc", TRINP.afcont);

                    if (!TRINP.afmsi.Equals(""))
                        TRINP_Qualitas.strAfiliacionQualitas = utilidadesMIT.GetDataXML("desc", TRINP.afmsi);
                }
            }

            return Merchant;
        }
        public String GetMerchantMoto(String Type, String numberTDC,
                                      [Optional]String tp_canal,
                                      [Optional]String originante)
        {
            TRINP.panmask = numberTDC;

            //Validar Tarjeta AMEX
            if (utilidadesMIT.ExisteArchivo("amex.txt"))
            {
                if (TRINP.panmask.Length >= 6)
                {
                    if (utilidadesMIT.ValidaBinAMEX(TRINP.panmask.Substring(0, 6)))
                        TRINP.isAMEX = true;
                    else
                        TRINP.isAMEX = false;
                }
                else
                    TRINP.isAMEX = false;
            }

            return GetMerchant(Type, tp_canal, originante);
        }
        
        //Print Voucher
        public void printvoucher(String voucher)
        {
            try
            {
                
                if (voucher.Contains("voucher_cliente"))
                {
                    //Pendiente mientras PGS lo corrige
                    if (voucher.Contains("</voucher_cliente>"))
                        utilidadesMIT.dbgPrintTwoVouchers(utilidadesMIT.GetDataXML("voucher_comercio", voucher), utilidadesMIT.GetDataXML("voucher_cliente", voucher));
                    else
                    {
                        voucher = voucher.Replace("</voucher_cliente", "</voucher_cliente>");
                        utilidadesMIT.dbgPrintTwoVouchers(utilidadesMIT.GetDataXML("voucher_comercio", voucher), utilidadesMIT.GetDataXML("voucher_cliente", voucher));
                    }
                }
                else
                {
                    string voucherAux = voucher;
                    int cont = 0;
                    string FinVoucher = "@br @logo3 @br @cnn " + Info.dll_version + "@br @br @br ";

                inicia:

                    voucher = voucherAux;
                    utilidadesMIT.TrataVoucher(voucher);
                    utilidadesMIT.QuitaAcentos(voucher);

                    voucher = voucher.Substring(0, voucher.Length - 1);
                    voucher = voucher.Replace(Convert.ToChar(3).ToString(), "");
                    voucher = voucher.Replace("MXN", "MXN  ");

                    if (cont == 0)
                        voucher = voucher + " @br @cnn *** C O M E R C I O ***  " + FinVoucher;
                    else
                        voucher = voucher + " @br @cnn *** C L I E N T E ***  " + FinVoucher;

                    if (mp.SendC59(voucher))
                    {
                        cont++;

                        if (cont >= 2)
                            return;
                        else
                            goto inicia;
                    }
                    else
                    {
                        return;
                    }
                }


            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
            }

        }

        public String GetVoucherRecompensas(String Bs_Company,
                                            String Bs_Branch,
                                            String Bs_Country,
                                            String Bs_User,
                                            String Bs_Pwd,
                                            String opcionImpresion,
                                            String Tx_OperationNumber)
        {
            clear.ClearTXN();
            TRINP.TRX_TYPE = Transaction.PRINTRECOMP;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/recomp/pcpay";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = Bs_Pwd;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.RecomPrtOpt = opcionImpresion;
            TRINP.foliocpagos = Tx_OperationNumber;
            TRINP.canal = "pcpay";

            if (opcionImpresion.ToLower().Equals("impticket"))
            {
                TRINP.RecomCopia = "false";
            }
            else
            {
                TRINP.RecomCopia = "true";
            }
            //Send message to host.
            if (sendMsg("strCadEncriptar"))
            {
                if (TRRSP.xml.Contains(", evite usar los botones de back o atr"))
                    return "";
                else
                    return TRRSP.xml;
            }            
            return "";
        }

        public String ejecutaCorteRecompensas(string Bs_Company,
                                             string Bs_Branch,
                                             string Bs_Country,
                                             string Bs_User,
                                             string Bs_Pwd,
                                             string tipoReporte,
                                             string tipoTicket)
        {
            clear.ClearTXN();
            TRINP.TRX_TYPE = Transaction.CORTERECOMPENSAS;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/recomp/pcpay";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = Bs_Pwd;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.RecomTipoReporte = tipoReporte;
            TRINP.RecomTipoTicket = tipoTicket;
            TRINP.canal = "pcpay";
            TRINP.RecomPrtOpt = "corte";

            try
            {
                //Send message to host.
                if (sendMsg("strCadEncriptar"))
                {
                    if (TRRSP.xml.Contains(", evite usar los botones de back o atr"))
                        return "";
                    else
                        return TRRSP.xml;
                }
                else
                    return xmlErrores.xmlConectionError;
            }
            catch (Exception ex)
            {
                return xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message);
            }
        }


        
        public String ejecutaReimpresionRecompensas(string Bs_User,
                                    string Bs_Pwd,
                                    string Bs_Company,
                                    string Bs_Branch,
                                    string Bs_Country,
                                    string Tx_OperationNumber)
        {
            clear.ClearTXN();
            TRINP.TRX_TYPE = Transaction.REIMPRESION_RECOMPENSAS;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/recomp/ReprintXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.foliocpagos = Tx_OperationNumber;

            try
            {
                //Send message to host.
                if (sendMsg())
                {
                    if (!TRRSP.xml.Contains("voucher_comercio"))
                    { 
                        string voucherBe;
                        string voucher;
                        voucherBe = utilidadesMIT.GetDataXML("voucher", TRRSP.xml);
                        voucher = utilidadesMIT.VerificaVoucher(voucherBe);// +"@cbb COPIA ";
                        voucher = voucher.Replace("</voucher_cliente", "</voucher_cliente>");
                        TRRSP.xml = TRRSP.xml.Replace(voucherBe, voucher);
                    }

                    setResponse(TRRSP.xml);
                    return TRRSP.voucher;
                        
                        //return TRRSP.xml;
                }
                else
                    return xmlErrores.xmlConectionError;
            }
            catch (Exception ex)
            {
                return xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message);
            }


        }


        //Get RSA Key 
        public void getRSA()
        {
            TypeUsuario.RSAresp = cr.getRSA();
            if (TypeUsuario.RSAresp.Equals("true"))
            {
                if (Info.DukptKey != null && Info.Dukpt != null)
                {
                    if (!Info.DukptKey.Trim().Equals("1") && Info.Dukpt.Equals("1"))
                    {
                        if (dukptInit())
                            setDUKPT(Info.ksn, Info.kcv, Info.ipek);
                    }
                }
            }
            else
            {
                TypeUsuario.rspError = "Error - Código 99";
                MITLog.PrintLn("Error al obtener la llave RSA!");
            }
            
        }
        //dukptInit
        public Boolean dukptInit()
        {
            TRINP.Command = "C91";
            getTxDate(); // get Date
            getTxTime(); //get Time                

            TRINP.TRX_TYPE = Transaction.INITDUKPT;
            
            if (mp.dukptInit())
            {
                TRINP.url = TypeUsuario.URL;
                TRINP.contexto = "/pgs/ik";
                TRINP.id_company = TypeUsuario.Id_Company;
                TRINP.id_branch = TypeUsuario.Id_Branch;
                TRINP.Bs_Country = TypeUsuario.country;
                TRINP.Bs_User = TypeUsuario.User;
                TRINP.Bs_Pwd = TypeUsuario.Pass;
                if(sendMsg())
                {
                    Info.ksn = TRRSP.nb_ksn;
                    Info.kcv = TRRSP.nb_kcv;
                    Info.ipek = TRRSP.nb_ipek;
                    return true;
                }
            }
            return false;
        }
        //setDUKPT
        public Boolean setDUKPT(String KSN, String KCV, String IPEK)
        {
            TRINP.Command = "C92";
            Info.ksn = KSN;
            Info.kcv = KCV;
            Info.ipek = IPEK;
            
            return mp.setDUKPT();

        }
        //Firma en panel
        public Boolean GuardaFirmaPanel(String strHexFirmaPanel, 
                                        String urlFirma, 
                                        String OperationNumber, 
                                        String Pp_Serial)
        {
            String strData = "", xml = "", xmlResponse="";

            strData = "<folio>" + OperationNumber + "</folio>"
                    + "<geo>0,0</geo>"
                    + "<device>3</device>"
                    + "<serial>" + Pp_Serial + "</serial>";
            //Cipher data
            strData = RC4.Encrypt(strData,Info.RC4_FIRMAPANEL);
            
            //Build complete msg.
            xml = "xml=<mpgs>";
            xml += "<action>2</action>";
            xml += "<data>" + strData + "</data>";
            xml += "<data2>" + strHexFirmaPanel + "</data2>";
            xml += "</mpgs>";

            try
            {
                MITLog.PrintLn("xml firma request=" + xml);
                xmlResponse = ws.SendWS(urlFirma + "/pgs/paymentRequestMAction", xml);
                MITLog.PrintLn("xml firma response=" + xml);
                if (!xmlResponse.Equals(""))
                {
                    return true;
                }                
            }
            catch(Exception ex)
            {
                MITLog.PrintLn(ex.Message);
            }
            return false;
        }

        public bool EnviaMailFirmaPanel(String strMail,
                                                String OperationNumber,
                                                String Id_Company,
                                                String Id_Branch,
                                                String country,
                                                String User,
                                                String Pass,
                                                String urlFirma)
        {
            String strData = "", xml = "", xmlResponse="";
            strData = "<data1>"
                        + "<folio>" + OperationNumber + "</folio>"
                        + "<mail>" + strMail + "</mail>"
                    + "</data1>"    
                    + "<data2>"
                        + "xml=<REPRINTVOUCHER>"    
                            + "<business>"
                                + "<id_company>" + Id_Company + "</id_company>"
                                + "<id_branch>" + Id_Branch + "</id_branch>"
                                + "<country>" + country + "</country>"
                                + "<user>" + User + "</user>"
                                + "<pwd>" + RC4.Encrypt(Pass, "KEY CREDIT CARD KEY") + "</pwd>"
                            + "</business>"     
                            + "<no_operacion>" + OperationNumber + "</no_operacion>"
                            + "<crypto>2</crypto>"   
                        + "</REPRINTVOUCHER>"
                    + "</data2>";
    
            //Se cifra la cadena de datos
            strData = RC4.Encrypt(strData, Info.RC4_FIRMAPANEL);

            //Build complete msg.
            xml = "xml=<mpgs>"
                + "<action>3</action>"
                + "<data>" + strData + "</data>"
                + "</mpgs>";

            try
            {
                MITLog.PrintLn("xml Mailfirma request=" + xml);
                xmlResponse = ws.SendWS(urlFirma + "/pgs/paymentRequestMAction", xml);
                MITLog.PrintLn("xml Mailfirma response=" + xml);
                if (!xmlResponse.Equals(""))
                {
                    if (xmlResponse.ToUpper().Contains("ERROR"))
                    {
                        TRRSP.nb_error = xmlErrores.xmlConectionError;
                        TRRSP.xml = TRRSP.nb_error;
                        return false;
                    }
                    else
                    {
                        TRRSP.xml = xmlResponse;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
                TRRSP.nb_error = xmlErrores.xmlConectionError;
                return false;
            }

            return false;
            
        }
        public string ObtieneFirmaPanel(Boolean esTouch,
                                            String textoAgua,
                                            String RspVoucher,
                                            Boolean IsChipAndPin,
                                            String MarcaTerminal,
                                            int tipoVtaDirecta,
                                            Boolean esQPS)
        {
            
            try
            {
                //Validando tipo de Venta
                //tipoVtaDirecta = 1 = Venta Directa
                string firmaObtenida = "";
                string strAscii;
    
                //*****************************************************************************************
                //se guarda la imagen
                string fileOriginal, fileMarcaAgua, fileFirmaEjemplo;
                string sPathUserData;
                string strTituloFirma;
            
                fileOriginal = "\\firmaOriginal.png";
                fileMarcaAgua = "\\firmaMarcaAgua.png";
                fileFirmaEjemplo = "c:\\pcpay\\firmaEjemplo.png";
                sPathUserData = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\MIT";

                if (!utilidadesMIT.ExisteCarpeta(sPathUserData))
                {
                    if (!utilidadesMIT.CreaCarpeta(sPathUserData))
                        return "";
                }

                //**********************************************************************************
               //SI NO EXISTE LA IMAGEN "c:\pcpay\firmaEjemplo.png", LA DESCARGO
                //if(!utilidadesMIT.ExisteArchivo(fileFirmaEjemplo))
                if (!System.IO.File.Exists(fileFirmaEjemplo))
                {
                    fileFirmaEjemplo = sPathUserData + "\\firmaEjemplo.png";

                    if (!System.IO.File.Exists(fileFirmaEjemplo))
                        utilidadesMIT.DownloadFile(TRINP.url + "/pgs/jsp/cpagos/cargas/Picture/firmaEjemplo.png", fileFirmaEjemplo);
                }
           
                //**********************************************************************************
                //Titulo de la firma cuando no es necesaria la firma en Panel
                strTituloFirma = "Autorizado por" + "\r\n" + " Firma Electrónica";
    
                //**********************************************************************************
                //valida QPS
                if (esQPS)
                    strTituloFirma = "Autorizado " + "\r\n" + " sin Firma";
            
                //**********************************************************************************
                //MENSAJES DE VALIDACION SIN FIRMA
                //if (IsChipAndPin && tipoVtaDirecta == 1)
                //    MessageBox.Show("Voucher" + "\r\n" + "Autorizado por Firma Electrónica", "Obtiene Firma en Panel");
            
                //************************************************************************************************
                //valida si la venta es QPS
                //if (esQPS)
                //    MessageBox.Show("Voucher" + "\r\n" + "Autorizado sin Firma", "Obtiene Firma en Panel");
            
                //**********************************************************************************
                //es una tarjeta con CHIP and PIN
                if ((IsChipAndPin && tipoVtaDirecta == 1) || esQPS)
                {
                    if (!objImage.ConvertirTextoAImagen(fileFirmaEjemplo, sPathUserData + fileOriginal, strTituloFirma))
                        MessageBox.Show("No se pudo generar la imagen sin Firma." + "\r\n" + "Error: " + objImage.Error, "Firma en Panel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //**********************************************************************************
                    //No es una tarjeta con CHIP and PIN ni QPS
                    if (esTouch)
                    {
                        //es una tarjeta con CHIP and PIN o QPS
                        if (!((IsChipAndPin && tipoVtaDirecta == 1) || esQPS))
                        {
                            //llama al form para poder firmar
                            ClsFirmaPanel.ObtieneFirmaPanel(textoAgua);

                            if (ClsFirmaPanel.Error == "")
                                firmaObtenida = ClsFirmaPanel.TextoHEXFirmaPanel;
                            else
                            {
                                MessageBox.Show("No se pudo obtener la firma desde el dispositivo" + "\r\n" + ClsFirmaPanel.Error, "Firma en Panel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                firmaObtenida = ClsFirmaPanel.Error;
                            }

                        } // fin del IF si es chip an pin or QPS
                    }
                    else
                    {
                        MessageBox.Show("Favor de obtener la Firma en el PIN Pad", "Firma en PIN Pad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    inicio:

                        strAscii = this.ejecutadbgSetCmd61();

                        //si se da cancelar en la pinPad
                        if (strAscii.ToUpper().Equals("ERROR-CANCELADO"))
                        {
                            strAscii = "";
                            MessageBox.Show("Error en la obtención de la Firma en Panel" + "\r\n" + "Debes firmar en el dispositivo.", "Firma en Panel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto inicio;
                        }

                        //if (MarcaTerminal.Equals("VERIFONE"))
                        //    strAscii = AsciiToHexadecimal(strAscii);

                        firmaObtenida = strAscii;
                        //firmaObtenida = HexadecimalToAscii(firmaObtenida);

                        BinaryWriter bw;
                        bw = new BinaryWriter(new FileStream(sPathUserData + fileOriginal, FileMode.Create));
                        bw.Write(TRINP.FirmaPinPadByte);
                        bw.Close();

                    }
                }

                //**********************************************************************************
                //aqui debo llamar para ponerle el fondo a la imagen
                if( !esTouch || (IsChipAndPin && tipoVtaDirecta == 1) || esQPS )
                {
                    if(objImage.MarcaAguaTexto(sPathUserData + fileOriginal, textoAgua, sPathUserData + fileMarcaAgua))
                    {
                        firmaObtenida = objImage.ImagenToHexadecimal(sPathUserData, sPathUserData + fileMarcaAgua);
                
                        if( firmaObtenida.Contains("Error:"))
                            MessageBox.Show("Error en la obtención de la imagen" + "\r\n" + firmaObtenida, "Firma en Panel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo generar la imagen con marca de agua." + "\r\n" + "Error: " + objImage.Error, "Firma en Panel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        firmaObtenida = "Error-" + objImage.Error;
                    }
                }

                return firmaObtenida;
            }
            catch (Exception ex)
            {
                setResponse(xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message));
                return "";
            }
            
        }

        public bool ejecutaFirmaEnPanel(bool esTouch,
                                        string strHex,
                                        string urlFirma,
                                        string textoAgua,
                                        string OperationNumber,
                                        string Pp_Serial,
                                        string Id_Company,
                                        string Id_Branch,
                                        string country,
                                        string User,
                                        string Pass,
                                        string RspVoucher,
                                        bool IsChipAndPin,
                                        string MarcaTerminal,
                                        int tipoVta)
        {
            
            //Validando tipo de Venta
            //tipoVtaDirecta = 1 = Venta Directa
            bool ejecucion = false;
            string strFirmaPanel = "";

            bool esQPS;
            string strAscii;
            string strError;
            int reintentos = 0;

            try
            {
                //*****************************************************************************************
                //se guarda la imagen
                string fileOriginal, fileMarcaAgua, fileFirmaEjemplo;
                string sPathUserData;
                string strTituloFirma;

                fileOriginal = "\\firmaOriginal.png";
                fileMarcaAgua = "\\firmaMarcaAgua.png";
                fileFirmaEjemplo = "c:\\pcpay\\firmaEjemplo.png";
                sPathUserData = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\MIT";

                if (!utilidadesMIT.ExisteCarpeta(sPathUserData))
                {
                    if (!utilidadesMIT.CreaCarpeta(sPathUserData))
                        return false;
                }

                //**********************************************************************************
                //SI NO EXISTE LA IMAGEN "c:\pcpay\firmaEjemplo.png", LA DESCARGO
                if (!System.IO.File.Exists(fileFirmaEjemplo))
                {
                    fileFirmaEjemplo = sPathUserData + "\\firmaEjemplo.png";

                    if(!System.IO.File.Exists(fileFirmaEjemplo))
                        utilidadesMIT.DownloadFile(TRINP.url + "/pgs/jsp/cpagos/cargas/Picture/firmaEjemplo.png", fileFirmaEjemplo);
                }

                //**********************************************************************************
                //Titulo de la firma cuando no es necesaria la firma en Panel
                strTituloFirma = "Autorizado por" + "\r\n" + " Firma Electrónica";

                //**********************************************************************************
                //valida QPS
                if( RspVoucher.Contains("@cnn Autorizado sin Firma ") && tipoVta == 1 && !IsChipAndPin )
                {
                    esQPS = true;
                    strTituloFirma = "Autorizado " + "\r\n" + " sin Firma";
                }
                else
                    esQPS = false;

                //**********************************************************************************
                //es una tarjeta con CHIP and PIN
                if ((IsChipAndPin && tipoVta == 1) || esQPS)
                {
                    if (!objImage.ConvertirTextoAImagen(fileFirmaEjemplo, sPathUserData + fileOriginal, strTituloFirma))
                    {
                        MessageBox.Show("No se pudo generar la imagen sin Firma." + "\r\n" + "Error: " + objImage.Error, "Firma en Panel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        ejecucion = false;
                    }
                }
                else
                {

                    //**********************************************************************************
                    //No es una tarjeta con CHIP and PIN ni QPS
                    if (esTouch)
                    {
                        strFirmaPanel = strHex;
                    }
                    else
                    {
                        MessageBox.Show("Favor de obtener la Firma en el PIN Pad", "Firma en PIN Pad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    inicio:

                        strAscii = this.ejecutadbgSetCmd61();

                        //si se da cancelar en la pinPad
                        if (strAscii.ToUpper().Equals("ERROR-CANCELADO"))
                        {
                            strAscii = "";
                            MessageBox.Show("Error en la obtención de la Firma en Panel" + "\r\n" + "Debes firmar en el dispositivo.", "Firma en Panel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto inicio;
                        }

                        //if (MarcaTerminal.Equals("VERIFONE"))
                        //    strAscii = AsciiToHexadecimal(strAscii);

                        strFirmaPanel = strAscii;
                        //firmaObtenida = HexadecimalToAscii(firmaObtenida);

                        BinaryWriter bw;
                        bw = new BinaryWriter(new FileStream(sPathUserData + fileOriginal, FileMode.Create));
                        bw.Write(TRINP.FirmaPinPadByte);
                        bw.Close();

                    }
                                
                }

                //**********************************************************************************
                //aqui debo llamar para ponerle el fondo a la imagen
                if (!esTouch || (IsChipAndPin && tipoVta == 1) || esQPS)
                {
                    if (objImage.MarcaAguaTexto(sPathUserData + fileOriginal, textoAgua, sPathUserData + fileMarcaAgua))
                    {
                        strFirmaPanel = objImage.ImagenToHexadecimal(sPathUserData, sPathUserData + fileMarcaAgua);

                        if (strFirmaPanel.Contains("Error:"))
                        {
                            MessageBox.Show("Error en la obtención de la imagen" + "\r\n" + strFirmaPanel, "Firma en Panel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo generar la imagen con marca de agua." + "\r\n" + "Error: " + objImage.Error, "Firma en Panel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //strFirmaPanel = "Error-" + objImage.Error;
                    }
                }

                //************************************************************************************
                //Se llama a la funcion de Guardar en la base de datos

    ReintentaGuardar:

                if (GuardaFirmaPanel(strFirmaPanel, urlFirma, OperationNumber, Pp_Serial))
                {
                    reintentos = 0;
                    ejecutaObtieneFormMail("-1", "");

                    if(!TRINP.strMailFirma.Equals(""))
                    {  
ReintentaEnviarMail:

                        if (EnviaMailFirmaPanel(TRINP.strMailFirma, OperationNumber, Id_Company, Id_Branch, country, User, Pass, urlFirma))
                        { 
                            ejecutaObtieneFormMail("0", TRINP.strMailFirma);
                            ejecucion = true;
                        }
                        else
                        {
                            if (reintentos > 2)
                            {
                                strError = utilidadesMIT.GetDataXML("nb_error", xmlErrores.xmlConectionError);
                                MessageBox.Show("No se pudo enviar el Voucher." + "\r\n" + "Error: " + strError, "Enviar Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                ejecucion = false;
                            }
                            else
                            {
                                reintentos = reintentos + 1;
                                goto ReintentaEnviarMail;
                            }

                        }


                    }
                    else
                    {
                        MessageBox.Show("No se pudo enviar el Correo electrónico." + "\r\n" + "Correo electrónico vacío", "Enviar Correo Electrónico", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        ejecucion = false;
                    }
                }
                else
                { 
                    if( reintentos > 2 )
                    {
                        strError = utilidadesMIT.GetDataXML("nb_error", xmlErrores.xmlConectionError);
                        MessageBox.Show("No se pudo guardar la imagen." + "\r\n" + "Error: " + strError, "Firma en Panel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        ejecucion = false;
                    }
                    else
                    {
                            reintentos = reintentos + 1;
                            goto ReintentaGuardar;
                    }
                 
                }
            }
            catch(Exception ex)
            {
                ejecucion = false;
                setResponse(xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message));
            }

            return ejecucion;
        }

        //Cancel Operation
        public Boolean CancelOperation()
        {
            TRINP.cancelop = true;
            if (TRINP.thread)
            {
                while (TRINP.cancelop) ;
            }

            if(mp.Cancel())
            {
                setResponse(xmlErrores.xmlPinPadError10);
                Info.ErrorPP = TRRSP.nb_error;
                return true;
            }
            return false;
        }
        //***********************************************************************************
        //***********************************************************************************
        //**                       AplicaCobroRecompensas()                                **
        //**                                                                               **
        //**  Descripción    : Inicia la busqueda y seteo del lector de tarjetas           **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public bool AplicaCobroRecompensas(string binTarjeta)
        {

            bool aplicaRecom = false;
            bool Bin = false;
            string bines;

            try
            {
                bines = utilidadesMIT.DesencriptaBines("numsant.txt");

                if (bines.Contains("|" + binTarjeta))
                    Bin = true;

                if( TypeUsuario.isRecompensas && Bin)
                    aplicaRecom = true;
            
            }
            catch(Exception ex)
            {
                aplicaRecom = false;
                setResponse(xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message));
            }

            return aplicaRecom;
        }
        //DCC
        private Boolean DCC_Validate([Optional]bool isTrxMoto)
        {
            if (Info.Dukpt == "1")
            {
                if (!isTrxMoto)
                    mp.SendFinishEMV();
            }
            
            frmDCC frmdcc = new frmDCC();
            frmdcc.ShowDialog();

            TRINP.Tx_AmountBase = TRINP.Tx_Amount;

            if (TRINP.dcc_type.Equals("EXTRANJERO"))
            {

                if (Info.Dukpt == "0")
                {
                    if (!isTrxMoto)
                        if(TRINP.chip == "1")
                            mp.SendFinishEMV();
                }

                //DCC
                TRINP.dcc_status = "1";
                TRINP.dcc_amount = TRRSP.dcc_amount;

                //TRINP.Tx_Currency = TRRSP.cc_nbCurrencyCode;
                //if (Info.Dukpt.Equals("1"))
                //{
                    
                    //////Chip
                    ////if(TRINP.chip.Equals("1"))
                    ////{
                    if (!isTrxMoto)
                    {
                        if (StartTxEMVProcess(TRRSP.dcc_amount))
                        {
                            //TRINP.Tx_Amount = TRINP.Tx_AmountBase;


                            //Crypto type
                            if (TRINP.Command.Equals("C71"))
                            {
                                //Encrypt Tracks
                                string RC4Key = RC4.Encrypt(TRINP.id_company, TRINP.id_company);
                                TRINP.tracks = RC4.Encrypt(TRINP.tracks, RC4Key);
                            }



                            return true;
                        }
                        else
                            return false;
                    }
                    else
                    {
                        return true;
                    }
                    ////}
                    ////else
                    ////{
                    ////    return true;
                    ////}
                //}
                //else
                //{
                //    return true;
                //}
            }
            else
            {
                TRINP.dcc_status = "0";
                if (Info.Dukpt.Equals("1"))
                {
                    //Chip
                    ////if (TRINP.chip.Equals("1"))
                    ////{
                    if (!isTrxMoto)
                    {
                        if (StartTxEMVProcess(TRINP.Tx_Amount))
                        {
                            return true;
                        }

                    }
                    else
                    {
                        return true;
                    }
                    ////}
                    ////else
                    ////{
                    ////    return true;
                    ////}
                }
                else
                {
                    return true;
                }
                
            }
            return false;
        }

        
        public bool ObtieneReporte(string Bs_User,
                                        string Bs_Branch,
                                        string Bs_Company,
                                        string tipoVenta,
                                        string tipoCorte,
                                        string opcion,
                                        string cdGiro,
                                        string app,
                                        string etiqueta,
                                        string version
                              )
        {
            string strCadena = "";
            String xmlResponse = "";

            StringBuilder strCadEncriptar = new StringBuilder();
            strCadEncriptar.Append("&usuario=" + Bs_User);
            strCadEncriptar.Append("&sucursal=" + Bs_Branch);
            strCadEncriptar.Append("&cdEmpresa=" + Bs_Company);
            strCadEncriptar.Append("&tipoVenta=" + tipoVenta);
            strCadEncriptar.Append("&tipoCorte=" + tipoCorte);
            strCadEncriptar.Append("&op=" + opcion);
            strCadEncriptar.Append("&cdGiro=" + cdGiro);
            strCadEncriptar.Append("&app=" + app);
            strCadEncriptar.Append("&etiqueta=" + etiqueta);
            strCadEncriptar.Append("&version=" + version.Replace("CP-D ",""));
            
            strCadena = strCadEncriptar.ToString();
            strCadena = "enc=" + RC4.Encrypt(strCadena,Info.RC4_AEROPAY);

            try
            {
                TRINP.url = TypeUsuario.URL;
                TRINP.contexto = "/pgs/pcpayAgencia";

                xmlResponse = ws.SendWS(TRINP.url + "" + TRINP.contexto, strCadena);
                if (xmlResponse != "")
                {
                    utilidadesMIT.ImprimeHTML(xmlResponse);
                    return true;
                }
                else
                {
                    MessageBox.Show("No existen Transacciones para este usuario.");
                    return false;
                }
            }
            catch(Exception ex)
            {
                MITLog.PrintLn(ex.Message);
                return false;
            }
            
        }

        public bool ObtieneCorteUsuario(string Bs_User,
                                        string opcion
                                        )
        {
            string strCadena = "";
            String xmlResponse = "";

            StringBuilder strCadEncriptar = new StringBuilder();
            strCadEncriptar.Append("&usuario=" + Bs_User);
            strCadEncriptar.Append("&op=" + opcion);
            strCadena = strCadEncriptar.ToString();
            strCadena = "enc=" + RC4.Encrypt(strCadena, Info.RC4_AEROPAY);

            try
            {
                TRINP.url = TypeUsuario.URL;
                TRINP.contexto = "/pgs/pcpayAgencia";

                xmlResponse = ws.SendWS(TRINP.url + "" + TRINP.contexto, strCadena);
                if (xmlResponse != "")
                {

                    xmlResponse = xmlResponse.Replace("<?xml version=\"1.0\" ?>","");

                    if (Info.Printer.Equals("1"))
                    {
                        string[] linea = xmlResponse.Split('@');

                        if (linea.Length < 35)
                            mp.SendC59(xmlResponse);
                        else
                            ImprimeBloque(linea);
                    }
                    else
                        MessageBox.Show(xmlResponse);

                    return true;
                }
                else
                {
                    MessageBox.Show("No existen Transacciones para este usuario.");
                    return false;
                }
            }
            catch(Exception ex)
            {
                MITLog.PrintLn(ex.Message);
                return false;
            }
        }

        private void ImprimeBloque(string[] linea)
        {
            int lineaMax = 30;
            int cont = 0;
            string cadena = "";

            for (int i = 0; i < linea.Length; i++)
            {
                
                cont++;
                cadena = cadena + " @" + linea[i];
            
                if(cont.Equals(lineaMax))
                {
                    mp.SendC59(cadena);
                    cadena = "";
                    cont = 0;
                }

            }

            if (!cadena.Equals(""))
            {
                Thread.Sleep(500);
                mp.SendC59(cadena.Trim() + "@br@br@br@br@br@br"); 
            }
        }

        
        /// <summary>
        /// Imprime el corte de Recompensas
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool ejecutaImprimeCorteRecom(string texto)
        { 
                return Print(texto);
        }

        /// <summary>
        /// Imprime el texto enviado a la función
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool Print(string texto)
        {
            try
            {
                if (texto.Contains("@"))
                {
                    string[] linea = texto.Split('@');

                    if (linea.Length < 35)
                        mp.SendC59(texto);
                    else
                        ImprimeBloque(linea);
                }
                
                return true;
            }
            catch(Exception ex)
            {
                MITLog.PrintLn(ex.Message);
                return false;
            }
        }


        public string ejecutaConsulta(string Bs_User,
                                    string Bs_Pwd,
                                    string Bs_Company,
                                    string Bs_Branch,
                                    string Tx_Date,
                                    string Tx_Reference)
        {
            string strCadena = "";
            String xmlResponse = "";

            StringBuilder strCadEncriptar = new StringBuilder();
            strCadEncriptar.Append("<q0:in0>" + Bs_User + "</q0:in0>");
            strCadEncriptar.Append("<q0:in1>" + RC4.Encrypt(Bs_Pwd, "RepGEmPgs") + "</q0:in1>");
            strCadEncriptar.Append("<q0:in2>" + Bs_Company + "</q0:in2>");
            strCadEncriptar.Append("<q0:in3>" + Tx_Date + "</q0:in3>");
            strCadEncriptar.Append("<q0:in4>" + Bs_Branch + "</q0:in4>");
            strCadEncriptar.Append("<q0:in5>" + Tx_Reference + "</q0:in5>");
            strCadena = strCadEncriptar.ToString();

            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/services/xmltransacciones";

            //Send message soap to host
            xmlResponse =  sendMsgSoap("http://wstrans.cpagos", "in0,in1,in2,in3,in4,in5", "transacciones", strCadena);
            if (!xmlResponse.Equals(""))
            {
                xmlResponse = xmlResponse.Replace("<ns1:out>", "<nodoPadre>");
                xmlResponse = xmlResponse.Replace("</ns1:out>", "</nodoPadre>");
                xmlResponse = utilidadesMIT.GetDataXML("nodoPadre", xmlResponse);
                return xmlResponse;
            }
            else
                return "Sin información";

        }


        public string ejecutaReimpresion(string Bs_User,
                                      string Bs_Pwd,
                                      string Bs_Company,
                                      string Bs_Branch,
                                      string Bs_Country,
                                      string Tx_OperationNumber)
        {
            TRINP.TRX_TYPE = Transaction.REIMPRESION;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/ReImpresionXml";

            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Tx_OperationNumber = Tx_OperationNumber;
            TRINP.Crypto = "2";

            //Send message to host.
            if (sendMsg())
            {
                return TRRSP.xml;
            }

            return "";


        }

        public string ejecutaConsultaPreventa(string Bs_User,
                                        string referencia,
                                        string tipoOpcion)
        {
            string strCadena = "";
            String xmlResponse = "";

            StringBuilder strCadEncriptar = new StringBuilder();
            strCadEncriptar.Append("&usuario=" + Bs_User);
            strCadEncriptar.Append("&referencia=" + referencia.ToUpper());
            strCadEncriptar.Append("&op=" + tipoOpcion);
            
            strCadena = strCadEncriptar.ToString();
            strCadena = "enc=" + RC4.Encrypt(strCadena, Info.RC4_AEROPAY);

            try
            {
                TRINP.url = TypeUsuario.URL;
                TRINP.contexto = "/pgs/pcpayAgencia";

                xmlResponse = ws.SendWS(TRINP.url + "" + TRINP.contexto, strCadena);
                if (xmlResponse != "")
                    return xmlResponse;
                else
                    return "<response>0</response><nb_response>Sin respuesta de Centro de Pagos</nb_response>";
            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
                return ex.Message;
            }
        }


        //Cierre preventa
        public bool ejecutaCierrePreventaMOTO(string Bs_User,
                                        string Bs_Pwd,
                                        string Bs_UsrTransaction,
                                        string Bs_Company,
                                        string Bs_Branch,
                                        string Bs_Country,
                                        string Tx_Amount,
                                        string Tx_Tip,
                                        string Tx_OperationNumber)
        {
            //Clear transaction vars
            clear.ClearTXN();

            bool status = false;
            bool _continue;
            
            TRINP.TRX_TYPE = Transaction.CIERREPREVENTAMOTO;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/CierrePreventaXml";

            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Tx_Amount = Tx_Amount;
            TRINP.Tx_Tip = Tx_Tip;
            TRINP.Tx_OperationNumber = Tx_OperationNumber;
            TRINP.Crypto = "2";
            //TRINP.dcc_status = "I";

            try
            {

                do
                {
                    _continue = false;

                    if (sendMsg())
                    {

                        if (TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;
                            if (DCC_Validate(true))
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                        TRRSP.xml = xmlErrores.xmlConectionError;
                } while (_continue);

                status = true;
            }
            catch (Exception ex)
            {
                setResponse(xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message));
            }
            return status;
        }



        public bool ejecutaPreventaPropinaEMV(string Bs_User,
                                    string Bs_Pwd,
                                    string Bs_UsrTransaction,
                                    string Bs_Company,
                                    string Bs_Branch,
                                    string Bs_Country,
                                    string Cc_Type,
                                    string Tx_Merchant,
                                    string Tx_Reference,
                                    string Tx_OperationType,
                                    string Tx_Currency,
                                    string Tx_Waiter,
                                    string Tx_Shifts,
                                    string Tx_Propina,
                                    string Cc_AMEXCvvCsc,
                                    string Tx_Table)
        {

            if (!TRINP.chkPp_CdError.Equals(""))
            {
                setResponse(TRINP.chkPp_XmlError);
                return false;
            }
            
            string RC4Key;
            bool status = false;
            bool _continue;

            TRINP.TRX_TYPE = Transaction.PREVENTAPROPINAEMV;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/CobroPropinaXml";

            TRINP.Bs_User = Bs_User;
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Cc_Type = Cc_Type;
            TRINP.Tx_Merchant = Tx_Merchant;
            TRINP.Tx_Reference = Tx_Reference.ToUpper();
            TRINP.Tx_OperationType = Tx_OperationType;
            TRINP.Tx_Currency = Tx_Currency;
            TRINP.Tx_Waiter = Tx_Waiter;
            TRINP.Tx_Shifts = Tx_Shifts;
            TRINP.Tx_Tip = Tx_Propina;
            TRINP.cvvcsc = Cc_AMEXCvvCsc;
            TRINP.Tx_Table = Tx_Table;
            TRINP.tp_resp = "1"; 
            
            RC4Key = RC4.Encrypt(TRINP.id_company, TRINP.id_company);
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, RC4Key);
            TRINP.dcc_status = "I";

            //terminal version
            TRINP.version_terminal = Info.version;
            //terminal model
            TRINP.modelo_terminal = Info.model;
            //is there printer?
            TRINP.printer = Info.Printer;
            //is mobile
            TRINP.is_mobile = "0";
            //is there display
            TRINP.display = "1";



            try
            {
                //*******************************************************************************
                //Crypto type
                if (TRINP.Command.Equals("C71") || TRINP.Command.Equals("C93"))
                {
                    TRINP.Crypto = "4";

                    if (!TRINP.Command.Equals("C93"))
                    {
                        //Encrypt Tracks
                        TRINP.tracks = RC4.Encrypt(TRINP.tracks, RC4Key);
                    }
                    
                        
                }
                else
                    TRINP.Crypto = "2";

                

                if (TRINP.isAMEX)
                    TRINP.Cc_Type = "AMEX";
                else
                    TRINP.Cc_Type = "V/MC";

                if (TRINP.isAMEX)
                {
                    //Se agrega funcionalidad para pedir CVV AMEX desde la DLL.
                    //Se agrega validación para solo pedir CVV en tarjetas banda AMEX
                    if (TRINP.chip.Equals("0") && TRINP.cvvcsc.Equals("") && !TRINP.Command.Equals("C93"))
                    {
                        frmCsvAMEX formAmex = new frmCsvAMEX();
                        formAmex.ShowDialog();
                        TRINP.cvvcsc = RC4.Encrypt(Cc_AMEXCvvCsc, Info.RC4Key);
                    }

                    //Se agrega validación para no enviar el CVV en caso de que las tarjetas sean con CHIP
                    if (TRINP.chip.Equals("1") && !TRINP.cvvcsc.Equals(""))
                        TRINP.cvvcsc = "";
                }

                if (Info.Kiosco.Equals("1"))
                    TRINP.Tx_OperationType = "29";

                //*******************************************************************************
                do
                {
                    //*******************************************************************************
                    //CHIP
                    if (TRINP.chip.Equals("1"))
                    {
                        TRINP.emv = "3";
                        //Encrypt Chip Name
                        TRINP.CHName = RC4.Encrypt(TRINP.CHName, RC4Key);
                        TRINP.chipnameenc = "1";
                    }
                    else
                    {
                        //Swipe card
                        TRINP.emv = "2";
                    }

                    _continue = false;
                    //Send Message
                    if (sendMsg())
                    {
                        if (TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;
                            if (DCC_Validate())
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            //End pinpad flow.
                            if (mp.SendFinishEMV())
                            {
                                //Pinpad Response, 00-Approved, !00-Declined
                                if (!TRINP.pinpadrsp.Trim().Equals("00"))
                                {
                                    if (TRRSP.response.Equals("approved"))
                                    {
                                        //Host approved, but pinpad declined.
                                        if (TypeUsuario.emvReverso)
                                        {
                                            ejecutaReverso(Bs_Pwd);
                                        }
                                    }
                                }
                                status = true;
                            }
                        }
                    }
                } while (_continue);
            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
            }
            return status;

        }

        public bool ejecutaPreventaEMV(string Bs_User,
                                    string Bs_Pwd,
                                    string Bs_UsrTransaction,
                                    string Bs_Company,
                                    string Bs_Branch,
                                    string Bs_Country,
                                    string Cc_Type,
                                    string Tx_Merchant,
                                    string Tx_Reference,
                                    string Tx_OperationType,
                                    string Tx_Currency,
                                    string Tx_Waiter,
                                    string Tx_Shifts,
                                    [Optional]string Cc_AMEXCvvCsc,
                                    [Optional]string Tx_Table)
        {

            if (!TRINP.chkPp_CdError.Equals(""))
            {
                setResponse(TRINP.chkPp_XmlError);
                return false;
            }

            string RC4Key;
            bool status = false;
            bool _continue;

            TRINP.TRX_TYPE = Transaction.PREVENTAEMV;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/PreventaXml";

            TRINP.Bs_User = Bs_User;
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Cc_Type = Cc_Type;
            TRINP.Tx_Merchant = Tx_Merchant;
            TRINP.Tx_Reference = Tx_Reference.ToUpper();
            TRINP.Tx_OperationType = Tx_OperationType;
            TRINP.Tx_Currency = Tx_Currency;
            TRINP.Tx_Waiter = Tx_Waiter;
            TRINP.Tx_Shifts = Tx_Shifts;
            //TRINP.Tx_Tip = Tx_Propina;
            //TRINP.cvvcsc = Cc_AMEXCvvCsc;
            //TRINP.Tx_Table = Tx_Table;
            TRINP.tp_resp = "1";
            
            RC4Key = RC4.Encrypt(TRINP.id_company, TRINP.id_company);
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, RC4Key);
            TRINP.dcc_status = "I";

            //terminal version
            TRINP.version_terminal = Info.version;
            //terminal model
            TRINP.modelo_terminal = Info.model;
            //is there printer?
            TRINP.printer = Info.Printer;
            //is mobile
            TRINP.is_mobile = "0";
            //is there display
            TRINP.display = "1";

            try
            {
                //*******************************************************************************
                //Crypto type
                if (TRINP.Command.Equals("C71") || TRINP.Command.Equals("C93"))
                {
                    TRINP.Crypto = "4";

                    if (!TRINP.Command.Equals("C93"))
                    {
                        //Encrypt Tracks
                        TRINP.tracks = RC4.Encrypt(TRINP.tracks, RC4Key);
                    }


                }
                else
                    TRINP.Crypto = "2";

                //*******************************************************************************
                //CHIP
                if (TRINP.chip.Equals("1"))
                {
                    TRINP.emv = "3";
                    //Encrypt Chip Name
                    TRINP.CHName = RC4.Encrypt(TRINP.CHName, RC4Key);
                    TRINP.chipnameenc = "1";
                }
                else
                {
                    //Swipe card
                    TRINP.emv = "2";
                }

                if (TRINP.isAMEX)
                    TRINP.Cc_Type = "AMEX";
                else
                    TRINP.Cc_Type = "V/MC";

                if (TRINP.isAMEX)
                {
                    //Se agrega funcionalidad para pedir CVV AMEX desde la DLL.
                    //Se agrega validación para solo pedir CVV en tarjetas banda AMEX
                    if (TRINP.chip.Equals("0") && TRINP.cvvcsc.Equals("") && !TRINP.Command.Equals("C93"))
                    {
                        frmCsvAMEX formAmex = new frmCsvAMEX();
                        formAmex.ShowDialog();
                        TRINP.cvvcsc = RC4.Encrypt(Cc_AMEXCvvCsc, Info.RC4Key);
                    }

                    //Se agrega validación para no enviar el CVV en caso de que las tarjetas sean con CHIP
                    if (TRINP.chip.Equals("1") && !TRINP.cvvcsc.Equals(""))
                        TRINP.cvvcsc = "";
                }

                if (Info.Kiosco.Equals("1"))
                    TRINP.Tx_OperationType = "29";

                //*******************************************************************************
                do
                {
                    _continue = false;
                    //Send Message
                    if (sendMsg())
                    {
                        if (TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;
                            if (DCC_Validate())
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            //End pinpad flow.
                            if (mp.SendFinishEMV())
                            {
                                //Pinpad Response, 00-Approved, !00-Declined
                                if (!TRINP.pinpadrsp.Trim().Equals("00"))
                                {
                                    if (TRRSP.response.Equals("approved"))
                                    {
                                        //Host approved, but pinpad declined.
                                        if (TypeUsuario.emvReverso)
                                        {
                                            ejecutaReverso(Bs_Pwd);
                                        }
                                    }
                                }
                                status = true;
                            }
                        }
                    }
                } while (_continue);
            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
            }
            return status;

        }

        public bool ejecutaActivaMenuToken()
        {
            return TypeUsuario.MenuToken;
        }

        public String getCardRSA(String mensaje, String longitud)
        {
            TypeUsuario.RSAKeyData = EncryptC.ObtieneDatosRSA(TypeUsuario.publicKeyRSA);
            TypeUsuario.RSAKeyDataLength = longitud;
            TypeUsuario.RSAKeyDataMSG = mensaje;
            mp.getCipherCard();

            return TRINP.pinpadrsp;
            
        }

        public String getToken(String Bs_Company,
                                 String Bs_Branch,
                                 String Bs_User,
                                 String Bs_Pwd,
                                 String numTDC,
                                 String Referencia)
        {
            TRINP.TRX_TYPE = Transaction.TOKEN;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/tokenize";
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_User = Bs_User;
            //TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_Pwd = Bs_Pwd;
            TRINP.cc_number = numTDC;
            TRINP.Tx_Reference = Referencia;

            //Send message to host.
            if (sendMsg())
            {
                return TRRSP.xml;
            }
            return "";
        }


        public Boolean ejecutaReautorizacionMOTO(String Bs_User,
                                              String Bs_Pwd,
                                              String Bs_UsrTransaction,
                                              String Bs_Company,
                                              String Bs_Branch,
                                              String Bs_Country,
                                              String Tx_Amount,
                                              String Tx_OperationNumber)
        {
            //Clear transaction vars
            clear.ClearTXN();

            Boolean _continue;

            TRINP.TRX_TYPE = Transaction.REAUTORIZACION_MOTO;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/ReAutorizacionXml";
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Tx_Amount = Tx_Amount;
            TRINP.Tx_OperationNumber = Tx_OperationNumber;
            TRINP.Crypto = "2";
            TRINP.dcc_status = "I";

            try
            {
                do
                {
                    _continue = false;

                    if (sendMsg())
                    {

                        if (TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;
                            if (DCC_Validate(true))
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                        TRRSP.xml = xmlErrores.xmlConectionError;
                } while (_continue);
            }
            catch (Exception ex)
            {
                setResponse(xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message));
            }
            return false;
        }


        public string ejecutaObtieneIDFacturaElectronica(string FormaPago)
        {
            string id = "-1";

            switch (FormaPago.ToUpper())
            {
                case "EFECTIVO":
                    id = "01";
                    break;
                case "CHEQUE NOMINATIVO":
                    id = "03";
                    break;
                case "TRANSF. ELECTRÓNICA":
                    id = "04";
                    break;
                case "TARJETA DE CRÉDITO":
                    id = "02";
                    break;
                case "MONEDERO ELECTRÓNICO":
                    id = "10";
                    break;
                case "DINERO ELECTRÓNICO":
                    id = "06";
                    break;
                case "VALES DE DESPENSA":
                    id = "07";
                    break;
                case "TARJETA DE DÉBITO":
                    id = "08";
                    break;
                case "TARJETA DE SERVICIO":
                    id = "09";
                    break;
                case "OTROS":
                    id = "05";
                    break;
                default:
                    id = "-1";
                    break;

            }

            return id;
        }

        public string ejecutaFacturaElectronica(string Bs_User,
                                              string Bs_Company,
                                              string Bs_Branch,
                                              string Tx_Amount,
                                              string Tx_Tip,
                                              string Tx_Ticket,
                                              string Tx_OperationNumber,
                                              string Tx_Date,
                                              string Tx_IdTpOperation,
                                              string Tx_TpOperation,
                                              string Tx_Concept,
                                              string Tx_DigitosTarjeta)
        {
            StringBuilder strCadEncriptar = new StringBuilder();
            string strCadena = "";
            string xmlResponse = "";

            strCadEncriptar.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            strCadEncriptar.Append("<transaccion>");
            strCadEncriptar.Append("<cd_empresa>" + Bs_Company + "</cd_empresa>");
            strCadEncriptar.Append("<cd_sucursal>" + Bs_Branch + "</cd_sucursal>");
            strCadEncriptar.Append("<importe_total>" + Tx_Amount + "</importe_total>");
            strCadEncriptar.Append("<propina>" + Tx_Tip + "</propina>");
            strCadEncriptar.Append("<num_ticket>" + Tx_Ticket + "</num_ticket>");
            strCadEncriptar.Append("<cd_usuario>" + Bs_User + "</cd_usuario>");
            strCadEncriptar.Append("<folio_pagos>" + Tx_OperationNumber + "</folio_pagos>");
            strCadEncriptar.Append("<fecha_hora>" + Tx_Date + "</fecha_hora>");
            strCadEncriptar.Append("<idMetodoPago>" + Tx_IdTpOperation + "</idMetodoPago>");
            strCadEncriptar.Append("<tp_operacion>" + Tx_TpOperation + "</tp_operacion>");
            strCadEncriptar.Append("<concepto>" + Tx_Concept + "</concepto>");
            strCadEncriptar.Append("<digitos_tarjeta>" + Tx_DigitosTarjeta + "</digitos_tarjeta>");
            strCadEncriptar.Append("</transaccion>");
            strCadena = strCadEncriptar.ToString();

            //pendiente
            strCadena = EncryptC.EncryptTripleDES(strCadena, Info.TRIPLEDES_KEY);
            //strCadena = utilidadesMIT.Base64ToHEX(strCadena);
            
            //strCadEncriptar.Append("<q0:in0>" + Bs_User + "</q0:in0>");
            strCadEncriptar = new StringBuilder();
            strCadEncriptar.Append("<q0:in0>true</q0:in0>");
            strCadEncriptar.Append("<q0:in1>" + strCadena + "</q0:in1>");
            strCadena = strCadEncriptar.ToString();

            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/facturaelectronica/services/factelectronica";

            //Send message soap to host
            xmlResponse = sendMsgSoap("http://services.mit.com", "in0,in1", "enviaTransaccion", strCadena);

            if (!xmlResponse.Equals(""))
            {
                xmlResponse = xmlResponse.Replace("<ns1:out>", "<nodoPadre>");
                xmlResponse = xmlResponse.Replace("</ns1:out>", "</nodoPadre>");
                xmlResponse = utilidadesMIT.GetDataXML("nodoPadre", xmlResponse);
                setResponse(xmlResponse);
                return xmlResponse;
            }
            else
            {
                string err = xmlErrores.xmlApplicationError;
                err = err.Replace("#ERROR", "0");
                err = err.Replace("$ERROR", TRINP.DsError);

                err = err.Replace("<cd_response/>", "<cd_response>0</cd_response>");
                err = err + "<nb_response>" + TRINP.DsError + "</nb_response>";

                setResponse(err);
                return err;
            }
        }

        public string EjecutaFacturaElectronicaDatos(string Bs_User,
                                        string fechaYHora,
                                        string rfcEmisor,
                                        string sucursal,
                                        string industria,
                                        string codLook,
                                        string idMetodoPago,
                                        string metodoPago,
                                        string importeTotal,
                                        string noTicket,
                                        string rfcReceptor,
                                        string idTransaccion,
                                        string email,
                                        string Propina,
                                        string Concepto,
                                        string digitosTarjeta,
                                        string idMoneda,
                                        string tipoCambio,
                                        string tipoCFDI,
                                        string observaciones,
                                        string nombre,
                                        string codigoPostal,
                                        string noInterior,
                                        string pais,
                                        string estado,
                                        string localidad,
                                        string colonia,
                                        string noExterior,
                                        string municipio,
                                        string calle)
        {
            StringBuilder strCadEncriptar = new StringBuilder();
            string strCadena = "";
            string xmlResponse = "";

            strCadEncriptar.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            strCadEncriptar.Append("<facturizate>");
            strCadEncriptar.Append("<transaccion");
            strCadEncriptar.Append(" fechaYHora = \"" + fechaYHora + "\"");
            strCadEncriptar.Append(" rfcEmisor = \"" +  "1234567891234" + "\"");
            strCadEncriptar.Append(" sucursal = \"" + sucursal + "\"");
            strCadEncriptar.Append(" industria = \"" + "\"");
            strCadEncriptar.Append(" codLook = \"" + codLook + "\"");
            strCadEncriptar.Append(" idMetodoPago = \"" + idMetodoPago + "\"");
            strCadEncriptar.Append(" metodoPago = \"" + metodoPago + "\"");
            strCadEncriptar.Append(" importeTotal = \"" + importeTotal + "\"");
            strCadEncriptar.Append(" noTicket = \"" + noTicket + "\"");
            strCadEncriptar.Append(" rfcReceptor = \"" + rfcReceptor + "\"");
            strCadEncriptar.Append(" idTransaccion = \"" + idTransaccion + "\"");
            strCadEncriptar.Append(" email = \"" + email + "\"");
            strCadEncriptar.Append(" digitosTarjeta = \"" + digitosTarjeta + "\"");
            strCadEncriptar.Append(" idMoneda = \"" + idMoneda + "\"");
            strCadEncriptar.Append(" tipoCambio = \"" + tipoCambio + "\"");
            strCadEncriptar.Append(" tipoCFDI = \"" + tipoCFDI + "\"");
            strCadEncriptar.Append(" observaciones = \"" + observaciones + "\"");
            strCadEncriptar.Append(" usuario  = \"" + Bs_User + "\"");
            strCadEncriptar.Append(" propina = \"" + Propina + "\"");
            strCadEncriptar.Append(" concepto= \"" + Concepto + "\"/>");

            strCadEncriptar.Append("<receptor rfcReceptor= \"" + rfcReceptor + "\" nombre= \"" + nombre + "\">");
            strCadEncriptar.Append("<domicilio codigoPostal = \"" + codigoPostal + "\"");
            strCadEncriptar.Append(" noInterior = \"" + noInterior + "\"");
            strCadEncriptar.Append(" pais = \"" + pais + "\"");
            strCadEncriptar.Append(" estado = \"" + estado + "\"");
            strCadEncriptar.Append(" localidad = \"" + localidad + "\"");
            strCadEncriptar.Append(" colonia = \"" + colonia + "\"");
            strCadEncriptar.Append(" noExterior = \"" + noExterior + "\"");
            strCadEncriptar.Append(" municipio = \"" + municipio + "\"");
            strCadEncriptar.Append(" calle = \"" + calle + "\"/>");
            strCadEncriptar.Append("</receptor>");
            strCadEncriptar.Append("</facturizate>");
            strCadena = strCadEncriptar.ToString();

            //pendiente
            strCadena = EncryptC.EncryptTripleDES(strCadena, Info.TRIPLEDES_KEY);
            //strCadena = utilidadesMIT.Base64ToHEX(strCadena);

            strCadEncriptar = new StringBuilder();
            strCadEncriptar.Append("<q0:xmlCrypt0>" + strCadena + "</q0:xmlCrypt0>");
            //strCadEncriptar.Append("<q0:xmlCrypt1>true</q0:xmlCrypt1>");
            strCadena = strCadEncriptar.ToString();

            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/facturaelectronica/services/factelectronica";

            //Send message soap to host
            xmlResponse = sendMsgSoap("http://service.mit.com", "xmlCrypt0,xmlCrypt1", "partidasCryptECB", strCadena);

            if (!xmlResponse.Equals(""))
            {
                xmlResponse = xmlResponse.Replace("<ns1:out>", "<nodoPadre>");
                xmlResponse = xmlResponse.Replace("</ns1:out>", "</nodoPadre>");
                xmlResponse = utilidadesMIT.GetDataXML("nodoPadre", xmlResponse);

                xmlResponse = EncryptC.DecryptTripleDES(xmlResponse, Info.TRIPLEDES_KEY);

                setResponse(xmlResponse);
                return xmlResponse;
            }
            else
            {
                string err = xmlErrores.xmlApplicationError;
                err = err.Replace("#ERROR", "0");
                err = err.Replace("$ERROR", TRINP.DsError);

                err = err.Replace("<cd_response/>", "<cd_response>0</cd_response>");
                err = err + "<nb_response>" + TRINP.DsError + "</nb_response>";

                setResponse(err);
                return err;
            }

        }

        public string ejecutadbgSetCmd61()
        {
           string StrBufferTx;
           string aux = "";
           int numBloques;

           try
           {

               StrBufferTx = "00";
               if (mp.SendC61(StrBufferTx))
               {
                   aux = TRINP.pinpadrsp;

                   //SI SE PRESENTA UN CANCELAR EN LA PINPAD
                   if (aux.Substring(0, 3) == "A02")
                       return "ERROR-CANCELADO";

                   int.TryParse(TRINP.NumeroBloquesFPP, out numBloques);

                   for (int i = 1; i <= numBloques; i++)
                   {
                       StrBufferTx = "0" + i.ToString();
                       mp.SendC61(StrBufferTx);
                   }

                   TRINP.FirmaPinPad = System.Text.Encoding.UTF8.GetString(TRINP.FirmaPinPadByte);
                   return TRINP.FirmaPinPad;
               }
               else
                   return "";
           }
           catch(Exception ex)
           {
               MITLog.PrintLn(ex.Message);
               return "";
           }
        }

        public string ejecutaObtieneFormMail(string strTipoRespuesta, string strMail)
        {
            TRINP.strMailFirma = "";
            frmFirmaenPanel formMail = new frmFirmaenPanel();
            formMail.strTipoRespuesta = strTipoRespuesta;
            formMail.ShowDialog();
            return TRINP.strMailFirma;
        }

        public bool ejecutaReenviaVoucherFirmaPanel(string urlFirma,
                                            string Id_Company,
                                            string Id_Branch,
                                            string country,
                                            string User,
                                            string Pass,
                                            string strFolio)
        {

            StringBuilder xml = new StringBuilder();
            string msg = "";

            try
            {
                if (strFolio.Equals(""))
                {
                    msg = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/> <auth/><cd_response/><cd_error></cd_error><nb_error>Número de Operación o Referencia Vacía</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/> <tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
                    setResponse(msg);
                    return false;
                }

                ejecutaObtieneFormMail("-1", "");

                if (!TRINP.strMailFirma.Equals(""))
                {
                   
                    if (EnviaMailFirmaPanel(TRINP.strMailFirma, strFolio, Id_Company, Id_Branch, country, User, Pass, urlFirma))
                    {
                        ejecutaObtieneFormMail("0", TRINP.strMailFirma);
                        msg = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>true</response><foliocpagos/> <auth/><cd_response>00 - Correo electrónico enviado correctamente</cd_response><cd_error></cd_error><nb_error></nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/> <tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
                        setResponse(msg);
                        return true;
                    }
                    else
                    {
                        setResponse(xmlErrores.xmlConectionError);
                        return false;
                    }
                }
                else
                {
                    setResponse(xmlErrores.xmlMailVacio);
                    return false;
                }

            }
            catch(Exception ex)
            {
                setResponse(xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message));
                return false;
            }
        }
        public String getEMVParams(String op)
        {
            String xml = "";
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/EMVKeysService"; 
            xml = "op="+op;
            try
            {
                return ws.SendWS(TRINP.url + "" + TRINP.contexto, xml);
            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
            }
            return "";
        }
        //EMV Update
        public Boolean updateEMVParams()
        {
            Boolean update = false;
            String ppVersion = "";
            String dllVersion = "";
            int ippversion = 0;
            int idllversion = 0;
            if((Info.KeysVersion!=null) && (TypeUsuario.keys_version!=null))
            {
                ppVersion = Info.KeysVersion.Replace(".", "");
                dllVersion = TypeUsuario.keys_version.Replace(".", "");
                if (ppVersion.Equals("")||ppVersion.Equals("\0\0\0\0\0"))
                {
                    ppVersion = "0";
                }
                if (dllVersion.Equals(""))
                {
                    dllVersion = "0";
                }
                try
                {
                    ippversion = Int32.Parse(ppVersion);
                    idllversion = Int32.Parse(dllVersion);
                    if (ippversion < idllversion)
                    {
                        update = true;
                    }
                }
                catch (Exception ex)
                {
                    MITLog.PrintLn(ex.Message);
                }
            }

            if (TypeUsuario.force_update.Trim().Equals("1")
                || (TypeUsuario.st_update.Trim().Equals("1")
                    && update)
                )
            {
                frmDownload frmdwnld = new frmDownload();
                frmdwnld.ShowDialog();
            }
            return false;
        }


        public bool EjecutaCPIntegracion_cpAVSs2(string Id_Company,
                                     string Id_Branch,
                                     string country,
                                     string User,
                                     string Pwd,
                                     string merchant,
                                     string reference,
                                     string tp_operation,
                                     string typeC,
                                     string nameC,
                                     string numberC,
                                     string expmonthC,
                                     string expyearC,
                                     string cvvcscC,
                                     string Amount,
                                     string currencyC,
                                     string direccion,
                                     string NumInt,
                                     string NumExt,
                                     string delegacion,
                                     string ciudad,
                                     string Estado,
                                     string cp,
                                     string colonia,
                                     string nombreC,
                                     string PaisC,
                                     string TelefonoC,
                                     string CorreoC,
                                     string Tx_isCheckin,
                                     [Optional]string Tx_boletos,
                                     [Optional]string Tx_fechaSalida,
                                     [Optional]string Tx_fechaRetorno)
        {

            ////int tpCc = 0;
            ////clear.ClearTXN();

            ////TRINP.TRX_TYPE = Transaction.VTAFRZDMOTO;
            ////TRINP.url = TypeUsuario.URL;
            ////TRINP.contexto = "/pgs/VentaForzadaXml";
            ////TRINP.Bs_User = Bs_User;
            ////TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            ////TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            ////TRINP.id_company = Bs_Company;
            ////TRINP.id_branch = Bs_Branch;
            ////TRINP.Bs_Country = Bs_Country;
            ////TRINP.Tx_Merchant = Tx_Merchant;
            ////TRINP.Tx_Reference = Tx_Reference;
            ////TRINP.Tx_OperationType = Tx_OperationType;
            ////TRINP.Tx_Amount = Tx_Amount;
            ////TRINP.Tx_Currency = Tx_Currency;
            ////TRINP.Tx_Auth = Tx_Auth;
            ////TRINP.Cc_Type = Cc_Type;
            ////TRINP.CHName = RC4.Encrypt(Cc_Name, Info.RC4Key);
            ////TRINP.tracks = RC4.Encrypt(Cc_Number, Info.RC4Key);
            ////TRINP.expmonth = RC4.Encrypt(Cc_ExpMonth, Info.RC4Key);
            ////TRINP.expyear = RC4.Encrypt(Cc_ExpYear, Info.RC4Key);
            ////TRINP.cvvcsc = RC4.Encrypt(Cc_CvvCsc, Info.RC4Key);
            ////TRINP.tx_boleto = Tx_boleto;
            ////TRINP.fh_salida = fh_salida;
            ////TRINP.fh_retorno = fh_retorno;

            ////TRINP.Crypto = "2";

            ////if (TRINP.isAMEX)
            ////{
            ////    TRINP.Cc_Type = "AMEX";
            ////    tpCc = 4;
            ////}
            ////else
            ////{
            ////    TRINP.Cc_Type = "V/MC";
            ////    tpCc = 3;
            ////}

            ////if (!Cc_CvvCsc.Equals("") && ((Cc_CvvCsc.Trim().Length == tpCc) || (Cc_CvvCsc.Equals("0000"))))
            ////{
            ////    if (sendMsg())
            ////    {
            ////        return false;
            ////    }
            ////}
            ////return true;
            
            
            //pendiente
            return false;
        }

        public bool EjecutaSendMessage(string message)
        {
            try
            {
                if (mp.SendC50(message))
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                MITLog.PrintLn(ex.Message);
                return false;
            }
        }

        public bool ejecutaVtaBoletosEMV(string Bs_User,
                                 string Bs_Pwd,
                                 string Bs_UsrTransaction,
                                 string Bs_Company,
                                 string Bs_Branch,
                                 string Bs_Country,
                                 string Cc_Type,
                                 string Tx_Merchant,
                                 string Tx_Reference,
                                 string Tx_OperationType,
                                 string Tx_Currency,
                                 string Tx_boleto,
                                 string fh_salida,
                                 string fh_retorno,
                                 [Optional] string Cc_AMEXCvvCsc)
        {

            if (!TRINP.chkPp_CdError.Equals(""))
            {
                setResponse(TRINP.chkPp_XmlError);
                return false;
            }

            bool _continue;
            string RC4Key;

            TRINP.TRX_TYPE = Transaction.VTA_BOLETOSEMV;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/cobroXml";

            TRINP.Bs_User = Bs_User;
            TRINP.id_company = Bs_Company;
            RC4Key = RC4.Encrypt(TRINP.id_company, TRINP.id_company);
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;

            TRINP.Cc_Type = Cc_Type;
            TRINP.Tx_Merchant = Tx_Merchant;
            TRINP.Tx_Reference = Tx_Reference.ToUpper();
            TRINP.Tx_OperationType = Tx_OperationType;
            TRINP.Tx_Currency = Tx_Currency;

            TRINP.cvvcsc = RC4.Encrypt(Cc_AMEXCvvCsc, Info.RC4Key);
            TRINP.tx_boleto = Tx_boleto;
            TRINP.fh_salida = fh_salida;
            TRINP.fh_retorno = fh_retorno;

            TRINP.dcc_status = "I";
            TRINP.version_terminal = Info.version;
            TRINP.modelo_terminal = Info.model;
            TRINP.printer = Info.Printer;
            TRINP.is_mobile = "0";
            TRINP.display = "1";

            TRINP.Crypto = "2";


            try
            {
                //*******************************************************************************
                //Crypto type
                if (TRINP.Command.Equals("C71") || TRINP.Command.Equals("C93"))
                {
                    TRINP.Crypto = "4";

                    if (!TRINP.Command.Equals("C93"))
                    {
                        //Encrypt Tracks
                        TRINP.tracks = RC4.Encrypt(TRINP.tracks, RC4Key);
                    }


                }
                else
                    TRINP.Crypto = "2";

                //*******************************************************************************
                //CHIP
                if (TRINP.chip.Equals("1"))
                {
                    TRINP.emv = "3";
                    TRINP.CHName = RC4.Encrypt(TRINP.CHName, RC4Key);
                    TRINP.chipnameenc = "1";
                }
                else
                {
                    //Swipe card
                    TRINP.emv = "2";
                }

                if (TRINP.isAMEX)
                    TRINP.Cc_Type = "AMEX";
                else
                    TRINP.Cc_Type = "V/MC";

                if (TRINP.isAMEX)
                {
                    //Se agrega funcionalidad para pedir CVV AMEX desde la DLL.
                    //Se agrega validación para solo pedir CVV en tarjetas banda AMEX
                    if (TRINP.chip.Equals("0") && TRINP.cvvcsc.Equals("") && !TRINP.Command.Equals("C93"))
                    {
                        frmCsvAMEX formAmex = new frmCsvAMEX();
                        formAmex.ShowDialog();
                        TRINP.cvvcsc = RC4.Encrypt(Cc_AMEXCvvCsc, Info.RC4Key);
                    }

                    //Se agrega validación para no enviar el CVV en caso de que las tarjetas sean con CHIP
                    if (TRINP.chip.Equals("1") && !TRINP.cvvcsc.Equals(""))
                        TRINP.cvvcsc = "";
                }

                if (Info.Kiosco.Equals("1"))
                    TRINP.Tx_OperationType = "29";

                do
                {
                    _continue = false;
                    //Send Message
                    if (sendMsg())
                    {
                        if (TRRSP.response.Equals("dcc"))
                        {
                            TRINP.dcc_process = true;

                            if (DCC_Validate())
                            {
                                _continue = true;
                            }
                        }
                        else
                        {
                            //End pinpad flow.
                            if (mp.SendFinishEMV())
                            {
                                //Pinpad Response, 00-Approved, !00-Declined
                                if (!TRINP.pinpadrsp.Trim().Equals("00"))
                                {
                                    if (TRRSP.response.Equals("approved"))
                                    {
                                        //Host approved, but pinpad declined.
                                        if (TypeUsuario.emvReverso)
                                        {
                                            ejecutaReverso(Bs_Pwd);
                                        }
                                    }
                                }
                                return true;
                            }
                        }
                    }
                } while (_continue);
            }
            catch (Exception ex)
            {
                setResponse(xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message));
            }

            return false;
        }

        public bool ejecutaVtaBoletosMOTO(string Bs_User,
                                    string Bs_Pwd,
                                    string Bs_UsrTransaction,
                                    string Bs_Company,
                                    string Bs_Branch,
                                    string Bs_Country,
                                    string Tx_Merchant,
                                    string Tx_Reference,
                                    string Tx_OperationType,
                                    string Tx_Amount,
                                    string Tx_Currency,
                                    string Cc_Type,
                                    string Cc_Name,
                                    string Cc_Number,
                                    string Cc_ExpMonth,
                                    string Cc_ExpYear,
                                    string Cc_CvvCsc,
                                    string Tx_boleto,
                                    string fh_salida,
                                    string fh_retorno)
        {
            int tpCc = 0;
            bool _continue;
            clear.ClearTXN();

            TRINP.TRX_TYPE = Transaction.VTA_BOLETOSMOTO;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/cobroXml";

            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key);
            TRINP.Bs_UsrTransaction = Bs_UsrTransaction;
            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Tx_Merchant = Tx_Merchant;
            TRINP.Tx_Reference = Tx_Reference;
            TRINP.Tx_OperationType = Tx_OperationType;
            TRINP.Tx_Amount = Tx_Amount;
            TRINP.Tx_Currency = Tx_Currency;
            TRINP.Cc_Type = Cc_Type;
            TRINP.CHName = RC4.Encrypt(Cc_Name, Info.RC4Key);
            TRINP.tracks = RC4.Encrypt(Cc_Number, Info.RC4Key);
            TRINP.expmonth = RC4.Encrypt(Cc_ExpMonth, Info.RC4Key);
            TRINP.expyear = RC4.Encrypt(Cc_ExpYear, Info.RC4Key);
            TRINP.cvvcsc = RC4.Encrypt(Cc_CvvCsc, Info.RC4Key);
            TRINP.tx_boleto = Tx_boleto;
            TRINP.fh_salida = fh_salida;
            TRINP.fh_retorno = fh_retorno;

            TRINP.Crypto = "2";
            TRINP.dcc_status = "I";

            try
            {
                if (TRINP.isAMEX)
                {
                    TRINP.Cc_Type = "AMEX";
                    tpCc = 4;
                }
                else
                {
                    TRINP.Cc_Type = "V/MC";
                    tpCc = 3;
                }

                if (!Cc_CvvCsc.Equals("") && ((Cc_CvvCsc.Trim().Length == tpCc) || (Cc_CvvCsc.Equals("0000"))))
                {
                    do
                    {
                        _continue = false;

                        if (sendMsg())
                        {

                            if (TRRSP.response.Equals("dcc"))
                            {
                                TRINP.dcc_process = true;
                                if (DCC_Validate(true))
                                {
                                    _continue = true;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                            TRRSP.xml = xmlErrores.xmlConectionError;
                    } while (_continue);
                }
                else
                {
                    setResponse(xmlErrores.xmlAmexError);
                    return false;
                }
            }
            catch (Exception ex)
            {
                setResponse(xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message));
            }

            return false;
        }

        #region TAE

        public bool ejecutasndVtaTiempoAire(string Bs_User,
                                string Bs_Pwd,
                                string Bs_Company,
                                string Bs_Branch,
                                string Bs_Country,
                                string Tx_OperationType,
                                string Tx_Currency,
                                string Cc_Type,
                                string Ta_NumTel,
                                string Ta_ConfNumTel,
                                string Ta_IdProveedor,
                                string Ta_IdCategoria,
                                string Ta_IdProducto,
                                bool Ta_IsEfectivo,
                                string csvAmexenBanda)
        {
            
            if(!Ta_NumTel.Equals(Ta_ConfNumTel))
            {
                setResponse(utilidadesMIT.SetError("TA01", "El número no coincide con su confirmación"));
                CancelOperation();
                return false;
            }

            if(Ta_NumTel.Length < 10 )
            {
                setResponse(utilidadesMIT.SetError("TA02", "Número incorrecto"));
                CancelOperation();
                return false;
            }

            if (!utilidadesMIT.IsNumber(Ta_NumTel) || !utilidadesMIT.IsNumber(Ta_ConfNumTel))
            {
                setResponse(utilidadesMIT.SetError("TA03", "Debe ingresar valores númericos"));
                CancelOperation();
                return false;
            }

            TRINP.TAENumTel = Ta_NumTel;
            TRINP.TAEisEfectivo = Ta_IsEfectivo;

            return ejecutaSndVentaServiciosTAE(Bs_User, Bs_Pwd, Bs_Company, Bs_Branch, Bs_Country,
                                Tx_OperationType, Tx_Currency, Cc_Type, Ta_IdProveedor, Ta_IdCategoria, Ta_IdProducto, csvAmexenBanda);
            
        }

        public bool ejecutaSndVentaServiciosTAE(string Bs_User,
                                string Bs_Pwd,
                                string Bs_Company,
                                string Bs_Branch,
                                string Bs_Country,
                                string Tx_OperationType,
                                string Tx_Currency,
                                string Cc_Type,
                                string Vs_IdProveedor,
                                string Vs_IdCategoria,
                                string Vs_IdProducto,
                                [Optional] string Cc_AMEXCvvCsc)
        {



            
            
            //pendiente
            return false;
        }

        #endregion

        //Se pone para eliminar las afiliaciones q no concuerden con la moneda solicitada. 
        private string dbgGetStringTpPago(string tpPago, string strCadena)
        {

            if (strCadena.Equals("") || string.IsNullOrEmpty(TRINP.Tx_Currency))
                return strCadena;

            string strAux;
            string[] mySplit;

            strCadena = strCadena.Replace("<" + tpPago + ">","");
            strCadena = strCadena.Replace("</" + tpPago + ">", "");

            strCadena = strCadena.Replace("<af>", "|<af>");
            mySplit = strCadena.Split('|');

            for (int i = 0; i < mySplit.Length; i++)
            {
                strAux = mySplit[i];

                if(utilidadesMIT.GetDataXML("nb_currency", strAux) != TRINP.Tx_Currency && strAux != "" )
                {
                    strCadena = strCadena.Replace(strAux, "");
                }

            }

            strCadena = "<" + tpPago + ">" + strCadena.Replace("|", "") + "</" + tpPago + ">"; 
            return strCadena;
        }

        //Establece el código de moneda
        public void dbgSetCurrencyType(string CurrencyType)
        {
            if (CurrencyType.ToUpper().Equals("MXN"))
                TRINP.Tx_CurrencyCode = "0484";
            else if (CurrencyType.ToUpper().Equals("USD"))
                TRINP.Tx_CurrencyCode = "0840";
            else
                TRINP.Tx_CurrencyCode = "0" + CurrencyType;
        }


        #region CUPONES

        public string ejecutaPayNoPayAltaCliente(string Bs_Company,
                                 string Bs_Branch,
                                 string Bs_Country,
                                 string Bs_User,
                                 string Bs_Pwd,
                                 string phone)
        {
            //Clear transaction vars
            clear.ClearTXN();

            TRINP.TRX_TYPE = Transaction.ALTA_CUPONES;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/PnpXml";

            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key); //Encrypt password
            TRINP.Tx_OperationType = "ALTA_CLIENTE";
            TRINP.phone = phone;

            try
            {
                //Send message to host.
                if (sendMsg())
                {
                    setResponse(TRRSP.xml);
                    return TRRSP.xml;
                }
                else
                    return xmlErrores.xmlConectionError;
            }
            catch (Exception ex)
            {
                return xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message);
            }

        }


        public string ejecutaPayNoPayBusquedaCupon(string Bs_Company,
                                 string Bs_Branch,
                                 string Bs_Country,
                                 string Bs_User,
                                 string Bs_Pwd,
                                 string searchValue)
        {
            //Clear transaction vars
            clear.ClearTXN();

            TRINP.TRX_TYPE = Transaction.BUSCAR_CUPONES;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/PnpXml";

            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key); //Encrypt password
            TRINP.Tx_OperationType = "BUSQUEDA";
            TRINP.phone = searchValue;

            try
            {
                //Send message to host.
                if (sendMsg())
                {
                    setResponse(TRRSP.xml);
                    return TRRSP.xml;
                }
                else
                    return xmlErrores.xmlConectionError;
            }
            catch (Exception ex)
            {
                return xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message);
            }

        }

        public string ejecutaPayNoPayRedimirCupon(string Bs_Company,
                                 string Bs_Branch,
                                 string Bs_Country,
                                 string Bs_User,
                                 string Bs_Pwd,
                                 string value)
        {
            //Clear transaction vars
            clear.ClearTXN();

            TRINP.TRX_TYPE = Transaction.REDIMIR_CUPONES;
            TRINP.url = TypeUsuario.URL;
            TRINP.contexto = "/pgs/PnpXml";

            TRINP.id_company = Bs_Company;
            TRINP.id_branch = Bs_Branch;
            TRINP.Bs_Country = Bs_Country;
            TRINP.Bs_User = Bs_User;
            TRINP.Bs_Pwd = RC4.Encrypt(Bs_Pwd, Info.RC4Key); //Encrypt password
            TRINP.Tx_OperationType = "REDENCION";
            TRINP.phone = value;

            try
            {
                //Send message to host.
                if (sendMsg())
                {
                    setResponse(TRRSP.xml);
                    return TRRSP.xml;
                }
                else
                    return xmlErrores.xmlConectionError;
            }
            catch (Exception ex)
            {
                return xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message);
            }

        }


        #endregion

        #region CONVERSOR DCC

        public string ConsultaXMLConversorDCC(string URL, [Optional]string numAfiliacion)
        {
            string respuesta;
            string xml;
            TRINP.url = URL;
            TRINP.contexto = "/pgs/dccRates";

            if(!numAfiliacion.Equals(""))
                xml = "afiliacion=" + numAfiliacion;
            else
                xml = "";

            try
            {

            respuesta = ws.SendWS(TRINP.url + "" + TRINP.contexto, xml);

            //Is there host response?
            if (!respuesta.Equals(""))
                return respuesta;              
            else
                return xmlErrores.xmlConectionError;
               
            }
            catch (Exception ex)
            {
                MITLog.PrintLn("ConsultaXMLConversorDCC: -- " + ex.Message);
                return xmlErrores.xmlApplicationError.Replace("$ERROR", ex.Message);
            }
        }

        #endregion

        public bool ejecutaSetReader()
        {
            try
            {
                return mp.connect();
            }
            catch(Exception ex)
            {
                MITLog.PrintLn(ex.Message);
                return false;
            }
        }

        public void EnabledLog()
        {
            if (TypeUsuario.SaveLogTransaction)
            {
                Info.LOGS_CONSOLE = true;
                Info.LOGS_FILE = true;
            }
            else
            {
                Info.LOGS_CONSOLE = false;
                Info.LOGS_FILE = false;
            }
        }


        #region ACTUALIZACION PINPAD

        public bool EjecutaUpdatePinPad()
        {
            int intentos;
            string intentosUpdate;

            try
            {
                if (utilidadesMIT.ObtieneParametrosMIT("TryUpdate").Equals(""))
                    utilidadesMIT.GuardaParametrosMIT("TryUpdate", "1");
                
                intentosUpdate = utilidadesMIT.ObtieneParametrosMIT("TryUpdate");
                int.TryParse(intentosUpdate, out intentos);

                if(intentos >= 4)
                    utilidadesMIT.GuardaParametrosMIT("TryUpdate", "3");
                
                this.UpdatePinPad();
                return true;
            }
            catch(Exception ex)
            {
                MITLog.PrintLn("EjecutaUpdatePinPad:" + ex.Message);
                return false;
            }
        }

        //Verifica si la pinpad es actualizable
        public bool EjecutaIsUpgradeable()
        {
            bool dbgIsUpgradeable = false;
            TRINP.isCargaVerifone = false;
            string versionPP;
            string versionXML;

            try
            {
                if (string.IsNullOrEmpty(TRINP.xmlPinPad))
                {
                    TRINP.contexto = "/pgs/jsp/cpagos/xmlPinPad.jsp";
                    TRINP.xmlPinPad = ws.SendWS(TypeUsuario.URL + "" + TRINP.contexto, "op=");
                }

                if (!TRINP.xmlPinPad.Equals(""))
                {

                    //Valida si la versión de PcPay Soporta la Carga.
                    if (utilidadesMIT.CompareVersions(utilidadesMIT.GetVersionDLL(), utilidadesMIT.GetDataXML("versionPcPay", TRINP.xmlPinPad)) < 0)
                        return false;
                    
                    if(utilidadesMIT.GetDataXML("update_terminal", TypeUsuario.CadenaXML).Equals("1"))
                    {
                        if(!utilidadesMIT.GetDataXML("tipopagobVMC", TypeUsuario.CadenaXML).Equals("") || !utilidadesMIT.GetDataXML("tipopagobAMEX", TypeUsuario.CadenaXML).Equals("") || TypeUsuario.CadenaXML.Equals(""))
                        {
                            if (mp.connect())
                            {
                                    if (utilidadesMIT.GetDataXML(Info.model, TRINP.xmlPinPad).Equals(""))
                                    {
                                        dbgIsUpgradeable = false;
                                        TRINP.isCargaVerifone = false;
                                        TRINP.isCargaIngenico = false;
                                    }
                                    else
                                    { 
                                        if(Info.version.ToUpper().Contains("MITSBT"))
                                        {
                                            versionXML = utilidadesMIT.GetDataXML("minVersion",utilidadesMIT.GetDataXML("SBT", TRINP.xmlPinPad)).Trim();
                                            versionPP = Info.version.Trim();
                                            versionXML = versionXML.Substring(versionXML.Length - 5);
                                            versionPP = versionPP.Substring(versionPP.Length - 5);

                                            if(utilidadesMIT.CompareVersions(versionPP, versionXML) >= 0)
                                            {
                                                versionXML = utilidadesMIT.GetDataXML("version", utilidadesMIT.GetDataXML("SBT", TRINP.xmlPinPad)).Trim();
                                                versionXML = versionXML.Substring(versionXML.Length - 5);

                                                if (utilidadesMIT.CompareVersions(versionPP, versionXML) < 0)
                                                {
                                                    dbgIsUpgradeable = true;
                                                    TRINP.isCargaVerifone = false;
                                                    TRINP.isCargaIngenico = true;
                                                    return dbgIsUpgradeable;
                                                }
                                                else
                                                    return dbgIsUpgradeable = false;

                                            }
                                            else
                                                return dbgIsUpgradeable = false;
                                            
                                        }

                                        if( Info.marca.ToUpper().Equals("INGENICO") && ( Info.model.ToUpper().Equals("IPP320") || Info.model.ToUpper().Equals("ICT220") || Info.model.ToUpper().Equals("EFT930S") ))
                                        {
                                            TRINP.isCargaVerifone = false;
                                            TRINP.isCargaIngenico = true;
                                            
                                            dbgIsUpgradeable = false;
                                            return dbgIsUpgradeable;
                                        }
                                        
                                        if( Info.marca.ToUpper().Equals("VERIFONE"))
                                        {
                                            //Es la nueva Vx520 con carga de verifone   
                                            if( Info.version.Contains("MITP_") )
                                            {
                                                TRINP.isCargaIngenico = false;
                                                TRINP.isCargaVerifone = true;

                                                versionPP = Info.version.Trim().Replace("MITP_","");
                                                versionXML = utilidadesMIT.GetDataXML("minVersion",utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)).Trim();
                                                versionXML = versionXML.Replace("MITP_","");

                                                if( Info.model.ToUpper().Equals("VX810")  ||
                                                    Info.model.ToUpper().Equals("VX820") || Info.model.ToUpper().Equals("VX520"))
                                                {
                                                    if(utilidadesMIT.CompareVersions(versionPP, versionXML) >= 0)
                                                    {
                                                        versionXML = utilidadesMIT.GetDataXML("version",utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)).Trim();
                                                        versionXML = versionXML.Replace("MITP_","");

                                                        if (utilidadesMIT.CompareVersions(versionPP, versionXML) < 0)
                                                        {
                                                            dbgIsUpgradeable = true;
                                                            return dbgIsUpgradeable;
                                                        }
                                                    }
                                                }
                                                    
                                                versionPP = Info.version.Trim().Replace("MITP_","");
                                                versionXML = utilidadesMIT.GetDataXML("minVersion",utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)).Trim();
                                                versionXML = versionXML.Replace("MITP_","");

                                                if(utilidadesMIT.CompareVersions(versionPP, versionXML) >= 0)
                                                {
                                                    versionXML = utilidadesMIT.GetDataXML("version", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)).Trim();
                                                    versionXML = versionXML.Replace("MITP_", "");

                                                    if (utilidadesMIT.CompareVersions(versionPP, versionXML) < 0)
                                                    {
                                                        dbgIsUpgradeable = true;
                                                        return dbgIsUpgradeable;
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                versionXML = utilidadesMIT.GetDataXML("minVersion",utilidadesMIT.GetDataXML(Info.model, TRINP.xmlPinPad)).Trim();
                                                versionPP = Info.version.Trim();
                                                versionXML = versionXML.Substring(versionXML.Length - 5);
                                                versionPP = versionPP.Substring(versionPP.Length - 5);
                                            
                                            }

                                            if (!utilidadesMIT.IsNumber(Info.version.Trim().Replace("MITP_", "").Replace(".","").Replace(",","")))
                                                Info.version = "200";

                                            if (utilidadesMIT.CompareVersions(versionPP, versionXML) >= 0)
                                            {
                                                versionXML = utilidadesMIT.GetDataXML("version", utilidadesMIT.GetDataXML(Info.model, TRINP.xmlPinPad)).Trim();
                                                versionXML = versionXML.Substring(versionXML.Length - 5);

                                                if (utilidadesMIT.CompareVersions(versionPP, versionXML) < 0)
                                                {
                                                    dbgIsUpgradeable = true;
                                                    return dbgIsUpgradeable;
                                                }
                                                else
                                                    return dbgIsUpgradeable = false;

                                            }
                                            else
                                                return dbgIsUpgradeable = false;

                                        }

                                   }
                            }
                            else
                            {
                                dbgIsUpgradeable = false;
                                TRINP.isCargaVerifone = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MITLog.PrintLn("Centro de Pagos - Actualización firmware - EjecutaIsUpgradeable:" + ex.Message);
                TRINP.isCargaVerifone = false;
                dbgIsUpgradeable = false;
            }

            return dbgIsUpgradeable;
        }

        private void UpdatePinPad()
        {
            int tryIntentos;
            string tryUpdate;
            string batCarga;
            string line, line2;
            //string max;

            string pathExe = "";
            string timeWait = "ping 127.0.0.1 -n 50 > nul"; //45
            string timeWait2 = "ping 127.0.0.1 -n 60 > nul"; //55

            try
            {
                if (string.IsNullOrEmpty(TRINP.xmlPinPad))
                {
                    TRINP.contexto = "/pgs/jsp/cpagos/xmlPinPad.jsp";
                    TRINP.xmlPinPad = ws.SendWS(TypeUsuario.URL + "" + TRINP.contexto, "op=");
                }

                if (TRINP.xmlPinPad.Equals(""))
                    return;

                if (utilidadesMIT.ExisteCarpeta(Info.sPathCarpetaMIT + "\\Load"))
                    Directory.Delete(Info.sPathCarpetaMIT + "\\Load", true);

                //Crea la carpeta para guardar la información.
                if (!utilidadesMIT.ExisteCarpeta(Info.sPathCarpetaMIT + "\\Load"))
                    utilidadesMIT.CreaCarpeta(Info.sPathCarpetaMIT + "\\Load");

               pathExe = Directory.GetCurrentDirectory();
               if (!Directory.Exists(pathExe + "\\Load"))
                    Directory.CreateDirectory(pathExe + "\\Load");

                if (EjecutaIsUpgradeable())
                {
                    if (utilidadesMIT.ObtieneParametrosMIT("TryUpdate").Equals(""))
                        utilidadesMIT.GuardaParametrosMIT("TryUpdate", "1");

                    tryUpdate = utilidadesMIT.ObtieneParametrosMIT("TryUpdate");
                    int.TryParse(tryUpdate, out tryIntentos);
                    
                    if (TRINP.isCargaVerifone)
                        goto cargaVerifone;
                    else
                        goto iniciabloque;
                }
                else
                    return;
                
cargaVerifone:

                if (tryIntentos <= 3)
                {
                    int valorTemp;
                    string strTiempo = "5";

                    if (Info.model.Equals("VX520"))
                    {
                        strTiempo = "15";
                        timeWait = "ping 127.0.0.1 -n 46 > nul"; //41
                        timeWait2 = "ping 127.0.0.1 -n 54 > nul"; //49
                    }

                    string mensaje = "Existe una actualización del firmware para su terminal.\r\nEl proceso de actualización dura aproximadamente " + strTiempo + " minutos.\r\n¿Desea iniciar?";

                    if (MessageBox.Show(mensaje, "Centro de Pagos - Actualización firmware", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MessageBox.Show("Favor de no interrumpir la actualización.\r\nInterrumpir podría provocar daño permanente.", "Centro de Pagos - Actualización firmware", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        int.TryParse(Info.COM.Replace("COM",""),out valorTemp);
                        if(valorTemp > 9 )
                        {
                            MessageBox.Show("El proceso de actualización automatica no puede continuar\r\ndebido a que la terminal se encuentra conectada\r\nen un \"puerto COM\" superior al 9,\r\n para continuar con el proceso configure el \"puerto COM\"" + "\r\nal que está conectada su terminal entre el 2 y el 9.", "Centro de Pagos - Actualización firmware", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {

                            if (!TRINP.xmlPinPad.Equals(""))
                            {
                                TypeUsuario.isUpdate = false;
                                utilidadesMIT.GuardaParametrosMIT("Update", "1");
                                
                                //Actualiza la pinpad VX820
                                if(Info.model.ToUpper().Equals("VX820")
                                    || Info.model.ToUpper().Equals("VX520"))
                                {
                                    frmDownloadCarga frmDescarga = new frmDownloadCarga();
                                    frmDescarga.URLDownload = TypeUsuario.URL;
                                    frmDescarga.Path = utilidadesMIT.GetDataXML("path", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad));
                                    frmDescarga.model = Info.model.ToUpper();
                                    frmDescarga.PathDestino = pathExe + "\\Load\\" + utilidadesMIT.GetDataXML("name", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)); ;
                                    frmDescarga.ShowDialog();

                                    //verifica que se haya descargado el archivo
                                    if (!System.IO.File.Exists(pathExe + "\\Load\\" + utilidadesMIT.GetDataXML("name", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad))))
                                        goto Finaliza;

                                    utilidadesMIT.UnzipFile(pathExe + "\\Load\\" + utilidadesMIT.GetDataXML("name", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)), pathExe + "\\Load\\");

                                    batCarga = utilidadesMIT.GetDataXML("nameBatCarga", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad));
                                    line = utilidadesMIT.GetDataXML("line", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)) + "\r\n" +  timeWait + "\r\n" + batCarga;
                                    line2 = utilidadesMIT.GetDataXML("line2", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)) + "\r\n" + timeWait2;

                                    utilidadesMIT.CreateBat("ejecutaBAT.bat",
                                        "cd " + pathExe + "\\Load\\" + utilidadesMIT.GetDataXML("pathBat", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)),
                                        utilidadesMIT.GetDataXML("nameBat", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)),
                                        pathExe + "\\Load" + utilidadesMIT.GetDataXML("pathBat", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)));

                                    utilidadesMIT.CreateBat(utilidadesMIT.GetDataXML("nameBat", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)), line.Replace("-p*", "-p" + Info.COM.Replace("COM", "")), "", pathExe + "\\Load" + utilidadesMIT.GetDataXML("pathBat", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)));
                                    utilidadesMIT.CreateBat(batCarga, line2.Replace("-p*", "-p" + Info.COM.Replace("COM", "")), "", pathExe + "\\Load" + utilidadesMIT.GetDataXML("pathBat", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad)));
                                    //max = utilidadesMIT.GetDataXML("max", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad));

                                    this.dbgSetCmd84();

                                    string path = pathExe + "\\Load\\" + utilidadesMIT.GetDataXML("pathBat", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad));
                                    string bat = utilidadesMIT.GetDataXML("nameBat", utilidadesMIT.GetDataXML(Info.model.ToUpper(), TRINP.xmlPinPad));
                                    
                                    //frmActualizando frmActualiza = new frmActualizando(path, bat);
                                    frmActualizando frmActualiza = new frmActualizando(path, "ejecutaBAT.bat");
                                    frmActualiza.ShowDialog();

                                    goto Finaliza;
                                    

                                }
 
                            }
                            else
                            {
                                MessageBox.Show("No se pudo completar la actualización, intentelo mas tarde", "Centro de Pagos - Actualización firmware", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (utilidadesMIT.ExisteCarpeta(Info.PathExe + "\\Load"))
                                    Directory.Delete(Info.PathExe + "\\Load", true);

                                return;
                            }

                        Finaliza:

                            bc.setCom("");
                            Info.COM = "";
                            bc.setfindDevice(false);
                            ejecutaSetReader();

                            if( TypeUsuario.isUpdate)
                            {
                                MessageBox.Show("Terminal actualizada correctamente.", "Centro de Pagos - Actualización firmware", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                utilidadesMIT.GuardaParametrosMIT("Update", "");
                                utilidadesMIT.GuardaParametrosMIT("TryUpdate", "1");
                            }
                            else
                            {
                                if(tryIntentos == 0)
                                    tryIntentos = 1;

                                MessageBox.Show("La terminal no pudo ser actualizada." + "\r\nIntente nuevamente." + "\r\nIntento # " + tryIntentos.ToString(), "Centro de Pagos - Actualización firmware", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tryIntentos += 1;
                                utilidadesMIT.GuardaParametrosMIT("TryUpdate",tryIntentos.ToString());
                            }
                        }

                    }
                    else
                        TypeUsuario.isUpdate = true;
                }
                else
                {
                    if(utilidadesMIT.ObtieneParametrosMIT("ShwMsgPinPad").Equals(""))
                        utilidadesMIT.GuardaParametrosMIT("ShwMsgPinPad", "0");

                    if(utilidadesMIT.ObtieneParametrosMIT("ShwMsgPinPad").Equals("0"))
                    {
                        frmAvisoPinPad aviso = new frmAvisoPinPad();
                        aviso.ShowDialog();
                    }
                }

                if (utilidadesMIT.ExisteCarpeta(Info.sPathCarpetaMIT + "\\Load"))
                    Directory.Delete(Info.sPathCarpetaMIT + "\\Load", true);

                if (utilidadesMIT.ExisteCarpeta(Info.PathExe + "\\Load"))
                    Directory.Delete(Info.PathExe + "\\Load", true);
                
                return;

iniciabloque:

                return;

            }
            catch (Exception ex)
            {
                if (utilidadesMIT.ExisteCarpeta(Info.PathExe + "\\Load"))
                    Directory.Delete(Info.PathExe + "\\Load", true);

                MITLog.PrintLn("Centro de Pagos - Actualización firmware - EjecutaIsUpgradeable:" + ex.Message);
            }

       }


        #endregion


        private void dbgSetCmd84()
        {
            mp.SendC84();
        }


    }
}

