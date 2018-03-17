using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpIntegracionEMV.txn_flow
{
    public static class Transaction
    {
        public const byte VTADIRECTA                = 0x01;
        public const byte VTAMOTO                   = 0x02;
        public const byte VTAFRZDMOTO               = 0x03;
        public const byte VTAAVSMOTO                = 0x04;
        public const byte INITDUKPT                 = 0x05;
        public const byte CANCELACION               = 0x06;
        public const byte REVERSO                   = 0x07;
        public const byte RECOMPENSAS               = 0x08;
        public const byte PRINTRECOMP               = 0x09;
        public const byte REIMPRESION               = 0x0A;
        public const byte CHECKIN                   = 0x0B;
        public const byte CHECKINMOTO               = 0x0C;
        public const byte VTAMOTO3DS                = 0x0D;
        public const byte CHECKOUT                  = 0x0E;
        public const byte CIERREPREVENTAMOTO        = 0x0F;
        public const byte CORTERECOMPENSAS          = 0x10;
        public const byte REIMPRESION_RECOMPENSAS   = 0x11;
        public const byte PREVENTAPROPINAEMV        = 0x12;
        public const byte PREVENTAEMV               = 0x13;
        public const byte TOKEN                     = 0x14;
        public const byte REAUTORIZACION_MOTO       = 0x15;
        public const byte VTA_BOLETOSEMV            = 0x16;
        public const byte VTA_BOLETOSMOTO           = 0x17;
        public const byte ALTA_CUPONES              = 0x18;
        public const byte BUSCAR_CUPONES            = 0x19;
        public const byte REDIMIR_CUPONES           = 0x1A;
    }
}
