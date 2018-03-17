using cpIntegracionEMV.data;
using cpIntegracionEMV.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using cpIntegracionEMV.com;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;
using System.Net;
using cpIntegracionEMV.txn_flow;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.IO.Compression;


namespace cpIntegracionEMV.util
{
    public static class utilidadesMIT
    {

        private static  MITProtocol mp = new MITProtocol();
        
        public static string VerificaVoucher(string strVoucherCoP, [Optional]string leyenda, [Optional]string NumOpe)
        {
            string Impresora;
            string Legend;

            strVoucherCoP = strVoucherCoP.Replace("&amp;", "&");

            if (!strVoucherCoP.Equals(""))
            {
                if (!strVoucherCoP.Contains(":"))
                {
                    strVoucherCoP = strVoucherCoP.Replace("@","");
                    strVoucherCoP = RC4.Decrypt(strVoucherCoP, Info.RC4Key);
                }

                if(strVoucherCoP.Contains("voucher_cliente"))
                {
                    Impresora = ObtieneParametrosMIT("Printer");
                    Legend = ObtieneParametrosMIT("Legend");

                    if (!Impresora.Equals("1"))
                        strVoucherCoP = strVoucherCoP.Replace("@bc " + NumOpe, "");

                    if (!string.IsNullOrEmpty(leyenda))
                        strVoucherCoP = strVoucherCoP + leyenda;

                    strVoucherCoP = strVoucherCoP.Substring(0, strVoucherCoP.Length - 1);
                    strVoucherCoP = strVoucherCoP.Replace(Convert.ToChar(3).ToString(), "");
                    strVoucherCoP = strVoucherCoP.Replace("MXN", "MXN  ");

                    strVoucherCoP = QuitaAcentos(strVoucherCoP);

                    ////if( cpComm.chkPp_Printer = "0" Or cpComm.chkPp_Printer = "" )
                    ////{
                    ////    RevisaVoucher(strVoucherCoP);
                    ////}
                }
                ////else
                ////{
                ////    string FinVoucher = "@br @logo3 @br " + Info.dll_version + "@br @br ";

                ////    strVoucherCoP = strVoucherCoP.Substring(0, strVoucherCoP.Length - 1);
                ////    strVoucherCoP = strVoucherCoP.Replace(Convert.ToChar(3).ToString(), "");
                ////    strVoucherCoP = strVoucherCoP.Replace("MXN", "MXN  ");

                ////    strVoucherCoP = strVoucherCoP + FinVoucher;

                ////}
                

            }

            return strVoucherCoP;

        }

        public static string  RevisaVoucher(string voucher)
        {
            voucher = voucher.Replace("@", " @");
            voucher = voucher.Replace("@cnb logo_cpagos", "");
            voucher = voucher.Replace("@cnn ver_app", "");
            voucher = voucher.Replace("  @", "@");
            voucher = voucher.Replace("   @", "@");

            return voucher;
        }

        public static string QuitaAcentos(string cadena)
        {
            cadena = cadena.Replace("Á", "A");
            cadena = cadena.Replace("É", "E");
            cadena = cadena.Replace("Í", "I");
            cadena = cadena.Replace("Ó", "O");
            cadena = cadena.Replace("Ú", "U");
    
            cadena = cadena.Replace("á", "a");
            cadena = cadena.Replace("é", "e");
            cadena = cadena.Replace("í", "i");
            cadena = cadena.Replace("ó", "o");
            cadena = cadena.Replace("ú", "u");
    
            cadena = cadena.Replace("ñ", "n");
            cadena = cadena.Replace("Ñ", "N");
    
            cadena =cadena.Replace("Electr?a", "Electronica");

            return cadena;
        }

