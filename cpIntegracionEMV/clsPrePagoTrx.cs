using cpIntegracionEMV.data;
using cpIntegracionEMV.txn_flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpIntegracionEMV
{
    public class clsPrePagoTrx
    {
        EjecutaOperacion eo = new EjecutaOperacion();
        private string UrlPP;
        private string Trx_Amount;

        public clsPrePagoTrx()
        { }


        public void dbgSetUrl(string URL)
        {
            UrlPP = URL + "/prepago/webservices/PrepagoServiceImplPort";
        }

        public string dbgGetUrl()
        {
            return UrlPP;
        }

        public void setTrxAmount(string value)
        {
            Trx_Amount = value;
        }

        public string dbgGetVersion()
        {
            return Info.dll_version;
        }

        //public Boolean dbgCancelOperation()
        //{
        //    return eo.CancelOperation();
        //}

        //public bool dbgActivaLector()
        //{
        //    return eo.StartTxEMV(TRINP.Tx_Amount);
        //}




    }
}
