using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using cpIntegracionEMV.data; 
using System.Windows.Forms;

namespace cpIntegracionEMV.com
{
    public static class RS232
    {
        static bool _continue;
        static SerialPort _serialPort = new SerialPort();
        static BeanCom bcom = new BeanCom();
        
        public static String[] findPorts()
        {
            return SerialPort.GetPortNames();
        }
        //Set configuration RS232
        public static bool SetConfig(String com, int BaudRate)
        {
            if (bcom.getCom() != null)
            {
                // Create a new SerialPort object with default settings.
                // Allow the user to set the appropriate properties.
                _serialPort.PortName = bcom.getCom();
                _serialPort.BaudRate = bcom.getBaudRate();
                _serialPort.Parity = Parity.None;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.Handshake = Handshake.None;

                // Set the read/write timeouts
                _serialPort.ReadTimeout = 10000;
                _serialPort.WriteTimeout = 1000;

                return true;
            }
            else
            {
                return false;
            }                
        }

        //Open Port RS232.
        public static bool OpenPort()
        {
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }                
            }
            catch(System.IO.IOException)
            {
                return false;
            }
            return true;
        }
        //Is Open the port
        public static bool IsOpen()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.IO.IOException)
            {
                return false;
            }
        }
        //Write Port RS232.
        public static void WritePort()
        {
            _serialPort.Write(bcom.getSendBuf(), 0, bcom.getLenSendBuf());
            ReadPort();
        }
        
        //Read Port RS232.
        public static void ReadPort()
        {
            MITProtocol mp = new MITProtocol();
            Thread readThread;
            try
            {
                bcom.setstatusRead(false);
                readThread = new Thread(new ThreadStart(Read));
                readThread.IsBackground = true;
                readThread.Start();

                while (readThread.IsAlive)
                {
                    Application.DoEvents();
                }
                readThread.Abort();
                readThread.Join();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //Thread Read Port RS232.
        private static void Read()
        {
            MITProtocol mp = new MITProtocol();
            TRINP.thread = true;
            do
            {
                byte[] RecBuf = new byte[bcom.getBufSize()];
                int contBytes = 0;
                
                _continue = true;
                while (_continue)
                {
                    try
                    {
                        //tiempo de espera para lectura del puerto
                        Thread.Sleep(1000); 
                        //Bytes leidos
                        int lenbuff = _serialPort.BytesToRead;
                        if (lenbuff > 0)
                        {
                            byte[] buffer = new byte[lenbuff];
                            _serialPort.Read(buffer, 0, lenbuff);
                            System.Buffer.BlockCopy(buffer, 0, RecBuf, contBytes, lenbuff);
                            contBytes = contBytes + lenbuff;
                        }
                        else
                        {
                            _continue = false;
                        }
                    }
                    catch (TimeoutException) 
                    {
                        _continue = false;
                    }
                }

                TRINP.BRecBuf = RecBuf;

                bcom.setRecBuf(System.Text.Encoding.UTF8.GetString(RecBuf));
                bcom.setLenRecBuf(contBytes);

                if (mp.ProcesaRsp() && (bcom.getLenRecBuf() > 0))
                {
                    bcom.setRecBuf("");
                    bcom.setLenRecBuf(0);
                    bcom.setstatusRead(true);
                }
                bcom.setTimeOut(bcom.getTimeOut() - 1);
                if (TRINP.cancelop)
                {
                    bcom.setTimeOut(0);
                    TRINP.cancelop = false;
                }
            } while (bcom.getTimeOut() > 0);
            TRINP.thread = false;
        }
        //Close Port RS232
        public static void ClosePort()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

    }
}
