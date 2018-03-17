using cpIntegracionEMV.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using cpIntegracionEMV.UI;
using cpIntegracionEMV.txn_flow;
using cpIntegracionEMV.util;

namespace cpIntegracionEMV
{
    public class clsServicios
    {
        EjecutaOperacion eo = new EjecutaOperacion();

        public clsServicios()
        { }

        public void dbgSetUrl(string URL)
        {
            TRINP.url = URL;
        }

        public string dbgGetUrl()
        {
            return TRINP.url;
        }

        public String getRspOperationNumber()
        {
            return TRRSP.foliocpagos;
        }

        public String getRspAuth()
        {
            return TRRSP.auth;
        }

        public void setTrxAmount(string value)
        {
            TRINP.Tx_Amount = value;
        }

        public bool dbgActivaLector()
        {
            return eo.StartTxEMV(TRINP.Tx_Amount);
        }

        public string chkCc_Number()
        {
            return TRINP.panmask;
        }
        public string chkCc_Name()
        {
            return TRINP.CHName;
        }
        public string chkCc_ExpMonth()
        {
            return TRINP.expmonth;
        }
        public string chkCc_ExpYear()
        {
            return TRINP.expyear;
        }

        public bool sndVtaTiempoAire(string Bs_User,
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
            return eo.ejecutasndVtaTiempoAire(Bs_User, Bs_Pwd, Bs_Company, Bs_Branch, Bs_Country, Tx_OperationType, Tx_Currency, Cc_Type,
                                          Ta_NumTel, Ta_ConfNumTel, Ta_IdProveedor, Ta_IdCategoria, Ta_IdProducto, Ta_IsEfectivo, csvAmexenBanda);
        }

        ////public string sndVentaServicios(string Bs_User,
        ////                        string Bs_Pwd,
        ////                        string Bs_Company,
        ////                        string Bs_Branch,
        ////                        string Bs_Country,
        ////                        string Tx_OperationType,
        ////                        string Tx_Currency,
        ////                        string Cc_Type,
        ////                        string Vs_IdProveedor,
        ////                        string Vs_IdCategoria,
        ////                        string Vs_IdProducto)
        ////{
        ////    //pendiente
        ////    return "";
        ////}

        public string getRspDsResponse()
        {
            return TRRSP.response;
        }
        
        public string getRspXML()
        {
            return TRRSP.xml;
        }

        public String getRspVoucherCliente()
        {
            return TRRSP.voucher_cliente;
        }
        public String getRspVoucherComercio()
        {
            return TRRSP.voucher_comercio;
        }

        public string getRspVoucher()
        {
            return TRRSP.voucher;
        }

        public String getTx_Reference()
        {
            return TRRSP.reference;
        }


        public String getRspCdResponse()
        {
            return TRRSP.cd_response;
        }

        public String getRspCdError()
        {
            return TRRSP.cd_error;
        }

        public String getRspDsError()
        {
            return TRRSP.nb_error;
        }

        public string getRspTime()
        {
            return TRRSP.time;
        }

        public string getRspDate()
        {
            return TRRSP.date;
        }

        //sndVtaMOTOWebServices
        //sndVtaWebServicesTAE
        //sndVtaWebServices
        //sndGetEmpresasTGate
        //sndVentaTGate
        //dbgSetCard
        //sndComision3Gate
        //dbgCancelOperation
        //dbgPrintVoucher
        //dbgGetDisplay
        //dbgSetActivateReverse
        
       

        public void dbgGetProductos()
        {
            frmVtaSrvSeleccionar frmVta = new frmVtaSrvSeleccionar();
            frmVta.ShowDialog();
        }

        public string dbgGetIdCategoria()
        {
            return TRINP.TAEidCategoria; 
        }

        public string dbgGetIdProducto()
        {
            return TRINP.TAEidProducto;
        }

        public string dbgGetIdProveedor()
        {
            return TRINP.TAEidProveedor;
        }

        public string dbgGetAmount()
        {
            return TRINP.TAEAmount;
        }

        public bool dbgSetReader()
        {
            MITLog.PrintLn("*** dbgSetReader(), Deprecated!!! -> No need to call...");
            return true;
        }

        public Boolean dbgCancelOperation()
        {
            return eo.CancelOperation();
        }

        public void dbgPrintVoucher(String voucher)
        {
            eo.printvoucher(voucher);
        }

        public string getRspTicket()
        {
            return TRRSP.reciboTAE;
        }
        


    }
}
