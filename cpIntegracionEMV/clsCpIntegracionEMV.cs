using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using cpIntegracionEMV.txn_flow;
using cpIntegracionEMV.data;
using cpIntegracionEMV.security;
using cpIntegracionEMV.util;
using cpIntegracionEMV.UI;

namespace cpIntegracionEMV
{
    public class clsCpIntegracionEMV
    {
        EjecutaOperacion eo = new EjecutaOperacion();
        crypto cr = new crypto();
        Clear clear = new Clear();

        /******* Get Properties. *********/
        public String dbgGetXMLUser()
        {
            //PENDIENTE
            //VALIDAR QUE VALOR DEBE REGRESARS
            return TypeUsuario.CadenaXML;
        }
        public void dbgSetUrl (String URL)
        {
            TypeUsuario.URL = URL;
        }
        public String dbgGetUrl()
        {
            return TypeUsuario.URL;
        }
        public void dbgSetUrlIpKeyWeb(String URL)
        {
            TypeUsuario.ipKeyWeb = URL;
        }
        public void dbgSetUrlInstalador(String URL)
        {
            TypeUsuario.URL_Instalador = URL;
        }
        public String ObtieneLlavePublicaRSA()
        {
            eo.getRSA();
            return TypeUsuario.publicKeyRSA;
        }

        //ESTABLECE LA LLAVE PUBLICA RSA
        public bool SetLlavePublicaRSA(string Llave)
        {
            //ceriroji
            return false;
        }

        public String getRespPublicKeyRSA()
        {
            return TypeUsuario.publicKeyRSA;
        }
        public String getErrorPublicKeyRSA()
        {
            return TypeUsuario.rspError;
        }
        public String dbgGetId_Company()
        {
            return TypeUsuario.Id_Company;
        }
        public String dbgGetNb_Company()
        {
            return TypeUsuario.nb_company;
        }
        public String dbgGetNb_User()
        {
            return TypeUsuario.nb_user;
        }
        public String dbgGetNb_companystreet()
        {
            return TypeUsuario.nb_companystreet;
        }
        public String dbgGetId_Branch()
        {
            return TypeUsuario.Id_Branch;
        }
        public String dbgGetNb_Branch()
        {
            return TypeUsuario.nb_branch;
        }
        public String dbgGetCountry()
        {
            return TypeUsuario.country;
        }
        public String dbgGetUser()
        {
            return TypeUsuario.User;
        }
        public String dbgGetGiro()
        {
            return TypeUsuario.giro;
        }
        public Boolean dbgGetIsConsumo()
        {
            return TypeUsuario.consumo;
        }
        public String dbgGetPass()
        {
            return TypeUsuario.Pass;
        }
        public String dbgGetResProductos()
        {
            return TypeUsuario.RESPRODUCTOS;
        }
        public Boolean dbgGetEncuesta()
        {
            return TypeUsuario.encuesta;
        }
        public Boolean dbgGetPoints2()
        {
            return TypeUsuario.points2;
        }
        public Boolean dbgGetFacturaE()
        {
            return TypeUsuario.facturaE;
        }
        public String dbgGetRspError()
        {
            return TypeUsuario.rspError;
        }
        public String dbgGetpagomVMC()
        {
            return TypeUsuario.pagomVMC;
        }
        public String dbgGetpagomAMEX()
        {
            return TypeUsuario.pagoomAMEX;
        }
        public String dbgGetpagobVMC()
        {
            return TypeUsuario.pagobVMC;
        }
        public String dbgGetpagobAMEX()
        {
            return TypeUsuario.pagobAMEX;
        }
        public String dbgGetpagobSIP()
        {
            return TypeUsuario.pagobSIP;
        }
        public String dbgGetpagoavsVMC()
        {
            return TypeUsuario.pagoavsVMC;
        }
        public String dbgGetpagoavsAMEX()
        {
            return TypeUsuario.pagoavsAMEX;
        }
        public String dbgGetpagoomVMC()
        {
            return TypeUsuario.pagomVMC;
        }
        public String dbgGetpagoomAMEX()
        {
            return TypeUsuario.pagoomAMEX;
        }
        public String dbgGetpagovtaforzadaVMC()
        {
            return TypeUsuario.pagovtaforzadaVMC;
        }
        public String dbgGetpagovtaforzadaAMEX()
        {
            return TypeUsuario.pagovtaforzadaAMEX;
        }
        public Boolean dbgIsUpdate()
        {
            return TypeUsuario.isUpdate;
        }
        public String chkPp_CdError()
        {
            return TRINP.chkPp_CdError;
        }
        public String chkCc_Number()
        {
            return TRINP.panmask;
        }
        public String chkCc_Name()
        {
            return TRINP.CHName;         
        }
        public String chkCc_ExpMonth()
        {
            return TRINP.expmonth;
        }
        public String chkCc_ExpYear()
        {
            return TRINP.expyear;
        }
        /********* Function's *********/
        //login
        public bool dbgLoginUser(String strUsr, String strPwd)
        {            
            return eo.pcpayLogin8(strUsr, strPwd);
        }
        //CMD Venta
        public bool dbgStartTxEMV(String Tx_Amount)
        {
            return eo.StartTxEMV(Tx_Amount);
        }
        //Venta XML
        public Boolean sndVtaDirectaEMV( String Bs_User,
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
                                        [Optional]String Cc_AMEXCvvCsc
                                      )
        {
            if (Cc_AMEXCvvCsc == null) 
            {
                Cc_AMEXCvvCsc = "";
            }
            return eo.ejecutaVentadirecta(  Bs_User, 
                                            Bs_Pwd, 
                                            Bs_UsrTransaction, 
                                            Bs_Company, 
                                            Bs_Branch, 
                                            Bs_Country, 
                                            Cc_Type, 
                                            Tx_Merchant, 
                                            Tx_Reference, 
                                            Tx_OperationType, 
                                            Tx_Currency, 
                                            Cc_AMEXCvvCsc);
        }
        //Cancelación
        public Boolean sndCancelacion( String Bs_User,
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

            return eo.ejecutaCancelacion(   Bs_User,
                                            Bs_Pwd,
                                            Bs_UsrTransaction,
                                            Bs_Company,
                                            Bs_Branch,
                                            Bs_Country,
                                            Tx_Amount,
                                            Tx_OperationNumber,
                                            Tx_Auth);
        }
        //Venta Recompensas
        public Boolean sndVtaRecompensasDirecto(String Bs_User,
                                                String Bs_Pwd,
                                                String Bs_Company,
                                                String Bs_Branch,
                                                String Bs_Country,
                                                String Tx_Reference,
                                                String Tx_Amount,
                                                String tp_operation)
        {
            return eo.ejecutaVtaRecompensas(Bs_User,
                                            Bs_Pwd,
                                            Bs_Company,
                                            Bs_Branch,
                                            Bs_Country,
                                            Tx_Reference,
                                            Tx_Amount,
                                            tp_operation);
        }
        public Boolean sndVtaMOTO(String Bs_User,
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
            return eo.ejecutaVentaMOTO(Bs_User,
                                         Bs_Pwd,
                                         Bs_UsrTransaction,
                                         Bs_Company,
                                         Bs_Branch,
                                         Bs_Country,
                                         Tx_Merchant,
                                         Tx_Reference,
                                         Tx_OperationType,
                                         Tx_Amount,
                                         Tx_Currency,
                                         Cc_Type,
                                         Cc_Name,
                                         Cc_Number,
                                         Cc_ExpMonth,
                                         Cc_ExpYear,
                                         Cc_CvvCsc);
        }
        //Venta Forzada
        public Boolean sndVtaFzadaMOTO(String Bs_User,
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
            return eo.ejecutaVentaForzadaMOTO(Bs_User,
                                         Bs_Pwd,
                                         Bs_UsrTransaction,
                                         Bs_Company,
                                         Bs_Branch,
                                         Bs_Country,
                                         Tx_Merchant,
                                         Tx_Reference,
                                         Tx_OperationType,
                                         Tx_Amount,
                                         Tx_Currency,
                                         Tx_Auth,
                                         Cc_Type,
                                         Cc_Name,
                                         Cc_Number,
                                         Cc_ExpMonth,
                                         Cc_ExpYear,
                                         Cc_CvvCsc,
                                         Tx_boleto,
                                         fh_salida,
                                         fh_retorno);
        }
        //Venta AVS MOTO
        public Boolean sndVtaAvsMOTO(String Bs_User,
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
            return eo.ejecutaVentaAvsMOTO(Bs_User,
                                         Bs_Pwd,
                                         Bs_UsrTransaction,
                                         Bs_Company,
                                         Bs_Branch,
                                         Bs_Country,
                                         Tx_Merchant,
                                         Tx_Reference,
                                         Tx_OperationType,
                                         Tx_Amount,
                                         Tx_Currency,
                                         Cc_Type,
                                         Cc_Name,
                                         Cc_Number,
                                         Cc_ExpMonth,
                                         Cc_ExpYear,
                                         Cc_CvvCsc,
                                         Avs_Address,
                                         Avs_Municipality,
                                         Avs_City,
                                         Avs_State,
                                         Avs_zip,
                                         Avs_District);
        }
        
