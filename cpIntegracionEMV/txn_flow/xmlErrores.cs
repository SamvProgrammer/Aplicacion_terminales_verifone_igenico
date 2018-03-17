using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpIntegracionEMV.txn_flow
{
    public static class xmlErrores
    {

        #region  Constantes Errores

        public const string xmlConectionError = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/> <auth/><cd_response/><cd_error>19</cd_error><nb_error>Error de conexión, verifique su reporte.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/> <tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlApplicationError = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/> <auth/><cd_response/><cd_error>#ERROR</cd_error><nb_error>$ERROR</nb_error><nb_company/><nb_merchant/> <nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlMailVacio = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/> <auth/><cd_response/><cd_error></cd_error><nb_error>Correo electrónico vacío.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/> <tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlAmexError = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/> <auth/><cd_response/><cd_error>15</cd_error><nb_error>El campo cvv es incorrecto. </nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/> <tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlReversoTrue = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>17</cd_error><nb_error>Transacción declinada por el PinPad</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlReversoFalse = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos></foliocpagos><auth>925860</auth><cd_response></cd_response><cd_error>18</cd_error><nb_error>Sin comunicación, por favor verifique su reporte</nb_error><voucher></voucher></CENTEROFPAYMENTS>";
        //Errores del PinPad
        public const string xmlPinPadError01 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE01</cd_error><nb_error>Tarjeta Ilegible.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError02 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE02</cd_error><nb_error>Importe Incorrecto.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError03 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE03</cd_error><nb_error>No hay respuesta del lector, verifique que se encuentra conectado.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError04 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE04</cd_error><nb_error>No hay planes de pago para esta tarjeta, por favor cambie la tarjeta. </nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/> <tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError10 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE10</cd_error><nb_error>Operación cancelada. </nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/> <tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError11 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE11</cd_error><nb_error>Proceso cancelado por timeout. </nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/> <tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError12 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE12</cd_error><nb_error>Lectura errónea de banda/chip.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError13 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE13</cd_error><nb_error>Carga de llave fallida.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError14 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE14</cd_error><nb_error>Error de lectura de PIN.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError15 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE15</cd_error><nb_error>Tarjeta Vencida.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError16 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE16</cd_error><nb_error>Problemas al leer el chip.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError17 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE17</cd_error><nb_error>Impresora sin Papel.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError21 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE21</cd_error><nb_error>Información no almacenada correctamente.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";
        public const string xmlPinPadError22 = "<?xml version=1.0 encoding=UTF-8 standalone=yes?> <CENTEROFPAYMENTS><reference></reference><response>error</response><foliocpagos/><cd_response/><cd_error>PPE22</cd_error><nb_error>Tarjeta bloqueada.</nb_error><nb_company/><nb_merchant/><nb_street/><cc_type/><tp_operation/><amount/><voucher/></CENTEROFPAYMENTS>";

        #endregion

    }
}