        public static void dbgPrintTwoVouchers(string VoucherCompany, string VoucherClient, [Optional] string Publicidad)
        {

            string VoucherAgencia = "";
            string finvouher = "@br@br@br@br";

            if (!VoucherCompany.Contains(":"))
                VoucherCompany = utilidadesMIT.VerificaVoucher(VoucherCompany);

            if (!VoucherClient.Contains(":"))
                VoucherClient = utilidadesMIT.VerificaVoucher(VoucherClient);

            if(TypeUsuario.dbgGetIsAgencia.Equals("1"))
            {
                VoucherAgencia = VoucherCompany.Replace("-C-O-M-E-R-C-I-O-", "-A-G-E-N-C-I-A-");
                VoucherCompany = VoucherCompany.Replace("-C-O-M-E-R-C-I-O-", "-A-E-R-O-L-I-N-E-A-");
            }

            VoucherCompany = TrataVoucher(VoucherCompany);
            VoucherClient = TrataVoucher(VoucherClient);
            VoucherAgencia = TrataVoucher(VoucherAgencia);

            VoucherCompany = QuitaAcentos(VoucherCompany);
            VoucherAgencia = QuitaAcentos(VoucherAgencia);
            VoucherClient = QuitaAcentos(VoucherClient);

            //imprime voucher comercio

            string[] linea = VoucherCompany.Split('@');

            if (linea.Length < 35)
                mp.SendC59(VoucherCompany + finvouher);
            else
                ImprimeBloque(linea);

            Thread.Sleep(2000);

            //imprime voucher agencia
            if (TypeUsuario.dbgGetIsAgencia.Equals("1"))
            {
                linea = VoucherAgencia.Split('@');

                if (linea.Length < 35)
                    mp.SendC59(VoucherAgencia + finvouher);
                else
                    ImprimeBloque(linea);

                Thread.Sleep(2000);
            }

            //imprime voucher cliente
            linea = VoucherClient.Split('@');

            if (linea.Length < 35)
                mp.SendC59(VoucherClient + finvouher);
            else
                ImprimeBloque(linea);
        }

        public static string TrataVoucher(string voucher)
        {
            string cp;
            string version;

            voucher = TrataRenglon(voucher);

            if (voucher.Contains("@cnb Santander"))
                voucher = voucher.Replace("@cnb Santander", "@logo1 @br");
            else if (voucher.Contains("@cnb American Express"))
                voucher = voucher.Replace("@cnb American Express", "@logo2 @br");
            else if( voucher.Contains("@cnb HSBC"))
                voucher = voucher.Replace("@cnb HSBC", "@logo7 @br");
            else if(voucher.Contains("@cnb IXE") )
                voucher = voucher.Replace("@cnb IXE", "@logo11 @br");
            else if( voucher.Contains("@cnb MULTIVA") )
                voucher = voucher.Replace("@cnb MULTIVA", "@logo15 @br");
            else if(voucher.Contains("@cnb SCOTIA BANK"))
                voucher = voucher.Replace("@cnb SCOTIA BANK", "@logo16 @br");
            else if(voucher.Contains("@cnb BANCOMER") )
                voucher = voucher.Replace("@cnb BANCOMER", "@logo17 @br");
            
            cp = "@logo3 @br";
            version = "@cnn " + Info.dll_version;
            voucher = voucher.Replace( "@", " @");

            //if(!voucher.Equals(""))
            //    voucher = voucher.Replace(voucher.Substring(voucher.Length - 13),"");

            voucher = voucher.Replace("@cnb logo_cpagos", cp);
            voucher = voucher.Replace("@cnn ver_app", version);
            voucher = voucher.Replace("  @", "@");
            voucher = voucher.Replace("   @", "@");

            if(voucher.Contains("@lsn POR ESTE PAGARE ME OBLIGO INCONDI"))
            {
                int posLeyenda = voucher.IndexOf("@lsn POR ESTE PAGARE ME OBLIGO INCONDI");
                voucher = voucher.Substring(0,posLeyenda);
            }


            voucher = voucher.Replace("\r", "");
            voucher = voucher.Replace("\n", "");
            voucher = voucher.Trim();

            return voucher;
        }