        //Hotel
        public Boolean sndCheckInEMV(String Bs_User,
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
                                     [Optional]String Cc_AMEXCvvCsc
            )
        {
            return eo.ejecutaCheckInEMV(Bs_User,
                                        Bs_Pwd,
                                        Bs_UsrTransaction,
                                        Bs_Company,
                                        Bs_Branch,
                                        Bs_Country,
                                        Cc_Type,
                                        Tx_Merchant,
                                        Tx_Reference,
                                        Tx_OperationType,
                                        Tx_Currency,
                                        Tx_Room,
                                        Cc_AMEXCvvCsc);
        }
        public Boolean sndCheckOutBoletosMOTO(String Bs_User,
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
            return eo.ejecutaCheckOutBoletosMOTO(Bs_User,
                                      Bs_Pwd,
                                      Bs_UsrTransaction,
                                      Bs_Company,
                                      Bs_Branch,
                                      Bs_Country,
                                      Tx_Amount,
                                      Tx_OperationNumber,
                                      Tx_boleto,
                                      fh_salida,
                                      fh_retorno);
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                                  sndCheckInMOTO()                             **
        //**                                                                               **
        //**  Descripción    : Envia el check in a Centro de Pagos                         **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public Boolean sndCheckInMOTO(string Bs_User,
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
            return eo.ejecutaCheckInMoto(Bs_User,
                                           Bs_Pwd,
                                           Bs_UsrTransaction,
                                           Bs_Company,
                                           Bs_Branch,
                                           Bs_Country,
                                           Tx_Merchant,
                                           Tx_Reference,
                                           Tx_OperationType,
                                           Tx_Amount,
                                           Tx_Currency,
                                           Tx_Room,
                                           Cc_Type,
                                           Cc_Name,
                                           Cc_Number,
                                           Cc_ExpMonth,
                                           Cc_ExpYear,
                                           Cc_CvvCsc);
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                                  sndVtaMOTO3DS()                              **
        //**                                                                               **
        //**  Descripción    : Envia la venta MOTO con datos de 3Ds a Centro de Pagos      **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public Boolean sndVtaMOTO3DS(string Bs_User,
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
            return eo.ejecutaVtaMOTO3DS(Bs_User,
                                         Bs_Pwd,
                                         Bs_UsrTransaction,
                                         Bs_Company,
                                         Bs_Branch,
                                         Bs_Country,
                                         Tx_Merchant,
                                         Tx_Reference,
                                         Tx_OperationType,
                                         Tx_Amount,
                                         Tx_Currency,
                                         Cc_Type,
                                         Cc_Name,
                                         Cc_Number,
                                         Cc_ExpMonth,
                                         Cc_ExpYear,
                                         Cc_CvvCsc,
                                         Tx_cavv,
                                         Tx_eci,
                                         Tx_xid);
        }

