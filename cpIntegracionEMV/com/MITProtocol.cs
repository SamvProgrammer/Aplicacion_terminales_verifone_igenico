using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cpIntegracionEMV.data;
using cpIntegracionEMV.txn_flow;
using cpIntegracionEMV.util;
using System.Runtime.InteropServices;
namespace cpIntegracionEMV.com
{
    class MITProtocol
    {
        BeanCom bcom = new BeanCom();
        
        //Process PinPad Message Response.
        public bool ProcesaRsp()
        {
            String cmdType = "";
            byte[] bcmd = new byte[3];
            String cmd;
            //String CMD = "";
            int LenCmd = 0;
            int LenBuff = 0;
            Boolean continueparse=true;
            
            cmd = bcom.getRecBuf();
            LenBuff = cmd.Length;
            MITLog.PrintLn(cmd.Substring(0,bcom.getLenRecBuf()));
            
            do
            {
                //Find STX
                if (cmd.Length > 0)
                {
                    if (cmd.Substring(0, 1).Equals(bcom.getsSTX()))
                    {
                        LenCmd = Int32.Parse(cmd.Substring(1, 3));

                        if (cmd.Substring(4, 3).Equals("P61"))
                        {
                            bcom.setRecBuf(cmd.Substring(0, LenCmd + 6));
                            bcom.setLenRecBuf(LenCmd + 6);
                            //LenCmd = LenCmd - 1;
                            ProcessingP61();
                            return true;
                        }

                        //Find ETX
                        if (cmd.Substring(LenCmd + 4, 1).Equals(bcom.getsETX()))
                        {
                            //Set Command to process
                            bcom.setRecBuf(cmd.Substring(0, LenCmd + 6));
                            bcom.setLenRecBuf(LenCmd + 6);
                            //Check LRC Value
                            if (CheckLRC())
                            {
                                //Get number command.
                                cmdType = cmd.Substring(4, 3);

                                if (cmdType.Substring(0, 1).Equals("E"))
                                {
                                    if(!ProcessingError())
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    //About
                                    if (cmdType.Equals("P56") || cmdType.Equals(bcom.getNull() + "56"))
                                    {
                                        ProcessingP56();
                                    }
                                    //Printer
                                    else if (cmdType.Equals("P59"))
                                    {
                                        ProcessingP59();
                                    }
                                    //Cipher Card
                                    else if (cmdType.Equals("P62"))
                                    {
                                        ProcessingP62();
                                    }
                                    //Transaction Process
                                    else if (cmdType.Equals("P71") || cmdType.Equals("P93"))
                                    {
                                        ProcessingEMV();
                                    }
                                    //Display information
                                    else if (cmdType.Equals("P81"))
                                    {
                                        ProcessingP81();
                                    }
                                    //Parametros EMV
                                    else if (cmdType.Equals("P83"))
                                    {
                                        ProcessingP83();
                                    }
                                    //End EMV config
                                    else if (cmdType.Equals("P87"))
                                    {
                                        ProcessingP87();
                                    }
                                    //Dukpt Initialization.
                                    else if (cmdType.Equals("P91"))
                                    {
                                        ProcessingP91();
                                    }
                                    //IPEK Set
                                    else if (cmdType.Equals("P92"))
                                    {
                                        ProcessingP92();
                                    }
                                    //Cancel
                                    else if (cmdType.Equals("PXV"))
                                    {
                                        ProcessingCancel();
                                    }
                                    //Firma en pinPad
                                    else if (cmdType.Equals("P61"))
                                    {
                                        ProcessingP61();
                                    }
                                    //Parametros EMV
                                    else if (cmdType.Equals("P50"))
                                    {
                                        ProcessingP50();
                                    }
                                    //Carga Pinpad
                                    else if (cmdType.Equals("P84"))
                                    {
                                        ProcessingP84();
                                    }
                                    else
                                    {
                                        //Command not found.
                                        break;
                                    }

                                }
                            }
                            else
                            {
                                return false;
                            }
                            //Is there more information
                            if ((LenCmd + 6) < LenBuff)
                            {
                                cmd = cmd.Substring((LenCmd + 6), (LenBuff - (LenCmd + 6)));
                                //is it an invalid command?
                                if (cmd.Substring(0, 1).Equals(bcom.getNull()))
                                {
                                    cmd = "";
                                }
                                LenBuff = cmd.Length;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        
                        break;
                    }
                }
                else
                {
                    break;
                }
            } while (continueparse);
                                
            return true; 
        }
        //CheckLRC
        private Boolean CheckLRC()
        {
            byte[] buffer = new byte[bcom.getBufSize()];
            //Verifone Validate - without STX
            System.Buffer.BlockCopy(Encoding.ASCII.GetBytes(bcom.getRecBuf().Substring(1, bcom.getLenRecBuf()-1)), 0, buffer, 0, bcom.getLenRecBuf()-1);
            if(buffer[bcom.getLenRecBuf()-2]==calculateLRC(buffer, bcom.getLenRecBuf() -2))
            {
                return true;
            }
            else
            {
                //Ingenico Validate - With STX
                buffer = new byte[bcom.getBufSize()];
                System.Buffer.BlockCopy(Encoding.ASCII.GetBytes(bcom.getRecBuf().Substring(0, bcom.getLenRecBuf())), 0, buffer, 0, bcom.getLenRecBuf());
                if(buffer[bcom.getLenRecBuf()-1]==calculateLRC(buffer, bcom.getLenRecBuf() -1))
                {
                    return true;
                }
            }
            return false;
        }
        //LRC
        private byte calculateLRC(byte[] bytes, int lengtLRC)
        {
            byte LRC = 0;
            for (int i = 0; i < lengtLRC; i++)
            {
                LRC ^= bytes[i];
            }
            return LRC;
        }
        //Get data between two tokens.
        private String getData(String ini, String fin, String buffer)
        {
            int iIni = buffer.IndexOf(ini);
            int iFin = buffer.IndexOf(fin);
            if ((iIni >= 0) && (iFin > iIni))
            {
                return buffer.Substring(iIni + ini.Length, iFin - iIni - ini.Length);
            }
            else
            {
                return "";
            }
        }
        //Build command structure
        private Boolean BuildCMD(String Cmd)
        {
            int lenSend, lencmd;
            String slen = "";
            int countBytes = 0;
            byte[] buffer = new byte[bcom.getBufSize()];
            System.Text.ASCIIEncoding buffStr = new System.Text.ASCIIEncoding();
            SendMsg send = new SendMsg();
            lenSend = Cmd.Length;

            buffer[countBytes] = bcom.getSTX();
            countBytes++;

            if (Cmd.Trim().Equals("VXVCANCEL"))
            {
                lencmd = 12;
            }
            else
            {
                lencmd = lenSend;
            }
            slen = lencmd.ToString();
            slen = slen.PadLeft(3);
            slen = slen.Replace(" ", "0");
            System.Buffer.BlockCopy(Encoding.ASCII.GetBytes(slen), 0, buffer, countBytes, 3);
            countBytes += 3;
            System.Buffer.BlockCopy(Encoding.ASCII.GetBytes(Cmd), 0, buffer, countBytes, lenSend);
            countBytes += lenSend;
            buffer[countBytes] = bcom.getETX();
            countBytes++;
            buffer[countBytes] = calculateLRC(buffer, countBytes);
            countBytes++;

            MITLog.PrintLn(buffStr.GetString(buffer).Substring(0, countBytes));
            return send._SendMsg(buffer, countBytes);
            
        }
        //Find device port.
        private bool findPorts()
        {
            bool status=false;

            try
            {
                String[] ports = RS232.findPorts();
                bcom.setBaudRate(19200);
                for (int count = 0; count < ports.Length; count++)
                {
                    bcom.setCom(ports[count]);
                    Info.COM = ports[count];
                    if (SendC56())
                    {
                        status = true;
                        break;
                    }
                    else
                    {
                        bcom.setCom("");
                        Info.COM = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MITLog.PrintLn("findPorts" + ex.Message);
                status = false;
            }

            return status;
        }
        //connect
        public Boolean connect()
        {
            bool status = false;

            try
            {
                if (!bcom.getfindDevice())
                {
                    /*bcom.setTypeCom
                     *Set 1 to RS232
                     *Set 2 to IP
                     */
                    bcom.setTypeCom("1");
                    if (bcom.getTypeCom().Equals("1"))
                    {
                        status = findPorts();
                        bcom.setfindDevice(status);
                    }
                }
                else
                {
                    status = true;
                }
            }
            catch (Exception ex)
            {
                MITLog.PrintLn("connect" + ex.Message);
                status = false;
            }

            return status;
        }
        //getDinamicKey
        private String getDinamicKey()
        {
            String dinamicKey = "";
            String amount = "";

            if (!Info.SerialNumber.Equals(""))
            {

                if (TRINP.Tx_AmountBase == "")
                    amount = TRINP.Tx_Amount;
                else
                    amount = TRINP.Tx_AmountBase;

                dinamicKey = TypeUsuario.User.Substring(0, 4);
                dinamicKey = dinamicKey + pmonto(amount);
                dinamicKey = dinamicKey + Info.SerialNumber.Substring(Info.SerialNumber.Length - 6, 6);
                dinamicKey = dinamicKey + TypeUsuario.User.Substring(TypeUsuario.User.Length - 2, 2);
            }

            Info.DinamicKey = dinamicKey;
            return dinamicKey;
        }


        private string pmonto(string monto)
        {
            string izq;
            string der;
            string resultado = "";

            if(monto.Contains('.'))
            {
                izq = monto.Split('.')[0];
                der = monto.Split('.')[1];
                resultado = completaCadena(izq, "I") + completaCadena(der, "D");

            }
            else
                resultado = completaCadena(monto, "I") + "00";

            return resultado;
        }

        private string completaCadena(string monto, string tipo)
        {
            string  vuelta = "";

            monto = monto.Trim();

            if(monto.Equals(""))
                vuelta = "00";
            else
            {
                if(monto.Length == 1)
                {
                    if(tipo.ToUpper().Equals("I"))
                        vuelta = "0" + monto;
                    else
                        vuelta = monto + "0";
                }
            
                if(monto.Length >= 2)
                {
                    if(tipo.ToUpper().Equals("I"))
                    {
                        if (monto.Length == 2)
                            vuelta = monto.Substring(0, 2);
                        else
                            vuelta = monto.Substring(monto.Length - 2, 2);
                    }
                    else
                        vuelta = monto.Substring(0,2);
                }

            }

            return vuelta;
        }


        /*
         * REQUEST 
         */
        //about.
        public Boolean about()
        {
            //Clear clear = new Clear();
            //clear.ClearInfo();
            return connect();
        }
        //Comando C56 - About
        public Boolean SendC56()
        {
            String Cmd = "";
            Cmd = "C56AABOUT";
            //Timeout to wait response...
            bcom.setTimeOut(3);
            return BuildCMD(Cmd);
        }

        //Comando C59 - Print
        public bool SendC59(String voucher)
        {

            if (string.IsNullOrEmpty(Info.Printer) || Info.Printer == ("0"))
            {
                MITLog.PrintLn("Terminal sin impresora");
                return false;
            }

            String Cmd = "";
            
            //Set logo
            voucher = voucher.Replace("@cnb Santander", "@logo1 @br");
            voucher = voucher.Replace("@cnb American Express", "@logo2 @br");
            voucher = voucher.Replace("@cnb HSBC", "@logo7 @br");
            voucher = voucher.Replace("@cnb IXE", "@logo11 @br");
            voucher = voucher.Replace("@cnb MULTIVA", "@logo15 @br");
            voucher = voucher.Replace("@cnb SCOTIA BANK", "@logo16 @br");
            voucher = voucher.Replace("@cnb logo_cpagos", "@logo3 @br");
            voucher = voucher.Replace("@cnn ver_app", "@cnn "+ Info.dll_version);

            //if (!agregaSalto)
            //    voucher = voucher + "@br @br @br ";

            Cmd = "C59"
                + "A" + voucher;
            //Timeout to wait response...
            bcom.setTimeOut(30);
            return BuildCMD(Cmd);
        }
        //Comando SendStartEMV - Lectura de tarjeta.
        public Boolean SendStartEMV()
        {
            String Cmd = "";

            TRINP.Tx_Amount = utilidadesMIT.FormatoNumero(TRINP.Tx_Amount);
            TRINP.Tx_Add_Amount = utilidadesMIT.FormatoNumero(TRINP.Tx_Add_Amount);

            string lecturaCTLS;
            double valorTemp;
            double.TryParse(TRINP.Tx_Amount, out valorTemp);

            //para realizar la lectura CTLS
            if (valorTemp <= TypeUsuario.LimiteTrxCTLS)
            {
                lecturaCTLS = Info.Contactless;
            }
            else
            {
                lecturaCTLS = "0";
            }

            TRINP.Key = getDinamicKey();

            if (string.IsNullOrEmpty(Info.timeout))
            {
                Info.timeout = "60";
                TRINP.TimeOut = "60";
            }

            Cmd = TRINP.Command
                  + "A" + TRINP.DisplayTxt + bcom.getFS() //Display Message
                  + "B" + TRINP.Date + bcom.getFS()
                  + "C" + TRINP.Time + bcom.getFS()
                  + "D" + TRINP.Tx_Amount + bcom.getFS()
                  + "E" + TRINP.Tx_Add_Amount + bcom.getFS()
                  + "F" + TRINP.Tx_CurrencyCode + bcom.getFS();

                  if(Info.Dukpt.Equals("1"))
                  {
                      if (Info.TieneCargaCTLS)
                      {
                          Cmd = Cmd
                         + "G" + TRINP.TimeOut + bcom.getFS()
                         + "H" + TRINP.RspType + bcom.getFS()
                         + "I" + "1" + bcom.getFS()//cvvamex 
                         + "J" + Info.forceonline + bcom.getFS()
                         + "K" + lecturaCTLS + bcom.getFS()
                         + "L" + TRINP.NoEMVCard;
                      }
                      else
                      {
                          Cmd = Cmd
                              + "G" + TRINP.TimeOut + bcom.getFS()
                              + "H" + TRINP.RspType + bcom.getFS()
                              + "I" + "1" + bcom.getFS()//cvvamex 
                              + "L" + TRINP.NoEMVCard;
                      }

                      
                  }
                  else
                  {
                      Cmd = Cmd
                      + "G" + TRINP.GoOnline + bcom.getFS()
                      + "H" + TRINP.Key + bcom.getFS()
                      + "I" + Info.timeout + bcom.getFS()
                      + "J" + TRINP.RspType + bcom.getFS()
                      + "L" + TRINP.NoEMVCard;
                  }
            //Timeout to wait response...
            //bcom.setTimeOut(30);


                  if (TRINP.Tx_AmountBase != "")
                      TRINP.Tx_Amount = TRINP.Tx_AmountBase;

            return BuildCMD(Cmd);
        }
        //SendFinishEMV - Finish EMV Flow.
        public Boolean SendFinishEMV()
        {
            TRINP.pinpadrsp = TRRSP.cd_response;

            if (TRRSP.cd_error.Equals("92"))
            {
                TRINP.Command = "C93";
                TRRSP.cd_response = "01";
            }

            if (TRINP.contactless.Equals("1"))
                return true;

            if (TRINP.pose.Equals("022") || TRINP.pose.Equals("800"))
                return true;

            if (!TRRSP.cd_response.Equals("00"))
                TRRSP.cd_response = "01";

            String Cmd = "";

            if (Info.Dukpt.Equals("1"))
            {
                Cmd = TRINP.Command
                      + "A" + TRRSP.cd_response + bcom.getFS() //Display Message
                      + "B" + bcom.getFS()
                      + "C" + bcom.getFS()
                      + "D" + bcom.getFS()
                      + "E" + TRRSP.emv_key_date + bcom.getFS()
                      + "F" + TRRSP.icc_csn + bcom.getFS()
                      + "G" + TRRSP.icc_atc + bcom.getFS()
                      + "H" + TRRSP.icc_arpc + bcom.getFS()
                      + "I" + TRRSP.icc_issuer_script + bcom.getFS()
                      + "J" + TRRSP.authorized_amount + bcom.getFS()
                      + "K" + TRRSP.account_balance_1;
            }
            else
            {
                Cmd = TRINP.Command
                      + "A" + TRRSP.cd_response + bcom.getFS() //Display Message
                      + "B" + TRRSP.Cc_TokenB5 + bcom.getFS()
                      + "C" + TRRSP.Cc_TokenB6 + bcom.getFS()
                      + "D" + TRRSP.Cc_TokenBJ;
            }

            
            //Timeout to wait response...
            bcom.setTimeOut(30);
            return BuildCMD(Cmd);
        }
        //dukptInit - C91
        public Boolean dukptInit()
        {
            String Cmd = "";
            Cmd = TRINP.Command
                  + "A" + TRINP.Date + bcom.getFS()
                  + "B" + TRINP.Time ;
            //Timeout to wait response...
            bcom.setTimeOut(30);
            return BuildCMD(Cmd);
        }
        //setDUKPT - C92
        public Boolean setDUKPT()
        {
            String Cmd = "";
            Cmd = TRINP.Command
                  + "A" + Info.ksn + bcom.getFS()
                  + "B" + Info.kcv + bcom.getFS()
                  + "C" + Info.ipek;
            //Timeout to wait response...
            bcom.setTimeOut(30);
            return BuildCMD(Cmd);
        }
        //Cancel Operation
        public Boolean Cancel([Optional]string timeOutCancel)
        {
            String Cmd = "";
            if(Info.marca.Equals("INGENICO"))
            {
                Cmd = "C50AOPERACION       CANCELADA";
            }
            else
            {
                Cmd = "VXVCANCEL";
            }
            
            //Timeout to wait response...

            if (string.IsNullOrEmpty(timeOutCancel))
                bcom.setTimeOut(30);
            else
            {
                int to;
                int.TryParse(timeOutCancel, out to);
                bcom.setTimeOut(to);
            }


            return BuildCMD(Cmd);
        }
        //C62
        public Boolean getCipherCard()
        {
            String Cmd = "C62"
                        + "A" + TypeUsuario.RSAKeyDataMSG + bcom.getFS()
                        + "B" + TypeUsuario.RSAKeyDataLength + bcom.getFS()
                        + "C" + TypeUsuario.RSAKeyData;
            //Timeout to wait response...
            bcom.setTimeOut(30);
            return BuildCMD(Cmd);
        }
        //C83
        public Boolean sendEMVConfig(String type, String carga, String buffer)
        {
            String Cmd = "C83A" + type
                + bcom.getFS() + "B" + carga
                + bcom.getFS() + "C" + buffer
                + bcom.getFS() + "D";
            bcom.setTimeOut(30);
            return BuildCMD(Cmd);
        }
        //C87
        public Boolean endEMVConfig()
        {
            String Cmd = "C87A00";
            bcom.setTimeOut(30);
            return BuildCMD(Cmd);
        }
        //
        /*
         * RESPONSE
         */
        //Processing P56 Command
        private void ProcessingP56()
        {
            Clear clear = new Clear();
            clear.ClearInfo();
            
            String RecBuffer = bcom.getRecBuf();
            
            Info.marca = getData("A00", bcom.getFS() + "B", RecBuffer);
            Info.model = getData(bcom.getFS() + "B", bcom.getFS() + "C", RecBuffer);
            Info.SerialNumber = getData(bcom.getFS() + "C", bcom.getFS() + "D", RecBuffer);
            Info.version = getData(bcom.getFS() + "D", bcom.getFS() + "E", RecBuffer);
            Info.EMVFULL = getData(bcom.getFS() + "E", bcom.getFS() + "F", RecBuffer);
            Info.Printer = getData(bcom.getFS() + "F", bcom.getFS() + "G", RecBuffer);
            if (Info.Printer.Equals(""))
            {
                Info.Printer = getData(bcom.getFS() + "F", bcom.getsETX(), RecBuffer);
                if (Info.Printer.Length > 1)
                {
                    Info.Printer = "";
                }
            }

            Info.LoadKeys = getData(bcom.getFS() + "G", bcom.getFS() + "H", RecBuffer);
            Info.KeysVersion = getData(bcom.getFS() + "H", bcom.getFS() + "I", RecBuffer);
            Info.Kiosco = getData(bcom.getFS() + "I", bcom.getFS() + "J", RecBuffer);
            Info.Dukpt = getData(bcom.getFS() + "J", bcom.getFS() + "K", RecBuffer);
            Info.DukptKey = getData(bcom.getFS() + "K", bcom.getFS() + "L", RecBuffer);
            if(RecBuffer.IndexOf(bcom.getFS() +"M")>0)
            {
                Info.PanelSign = getData(bcom.getFS() + "L", bcom.getFS() + "M", RecBuffer);
                Info.Contactless = getData(bcom.getFS() + "M", bcom.getsETX(), RecBuffer);
                Info.TieneCargaCTLS = true;
            }
            else
            {
                Info.PanelSign = getData(bcom.getFS() + "L", bcom.getsETX(), RecBuffer);
                Info.TieneCargaCTLS = false;
            }
            bcom.setTimeOut(0);
        }
        //Processing P59 Command - Printer response.
        private void ProcessingP59()
        {
            String RecBuffer = bcom.getRecBuf();
            TRINP.pinpadrsp = getData("P59A", bcom.getsETX(), RecBuffer);
            bcom.setTimeOut(0);
        }
        //Cipher card
        private void ProcessingP62()
        {
            String RecBuffer = bcom.getRecBuf();
            if (RecBuffer.Contains("P62A10") || RecBuffer.Contains("P62E10"))
            {
                TRINP.pinpadrsp = "CANCELADO";
            }
            else
            {
                TRINP.pinpadrsp = getData("P62A", bcom.getFS() + "B", RecBuffer) + "|" + getData(bcom.getFS() + "B", bcom.getsETX(), RecBuffer);
            }
            bcom.setTimeOut(0);
        }
        //ProcessingEMV P71, P93 Command
        private void ProcessingEMV()
        {
            String RecBuffer = bcom.getRecBuf();
            if( ((RecBuffer.IndexOf("P71A00")>0) 
                 || (RecBuffer.IndexOf("P93A00")>0))
                 && (RecBuffer.IndexOf("P71A0051") == -1))
            {
                //Pinpad Response, after second generate.
                if ((RecBuffer.IndexOf("P71A00"+bcom.getFS()+"B0") > 0)
                     ||(RecBuffer.IndexOf("P93A00"+bcom.getFS()+"B0") > 0))
                {
                    //bcom.setCommContinue(false);
                    TRINP.pinpadrsp = getData(bcom.getFS() + "B", bcom.getFS() + "C", RecBuffer);
                    if (TRINP.pinpadrsp.Trim().Equals(""))
                    {
                        TRINP.pinpadrsp = getData(bcom.getFS() + "B", bcom.getsETX(), RecBuffer);
                    }
                    bcom.setTimeOut(0);
                }
                else
                {
                    int time = 60;

                    if (Info.marca.ToUpper().Equals("INGENICO") && Info.version.ToUpper().Contains("V0"))
                        time = 84;

                    //Waiting for response....
                    bcom.setCommContinue(true);
                    bcom.setTimeOut(time);
                }
            }
            else
            {
                //Get informarion card.
                if (Info.Dukpt.Equals("1"))
                {
                    TRINP.pose = getData("P93A", bcom.getFS() + "B", RecBuffer); //Pose Entry Mode
                }
                else
                {
                    TRINP.pose = getData("P71A", bcom.getFS() + "B", RecBuffer); //Pose Entry Mode
                }

                if (TRINP.pose.Length > 3)
                {
                    TRINP.pose = TRINP.pose.Substring(TRINP.pose.Length - 3);
                }

                //Is Chip card.
                if(TRINP.pose.Trim().Equals("051")
                   || TRINP.pose.Trim().Equals("071"))
                {
                    TRINP.chip = "1";
                    TRINP.tags = getData(bcom.getFS() + "B", bcom.getFS() + "C", RecBuffer);
                    TRINP.pin = getData(bcom.getFS() + "C", bcom.getFS() + "D", RecBuffer);
                    TRINP.tracks = getData(bcom.getFS() + "D", bcom.getFS() + "E", RecBuffer);
                    TRINP.CHName = getData(bcom.getFS() + "E", bcom.getFS() + "F", RecBuffer);
                    TRINP.arqc = getData(bcom.getFS() + "F", bcom.getFS() + "G", RecBuffer);
                    TRINP.aid = getData(bcom.getFS() + "G", bcom.getFS() + "H", RecBuffer);
                    TRINP.applabel = getData(bcom.getFS() + "H", bcom.getFS() + "I", RecBuffer);
                    TRINP.panmask = getData(bcom.getFS() + "I", bcom.getFS() + "J", RecBuffer);
                    if (Info.Dukpt.Equals("1"))
                    {
                        TRINP.expdate = getData(bcom.getFS() + "J", bcom.getFS() + "K", RecBuffer);

                        if (Info.TieneCargaCTLS)
                        {
                            TRINP.ksn = getData(bcom.getFS() + "K", bcom.getFS() + "M", RecBuffer);
                            TRINP.contactless = getData(bcom.getFS() + "M", bcom.getsETX(), RecBuffer);
                        }
                        else
                        {
                            TRINP.ksn = getData(bcom.getFS() + "K", bcom.getsETX(), RecBuffer);
                        }
                    }
                    else
                    {
                        TRINP.expdate = getData(bcom.getFS() + "J", bcom.getsETX(), RecBuffer);
                    }
                }
                else if (TRINP.pose.Trim().Equals("022") || TRINP.pose.Trim().Equals("800"))
                {
                    TRINP.chip = "0";
                    TRINP.tracks = getData(bcom.getFS() + "B", bcom.getFS() + "C", RecBuffer);
                    TRINP.fallback = getData(bcom.getFS() + "E", bcom.getFS() + "F", RecBuffer);
                    TRINP.panmask = getData(bcom.getFS() + "F", bcom.getFS() + "G", RecBuffer);
                    TRINP.expdate = getData(bcom.getFS() + "G", bcom.getFS() + "H", RecBuffer);
                    if (Info.Dukpt.Equals("1"))
                    {
                        TRINP.CHName = getData(bcom.getFS() + "H", bcom.getFS() + "I", RecBuffer);
                        TRINP.ksn = getData(bcom.getFS() + "I", bcom.getsETX(), RecBuffer);
                    }
                    else
                    {
                        TRINP.CHName = getData(bcom.getFS() + "H", bcom.getsETX(), RecBuffer);
                    }
                }
                else
                {
                    TRINP.chip = "0";
                }
                
                //Expiry date format.
                TRINP.expdate = TRINP.expdate.Trim().Replace("/", "");
                if (TRINP.expdate.Length == 4)
                {
                    TRINP.expmonth = TRINP.expdate.Substring(0, 2);
                    TRINP.expyear = TRINP.expdate.Substring(2, 2);
                }

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

                
                bcom.setTimeOut(0);
                bcom.setCommContinue(false);
            }
        }
        //Processing P81 Command - Command information.
        private void ProcessingP81()
        {
            String RecBuffer = bcom.getRecBuf();
            TRINP.P81MSG = getData("P81A", bcom.getsETX(), RecBuffer);
            //MITLog.PrintLn(TRINP.P81MSG);
        }
        //P83 Command - EMV Configuration
        private void ProcessingP83()
        {
            String RecBuffer = bcom.getRecBuf();
            bcom.setTimeOut(0);
            bcom.setCommContinue(false);
        }
        //P87 Command - EMV Configuration
        private void ProcessingP87()
        {
            String RecBuffer = bcom.getRecBuf();
            bcom.setTimeOut(0);
            bcom.setCommContinue(false);
        }
        //Processing P91 Comand - Dukpt Initilization.
        private void ProcessingP91()
        {
            String RecBuffer = bcom.getRecBuf();

            Info.marca = getData("A", bcom.getFS() + "B", RecBuffer);
            Info.model = getData(bcom.getFS() + "B", bcom.getFS() + "C", RecBuffer);
            Info.SerialNumber = getData(bcom.getFS() + "C", bcom.getFS() + "D", RecBuffer);
            Info.version = getData(bcom.getFS() + "D", bcom.getFS() + "E", RecBuffer);
            Info.kcv = getData(bcom.getFS() + "E", bcom.getFS() + "F", RecBuffer);
            Info.cipherdukptkey = getData(bcom.getFS() + "F", bcom.getsETX(), RecBuffer);
            bcom.setTimeOut(0);
        }
        //Processing P92 Comand - Load IPEK.
        private void ProcessingP92()
        {
            String RecBuffer = bcom.getRecBuf();
            TRINP.pinpadrsp = getData("P92A", bcom.getsETX(), RecBuffer);

            if (string.IsNullOrEmpty(TRINP.pinpadrsp))
            {
                Info.DukptKey = "0";
            }
            else
            {

                string cadena = TRINP.pinpadrsp;
                char[] caracteres = cadena.ToCharArray();
                string aux = "";
                foreach (char item in caracteres)
                {
                    if ((item >= 48 && item <= 57) || (item >= 48 && item <= 57) || (item >= 65 && item <= 90) || (item >= 97 && item <= 122))
                    {
                        aux += Convert.ToString(item);
                    }
                }

                if (aux == "00")
                    Info.DukptKey = "1";
                else
                    Info.DukptKey = "0";
            }

            bcom.setTimeOut(0);
        }
        //Processing Errors
        private bool ProcessingError()
        {
            String RecBuffer = bcom.getRecBuf();
            String cmdType = RecBuffer.Substring(4, 3);
            String rspType = RecBuffer.Substring(7, 3);
            MITLog.PrintLn("ProcessingError");

            //Stop wait for pinpad.
            bcom.setTimeOut(0);

            if (cmdType.Trim().Equals("E62"))
            {
                TRINP.pinpadrsp = "CANCELADO";
                return false;
            }
             
            if (cmdType.Trim().Equals("E71") && rspType.Trim().Equals("A10"))
            {
                Cancel("1");
                TRRSP.xml = xmlErrores.xmlPinPadError10;
                TRINP.chkPp_CdError = "10";
                return false;
            }

            if (rspType.Trim().Equals("A01"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError01;
                TRINP.chkPp_CdError = "01";
                return false;
            }
            else if (rspType.Trim().Equals("A02"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError02;
                TRINP.chkPp_CdError = "02";
                return false;
            }
            else if (rspType.Trim().Equals("A03"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError03;
                TRINP.chkPp_CdError = "03";
                return false;
            }
            else if (rspType.Trim().Equals("A04"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError04;
                TRINP.chkPp_CdError = "04";
                return false;
            }
            else if (rspType.Trim().Equals("A10"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError10;
                TRINP.chkPp_CdError = "10";
                return false;
            }
            else if (rspType.Trim().Equals("A11"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError11;
                TRINP.chkPp_CdError = "11";
                return false;
            }
            else if (rspType.Trim().Equals("A12"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError12;
                TRINP.chkPp_CdError = "12";
                return false;
            }
            else if (rspType.Trim().Equals("A13"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError13;
                TRINP.chkPp_CdError = "13";
                return false;
            }
            else if (rspType.Trim().Equals("A14"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError14;
                TRINP.chkPp_CdError = "14";
                return false;
            }
            else if (rspType.Trim().Equals("A15"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError15;
                TRINP.chkPp_CdError = "15";
                return false;
            }
            else if (rspType.Trim().Equals("A16"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError16;
                TRINP.chkPp_CdError = "16";
                return false;
            }
            else if (rspType.Trim().Equals("A17"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError17;
                TRINP.chkPp_CdError = "17";
                return false;
            }
            else if (rspType.Trim().Equals("A21"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError21;
                TRINP.chkPp_CdError = "21";
                return false;
            }
            else if (rspType.Trim().Equals("A22"))
            {
                TRRSP.xml = xmlErrores.xmlPinPadError22;
                TRINP.chkPp_CdError = "22";
                return false;
            }
            
            //pendiente
            //validar errores de pinpad
            TRINP.chkPp_CdError = "";
            TRINP.chkPp_XmlError = "";
            return true;
        }
        //Processing Cancel
        private void ProcessingCancel()
        {
            String RecBuffer = bcom.getRecBuf();

            MITLog.PrintLn("ProcessingCancel");
            String aux = RecBuffer.Substring(4, 6);
            if (RecBuffer.Substring(4, 6).Equals("PXVA00"))                                                  
            {
                MITLog.PrintLn("ProcessingCancel 1");
                TRRSP.xml = xmlErrores.xmlPinPadError10;
                TRINP.chkPp_CdError = "10";
            }

            //Stop wait for pinpad.
            bcom.setTimeOut(0);
        }

        //Comando C61 - FirmaPinPad
        public bool SendC61(string buffer)
        {
            String Cmd = "";
            
            if(buffer.Equals("00"))
            {
                Cmd = "C61"
                     + "A00" + bcom.getFS()
                     + "B " + bcom.getFS()
                     + "C60" + bcom.getFS()
                     + "DPNG";
            }
            else if (buffer.Equals("01"))
            {
                Cmd = "C61"
                     + "A01" + bcom.getFS()
                     + "DPNG";
            }
            else
            {
                Cmd = "C61"
                    + "A" + buffer;
            }
            //Timeout to wait response...
            bcom.setTimeOut(30);
            bcom.setCommContinue(true);
            return BuildCMD(Cmd);
        }

        //Processing P59 Command - Printer response.
        private void ProcessingP61()
        {
            String RecBuffer = bcom.getRecBuf();
            TRINP.pinpadrsp = getData("P61", bcom.getsETX(), RecBuffer);
            
            if (!TRINP.pinpadrsp.Substring(0, 3).Equals("A02"))
            {
                if (TRINP.pinpadrsp.Substring(0, 3).Equals("A00"))
                {
                    TRINP.NumeroBloquesFPP = getData(bcom.getFS() + "C", bcom.getFS() + "D", RecBuffer);
                    TRINP.FirmaPinPadByte = new byte[4094];
                    TRINP.contadorBytes = 0;
                }
                else
                {
                    //se captura la firma
                    //TRINP.FirmaPinPad += RecBuffer.Substring(RecBuffer.IndexOf("P61A") + 4, bcom.getLenRecBuf() - 10);
                    System.Buffer.BlockCopy(TRINP.BRecBuf, 8, TRINP.FirmaPinPadByte, TRINP.contadorBytes, bcom.getLenRecBuf() - 10);
                    TRINP.contadorBytes += bcom.getLenRecBuf() - 10;
                }
            }

            bcom.setCommContinue(false);
            bcom.setTimeOut(0);

        }

        //Comando C50 - Mensaje display
        public bool SendC50(string buffer)
        {
            String Cmd = "";

           Cmd = "C50"
                     + "A" + buffer;

            //Timeout to wait response...
            bcom.setTimeOut(0);
            bcom.setCommContinue(false);
            return BuildCMD(Cmd);
        }

        //Processing P50 Comand - 
        private void ProcessingP50()
        {
            String RecBuffer = bcom.getRecBuf();
            TRINP.pinpadrsp = getData("P50A", bcom.getsETX(), RecBuffer);
            bcom.setTimeOut(0);
        }


        //Comando C84 - pone  en modo carga a la terminal
        public bool SendC84()
        {
            String Cmd = "";

            Cmd = "C84A00";

            //Timeout to wait response...
            bcom.setTimeOut(30);
            bcom.setCommContinue(false);
            return BuildCMD(Cmd);

            ////cpComm.closePort
        }

        //Processing P50 Comand - 
        private void ProcessingP84()
        {
            String RecBuffer = bcom.getRecBuf();
            TRINP.pinpadrsp = getData("P84A", bcom.getsETX(), RecBuffer);
            bcom.setTimeOut(0);
        }

    }
}
