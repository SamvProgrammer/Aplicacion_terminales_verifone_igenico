using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cpIntegracionEMV.com;
using cpIntegracionEMV.data;

namespace cpIntegracionEMV.security
{
    public class crypto
    {
        WS ws = new WS();
        public String getRSA()
        {
            //TypeUsuario.ipKeyWeb = "https://dev10.mitec.com.mx";
            TypeUsuario.publicKeyRSA = ws.HTTP_GET(TypeUsuario.ipKeyWeb + "/keyWeb", "");
            if (TypeUsuario.publicKeyRSA.Contains("HTTP_ERROR") ||
                TypeUsuario.publicKeyRSA.Contains("Service Temporarily Unavailable") ||
                TypeUsuario.publicKeyRSA.Contains("HTTP Status 404")
                )
            {
                TypeUsuario.publicKeyRSA = "";
                return "false";
            }
            return "true";
        }
    }
}