        //Se agrega para la nueva consulta de afiliaciones de MCI. PcPay 7.2.1
        public String dbgGetMerchantBanda([Optional]String tp_canal)
        {
            return eo.GetMerchant("Tarjeta",tp_canal);
        }
        //Get merchant Moto
        public String dbgGetMerchantMoto(String numberTDC,
                                         [Optional]String tp_canal)
        {
            return eo.GetMerchantMoto("MOTO",numberTDC, tp_canal);
        }

        //*********************************Merchant AVS MCI*****************************************
        public string dbgGetMerchantAvs(string numberTDC)
        {
            return eo.GetMerchantMoto("AVS",numberTDC, "17");
        }

        //*********************************Merchant OpManual MCI*****************************************
        public string dbgGetMerchantOpManual(string numberTDC)
        {
            return eo.GetMerchantMoto("MANUAL", numberTDC, "14");
        }

        //*********************************Merchant VtaForzada MCI*****************************************
        public string dbgGetMerchantVtaForzada(string numberTDC)
        {
            return eo.GetMerchantMoto("VFORZADA", numberTDC, "18");
        }

        //*********************************Merchant BANDA RP3 MCI*****************************************
        public string dbgGetMerchantBandaRP3(string strOriginadora)
        {
            return eo.GetMerchant("BandaRP3", "8", strOriginadora);
        }

        //*********************************Merchant MOTORP3 MCI*****************************************
        public string dbgGetMerchantMOTORP3(string numberTDC, string strOriginadora)
        {
            return eo.GetMerchantMoto("BandaMotoRP3", numberTDC, "15", strOriginadora);
        }

        public void dbgEndOperation()
        {
            MITLog.PrintLn("*** dbgEndOperation(), Deprecated!!! -> No need to call...");
        }
        public Boolean dbgGetisAmex()
        {
            return TRINP.isAMEX;
        }

