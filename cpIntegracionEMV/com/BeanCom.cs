/*
 * BeanCom
 * lzuñiga 13/06/2016.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpIntegracionEMV.com
{
    class BeanCom
    {
        //1. Serie/RS232
        //2. Ethernet
        public static String TypeCom;
        public static String Com;
        public static String[] Coms;
        public static int BaudRate;
        public const int BuffSize = 2048;
        public static String RecBuf;
        public static byte[] SendBuf= new byte[BuffSize];
        public static int LenSendBuf;
        public static int LenRecBuf;
        public static bool CommContinue;
        public static int TimeOut;
        public static Boolean findDevice;
        public static Boolean statusRead;

        //Set communication type
        public void setTypeCom(String value)
        {
            TypeCom = value;
        }
        //get communication type
        public String getTypeCom()
        {
            return TypeCom;
        }
        //Set com port
        public void setCom(String value)
        {
            Com = value;
        }
        //get com port
        public String getCom()
        {
            return Com;
        }
        //Set coms port
        public void setComs(String[] value)
        {
            Coms = value;
        }
        //get coms port
        public String[] getComs()
        {
            return Coms;
        }
        //Set com port
        public void setBaudRate(int value)
        {
            BaudRate = value;
        }
        //get com port
        public int getBaudRate()
        {
            return BaudRate;
        }
        //get Buffer Size
        public int getBufSize() 
        {
            return BuffSize;
        }
        public byte getSTX()
        {
            return 0x02;
        }
        public String getsSTX()
        {
            byte[] bSTX = new byte[1];
            bSTX[0] = 0x02;
            return System.Text.Encoding.UTF8.GetString(bSTX);
        }
        public byte getETX()
        {
            return 0x03;
        }
        public String getsETX()
        {
            byte[] bETX = new byte[1];
            bETX[0] = 0x03;
            return System.Text.Encoding.UTF8.GetString(bETX);
        }
        public String getFS()
        {
            byte[] bFS = new byte[1];
            bFS[0] = 0x1C;
            return System.Text.Encoding.UTF8.GetString(bFS);
        }
        public String getNull()
        {
            byte[] bNull = new byte[1];
            bNull[0] = 0x00;
            return System.Text.Encoding.UTF8.GetString(bNull);
        }
        //set RecBuf
        public void setRecBuf(String value)
        {
            //Array.Clear(RecBuf, 0, RecBuf.Length);
            RecBuf = value;
        }
        //get RecBuf
        public String getRecBuf()
        {
            return RecBuf;
        }
        //set LenRecBuf
        public void setLenRecBuf(int value)
        {
            LenRecBuf = value;
        }
        //get SendBuf
        public int getLenRecBuf()
        {
            return LenRecBuf;
        }
        //set SendBuf
        public void setSendBuf(byte[] value)
        {
            Array.Clear(SendBuf, 0, SendBuf.Length);
            SendBuf = value;
        }
        //get SendBuf
        public byte[] getSendBuf()
        {
            return SendBuf;
        }
        //set LenSendBuf
        public void setLenSendBuf(int value)
        {
            LenSendBuf = value;
        }
        //get SendBuf
        public int getLenSendBuf()
        {
            return LenSendBuf;
        }
        //set CommContinue
        public void setCommContinue(bool value)
        {
            CommContinue = value;
        }
        //get CommContinue
        public bool getCommContinue()
        {
            return CommContinue;
        }
        //set TimeOut
        public void setTimeOut(int value)
        {
            TimeOut = value;
        }
        //get TimeOut
        public int getTimeOut()
        {
            return TimeOut;
        }
        //set findDevice
        public void setfindDevice(Boolean value)
        {
            findDevice = value;
        }
        //get findDevice
        public Boolean getfindDevice()
        {
            return findDevice;
        }
        //set statusRead
        public void setstatusRead(Boolean value)
        {
            statusRead = value;
        }
        //get statusRead
        public Boolean getstatusRead()
        {
            return statusRead;
        }
    }
}