        private static string TrataRenglon(string voucher)
        {

            string strLinea = "";
            string cadena = "";

            string[] linea = voucher.Split('@');

            for (int i = 0; i < linea.Length; i++)
            {
                strLinea = linea[i];

                if(strLinea.Contains("EUR") )
                    strLinea = strLinea.Replace("$", "€");
                else if (strLinea.Contains("GBP") )
                    strLinea = strLinea.Replace("$", "£");
                else if (strLinea.Contains("JPY"))
                    strLinea = strLinea.Replace("$", "¥");

                if(strLinea.Contains("cnn") || strLinea.Contains("lnn") ) //hasta 40 caracteres
                {
                    if (strLinea.Length > 44)
                    {
                        if (strLinea.Contains("cnn"))
                            strLinea = strLinea.Substring(0, 43) + " @cnn " + strLinea.Substring(44);
                        else
                            strLinea = strLinea.Substring(0, 43) + " @lnn " + strLinea.Substring(44);
                    }
                }
                else if (strLinea.Contains("cnb") || strLinea.Contains("lnb"))  //hasta 30 caracteres
                {
                    if (strLinea.Length > 34)
                    {
                        if (strLinea.Contains("cnb"))
                            strLinea = strLinea.Substring(0, 33) + " @cnb " + strLinea.Substring(0, 34);
                        else
                            strLinea = strLinea.Substring(0, 33) + " @lnb " + strLinea.Substring(0, 34);
                    }
                    
                }
                else if (strLinea.Contains("cbb") || strLinea.Contains("lbb"))  //hasta 20 caracteres
                {
                    if (strLinea.Length > 24)
                    {
                        if (strLinea.Contains("cbb"))
                            strLinea = strLinea.Substring(0, 23) + " @cbb " + strLinea.Substring(0, 24);
                        else
                            strLinea = strLinea.Substring(0, 23) + " @lbb " + strLinea.Substring(0, 24);
                            
                    }
                }

                cadena += "@" + strLinea.Trim();

            }

                return voucher;
        }

        /// <summary>
        /// LEE EL ARCHIVO PARAMETERS.MIT Y OBTIENE EL VALOR DEL PARAMETRO ENVIADO
        /// </summary>
        /// <param name="parametro">Parametro a buscar</param>
        /// <returns></returns>
        public static string ObtieneParametrosMIT(string parametro)
        {
            string parameter = "";
            string text;
            string file;
            string ruta;

            ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            file = System.IO.Path.Combine(ruta + "\\MIT\\Data", "parameters.mit");

            if (System.IO.File.Exists(file))
            {
                text = System.IO.File.ReadAllText(file);

                //DESCIFRA EL CONTENIDO DEL ARCHIVO PARAMETERS.MIT
                text = RC4.Decrypt(text, Info.RC4Key);

                //SE OBTIENE EL VALOR DEL XML
                parameter = GetDataXML(parametro, text);
            }
            else
            {
                parameter = "";
            }

            return parameter;

        }

