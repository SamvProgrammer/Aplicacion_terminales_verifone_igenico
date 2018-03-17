using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cpIntegracionEMV.data;
using cpIntegracionEMV.util;

namespace cpIntegracionEMV.txn_flow
{

    public class bitmapXML
    {
        //Number of available transactions
        const int LenBM             = 25; //TXN'S
        //Number of bytes per transaction
        const int LenByteTxn        = 12;

        //Transaction Defines.

        //Byte 0 - Mensaje Type
        
        //Byte 1 - Business
        const byte business         = 0x01;
        const byte id_company       = 0x02;
        const byte id_branch        = 0x04;
        const byte country          = 0x08;
        const byte user             = 0x10;
        const byte pwd              = 0x20;

        //Byte 2 - Transaction
        const byte transaction      = 0x01;
        const byte merchant         = 0x02;
        const byte reference        = 0x04;
        const byte tp_operation     = 0x08;
        const byte creditcard       = 0x10; // -> B5
        const byte amount           = 0x20;
        const byte currency         = 0x40;
        const byte usrtransaction   = 0x80;

        //Byte 3 - Transaction
        const byte emv              = 0x01;
        const byte version          = 0x02;
        const byte serie            = 0x04;
        const byte version_terminal = 0x08;
        const byte modelo_terminal  = 0x10;
        const byte terminal         = 0x20; // -> B8
        const byte tp_resp          = 0x40;
        const byte no_operacion     = 0x80;

        //Byte 4 - Transaction -> Byte 11
        const byte auth             = 0x01;
        const byte opcion           = 0x02;
        const byte copia            = 0x04;
        const byte foliocpagos      = 0x08;
        const byte canal            = 0x10;
        const byte agencia          = 0x20;
        const byte tx_room          = 0x40;
        const byte propina          = 0x80;

        //Byte 5 - Credit Card
        const byte crypto           = 0x01;
        const byte type             = 0x02;
        const byte tracks           = 0x04;
        const byte chip             = 0x08;
        const byte tags             = 0x10;
        const byte chipname         = 0x20;
        const byte chipnameenc      = 0x40;
        const byte pin              = 0x80;

        //Byte 6 - Credit Card
        const byte posentrymode     = 0x01;
        const byte arqc             = 0x02;
        const byte appid            = 0x04;
        const byte appidlabel       = 0x08;
        const byte name             = 0x10;
        const byte number           = 0x20;
        const byte expmonth         = 0x40;
        const byte expyear          = 0x80;

        //Byte 7 -Credit Card
        const byte cvvcsc           = 0x01;
        const byte cc_number        = 0x02;
        const byte dukpt            = 0x04;
        const byte contactless      = 0x08;

        //Byte 8 -Terminal
        const byte printer          = 0x01;
        const byte display          = 0x02;
        const byte is_mobile        = 0x04;
        const byte is_contactless   = 0x08;

        //Byte 9 -Terminal
        const byte nb_marca_terminal    = 0x01;
        const byte nb_modelo_terminal   = 0x02;
        const byte nb_serie_lector      = 0x04;
        const byte nb_version_terminal  = 0x08;
        const byte nb_tk                = 0x10;
        const byte nb_kcv               = 0x20;

        //Byte 10 - DCC / DUKPT / AVS
        const byte dcc              = 0x01;
        const byte dcc_status       = 0x02;
        const byte mci              = 0x04;
        const byte plazomci         = 0x08;
        const byte tp_dukpt         = 0x10;
        const byte nb_ksn           = 0x20;
        const byte nb_data          = 0x40;
        const byte avs              = 0x80;

        //Byte 11 - Transaction
        const byte t3ds             = 0x01;
        const byte tipoReporte      = 0x02; //Para el corte de recompensas
        const byte tipoTicket       = 0x04; //Para el corte de recompensas
        const byte consumo          = 0x08;
        const byte cupones          = 0x10;  //cupones