        public String GetTipoMoneda()
        {
            return TRINP.Tx_Currency;
        }
        public void dbgSetCurrencyType(String CurrencyType)
        {
            if (CurrencyType.Equals("MXN"))
            {
                TRINP.Tx_CurrencyCode = "0484";
            }
            else if(CurrencyType.Equals("USD"))
            {
                TRINP.Tx_CurrencyCode = "0840";
            }
            else
            {
                TRINP.Tx_Currency = "0" + CurrencyType;
            }
        }
        public String getRspDsResponse()
        {
            return TRRSP.response;
        }
        public String getRspAuth()
        {
            return TRRSP.auth;
        }
        public String getRspVoucher()
        {
            return TRRSP.voucher;
        }
        public void dbgPrintVoucher(String voucher)
        {
            eo.printvoucher(voucher);
        }
        public String getRspCdResponse()
        {
            return TRRSP.cd_response;
        }
        public String getRspFriendlyResponse()
        {
            return TRRSP.friendly_response;
        }
        public String getRspDsError()
        {
            return TRRSP.nb_error;
        }
        public void dbgSetHidePopUpMerchant(Boolean value)
        {
            TRINP.HidePopUpMerchant = value;
        }
        //Enable/Disable currency popup
        public void dbgHidePopUp(Boolean value)
        {
            TRINP.HidePopUpCurrency = value;
        }
        //Initialize Dukpt
        public String dbgInitDUKPT()
        {
            return ""+eo.dukptInit();
        }
        //dbgSetDUKPT - Deprecated
        public Boolean dbgSetDUKPT(String KSN, String KCV, String IPEK)
        {
            MITLog.PrintLn("*** dbgSetDUKPT(String KSN, String KCV, String IPEK), Deprecated!!! -> Call dbgSetDUKPT()");
            return eo.setDUKPT(KSN, KCV, IPEK);
        }
        public Boolean dbgSetDUKPT()
        {
            return eo.setDUKPT(Info.ksn, Info.kcv, Info.ipek);
        }
        public Boolean dbgSetReader()
        {
            return eo.ejecutaSetReader();
        }
        public void dbgSetActivateReverse(String value)
        {
            if (value.Trim().Equals("1"))
            {
                TypeUsuario.emvReverso = true;
            }
            else
            {
                TypeUsuario.emvReverso = false;
            }
        }
        public String dbgGetActivateReverse()
        {
            if(TypeUsuario.emvReverso)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public void dbgSetTimeOut(String timeout)
        {
            Info.timeout = timeout;
        }

        public void dbgEnabledLog(bool IsEnabled)
        {
            MITLog.PrintLn("*** dbgEnabledLog, Deprecated!!!");
        }
        

        //Recompensas
        public String getRspCd_StatusRecom()
        {
            return TRRSP.cd_status;
        }
        public String getRspNb_StatusRecom()
        {
            return TRRSP.nb_status;
        }
        public String getRspSaldoRecom()
        {
            return TRRSP.saldo;
        }

        /// <summary>
        /// Obtiene el voucher recompensas
        /// </summary>
        /// <param name="Bs_Company"></param>
        /// <param name="Bs_Branch"></param>
        /// <param name="Bs_Country"></param>
        /// <param name="Bs_User"></param>
        /// <param name="Bs_Pwd"></param>
        /// <param name="opcionImpresion"></param>
        /// <param name="Tx_OperationNumber"></param>
        /// <returns></returns>
        public String GetVoucherRecompensas(String Bs_Company,
                                            String Bs_Branch,
                                            String Bs_Country,
                                            String Bs_User,
                                            String Bs_Pwd,
                                            String opcionImpresion,
                                            String Tx_OperationNumber)
        {
            return eo.GetVoucherRecompensas(Bs_Company,
                                            Bs_Branch,
                                            Bs_Country,
                                            Bs_User,
                                            Bs_Pwd,
                                            opcionImpresion,
                                            Tx_OperationNumber);
        }
        //Firma en Panel
        public bool EsTouch()
        {
            return ClsFirmaPanel.EsTouch();
        }
        public void ObtieneFirmaPanel(String textoMarcaAgua)
        {
            ClsFirmaPanel.ObtieneFirmaPanel(textoMarcaAgua);
        }
        public String Error()
        {
            return ClsFirmaPanel.Error;
        }
        public String TextoHEXFirmaPanel()
        {
            return ClsFirmaPanel.TextoHEXFirmaPanel;
        }
        //Envia comando de firma en panel.
        public Boolean sndGuardaFirmaPanel(String strHexFirmaPanel, 
                                           String urlFirma, 
                                           String OperationNumber, 
                                           String Pp_Serial)
        {

            return eo.GuardaFirmaPanel(strHexFirmaPanel,
                                       urlFirma,
                                       OperationNumber,
                                       Pp_Serial);
        }
        public Boolean sndEnviaMailFirmaPanel(String strMail,
                                                String OperationNumber,
                                                String Id_Company,
                                                String Id_Branch,
                                                String country,
                                                String User,
                                                String Pass,
                                                String urlFirma)
        {
            return eo.EnviaMailFirmaPanel(strMail,
                                          OperationNumber,
                                          Id_Company,
                                          Id_Branch,
                                          country,
                                          User,
                                          Pass,
                                          urlFirma);
        }
        public Boolean getRspSoportaFirmaPanel()
        {
            if(Info.PanelSign.Trim().Equals("1"))
            {
                return true;
            }
            return false;
        }
        public string sndObtieneFirmaPanel(Boolean esTouch,
                                            String textoAgua,
                                            String RspVoucher,
                                            Boolean IsChipAndPin,
                                            String MarcaTerminal,
                                            int tipoVtaDirecta,
                                            Boolean esQPS)
        {
            return eo.ObtieneFirmaPanel(esTouch,
                                        textoAgua,
                                        RspVoucher,
                                        IsChipAndPin,
                                        MarcaTerminal,
                                        tipoVtaDirecta,
                                        esQPS);
        }

        public String getRspOperationNumber()
        {
            return TRRSP.foliocpagos;
        }
        public String getTx_Reference()
        {
            return TRRSP.reference;
        }
        public String getRspArqc()
        {
            return TRRSP.arqc;
        }
        public String getRspAppid()
        {
            return TRRSP.appid;
        }
        public String getRspAppidlabel()
        {
            return TRRSP.appidlabel;
        }
        public String getRspVoucherCliente()
        {
            return TRRSP.voucher_cliente;
        }
        public String getRspVoucherComercio()
        {
            return TRRSP.voucher_comercio;
        }
        public String getRspFeTxLeyenda()
        {
            return TRRSP.Fe_txLeyenda;
        }

         public String getRspFeDsResponse()
         {
             return TRRSP.Fe_nbResponse;
         }

        public string getRspFeCdResponse()
        {
            return TRRSP.Fe_cdResponse;
        }

        public void dbgSetTrxData(String Bs_User,
                                    String Bs_Pwd,
                                    String Bs_UsrTransaction,
                                    String Bs_Company,
                                    String Bs_Branch,
                                    String Bs_Country)
        {
            MITLog.PrintLn("*** dbgSetTrxData(), Deprecated!!! -> No need to call...");
        }
        public String dbgGetDisplay()
        {
            //esta funcion debe retornar lo que haya en el pinpad
            //MITLog.PrintLn("*** dbgGetDisplay(), Deprecated!!! -> No need to call...");
            return TRINP.P81MSG;
        }

        public Boolean chkPp_soportaDUKPT()
        {
            if(string.IsNullOrEmpty(Info.Dukpt))
                return false;

            if (Info.Dukpt.Trim().Equals("1"))
                return true;
            else
                return false;
        }

        public Boolean chkPp_llaveDUKPT()
        {
            if (string.IsNullOrEmpty(Info.DukptKey))
                return false;

            if (Info.DukptKey.Trim().Equals("1"))
                return true;
            else
                return false;
        }

        public String getRspXML()
        {
            return TRRSP.xml;
        }
        public String getTx_Amount()
        {
            return TRRSP.amount;
        }

        public string getRspTime()
        {
            return TRRSP.time;
        }

        public String getRspDate()
        {
            return TRRSP.date;
        }
        public String getRspDsMerchant()
        {
            return TRRSP.nb_merchant;
        }
        public String chkCc_IsPin()
        {
            return TRINP.pin;
        }
        public String getRspCdError()
        {
            return TRRSP.cd_error;
        }

        public string getCc_Type()
        {
            return TRRSP.cc_type;
        }

        public string  getCc_TypeTRX()
        {
            return TRRSP.cc_typeTemp;
        }

        public Boolean dbgCancelOperation()
        {
            return eo.CancelOperation();
        }
        public void dbgIsTerminalServerImpl(String value)
        {
            Info.IsTerminal = value;
        }
        public string chkPp_EMVFull()
        {
            return Info.EMVFULL;
        }

        public string chkPp_Model()
        {
            return Info.model;
        }


        public string chkPp_Serial()
        {
            return Info.SerialNumber;
        }

        public string chkPp_Trademark()
        {
            return Info.marca;
        }

        public string chkPp_Version()
        {
            return Info.version;
        }


        public string chkPp_DsError()
        {
            return Info.ErrorPP;
        }

        public string chkPp_Printer()
        {
            return Info.Printer;
        }

        public void dbgSetActivateMagtek(bool ActivateMagtek)
        {
            Info.useMagtek = ActivateMagtek;
        }

        public string dbgGetVersion()
        {
            return Info.dll_version;
        }
        
        //********************************************************************************************
        //ACTIVA LOS CUPONES MIT
        public bool dbgGetActivaCupones()
        {
            return TypeUsuario.PayNoPain;
        }

        //********************************************************************************************
        //ACTIVA RECOMPENSASS
        public bool isRecompensas()
        {
            return TypeUsuario.isRecompensas;
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                       AplicaCobroRecompensas()                                **
        //**                                                                               **
        //**  Descripción    : Valida si puede cobrar con puntos Recompensa                **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public bool  AplicaCobroRecompensas(string binTarjeta)
        {
            return eo.AplicaCobroRecompensas(binTarjeta);
        }

        /// <summary>
        /// Obtiene el Puerto COM, donde estableció la comunicación el lector
        /// </summary>
        /// <returns></returns>
        public string chkPp_Com()
        {
            return Info.COM;
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                              sndReimpresion()                                 **
        //**                                                                               **
        //**  Descripción    : Envia la reimpresión a Centro de Pagos                      **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************

        public string sndReimpresion(string Bs_User,
                                      string Bs_Pwd,
                                      string Bs_Company,
                                      string Bs_Branch,
                                      string Bs_Country,
                                      string Tx_OperationNumber)
        {

            return eo.ejecutaReimpresion( Bs_User, Bs_Pwd, Bs_Company, Bs_Branch, Bs_Country, Tx_OperationNumber);
        }
       
        //***********************************************************************************
        //***********************************************************************************
        //**                                   sndConsulta                                 **
        //**                                                                               **
        //**  Descripción    : Envia la consulta de trx con datos a Centro de Pagos        **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public string sndConsulta(string Bs_User,
                                    string Bs_Pwd,
                                    string Bs_Company,
                                    string Bs_Branch,
                                    string Tx_Date,
                                    string Tx_Reference)
        {

            return eo.ejecutaConsulta(Bs_User, Bs_Pwd,  Bs_Company, Bs_Branch, Tx_Date, Tx_Reference);
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                           sndReautorizacionMOTO()                             **
        //**                                                                               **
        //**  Descripción    : Envia la reautorización a Centro de Pagos                   **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public void sndReautorizacionMOTO(string Bs_User,
                                string Bs_Pwd,
                                string Bs_UsrTransaction,
                                string Bs_Company,
                                string Bs_Branch,
                                string Bs_Country,
                                string Tx_Amount,
                                string Tx_OperationNumber)
        {
            eo.ejecutaReautorizacionMOTO(Bs_User, Bs_Pwd, Bs_UsrTransaction, Bs_Company, Bs_Branch, Bs_Country, Tx_Amount, Tx_OperationNumber);
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                                  sndCheckOutMOTO()                            **
        //**                                                                               **
        //**  Descripción    : Envia el check out a Centro de Pagos                        **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public void sndCheckOutMOTO(string Bs_User,
                              string Bs_Pwd,
                              string Bs_UsrTransaction,
                              string Bs_Company,
                              string Bs_Branch,
                              string Bs_Country,
                              string Tx_Amount,
                              string Tx_OperationNumber)
        {
            eo.ejecutaCheckout(Bs_User, Bs_Pwd, Bs_UsrTransaction, Bs_Company, Bs_Branch, Bs_Country, Tx_Amount, Tx_OperationNumber);
        }


        public void dbgPrint(string inf)
        {
            eo.Print(inf);
        }

        public void dbgSetLogo(string Logo)
        {
            //"ceriroji";
        }

        /// <summary>
        /// Manda un mensaje al display del lector
        /// </summary>
        /// <param name="message"></param>
        public void dbgSendMessage(string message)
        {
            eo.EjecutaSendMessage(message);
        }


        /// <summary>
        /// Obtiene Reporte por Usuario
        /// </summary>
        /// <returns></returns>
        public bool ObtieneReporteUsuario(string Bs_User,
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
            return eo.ObtieneReporte(Bs_User, Bs_Branch, Bs_Company, tipoVenta, tipoCorte, opcion, cdGiro, app, etiqueta, version);
        }

        /// <summary>
        /// Obtiene reporte por sucursal
        /// </summary>
        /// <returns></returns>
        public bool ObtieneReporteSucursal(string Bs_User,
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
            return eo.ObtieneReporte(Bs_User, Bs_Branch, Bs_Company, tipoVenta, tipoCorte, opcion, cdGiro, app, etiqueta, version);
        }

        /// <summary>
        /// Obtiene corte usuario
        /// </summary>
        /// <returns></returns>
        public bool ObtieneCorteUsuario(string Bs_User,
                                        string opcion
                                        )
        {
            return eo.ObtieneCorteUsuario(Bs_User, opcion);
        }

        public bool isAgencias()
        {
            if (string.IsNullOrEmpty(TypeUsuario.isAgencia))
                return false;

            if (TypeUsuario.isAgencia.Equals("1"))
                return true;
            else
                return false;
            
        }

        /// <summary>
        /// Obtiene la etiqueta que se pinta como Referencia
        /// </summary>
        /// <returns></returns>
        public String ObtieneEtiquetaReferencia()
        {
            return TypeUsuario.etiquetaReference;
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                            sndPreventaPropinaEMV                              **
        //**                                                                               **
        //**  Descripción: Envia la preventa con propina (consumo) banda a Centro de Pagos **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public void sndPreventaPropinaEMV(string Bs_User,
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
            eo.ejecutaPreventaPropinaEMV( Bs_User, Bs_Pwd, Bs_UsrTransaction, Bs_Company, Bs_Branch, Bs_Country, Cc_Type, Tx_Merchant, Tx_Reference,
                                          Tx_OperationType, Tx_Currency, Tx_Waiter, Tx_Shifts, Tx_Propina, Cc_AMEXCvvCsc, Tx_Table);
        
        }

        //'***********************************************************************************
        //***********************************************************************************
        //**                              sndPreventaEMV                                   **
        //**                                                                               **
        //**  Descripción    : Envia la preventa banda a Centro de Pagos                   **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public void sndPreventaEMV(string Bs_User,
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
            eo.ejecutaPreventaEMV(Bs_User, Bs_Pwd, Bs_UsrTransaction, Bs_Company, Bs_Branch, Bs_Country, Cc_Type, Tx_Merchant, Tx_Reference,
                                    Tx_OperationType, Tx_Currency, Tx_Waiter, Tx_Shifts, Cc_AMEXCvvCsc, Tx_Table);
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                                 dbgTxMonitor()                                **
        //**                                                                               **
        //**  Descripción    : Inicia el time out para vigilar el envío de una transaccion **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public bool dbgTxMonitor()
        {
            //ceriroji;
            //PENDIENTE
            return true;
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                         sndReimpresionRecompensas()                           **
        //**                                                                               **
        //**  Descripción    : Envia la reimpresion de Recompensas                         **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public string sndReimpresionRecompensas(string Bs_User,
                                    string Bs_Pwd,
                                    string Bs_Company,
                                    string Bs_Branch,
                                    string Bs_Country,
                                    string Tx_OperationNumber)
        {
            return eo.ejecutaReimpresionRecompensas(Bs_User, Bs_Pwd, Bs_Company, Bs_Branch, Bs_Country, Tx_OperationNumber);
        }

        //***********************************************************************************
        //***********************************************************************************
        //***  CORTE RECOMPENSAS DIRECTO
        //***********************************************************************************
        //***********************************************************************************
        public string GetCorteRecompensas(string Bs_Company,
                                 string Bs_Branch,
                                 string Bs_Country,
                                 string Bs_User,
                                 string Bs_Pwd,
                                 string tipoReporte,
                                 string tipoTicket)

        {
            return eo.ejecutaCorteRecompensas(Bs_Company,
                                            Bs_Branch,
                                            Bs_Country,
                                            Bs_User,
                                            Bs_Pwd,
                                            tipoReporte,
                                            tipoTicket);
        }

        //**********************************************************************************************
        //IMPRIME EL CORTE IMPRESO DE RECOMPENSAS SANTANDER
        public void dbgImprimeCorteRecom(string voucher)
        {
            eo.ejecutaImprimeCorteRecom(voucher);
        }

        //***********************************************************************************
        //**                                dbgSetCmd62                                    **
        //**      PEDIR NUMERO  DE TARJETA DE CREDITO/ DEBITO CIFRADA POR RSA              **
        //***********************************************************************************

        public string dbgSetCmd62(string mensaje, string longitud)
        {
            return eo.getCardRSA(mensaje, longitud);

        }

        //***********************************************************************************
        //***********************************************************************************
        //**                        sndObtieneToken()                                      **
        //**                                                                               **
        //**  Descripción    : Obtiene el token de una TDC                                 **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public string sndObtieneToken(string Bs_Company,
                                 string Bs_Branch,
                                 string Bs_User,
                                 string Bs_Pwd,
                                 string numTDC,
                                 string Referencia)
        {
            return eo.getToken(Bs_Company,
                               Bs_Branch,
                               Bs_User,
                               Bs_Pwd,
                               numTDC,
                               Referencia);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ConsultaPreventa(string Bs_User,
                                        string referencia,
                                        string tipoOpcion)
        {
            return eo.ejecutaConsultaPreventa(Bs_User, referencia, tipoOpcion);
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                           sndCierrePreventaMOTO()                             **
        //**                                                                               **
        //**  Descripción    : Envia el cierre de prevena a Centro de Pagos                **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public bool sndCierrePreventaMOTO(string Bs_User,
                                        string Bs_Pwd,
                                        string Bs_UsrTransaction,
                                        string Bs_Company,
                                        string Bs_Branch,
                                        string Bs_Country,
                                        string Tx_Amount,
                                        string Tx_Tip,
                                        string Tx_OperationNumber)
        {
            return eo.ejecutaCierrePreventaMOTO(Bs_User, Bs_Pwd, Bs_UsrTransaction, Bs_Company, Bs_Branch, Bs_Country, Tx_Amount, Tx_Tip, Tx_OperationNumber);
        }

        //***********************************************************************************
        //***********
        //***********    ACTIVA LAS OPCIONES DE TOKEN SI EL USUARIO CUENTA
        //***********    CON ESTA OPCIÓN
        //***********
        //***********************************************************************************
        public bool ActivaMenuToken()
        {
            return eo.ejecutaActivaMenuToken();
        }
        /// Convierte una cadena Hex en Base64
        public string ConvierteHEX_To_BASE64(string inputHex)
        {
            return EncryptC.ConvierteHEX_To_BASE64(inputHex);

        }


        //********************************************************************
        //SE OBTIENE UN ID PARA LA FORMA DE PAGO, FACTURA ELECTRONICA
        public string ObtieneIDFacturaElectronica(string FormaPago)
        {
            return eo.ejecutaObtieneIDFacturaElectronica(FormaPago);
        }

        //**********************************************************************************************
        //INDICA SI SE DEBE IMPRIMIR O NO EL VOUCHER
        public string dbgGetEsImprimibleVoucher()
        {
            return TRRSP.esImprimibleVoucher;
        }

        public void dbgSetTipoPago(int setTipoPago)
        {
            switch(setTipoPago)
            {
                case 1:
                    TRINP.tipoPago = "tipopagoom";
                    break;
                case 2:
                    TRINP.tipoPago = "tipopagoavs";
                    break;
                case 3:
                    TRINP.tipoPago = "tipopagovtaforzada";
                    break;
                default:
                    TRINP.tipoPago = "";
                    break;
            }

        }

        //***********************************************************************************
        //***********************************************************************************
        //**                          ObtieneFormMail()                                    **
        //**                                                                               **
        //**  Descripción    : llama el formulario del Mail                                **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public string ObtieneFormMail(string strTipoRespuesta, string strMail)
        {
            return eo.ejecutaObtieneFormMail(strTipoRespuesta, strMail);
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                              sndFacturaElectronica                            **
        //**                                                                               **
        //**  Descripción    : Envia la Factura Electronica a Centro de Pagos              **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public string sndFacturaElectronica(string Bs_User,
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
            return eo.ejecutaFacturaElectronica(Bs_User, Bs_Company, Bs_Branch, Tx_Amount, Tx_Tip, Tx_Ticket, Tx_OperationNumber, Tx_Date, Tx_IdTpOperation, Tx_TpOperation, Tx_Concept, Tx_DigitosTarjeta);
        }


        //***********************************************************************************
        //***********************************************************************************
        //**                         sndFacturaElectronicaDatos                            **
        //**                                                                               **
        //**  Descripción    : Envia la Factura Electronica con datos a Centro de Pagos    **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public string sndFacturaElectronicaDatos(string Bs_User,
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
             return eo.EjecutaFacturaElectronicaDatos(Bs_User,
                                         fechaYHora, rfcEmisor, sucursal, industria, codLook, idMetodoPago, metodoPago,
                                         importeTotal, noTicket, rfcReceptor, idTransaccion, email, Propina, Concepto,
                                         digitosTarjeta, idMoneda, tipoCambio, tipoCFDI, observaciones,
                                         nombre, codigoPostal, noInterior, pais, estado, localidad, colonia, noExterior, municipio, calle);
         }

        //***********************************************************************************
        //***********************************************************************************
        //**                              sndFirmaEnPanel()                                **
        //**                                                                               **
        //**  Descripción    : Emvia el comando a Firmar en Panel                          **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public bool sndFirmaEnPanel(bool esTouch,
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
            return eo.ejecutaFirmaEnPanel(esTouch, strHex, urlFirma, textoAgua, OperationNumber, Pp_Serial, Id_Company, Id_Branch, country, User, Pass,
                                        RspVoucher, IsChipAndPin, MarcaTerminal, tipoVta);
        }