        /// <summary>
        /// GUARDA EN EL ARCHIVO PARAMETERS.MIT, EL PARAMETRO INDICADO
        /// </summary>
        /// <param name="parametro">parametro a guardar</param>
        /// <param name="valor">valor del parametro</param>
        /// <returns></returns>
        public static bool GuardaParametrosMIT(string parametro, string valor)
        {
            bool guardar = false;
            string parameter = "";
            string text;
            string file;
            string ruta;

            ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            file = System.IO.Path.Combine(ruta + "\\MIT\\Data", "parameters.mit");

            //se verica que exista el archivo
            if (!System.IO.File.Exists(file))
            {
                if (CreaCarpetaMIT())
                    System.IO.File.Create(file);
            }


            text = System.IO.File.ReadAllText(file);

            //DESCIFRA EL CONTENIDO DEL ARCHIVO PARAMETERS.MIT
            text = RC4.Decrypt(text, Info.RC4Key);
            
            //SE OBTIENE EL VALOR DEL XML
            parameter = GetDataXML(parametro, text);

            //se sustituye el valor anterior
            string valorOriginal = "";
            string valorNuevo = "";

            if (!parameter.Equals(""))
            {
                valorOriginal = "<" + parametro + ">" + parameter + "</" + parametro + ">";
                valorNuevo = "<" + parametro + ">" + valor + "</" + parametro + ">";
                text = text.Replace(valorOriginal, valorNuevo);
            }
            else
            {
                valorOriginal = "<" + parametro + ">" + parameter + "</" + parametro + ">";
                valorNuevo = "<" + parametro + ">" + valor + "</" + parametro + ">";
                text = text.Replace(valorOriginal, "");
                text = text + valorNuevo;
            }


            //se guarda el archivo nuevamente
            System.IO.File.WriteAllText(file, RC4.Encrypt(text, Info.RC4Key));

            return guardar;


        }

        /// <summary>
        /// Crea la carpeta MIT
        /// </summary>
        /// <returns></returns>
        private static bool CreaCarpetaMIT()
        {
            bool carpetaCreada = false;
            string directorio;
            string ruta;


            try
            {
                ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                directorio = ruta + "\\MIT";
                if (!System.IO.Directory.Exists(directorio))
                    System.IO.Directory.CreateDirectory(directorio);

                directorio = ruta + "\\MIT\\Data";
                if (!System.IO.Directory.Exists(directorio))
                    System.IO.Directory.CreateDirectory(directorio);

                directorio = ruta + "\\MIT\\Load";
                if (!System.IO.Directory.Exists(directorio))
                    System.IO.Directory.CreateDirectory(directorio);

                directorio = ruta + "\\MIT\\Log";
                if (!System.IO.Directory.Exists(directorio))
                    System.IO.Directory.CreateDirectory(directorio);


                carpetaCreada = true;
            }
            catch (Exception)
            {

                carpetaCreada = false;
            }


            return carpetaCreada;
        }


        //Get XML value response.
        public static string GetDataXML(String Tag, String Buffer)
        {
            String tagIni = "<" + Tag + ">";
            String tagFin = "</" + Tag + ">";
            int iIni = Buffer.IndexOf(tagIni);
            int iFin = Buffer.IndexOf(tagFin);
            if ((iIni >= 0) && (iFin > iIni))
            {
                return Buffer.Substring(iIni + tagIni.Length, iFin - iIni - tagIni.Length);
            }
            return "";
        }

        /// <summary>
        /// Crea un archivo .hta para visualizar archivos HTML
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool ImprimeHTML(string text)
        {
            bool creado = false;


            try
            {
                string fileName;
                string ruta;
                string sourceFile;
                //ruta = Environment.CurrentDirectory;
                ruta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                fileName = "voucher.hta";
                sourceFile = System.IO.Path.Combine(ruta, fileName);
                System.IO.File.WriteAllText(sourceFile, text);

                if (System.IO.File.Exists(sourceFile))
                {
                    Process.Start(sourceFile);
                }
                else
                {
                    MessageBox.Show("Archivo no encontrado - " + sourceFile);
                }

                creado = true;
            }
            catch
            {
                creado = false;
            }


            return creado;

        }

        public static bool isNumeric(KeyPressEventArgs e, string texto)
        {
            try
            {
                if (e.KeyChar == 8)
                {
                    return false;
                }

                if (e.KeyChar >= 48 && e.KeyChar <= 57)
                {
                    return false;
                }
                else { return true; }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                throw;
            }
        }

        public static bool IsNumber(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }


        /// <summary>
        /// Retorna la version de la DLL
        /// </summary>
        /// <returns></returns>
        public static string GetVersionDLL()
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;
            return ver.Major + "." + ver.Minor + "." + ver.Revision;
        }