        //Bit Map XML
        byte[,] bitmap = new byte[LenBM, LenByteTxn] 
        {
            { 
                Transaction.VTADIRECTA,                                                                                 //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + merchant + reference + tp_operation + creditcard + amount + currency + usrtransaction,    //Byte 2
                emv + version + serie + version_terminal + modelo_terminal + terminal + tp_resp,                        //Byte 3
                0x00,                                                                                                   //Byte 4
                crypto + type + tracks + chip + tags + chipname + chipnameenc + pin,                                    //Byte 5
                posentrymode + arqc + appid + appidlabel,                                                               //Byte 6
                cvvcsc + dukpt + contactless,                                                                           //Byte 7
                printer + display + is_mobile + is_contactless,                                                         //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status + mci + plazomci + tp_dukpt + nb_ksn + nb_data,                                        //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.VTAMOTO,                                                                                    //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + merchant + reference + tp_operation + creditcard + amount + currency + usrtransaction,    //Byte 2
                version,                                                                                                //Byte 3
                0x00,                                                                                                   //Byte 4
                crypto + type,                                                                                          //Byte 5
                name + number + expmonth + expyear,                                                                     //Byte 6
                cvvcsc,                                                                                                 //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status + mci + plazomci,                                                                      //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.VTAFRZDMOTO,                                                                                //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + merchant + reference + tp_operation + creditcard + amount + currency + usrtransaction,    //Byte 2
                version,                                                                                                //Byte 3
                auth + agencia,                                                                                         //Byte 4
                crypto + type,                                                                                          //Byte 5
                name + number + expmonth + expyear,                                                                     //Byte 6
                cvvcsc,                                                                                                 //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status + mci + plazomci,                                                                      //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.VTAAVSMOTO,                                                                                 //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + merchant + reference + tp_operation + creditcard + amount + currency + usrtransaction,    //Byte 2
                version,                                                                                                //Byte 3
                0x00,                                                                                                   //Byte 4
                crypto + type,                                                                                          //Byte 5
                name + number + expmonth + expyear,                                                                     //Byte 6
                cvvcsc,                                                                                                 //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                mci + plazomci + avs,                                                                                   //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.INITDUKPT,                                                                                  //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction,                                                                                            //Byte 2
                terminal,                                                                                               //Byte 3
                0x00,                                                                                                   //Byte 4
                0x00,                                                                                                   //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                nb_marca_terminal + nb_modelo_terminal + nb_serie_lector + nb_version_terminal + nb_tk + nb_kcv,        //Byte 9
                0x00,                                                                                                    //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.CANCELACION,                                                                                //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + amount + usrtransaction,                                                                  //Byte 2
                version + no_operacion,                                                                                 //Byte 3
                auth,                                                                                                   //Byte 4
                crypto,                                                                                                 //Byte 5
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.REVERSO,                                                                                    //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + amount,                                                                                   //Byte 2
                version + no_operacion,                                                                                 //Byte 3
                auth,                                                                                                   //Byte 4
                crypto,                                                                                                 //Byte 5
                0x00,
                0x00,
                0x00,
                0x00,
                0x00,
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.RECOMPENSAS,                                                                                //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + reference + tp_operation + creditcard + amount + currency,                                //Byte 2
                version + serie + version_terminal + modelo_terminal,                                                   //Byte 3
                0x00,                                                                                                   //Byte 4
                crypto,                                                                                                 //Byte 5
                number,                                                                                                 //Byte 6
                cc_number + dukpt,                                                                                      //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                tp_dukpt + nb_ksn + nb_data,                                                                            //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.PRINTRECOMP,                                                                                //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction,                                                                                            //Byte 2
                0x00,                                                                                                   //Byte 3
                opcion + copia + foliocpagos + canal,                                                                   //Byte 4
                0x00,                                                                                                   //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                0x00,                                                                                                   //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.REIMPRESION,                                                                                //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                0x00,                                                                                                   //Byte 2
                no_operacion,                                                                                           //Byte 3
                0x00,                                                                                                   //Byte 4
                crypto,                                                                                                 //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                0x00,                                                                                                   //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.CHECKIN,                                                                                    //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + merchant + reference + tp_operation + creditcard + amount + currency + usrtransaction,    //Byte 2
                emv + version + serie + version_terminal + modelo_terminal + terminal,                                  //Byte 3
                tx_room,                                                                                                //Byte 4
                crypto + type + tracks + chip + tags + chipname + chipnameenc + pin,                                    //Byte 5
                posentrymode + arqc + appid + appidlabel,                                                               //Byte 6
                cvvcsc + dukpt + contactless,                                                                           //Byte 7
                printer + display + is_mobile + is_contactless,                                                         //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status + mci + plazomci + tp_dukpt + nb_ksn + nb_data,                                        //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.CHECKINMOTO,                                                                                //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + merchant + reference + tp_operation + creditcard + amount + currency + usrtransaction,    //Byte 2
                version,                                                                                                //Byte 3
                tx_room,                                                                                                //Byte 4
                crypto + type,                                                                                          //Byte 5
                name + number + expmonth + expyear,                                                                     //Byte 6
                cvvcsc,                                                                                                 //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status + mci + plazomci,                                                                      //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.CHECKOUT,                                                                                   //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + amount + usrtransaction,                                                                  //Byte 2
                version + no_operacion,                                                                                 //Byte 3
                0x00,                                                                                                   //Byte 4
                crypto,                                                                                                 //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status,                                                                                       //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.CIERREPREVENTAMOTO,                                                                         //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + amount,                                                                                   //Byte 2
                version + no_operacion,                                                                                 //Byte 3
                propina,                                                                                                //Byte 4
                crypto,                                                                                                 //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status,                                                                                       //Byte 12
                0x00                                                                                                   //Byte 11
            },
            { 
                Transaction.CORTERECOMPENSAS,                                                                           //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction,                                                                                            //Byte 2
                0x00,                                                                                                   //Byte 3
                opcion + canal,                                                                                         //Byte 4
                0x00,                                                                                                   //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                0x00,                                                                                                   //Byte 10
                tipoReporte + tipoTicket                                                                                //Byte 11
            },
            { 
                Transaction.REIMPRESION_RECOMPENSAS,                                                                    //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction,                                                                                            //Byte 2
                0x00,                                                                                                   //Byte 3
                foliocpagos,                                                                                            //Byte 4
                0x00,                                                                                                   //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                0x00,                                                                                                   //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.PREVENTAPROPINAEMV,                                                                         //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + merchant + reference + tp_operation + creditcard + amount + currency + usrtransaction,    //Byte 2
                emv + version + serie + version_terminal + modelo_terminal + terminal + tp_resp,                        //Byte 3
                propina,                                                                                                //Byte 4
                crypto + type + tracks + chip + tags + chipname + chipnameenc + pin,                                    //Byte 5
                posentrymode + arqc + appid + appidlabel,                                                               //Byte 6
                cvvcsc + dukpt + contactless,                                                                           //Byte 7
                printer + display + is_mobile + is_contactless,                                                         //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status + mci + plazomci + tp_dukpt + nb_ksn + nb_data,                                        //Byte 10
                consumo                                                                                                 //Byte 11
            },
            { 
                Transaction.PREVENTAEMV,                                                                                //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + merchant + reference + tp_operation + creditcard + amount + currency + usrtransaction,    //Byte 2
                emv + version + serie + version_terminal + modelo_terminal + terminal + tp_resp,                        //Byte 3
                0x00,                                                                                                   //Byte 4
                crypto + type + tracks + chip + tags + chipname + chipnameenc + pin,                                    //Byte 5
                posentrymode + arqc + appid + appidlabel,                                                               //Byte 6
                cvvcsc + dukpt + contactless,                                                                           //Byte 7
                printer + display + is_mobile + is_contactless,                                                         //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status + mci + plazomci + tp_dukpt + nb_ksn + nb_data,                                        //Byte 10
                consumo                                                                                                 //Byte 11
            },
            { 
                Transaction.TOKEN,                                                                                      //Byte 0
                business + id_company + id_branch + user + pwd,                                                         //Byte 1
                transaction + reference,                                                                                //Byte 2
                0x00,                                                                                                   //Byte 3
                0X00,                                                                                                   //Byte 4
                0x00,                                                                                                   //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                0x00,                                                                                                   //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.REAUTORIZACION_MOTO,                                                                        //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + amount + usrtransaction,                                                                  //Byte 2
                version + no_operacion,                                                                                 //Byte 3
                0x00,                                                                                                   //Byte 4
                crypto,                                                                                                 //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status,                                                                                       //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.VTA_BOLETOSEMV,                                                                             //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + merchant + reference + tp_operation + creditcard + amount + currency + usrtransaction,    //Byte 2
                emv + version + serie + version_terminal + modelo_terminal + terminal,                                  //Byte 3
                agencia,                                                                                                //Byte 4
                crypto + type + tracks + chip + tags + chipname + chipnameenc + pin,                                    //Byte 5
                posentrymode + arqc + appid + appidlabel,                                                               //Byte 6
                cvvcsc + dukpt + contactless,                                                                           //Byte 7
                printer + display + is_mobile + is_contactless,                                                         //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status + mci + plazomci + tp_dukpt + nb_ksn + nb_data,                                        //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.VTA_BOLETOSMOTO,                                                                            //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                transaction + merchant + reference + tp_operation + creditcard + amount + currency + usrtransaction,    //Byte 2
                version,                                                                                                //Byte 3
                agencia,                                                                                                //Byte 4
                crypto + type,                                                                                          //Byte 5
                name + number + expmonth + expyear,                                                                     //Byte 6
                cvvcsc,                                                                                                 //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                dcc + dcc_status + mci + plazomci,                                                                      //Byte 10
                0x00                                                                                                    //Byte 11
            },
            { 
                Transaction.ALTA_CUPONES,                                                                               //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                0x00,                                                                                                   //Byte 2
                0x00,                                                                                                   //Byte 3
                0x00,                                                                                                   //Byte 4
                0x00,                                                                                                   //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                0x00,                                                                                                   //Byte 10
                cupones                                                                                                 //Byte 11
            },
            { 
                Transaction.BUSCAR_CUPONES,                                                                             //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                0x00,                                                                                                   //Byte 2
                0x00,                                                                                                   //Byte 3
                0x00,                                                                                                   //Byte 4
                0x00,                                                                                                   //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                0x00,                                                                                                   //Byte 10
                cupones                                                                                                 //Byte 11
            },
            { 
                Transaction.REDIMIR_CUPONES,                                                                            //Byte 0
                business + id_company + id_branch + country + user + pwd,                                               //Byte 1
                0x00,                                                                                                   //Byte 2
                0x00,                                                                                                   //Byte 3
                0x00,                                                                                                   //Byte 4
                0x00,                                                                                                   //Byte 5
                0x00,                                                                                                   //Byte 6
                0x00,                                                                                                   //Byte 7
                0x00,                                                                                                   //Byte 8
                0x00,                                                                                                   //Byte 9
                0x00,                                                                                                   //Byte 10
                cupones                                                                                                 //Byte 11
            }

        };
        public String setTag(String Tag, String value)
        {
            return "<" + Tag + ">" + value + "</" + Tag + ">";
        }
        public String ArmaXML()
        {
            String XML = "";
            String TmpXML = "";
            String TmpCC = "";
            String TmpTerm = "";
            String TmpAgencia = "";
            String Tmpt3ds = "";
            String Tmpdukpt = "";
            Boolean findTxn = false;
            byte[] ByteTxn = new byte[LenByteTxn];
            int count = 0, idtxn = 0;

            //Find id position of transaction.
            for(count=0; count<LenBM; count++)
            {
                if (TRINP.TRX_TYPE == bitmap[count, 0])
                {
                    findTxn = true;
                    break;
                }
            }
            //Set id position of transaction.
            idtxn = count;
            try
            {
                //Fill Transaction bytes
                for (count=0; count<LenByteTxn; count++)
                {
                    ByteTxn[count] = bitmap[idtxn, count];
                }
            }
            catch (System.StackOverflowException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //If know transaction, Build XML.
            if (findTxn)
            {
                //Business Header
                if ((ByteTxn[1] & business) == business)
                {
                    if ((ByteTxn[1] & id_company) == id_company)
                    {
                        TmpXML = setTag("id_company", TRINP.id_company);
                    }
                    if ((ByteTxn[1] & id_branch) == id_branch)
                    {
                        TmpXML = TmpXML + setTag("id_branch", TRINP.id_branch);
                    }
                    if ((ByteTxn[1] & country) == country)
                    {
                        TmpXML = TmpXML + setTag("country", TRINP.Bs_Country);
                    }
                    if ((ByteTxn[1] & user) == user)
                    {
                        TmpXML = TmpXML + setTag("user", TRINP.Bs_User);
                    }
                    if ((ByteTxn[1] & pwd) == pwd)
                    {
                        TmpXML = TmpXML + setTag("pwd", TRINP.Bs_Pwd);
                    }
                    TmpXML = setTag("business", TmpXML);
                }
                XML = XML + TmpXML;
                TmpXML = "";
                
                //Transaction properties
                if ((ByteTxn[2] & transaction) == transaction)
                {
                    if ((ByteTxn[2] & merchant) == merchant)
                    {
                        TmpXML = setTag("merchant", TRINP.Tx_Merchant);
                    }

                    if ((ByteTxn[2] & reference) == reference)
                    {
                        TmpXML = TmpXML + setTag("reference", TRINP.Tx_Reference);
                    }

                    if ((ByteTxn[2] & tp_operation) == tp_operation)
                    {
                        TmpXML = TmpXML + setTag("tp_operation", TRINP.Tx_OperationType);
                    }

  
                    //Credit card properties.
                    if ((ByteTxn[2] & creditcard) == creditcard)
                    {
                        if ((ByteTxn[5] & crypto) == crypto)
                        {
                            TmpCC = setTag("crypto", TRINP.Crypto);
                        }
                        if ((ByteTxn[5] & type) == type)
                        {
                            TmpCC = TmpCC + setTag("type", TRINP.Cc_Type);
                        }
                        if ((ByteTxn[5] & tracks) == tracks)
                        {
                            if (Info.Dukpt == "1")
                            {
                                TmpCC = TmpCC + setTag("tracks", "");
                            }
                            else
                            {
                                TmpCC = TmpCC + setTag("tracks", TRINP.tracks);
                            }
                        }
                        if ((ByteTxn[5] & chip) == chip)
                        {
                            TmpCC = TmpCC + setTag("chip", TRINP.chip);
                        }
                        if ((ByteTxn[7] & cc_number) == cc_number)
                        {
                            TmpCC = TmpCC + setTag("cc_number", TRINP.cc_number);
                        }
                        //CHIP
                        if (!string.IsNullOrEmpty(TRINP.chip) && TRINP.chip.Equals("1"))
                        {
                            if ((ByteTxn[5] & tags) == tags)
                            {
                                TmpCC = TmpCC + setTag("tags", TRINP.tags);
                            }
                            if ((ByteTxn[5] & chipname) == chipname)
                            {
                                TmpCC = TmpCC + setTag("chipname", TRINP.CHName);
                            }
                            if ((ByteTxn[5] & chipnameenc) == chipnameenc)
                            {
                                TmpCC = TmpCC + setTag("chipnameenc", TRINP.chipnameenc);
                            }
                            if ((ByteTxn[5] & pin) == pin)
                            {
                                TmpCC = TmpCC + setTag("pin", TRINP.pin);
                            }
                            if ((ByteTxn[6] & posentrymode) == posentrymode)
                            {
                                TmpCC = TmpCC + setTag("posentrymode", TRINP.pose);
                            }
                            if ((ByteTxn[6] & arqc) == arqc)
                            {
                                TmpCC = TmpCC + setTag("arqc", TRINP.arqc);
                            }
                            if ((ByteTxn[6] & appid) == appid)
                            {
                                TmpCC = TmpCC + setTag("appid", TRINP.aid);
                            }
                            if ((ByteTxn[6] & appidlabel) == appidlabel)
                            {
                                TmpCC = TmpCC + setTag("appidlabel", TRINP.applabel);
                            }
                            if (Info.Dukpt =="1")
                            {
                                if ((ByteTxn[7] & dukpt) == dukpt)
                                {
                                    if ((ByteTxn[10] & tp_dukpt) == tp_dukpt)
                                    {
                                        Tmpdukpt = Tmpdukpt + setTag("tp_dukpt", Info.Dukpt.Trim());
                                    }
                                    if ((ByteTxn[10] & nb_ksn) == nb_ksn)
                                    {
                                        Tmpdukpt = Tmpdukpt + setTag("nb_ksn", TRINP.ksn);
                                    }
                                    if ((ByteTxn[10] & nb_data) == nb_data)
                                    {
                                        Tmpdukpt = Tmpdukpt + setTag("nb_data", TRINP.tracks);
                                    }
                                    TmpCC = TmpCC + setTag("dukpt", Tmpdukpt);
                                }
                            }
                            if ((ByteTxn[7] & contactless) == contactless)
                            {
                                TmpCC = TmpCC + setTag("contactless", TRINP.contactless);
                            }
                        }
                        else
                        {
                            if ((ByteTxn[6] & posentrymode) == posentrymode)
                            {
                                TmpCC = TmpCC + setTag("posentrymode", TRINP.pose);
                            }
                            
                            //MOTO Transaction.
                            if ((ByteTxn[6] & name) == name)
                            {
                                TmpCC = TmpCC + setTag("name", TRINP.CHName);
                            }
                            if ((ByteTxn[6] & number) == number)
                            {
                                TmpCC = TmpCC + setTag("number", TRINP.tracks);
                            }
                            if ((ByteTxn[6] & expmonth) == expmonth)
                            {
                                TmpCC = TmpCC + setTag("expmonth", TRINP.expmonth);
                            }
                            if ((ByteTxn[6] & expyear) == expyear)
                            {
                                TmpCC = TmpCC + setTag("expyear", TRINP.expyear);
                            }


                            if (Info.Dukpt == "1")
                            {
                                if ((ByteTxn[7] & dukpt) == dukpt)
                                {
                                    if ((ByteTxn[10] & tp_dukpt) == tp_dukpt)
                                    {
                                        Tmpdukpt = Tmpdukpt + setTag("tp_dukpt", Info.Dukpt.Trim());
                                    }
                                    if ((ByteTxn[10] & nb_ksn) == nb_ksn)
                                    {
                                        Tmpdukpt = Tmpdukpt + setTag("nb_ksn", TRINP.ksn);
                                    }
                                    if ((ByteTxn[10] & nb_data) == nb_data)
                                    {
                                        Tmpdukpt = Tmpdukpt + setTag("nb_data", TRINP.tracks);
                                    }
                                    TmpCC = TmpCC + setTag("dukpt", Tmpdukpt);
                                }
                            }
                            if ((ByteTxn[7] & contactless) == contactless)
                            {
                                TmpCC = TmpCC + setTag("contactless", TRINP.contactless);
                            }

                            if ((ByteTxn[7] & cvvcsc) == cvvcsc)
                            {
                                if (!string.IsNullOrEmpty(TRINP.cvvcsc))
                                    TmpCC = TmpCC + setTag("cvv-csc", TRINP.cvvcsc);
                            }
                        }

                        TmpCC = setTag("creditcard", TmpCC);
                        TmpXML = TmpXML + TmpCC;
                    }

                    if ((ByteTxn[2] & amount) == amount)
                    {
                        if (TRINP.TRX_TYPE == Transaction.RECOMPENSAS)
                           TmpXML = TmpXML + setTag("amount", TRINP.RecomAmount);
                        else
                            TmpXML = TmpXML + setTag("amount", TRINP.Tx_Amount);
                    }
                    if ((ByteTxn[2] & currency) == currency)
                    {
                        TmpXML = TmpXML + setTag("currency", TRINP.Tx_Currency);
                    }
                    if ((ByteTxn[3] & no_operacion) == no_operacion)
                    {
                        TmpXML = TmpXML + setTag("no_operacion", TRINP.Tx_OperationNumber);
                    }
                    if ((ByteTxn[4] & auth) == auth)
                    {
                        TmpXML = TmpXML + setTag("auth", TRINP.Tx_Auth);
                    }
                    
                    if ((ByteTxn[4] & opcion) == opcion)
                    {
                        TmpXML = TmpXML + setTag("opcion", TRINP.RecomPrtOpt);
                    }

                    if ((ByteTxn[4] & copia) == copia)
                    {
                        TmpXML = TmpXML + setTag("copia", TRINP.RecomCopia);
                    }

                    //Corte Recompensas
                    if ((ByteTxn[11] & tipoReporte) == tipoReporte)
                    {
                        TmpXML = TmpXML + setTag("tp_reporte", TRINP.RecomTipoReporte);
                    }

                    //corte recompensas
                    if ((ByteTxn[11] & tipoTicket) == tipoTicket)
                    {
                        TmpXML = TmpXML + setTag("tp_ticket", TRINP.RecomTipoTicket);
                    }

                    if ((ByteTxn[4] & foliocpagos) == foliocpagos)
                    {
                        if (TRINP.TRX_TYPE == Transaction.REIMPRESION_RECOMPENSAS)
                            TmpXML = TmpXML + setTag("nu_operacion", TRINP.foliocpagos);
                        else
                            TmpXML = TmpXML + setTag("foliocpagos", TRINP.foliocpagos);
                    }

                    if ((ByteTxn[4] & canal) == canal)
                    {
                        TmpXML = TmpXML + setTag("canal", TRINP.canal);
                    }

                    if ((ByteTxn[2] & usrtransaction) == usrtransaction)
                    {
                        TmpXML = TmpXML + setTag("usrtransacction", TRINP.Bs_UsrTransaction);
                    }

                    //Consumo restaurant
                    if ((ByteTxn[11] & consumo) == consumo)
                    {
                        TmpXML = TmpXML + setTag("cd_mesero", TRINP.Tx_Waiter);
                        TmpXML = TmpXML + setTag("cd_turno", TRINP.Tx_Shifts);

                        if (TRINP.TRX_TYPE == Transaction.PREVENTAPROPINAEMV)
                            TmpXML = TmpXML + setTag("cd_mesa", TRINP.Tx_Table);
                    }

                    //Hotel
                    if ((ByteTxn[4] & tx_room) == tx_room)
                    {

                        if(!string.IsNullOrEmpty(TRINP.Tx_Room))
                            TmpXML = TmpXML + setTag("room", TRINP.Tx_Room);
                    }

                    if ((ByteTxn[3] & emv) == emv)
                    {
                        TmpXML = TmpXML + setTag("emv", TRINP.emv);
                    }
                    if (TRINP.TRX_TYPE == Transaction.REVERSO)
                    {
                        TmpXML = TmpXML + setTag("tracks", "");
                    }
                    if (TRINP.TRX_TYPE == Transaction.CANCELACION
                        || TRINP.TRX_TYPE == Transaction.REVERSO
                        || TRINP.TRX_TYPE == Transaction.CIERREPREVENTAMOTO
                        || TRINP.TRX_TYPE == Transaction.CHECKOUT
                        || TRINP.TRX_TYPE == Transaction.REAUTORIZACION_MOTO)
                    {
                        if ((ByteTxn[5] & crypto) == crypto)
                        {
                            TmpXML = TmpXML + setTag("crypto", TRINP.Crypto);
                        }
                    }

                    //PROPINA CIERRE PREVENTA
                    if ((ByteTxn[4] & propina) == propina && TRINP.TRX_TYPE == Transaction.CIERREPREVENTAMOTO)
                    {
                        TmpXML = TmpXML + setTag("propina", TRINP.Tx_Tip);
                    }

                    
                    if ((ByteTxn[11] & t3ds) == t3ds)
                    {
                        Tmpt3ds = "";
                        Tmpt3ds = Tmpt3ds + setTag("t3ds", "1");
                        Tmpt3ds = Tmpt3ds + setTag("cavv", TRINP.cavv);
                        Tmpt3ds = Tmpt3ds + setTag("eci", TRINP.eci);
                        Tmpt3ds = Tmpt3ds + setTag("xid", TRINP.xid);
                        TmpXML = TmpXML + Tmpt3ds;
                    }
                    if ((ByteTxn[3] & version) == version)
                    {
                        //TmpXML = TmpXML + setTag("version", Info.super_version);
                        TmpXML = TmpXML + setTag("version", utilidadesMIT.GetVersionApp());
                    }
                    if ((ByteTxn[3] & serie) == serie)
                    {
                        TmpXML = TmpXML + setTag("serie", Info.SerialNumber);
                    }

                    //PROPINA RESTAURANT CONSUMO
                    if ((ByteTxn[4] & propina) == propina && TRINP.TRX_TYPE == Transaction.PREVENTAPROPINAEMV)
                    {
                        TmpXML = TmpXML + setTag("propina", TRINP.Tx_Tip);
                    }

                    if ((ByteTxn[3] & version_terminal) == version_terminal)
                    {
                        TmpXML = TmpXML + setTag("version_terminal", TRINP.version_terminal);
                    }
                    if ((ByteTxn[3] & modelo_terminal) == modelo_terminal)
                    {
                        TmpXML = TmpXML + setTag("modelo_terminal", TRINP.modelo_terminal);
                    }

                    //Agencia
                    if ((ByteTxn[4] & agencia) == agencia)
                    {
                        if ((TRINP.tx_boleto != null) && (TRINP.fh_salida != null))
                        {
                            if(!TRINP.tx_boleto.Equals("") || !TRINP.fh_salida.Equals(""))
                            {
                                TmpAgencia = setTag("tx_boleto", TRINP.tx_boleto);
                                TmpAgencia = TmpAgencia + setTag("fh_salida", TRINP.fh_salida);
                                TmpAgencia = TmpAgencia + setTag("fh_retorno", TRINP.fh_retorno);                                
                            }
                            TmpAgencia = setTag("agencia", TmpAgencia);
                            TmpXML = TmpXML + TmpAgencia;
                        }
                    }
                    

                    if ((ByteTxn[3] & terminal) == terminal)
                    {
                        if ((ByteTxn[8] & printer) == printer)
                        {
                            TmpTerm = setTag("printer", Info.Printer);
                        }
                        if ((ByteTxn[8] & display) == display)
                        {
                            TmpTerm = TmpTerm + setTag("display", TRINP.display);
                        }
                        if ((ByteTxn[8] & is_mobile) == is_mobile)
                        {
                            TmpTerm = TmpTerm + setTag("is_mobile", TRINP.is_mobile);
                        }
                        if ((ByteTxn[9] & nb_marca_terminal) == nb_marca_terminal)
                        {
                            TmpTerm = TmpTerm + setTag("nb_marca_terminal", Info.marca);
                        }
                        if ((ByteTxn[9] & nb_modelo_terminal) == nb_modelo_terminal)
                        {
                            TmpTerm = TmpTerm + setTag("nb_modelo_terminal", Info.model);
                        }
                        if ((ByteTxn[9] & nb_serie_lector) == nb_serie_lector)
                        {
                            TmpTerm = TmpTerm + setTag("nb_serie_lector", Info.SerialNumber);
                        }
                        if ((ByteTxn[9] & nb_version_terminal) == nb_version_terminal)
                        {
                            TmpTerm = TmpTerm + setTag("nb_version_terminal", Info.version);
                        }
                        if ((ByteTxn[9] & nb_tk) == nb_tk)
                        {
                            TmpTerm = TmpTerm + setTag("nb_tk", Info.cipherdukptkey);
                        }
                        if ((ByteTxn[9] & nb_kcv) == nb_kcv)
                        {
                            TmpTerm = TmpTerm + setTag("nb_kcv", Info.kcv);
                        }
                        
                        if ((ByteTxn[8] & is_contactless) == is_contactless)
                        {
                            TmpTerm = TmpTerm + setTag("is_contactless", Info.Contactless);
                        }
                        TmpTerm = setTag("terminal", TmpTerm);
                        TmpXML = TmpXML + TmpTerm;
                    }

                    if ((ByteTxn[3] & tp_resp) == tp_resp)
                    {
                        TmpXML = TmpXML + setTag("tp_resp", TRINP.tp_resp);
                    }
                    if (TRINP.TRX_TYPE != Transaction.INITDUKPT)
                    {
                        TmpXML = setTag("transacction", TmpXML);
                    }

                }

                //*****************************************************************************************************
                //Cupones
                if (TRINP.TRX_TYPE == Transaction.ALTA_CUPONES
                    || TRINP.TRX_TYPE == Transaction.BUSCAR_CUPONES
                    || TRINP.TRX_TYPE == Transaction.REDIMIR_CUPONES)
                {
                    if ((ByteTxn[11] & cupones) == cupones)
                    {
                        if (TRINP.TRX_TYPE == Transaction.ALTA_CUPONES)
                        {
                            TmpXML = TmpXML + setTag("tpOperation", TRINP.Tx_OperationType);
                            TmpXML = TmpXML + setTag("phone", TRINP.phone);
                            TmpXML = setTag("transaction", TmpXML);
                        }

                        if (TRINP.TRX_TYPE == Transaction.BUSCAR_CUPONES
                            || TRINP.TRX_TYPE == Transaction.REDIMIR_CUPONES)
                        {
                            TmpXML = TmpXML + setTag("tpOperation", TRINP.Tx_OperationType);
                            TmpXML = TmpXML + setTag("search_value", TRINP.phone);
                            TmpXML = setTag("transaction", TmpXML);
                        }

                    }
                }

                XML = XML + TmpXML;
                TmpXML = "";
                if ((ByteTxn[10] & dcc) == dcc)
                {
                    if (TRINP.dcc_status != null)
                    {
                        if (!TRINP.dcc_status.Equals(""))
                        {
                            if ((ByteTxn[10] & dcc_status) == dcc_status)
                            {
                                TmpXML = setTag("dcc_status", TRINP.dcc_status);
                                if (TRINP.dcc_status.Trim().Equals("1"))
                                {
                                    TmpXML = TmpXML + setTag("dcc_amount", TRINP.dcc_amount);
                                }
                            }
                            TmpXML = setTag("dcc", TmpXML);
                        }
                    }
                }
                XML = XML + TmpXML;
                TmpXML = "";
                if ((ByteTxn[10] & mci) == mci)
                {
                    if (TRINP.plazoMCI!=null)
                    {
                        if (!TRINP.plazoMCI.Equals(""))
                        {
                            if ((ByteTxn[10] & plazomci) == plazomci)
                            {
                                TmpXML = setTag("plazomci", TRINP.plazoMCI);
                            }
                            TmpXML = setTag("mci", TmpXML);
                        }
                    }
                }
                XML = XML + TmpXML;
                TmpXML = "";
                if ((ByteTxn[10] & avs) == avs)
                {
                    TmpXML = setTag("direccion", TRINP.avs_address);
                    TmpXML = TmpXML + setTag("delegacion", TRINP.avs_municipality);
                    TmpXML = TmpXML + setTag("ciudad", TRINP.avs_city);
                    TmpXML = TmpXML + setTag("estado", TRINP.avs_state);
                    TmpXML = TmpXML + setTag("cp", TRINP.avs_zip);
                    TmpXML = TmpXML + setTag("colonia", TRINP.avs_district);
                    TmpXML = TmpXML + setTag("version", "avs100");
                    TmpXML = TmpXML + setTag("avs", TmpXML);
                }
                XML = XML + TmpXML;
                TmpXML = "";
                
                //Transaction Type
                if(TRINP.TRX_TYPE == Transaction.VTADIRECTA
                   || TRINP.TRX_TYPE == Transaction.VTA_BOLETOSEMV)
                {
                    XML = setTag("VMCAMEXB", XML); 
                }
                else if ((TRINP.TRX_TYPE == Transaction.VTAMOTO)    ||
                         (TRINP.TRX_TYPE == Transaction.VTAMOTO3DS) ||
                         (TRINP.TRX_TYPE == Transaction.VTA_BOLETOSMOTO))
                {
                    XML = setTag("VMCAMEXM", XML); 
                }
                else if (TRINP.TRX_TYPE == Transaction.VTAFRZDMOTO)
                {
                    XML = setTag("VMCAMEXMFORZADA", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.VTAAVSMOTO)
                {
                    XML = setTag("VMCAVS", XML);
                }
                else if(TRINP.TRX_TYPE == Transaction.INITDUKPT)
                {
                    XML = setTag("IPEK_REQUESTType", XML); 
                }
                else if (TRINP.TRX_TYPE == Transaction.CANCELACION)
                {
                    XML = setTag("VMCAMEXMCANCELACION", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.REVERSO)
                {
                    XML = setTag("VMCAMEXMREVERSO", XML);
                }
                else if ((TRINP.TRX_TYPE == Transaction.RECOMPENSAS) 
                         || (TRINP.TRX_TYPE == Transaction.PRINTRECOMP)
                         || (TRINP.TRX_TYPE == Transaction.CORTERECOMPENSAS))
                {
                    XML = setTag("VMCAMEXRECOMP", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.REIMPRESION)
                {
                    TmpXML = "";
                    if ((ByteTxn[3] & no_operacion) == no_operacion)
                    {
                        TmpXML = setTag("no_operacion", TRINP.Tx_OperationNumber);
                    }
                    if ((ByteTxn[5] & crypto) == crypto)
                    {
                        TmpXML = TmpXML + setTag("crypto", TRINP.Crypto);
                    }
                    XML = XML + TmpXML;
                    XML = setTag("REPRINTVOUCHER", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.CHECKIN)
                {
                    XML = setTag("VMCAMEXBCHECKIN", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.CHECKINMOTO)
                {
                    XML = setTag("VMCAMEXMCHECKIN", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.CIERREPREVENTAMOTO)
                {
                    XML = setTag("VMCAMEXMCIERREPREVENTA", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.CHECKOUT)
                {
                    XML = setTag("VMCAMEXMCHECKOUTEXPRESS", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.REIMPRESION_RECOMPENSAS)
                {
                    XML = setTag("VMCAMEXRECOMPREPRINT", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.PREVENTAPROPINAEMV) //Preventa Consumo
                {
                    XML = setTag("VMCAMEXBVTADPROPINA", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.PREVENTAEMV) //Preventa
                {
                    XML = setTag("VMCAMEXBPREVENTA", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.TOKEN)
                {
                    TmpXML = setTag("cc_number", TRINP.cc_number);
                    TmpXML = TmpXML + setTag("tkn_reference", TRINP.Tx_Reference);
                    XML = XML + TmpXML;
                    XML = setTag("TOKENIZATION", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.REAUTORIZACION_MOTO) //Reautorizacion
                {
                    XML = setTag("VMCAMEXMREAUTORIZACION", XML);
                }
                else if (TRINP.TRX_TYPE == Transaction.ALTA_CUPONES
                        || TRINP.TRX_TYPE == Transaction.BUSCAR_CUPONES
                        || TRINP.TRX_TYPE == Transaction.REDIMIR_CUPONES) //Alta cupones
                {
                    XML = setTag("PNPREQUEST", XML);
                }

                //Header
                if (TRINP.TRX_TYPE != Transaction.REVERSO)
                {
                    //Header xml
                    XML = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + XML;
                }
            }
            MITLog.PrintLn("XML: " + XML);
            return XML;
        }
        //Get XML value response.
        public String getDataXML(String Tag, String Buffer)
        {
            String tagIni = "<" + Tag + ">";
            String tagFin = "</" + Tag + ">";
            try
            {
                int iIni = Buffer.IndexOf(tagIni);
                int iFin = Buffer.IndexOf(tagFin);
                if ((iIni >= 0) && (iFin > iIni))
                {
                    return Buffer.Substring(iIni + tagIni.Length, iFin - iIni - tagIni.Length);
                }
            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
                return "";
            }
            return "";
        }
    }
}