        //Actualiza las PinPad
        public bool dbgUpdatePinPad()
        {
            return eo.EjecutaUpdatePinPad();
        }

        //Verifica si la pinpad es actualizable
        public bool dbgIsUpgradeable()
        {
            return eo.EjecutaIsUpgradeable();
        }

        //Se crea función para poder actualizar los parámetros EMV de la terminal
        public void dbgEjecutaInfoEMV(bool forceUpdate)
        {
            if (forceUpdate)
            {
                eo.updateEMVParams();
            }
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                                dbgSetCmd61                                    **
        //**                                                                               **
        //**  Descripción: Obtiene la Firma en Panel                                       **
        //**                                                                               **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public string dbgSetCmd61()
        {
            return eo.ejecutadbgSetCmd61();
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                   sndReenviaVoucherFirmaEnPanel()                             **
        //**                                                                               **
        //**  Descripción    : Envia Mail con el voucher Firmado                           **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public bool sndReenviaVoucherFirmaEnPanel(string urlFirma,
                                            string Id_Company,
                                            string Id_Branch,
                                            string country,
                                            string User,
                                            string Pass,
                                            string strFolio)
        {
            return eo.ejecutaReenviaVoucherFirmaPanel(urlFirma, Id_Company, Id_Branch, country, User, Pass, strFolio);
        }

        /// <summary>
        /// Venta con autenticacion
        /// </summary>
        public bool cpIntegracion_cpAVSs2(string Id_Company, 
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
            return eo.EjecutaCPIntegracion_cpAVSs2(Id_Company, Id_Branch, country, User, Pwd,
                                     merchant, reference, tp_operation, typeC, nameC, numberC, expmonthC, expyearC, cvvcscC,
                                     Amount, currencyC, direccion, NumInt, NumExt, delegacion, ciudad, Estado,
                                     cp, colonia, nombreC, PaisC, TelefonoC, CorreoC, Tx_isCheckin,
                                     Tx_boletos, Tx_fechaSalida, Tx_fechaRetorno);
        }

        //*****************************************************************************************
        //Travel
        public void dbgSetIsAgencia(string IsAgencia)
        {
            TypeUsuario.isAgencia = IsAgencia;
            TypeUsuario.dbgGetIsAgencia = IsAgencia;
        }

        public void dbgSetTipoRP3(bool setTipoRP3, string numEmpresa)
        {
            TRINP.isEmpRp3 = setTipoRP3;
            TRINP.numEmpresaRp3 = numEmpresa;
        }

        //***********************************************************************************
        //**                              sndVtaBoletosEMV()                               **
        //**                                                                               **
        //**  Descripción    : Envia la venta banda a Centro de Pagos                      **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public void sndVtaBoletosEMV(string Bs_User,
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
            eo.ejecutaVtaBoletosEMV(Bs_User, Bs_Pwd, Bs_UsrTransaction, Bs_Company, Bs_Branch, Bs_Country, Cc_Type, Tx_Merchant, Tx_Reference,
                                 Tx_OperationType, Tx_Currency, Tx_boleto, fh_salida, fh_retorno, Cc_AMEXCvvCsc);
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                              sndVtaBoletosMOTO()                              **
        //**                                                                               **
        //**  Descripción    : Envia la venta MOTO con boletos a Centro de Pagos           **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public void sndVtaBoletosMOTO(string Bs_User,
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
            eo.ejecutaVtaBoletosMOTO(Bs_User, Bs_Pwd, Bs_UsrTransaction, Bs_Company, Bs_Branch, Bs_Country, Tx_Merchant, Tx_Reference,
                                    Tx_OperationType, Tx_Amount, Tx_Currency, Cc_Type, Cc_Name, Cc_Number, Cc_ExpMonth, Cc_ExpYear, Cc_CvvCsc,
                                    Tx_boleto,  fh_salida, fh_retorno);
        }