        /// <summary>
        /// Retorna la version de la aplicación (pcpay/integrador)
        /// </summary>
        /// <returns></returns>
        public static string GetVersionApp()
        {
            string verAPP;
            verAPP = ObtieneParametrosMIT("VERSION");

            if(!verAPP.Equals(""))
                return verAPP;
            else
                return "integraemv " + GetVersionDLL();

        }

        public static bool ExisteArchivo(string fileName)
        {
            string ruta;
            ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            ruta += "\\MIT\\Data\\" + fileName;

            return System.IO.File.Exists(ruta);
             
        }

        /// <summary>
        /// Valida que exista una carpeta
        /// </summary>
        /// <param name="carpeta"></param>
        /// <returns></returns>
        public static bool ExisteCarpeta(string carpeta)
        {
            return System.IO.Directory.Exists(carpeta);
        }

        /// <summary>
        /// Crea carpeta
        /// </summary>
        /// <param name="carpeta"></param>
        /// <returns></returns>
        public static bool CreaCarpeta(string carpeta)
        {
            try
            {
                System.IO.Directory.CreateDirectory(carpeta);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool EncriptaBines(string fileName)
        {
            string text;

            if (System.IO.File.Exists(fileName))
            {
                text = System.IO.File.ReadAllText(fileName);
                text = RC4.Encrypt(text, Info.RC4Key);

                //se guarda el archivo
                System.IO.File.WriteAllText(fileName, text);

                return true;
            }

            return false;

        }

        public static string DesencriptaBines(string fileName)
        {
            string ruta;
            string file;
            string text;
            ruta = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            file = System.IO.Path.Combine(ruta + "\\MIT\\Data", fileName);

            if (ExisteArchivo(fileName))
            {
                text = System.IO.File.ReadAllText(file);
                text = text.Replace("\r\n","");
                text = RC4.Decrypt(text, Info.RC4Key);

                if (!text.Equals(""))
                    return text;
                else
                    return "";
            }
            else
                return "";
            
        }

        public static bool ValidaBinAMEX(string binAmex)
        {
            string bines;
            int binIni;
            int binFin;
            int bin;
            string[] binTarjeta;
            
            bines = utilidadesMIT.DesencriptaBines("amex.txt");
            bines = bines.Replace("\r\n", "$");

            if (bines.Equals(""))
                return false;
            else
            {
                if (binAmex.Length == 6)
                    binAmex += "00";
                
                binTarjeta = bines.Split('$');

                for (int i = 0; i < binTarjeta.Length;i++)
                {
                    int.TryParse(binTarjeta[i].Substring(0, 8), out binIni);
                    int.TryParse(binTarjeta[i].Substring(9),out binFin);
                    int.TryParse(binAmex, out bin);

                    if (bin >= binIni && bin <= binFin)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// descarga el archivo indicado
        /// </summary>
        /// <param name="pathXML"></param>
        /// <param name="nameFile"></param>
        /// <param name="nombreCarpeta"></param>
        public static void DownloadFile(string urlDescarga, string path)
        {
            
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(urlDescarga, path);
            }
            catch (Exception ex)
            {
                MITLog.PrintLn(ex.Message);
            }

        }

        /// <summary>
        /// convierte una cadena base64 en hexadecimal
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Base64ToHEX(string strBase64)
        {
            byte[] ba = Encoding.Default.GetBytes(strBase64);
            var hexString = BitConverter.ToString(ba);
            hexString = hexString.Replace("-", "");
            return hexString;
        }

        /// <summary>
        /// convierte una cadena en hexadecimal a base64
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string HEXToBase64(string hexString)
        {
            if (hexString == null)
                throw new ArgumentNullException("hexString");
            if (hexString.Length % 2 != 0)
                throw new ArgumentException("hexString must have an even length", "hexString");
            var bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                string currentHex = hexString.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(currentHex, 16);
            }
            //return bytes;
            return Encoding.GetEncoding("UTF-8").GetString(bytes);
        }

        /// <summary>
        /// Establece un xml de error
        /// </summary>
        public static string SetError(string Num, string Desc)
        {
            string error = xmlErrores.xmlApplicationError; 
            error = error.Replace("#ERROR", Num);
            error = error.Replace("$ERROR", Desc);
            return error;    
        }

        public static bool isAmount(KeyPressEventArgs e, string texto)
        {
            bool IsDec = false;
            int nroDec = 0;

            try
            {
                if (e.KeyChar == 8) { return false; }

                for (int i = 0; i < texto.Length; i++)
                {
                    if (texto[i] == '.') { IsDec = true; }
                    if (IsDec && nroDec++ >= 2) { return true; }
                }

                if (e.KeyChar >= 48 && e.KeyChar <= 57)
                {
                    return false;
                }
                else if (e.KeyChar == 46)
                {
                    if (IsDec) { return true; }
                    else { return false; }
                }
                else { return true; }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                throw;
            }
        }


        public static string FormatoNumero(string value)
        {
            if (value.Equals(""))
                return "0.00";

            double valor;
            double.TryParse(value, out valor);
            return String.Format(CultureInfo.InvariantCulture, "{0:0.00}", valor);
        }

        /// <summary>
        /// Compare versions of form "1,2,3,4" or "1.2.3.4". Throws FormatException
        /// in case of invalid version.
        /// </summary>
        /// <param name="strA">the first version</param>
        /// <param name="strB">the second version</param>
        /// <returns>less than zero if strA is less than strB,
        /// equal to zero if strA equals strB,
        /// and greater than zero if strA is greater than strB</returns>
        public static int CompareVersions(String strA, String strB)
        {
            Version vA = new Version(strA.Replace(",", "."));
            Version vB = new Version(strB.Replace(",", "."));

            return  vA.CompareTo(vB);
            
            //if (a <= 0)
            //    return true;
            //else
            //    return false;
        }

        public static void UnzipFile(string pathFile, string pathExtract)
        {
            if (!Directory.Exists(pathExtract))
                Directory.CreateDirectory(pathExtract);

            ZipFile.ExtractToDirectory(pathFile, pathExtract);
        }

        public static void CreateBat(string nameBat, string line, [Optional]string line2, [Optional]string pathSave)
        {
            string contenidoBat;
            contenidoBat = line;

            if(!line2.Equals(""))
                contenidoBat = contenidoBat + "\r\n" + line2;
            
            System.IO.File.WriteAllText(pathSave + nameBat, contenidoBat);
        }

        public static string ValidaCadena(String cadena)
        {
            if (cadena.Equals("") || cadena.Contains("<html>"))
                return cadena;

            cadena = cadena.Replace("aaa", "á");
            cadena = cadena.Replace("eee", "é");
            cadena = cadena.Replace("iii", "í");
            cadena = cadena.Replace("ooo", "ó");
            cadena = cadena.Replace("uuu", "ú");
            cadena = cadena.Replace("NNN", "Ñ");
            cadena = cadena.Replace("nnn", "ñ");

            return cadena;
        }

        private static void ImprimeBloque(string[] linea)
        {
            int lineaMax = 30;
            int cont = 0;
            string cadena = "";

            for (int i = 0; i < linea.Length; i++)
            {

                cont++;

                if (linea[i] == "")
                    cadena = cadena + linea[i];
                else
                    cadena = cadena + " @" + linea[i];

                if (cont.Equals(lineaMax))
                {
                    mp.SendC59(cadena.Trim());
                    cadena = "";
                    cont = 0;
                }

            }

            if (!cadena.Equals(""))
            {
                Thread.Sleep(500);
                mp.SendC59(cadena.Trim() + "@br@br@br@br@br@br"); 
            }
        }

       

    }
}
