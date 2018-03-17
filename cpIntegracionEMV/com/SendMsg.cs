using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cpIntegracionEMV.util;

namespace cpIntegracionEMV.com
{
    class SendMsg
    {
        BeanCom bcom = new BeanCom();
        bool status=false;
        public bool _SendMsg(byte[] sendbuf, int lensendbuf) 
        {
            MITProtocol mprot = new MITProtocol();
            bcom.setSendBuf(sendbuf);
            bcom.setLenSendBuf(lensendbuf);

            if (bcom.getTypeCom().Equals("1")) //RS232
            {
                //COM / USB / RS232
                if(SendRS232())
                {
                    status=true;
                }
                //ETH / WIFI / LAN
                else if(SendIP())
                {

                }
            }
            return status;
        }
        //Send Port RS232.
        private bool SendRS232()
        {
            bool ReadStatus = false;
            bcom.setCommContinue(false);
            try
            {
                //bcom.setTimeOut(0);
                if (!RS232.IsOpen())
                {
                    RS232.SetConfig(bcom.getCom(), bcom.getBaudRate());
                }

                if (RS232.OpenPort())
                {
                    RS232.WritePort();
                    if (bcom.getCommContinue())
                    {
                        RS232.ReadPort();
                    }
                }
                else
                {
                    bcom.setfindDevice(false);
                    return false;
                }
                RS232.ClosePort();

                if (bcom.getstatusRead())
                {
                    ReadStatus = true;
                }
            }
            catch(Exception ex)
            {
                MITLog.PrintLn("SendMsg:"+ex.Message);
                return false;
            }
            return ReadStatus;
        }
        //Send Port IP.
        private bool SendIP()
        {
            return true;
        }
    }
}