        //**********************************************************************************
        //**********************************************************************************
        //**                        sndPayNoPayAltaCliente()                               **
        //**                                                                               **
        //**  Descripción    : Da de alta a un cliente en PayNoPay                         **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public string  sndPayNoPayAltaCliente(string Bs_Company,
                                 string Bs_Branch,
                                 string Bs_Country,
                                 string Bs_User,
                                 string Bs_Pwd,
                                 string phone)
        {
            return eo.ejecutaPayNoPayAltaCliente(Bs_Company, Bs_Branch, Bs_Country, Bs_User, Bs_Pwd, phone);
        }

        //***********************************************************************************
        //***********************************************************************************
        //**                        sndPayNoPayBusquedaCupon()                             **
        //**                                                                               **
        //**  Descripción    : Busca Cupones en PayNoPay                                   **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public string sndPayNoPayBusquedaCupon(string Bs_Company,
                                 string Bs_Branch,
                                 string Bs_Country,
                                 string Bs_User,
                                 string Bs_Pwd,
                                 string searchValue)
        {
            return eo.ejecutaPayNoPayBusquedaCupon(Bs_Company, Bs_Branch, Bs_Country, Bs_User, Bs_Pwd, searchValue);
        }

        //***********************************************************************************
        //**                        sndPayNoPayRedimirCupon()                              **
        //**                                                                               **
        //**  Descripción    : Redime Cupon en PayNoPay                                    **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public string sndPayNoPayRedimirCupon(string Bs_Company,
                                 string Bs_Branch,
                                 string Bs_Country,
                                 string Bs_User,
                                 string Bs_Pwd,
                                 string value)
         {
            return eo.ejecutaPayNoPayRedimirCupon(Bs_Company, Bs_Branch, Bs_Country, Bs_User, Bs_Pwd, value);
         }

        /// <summary>
        /// Muestra el form que contiene los cupones
        /// </summary>
        /// <param name="xml"></param>
        public void ObtieneCupones(string xml)
        { 
            ClsFirmaPanel.ObtieneCupones(xml);
        }

        //Codigo del cupon seleccionado
        public int CodigoCupon()
        {
            return ClsFirmaPanel.CodigoCupon;
        }

        //Almacena la respuesta
        public string RespuestaFormCupon()
        {
            return ClsFirmaPanel.RespuestaFormCupon;
        }
        //Limpiar variables utilizadas.
        public void dbgClearDLL()
        {
            clear.ClearTXN();
        }
        
        public String dbgGetPlazoMCI()
        {
            return TRINP.plazoMCI;
        }

        #region QUALITAS

        //***********************************************************************************
        //**                          dbgGetIsUserQualitas()                               **
        //**                                                                               **
        //**  Descripción: Setea el valor para saber si es un usuario de Qualitas          **
        //**                                                                               **
        //***********************************************************************************
        public void dbgGetIsUserQualitas(bool IsUserQualitas)
        {
            TRINP_Qualitas.isQualitas = IsUserQualitas;
        }

        //***********************************************************************************
        //**                          dbgSetQualitasActivaMSI()                            **
        //**                                                                               **
        //**  Descripción: Activa los MSI en la opción de otros                            **
        //**                                                                               **
        //***********************************************************************************
        public void dbgSetQualitasActivaMSI(bool ActivaMSI)
        {
            TRINP_Qualitas.ActivaMSI = ActivaMSI;
        }

        //***********************************************************************************
        //**                          dbgSetQualitasTipoPagosContado()                     **
        //**                                                                               **
        //**  Descripción: Setea el valor para el Pago de contado                          **
        //**                                                                               **
        //***********************************************************************************
        public void dbgSetQualitasTipoPagosContado(string tipoPago)
        {
            TRINP_Qualitas.TipoPagosContado = tipoPago;
        }

        //***********************************************************************************
        //**                          dbgSetQualitasTipoPagosMSI()                         **
        //**                                                                               **
        //**  Descripción: Setea el valor para el pago a Meses                             **
        //**                                                                               **
        //***********************************************************************************
        public void dbgSetQualitasTipoPagosMSI(string tipoPago)
        {
            TRINP_Qualitas.TipoPagosMSI = tipoPago;
        }

        //***********************************************************************************
        //**                          dbgSetQualitasPlanPagosMSI()                         **
        //**                                                                               **
        //**  Descripción: Setea el plan de Pagos para meses sin intereses                 **
        //**                                                                               **
        //***********************************************************************************
        public void dbgSetQualitasPlanPagosMSI(string planPagos)
        {
            TRINP_Qualitas.TipoPagosMSIPlan = planPagos;
        }

        //***********************************************************************************
        //**                          dbgSetQualitasMoneda()                               **
        //**                                                                               **
        //**  Descripción: Setea el valor para la moneda                                   **
        //**                                                                               **
        //***********************************************************************************
        public void dbgSetQualitasMoneda(string Moneda)
        {
            TRINP_Qualitas.PolizaMoneda = Moneda;
        }

        //***********************************************************************************
        //**                  dbgGetQualitasFinanciamiento()                               **
        //**                                                                               **
        //**  Descripción: Obtiene el financiamiento de qualitas                           **
        //**                                                                               **
        //***********************************************************************************
        //***********************************************************************************
        public string dbgGetQualitasFinanciamiento()
        {
            return TRINP_Qualitas.Financiamiento;
        }

        //***********************************************************************************
        //**                  dbgGetQualitasTipoFinanciamiento()                            **
        //**                                                                               **
        //**  Descripción: Obtiene el financiamiento de qualitas                           **
        //**                                                                               **
        //***********************************************************************************
        public string dbgGetQualitasTipoFinanciamiento()
        {
            return TRINP_Qualitas.Tipofinanciamiento;
        }

        //***********************************************************************************
        //**                          GetMonedaQualitas()                                  **
        //**                                                                               **
        //**  Descripción: Ontiene la moneda para qualitas                                 **
        //**                                                                               **
        //***********************************************************************************
        public string GetMonedaQualitas()
        {
            return TRINP_Qualitas.Moneda;
        }

        //***********************************************************************************
        //**                          dbgSetQualitasTipocobro()                            **
        //**                                                                               **
        //**  Descripción: Setea el Tipo de Cobro                                          **
        //**                                                                               **
        //***********************************************************************************
        public void dbgSetQualitasTipocobro(string tipocobro)
        {
            TRINP_Qualitas.tipocobro = tipocobro;
        }

        //***********************************************************************************
        // Devuelve el tipo de cobro que se realiza, contado. MSI
        //***********************************************************************************
        public string strTipoCobroQualitas()
        {
            return TRINP_Qualitas.strTipoCobroQualitas;
        }
        
        //***********************************************************************************
        //  devuelve la descripción de la afilicación
        //***********************************************************************************
        public string strAfiliacionQualitas()
        {
            return TRINP_Qualitas.strAfiliacionQualitas;
        }

        #endregion


        //***********************************************************************************
        //**                              CapacidadTouch()                                 **
        //**                                                                               **
        //**  Descripción    : configura touch                                             **
        //**                                                                               **
        //***********************************************************************************
        public void CapacidadTouch()
        {
            frmCapacidadTouch form = new frmCapacidadTouch();
            form.ShowDialog();
        }

        //***********************************************************************************
        //**                              ConversorDCC()                                   **
        //**                                                                               **
        //**  Descripción    : Muestra el conversor de monedas                             **
        //**                                                                               **
        //***********************************************************************************
        public void ConversorDCC()
        {
            frmConversorDCC form = new frmConversorDCC();
            form.ShowDialog();
        }

        private void dbgSetUser(string User)
        {
            TypeUsuario.User = User;
        }



    }
}
