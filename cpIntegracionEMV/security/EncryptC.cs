using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace cpIntegracionEMV.security
{
    public static class EncryptC
    {

        private static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        //RSA
        private static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509key)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            MemoryStream mem = new MemoryStream(x509key);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                seq = binr.ReadBytes(15);       //read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8203)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00)     //expect null byte next
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                    lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                int modsize = BitConverter.ToInt32(modint, 0);

                byte firstbyte = binr.ReadByte();
                binr.BaseStream.Seek(-1, SeekOrigin.Current);

                if (firstbyte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it
                    binr.ReadByte();    //skip this null byte
                    modsize -= 1;   //reduce modulus buffer size by 1
                }

                byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                    return null;
                int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                byte[] exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAKeyInfo = new RSAParameters();
                RSAKeyInfo.Modulus = modulus;
                RSAKeyInfo.Exponent = exponent;
                RSA.ImportParameters(RSAKeyInfo);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }


        private static byte[] EncryptStringToBytes_Aes128(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.KeySize = 128;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.BlockSize = 128;
                aesAlg.FeedbackSize = 128;
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private static string DecryptStringFromBytes_Aes128(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.KeySize = 128;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.BlockSize = 128;
                aesAlg.FeedbackSize = 128;
                aesAlg.Key = Key;
                aesAlg.IV = IV;


                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return plaintext;
        }

        /*************** Public AES Functions *******************/
        /// <summary>
        /// Obtiene una llave AES de manera aleatoria
        /// </summary>
        /// <returns></returns>
        public static string ObtieneRandomAESKey()
        {
            RijndaelManaged myRijndael = new RijndaelManaged();

            myRijndael.Mode = CipherMode.CBC;
            myRijndael.KeySize = 128;
            myRijndael.Padding = PaddingMode.PKCS7;
            myRijndael.BlockSize = 128;
            myRijndael.FeedbackSize = 128;

            myRijndael.GenerateKey();
            myRijndael.GenerateIV();

            return ByteArrayToString(myRijndael.Key).ToUpper();
        }

        public static string encrypInAES128(string strKey, string strText)
        {
            try
            {

                string original = strText.Replace("\r\n", "");

                // Create a new instance of the Aes
                // class.  This generates a new key and initialization 
                // vector (IV).
                using (Aes myAes = Aes.Create())
                {
                    myAes.Mode = CipherMode.CBC;
                    myAes.KeySize = 128;
                    myAes.Padding = PaddingMode.PKCS7;
                    myAes.BlockSize = 128;
                    myAes.FeedbackSize = 128;
                    byte[] key = new byte[] { };
                    String result = "";

                    string str = strKey;

                    key = StringToByteArray(str);

                    myAes.Key = key;

                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncryptStringToBytes_Aes128(original, myAes.Key, myAes.IV);

                    byte[] resultado = new byte[encrypted.Length + myAes.IV.Length];
                    Array.Copy(myAes.IV, 0, resultado, 0, myAes.IV.Length);
                    Array.Copy(encrypted, 0, resultado, myAes.IV.Length, encrypted.Length);
                    String textBase64 = System.Convert.ToBase64String(resultado);
                    return result = textBase64;

                    Int32 ii;

                    result = "";
                    ii = 0;
                    foreach (Byte tmp in encrypted)
                    {
                        result += tmp.ToString("X2");
                        ii++;
                        if ((ii % 4) == 0)
                            result += "";
                    }

                    return result;

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return "";
            }
        }


        public static string descrypInAES128(string strKey, string strText)
        {
            try
            {

                string original = strText;

                // Create a new instance of the Aes
                // class.  This generates a new key and initialization 
                // vector (IV).
                using (Aes myAes = Aes.Create())
                {
                    myAes.Mode = CipherMode.CBC;
                    myAes.KeySize = 128;
                    myAes.Padding = PaddingMode.PKCS7;
                    myAes.BlockSize = 128;
                    myAes.FeedbackSize = 128;
                    byte[] key = new byte[] { };
                    byte[] xmlByte = new byte[] { };
                    String result = "";


                    string str = strKey;

                    key = StringToByteArray(str);

                    var base64EncodedBytes = System.Convert.FromBase64String(strText);
                    byte[] IVAES128 = new byte[16];
                    Array.Copy(base64EncodedBytes, 0, IVAES128, 0, 16);
                    myAes.IV = IVAES128;

                    base64EncodedBytes = System.Convert.FromBase64String(strText);
                    xmlByte = new byte[base64EncodedBytes.Length - 16];
                    Array.Copy(base64EncodedBytes, 16, xmlByte, 0, base64EncodedBytes.Length - 16);

                    myAes.Key = key;

                    // Encrypt the string to an array of bytes.
                    result = DecryptStringFromBytes_Aes128(xmlByte, myAes.Key, myAes.IV);

                    return result;

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return "";
            }
        }
        /*************** Public RSA functions *********************/
        /// <summary>
        /// Función para encriptar una cadena con algoritmo RSA
        /// </summary>
        /// <param name="input"></param>
        /// <param name="llave"></param>
        /// <returns></returns>
        public static string EncryptRSA(string texto, string publicKey)
        {
            RSACryptoServiceProvider rsa = DecodeX509PublicKey(Convert.FromBase64String(publicKey));
            return (Convert.ToBase64String(rsa.Encrypt(Encoding.ASCII.GetBytes(texto), false)));
        }

        /// <summary>
        /// Desencripta una cadena con algoritmo RSA
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string DecryptRSA(string encryptedText, string privateKey)
        {
            RSACryptoServiceProvider rsa = DecodeX509PublicKey(Convert.FromBase64String(privateKey));
            return (Convert.ToBase64String(rsa.Decrypt(Encoding.ASCII.GetBytes(encryptedText), false)));
        }
        /// <summary>
        /// Se obtiene el modulo y el exponente de una llave pública RSA
        /// </summary>
        /// <param name="llave">llave publica RSA</param>
        /// <returns></returns>
        public static string ObtieneDatosRSA(string llave)
        {
            byte[] data = Convert.FromBase64String(llave);

            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            MemoryStream mem = new MemoryStream(data);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                seq = binr.ReadBytes(15);       //read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8203)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00)     //expect null byte next
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                    lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                int modsize = BitConverter.ToInt32(modint, 0);

                byte firstbyte = binr.ReadByte();
                binr.BaseStream.Seek(-1, SeekOrigin.Current);

                if (firstbyte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it
                    binr.ReadByte();    //skip this null byte
                    modsize -= 1;   //reduce modulus buffer size by 1
                }

                byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                    return null;
                int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                byte[] exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAKeyInfo = new RSAParameters();
                RSAKeyInfo.Modulus = modulus;
                RSAKeyInfo.Exponent = exponent;
                RSA.ImportParameters(RSAKeyInfo);

                return ByteArrayToString(exponent) + ByteArrayToString(modulus);

            }
            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }
        /// <summary>
        /// Convierte una cadena Hex en Base64
        /// </summary>
        /// <param name="inputHex"></param>
        /// <returns></returns>
        public static string ConvierteHEX_To_BASE64(string inputHex)
        {
            inputHex = inputHex.Replace("-", "");

            byte[] resultantArray = new byte[inputHex.Length / 2];
            for (int i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = Convert.ToByte(inputHex.Substring(i * 2, 2), 16);
            }

            string base64 = Convert.ToBase64String(resultantArray);
            return base64;

        }

        public static String CodificaCaracteresXML(String data)
        {
            return data.Replace("%", "%25").Replace(" ", "%20").Replace("+", "%2B").Replace("=", "%3D").Replace("/", "%2F");
        }
        public static String DescodificaCaracteresXML(String data)
        {
            return data.Replace("%25", "%").Replace("%20", " ").Replace("%2B", "+").Replace("%3D", "=").Replace("%2F", "/");
        }


        #region  3DES

        public static string EncryptTripleDES(string toEncrypt, string key3DES)
        {

            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            
            string key = key3DES;
            //keyArray = UTF8Encoding.UTF8.GetBytes(key);

            keyArray = Convert.FromBase64String(key);


                ////MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                ////keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //////Always release the resources and flush data
                ////// of the Cryptographic service provide. Best Practice

                ////hashmd5.Clear();


            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            //tdes.Mode = CipherMode.CBC;
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;
            //tdes.Padding = PaddingMode.None;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);

        }

        public static string DecryptTripleDES(string cipherString, string key3DES)
        {

            byte[] keyArray;
            //get the byte code of the string
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            string key = key3DES;
            //keyArray = UTF8Encoding.UTF8.GetBytes(key);
            keyArray = Convert.FromBase64String(key);

                //////if hashing was used get the hash code with regards to your key
                ////MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                ////keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //////release any resource held by the MD5CryptoServiceProvider

                ////hashmd5.Clear();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);

        }

        #endregion


        #region 

        public static string TresDesEncrypt(String Text, String key)
        {
            try
            {
                byte[] bResult = new byte[] { };
                byte[] bText = new byte[] { };
                byte[] bKey = new byte[] { };
                String strResult = "";
                //StringToByteArray(Text);
                Text = ConvertStringToHex(Text);
                key = ConvertStringToHex(key);
                bText = StringToByteArray(Text);
                bKey = StringToByteArray(key);
                bResult = TripleDesEncryptOneBlock(bText, bKey);
                Int32 ii;

                strResult = "";
                ii = 0;
                foreach (Byte tmp in bResult)
                {
                    strResult += tmp.ToString("X2");
                    ii++;
                    if ((ii % 4) == 0)
                        strResult += "";
                }
                //strResult = strResult.Substring(0,strResult.Length - 16);  
                return strResult;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static string TresDesDescrypt(String Text, String key)
        {
            try
            {
                byte[] bResult = new byte[] { };
                byte[] bText = new byte[] { };
                byte[] bKey = new byte[] { };
                String strResult = "";
                //StringToByteArray(Text);
                //Text = ConvertStringToHex(Text);
                key = ConvertStringToHex(key);
                bText = StringToByteArray(Text);
                bKey = StringToByteArray(key);
                bResult = TripleDesDecryptBlock(bText, bKey);
                Int32 ii;

                strResult = "";
                ii = 0;
                foreach (Byte tmp in bResult)
                {
                    strResult += tmp.ToString("X2");
                    ii++;
                    if ((ii % 4) == 0)
                        strResult += "";
                }
                //strResult = strResult.Substring(0,strResult.Length - 16);  
                return ConvertHexToString(strResult);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        private static  byte[] TripleDesEncryptOneBlock(byte[] plainText, byte[] key)
        {
            // Create a new 3DES key.
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

            // Set the KeySize = 192 for 168-bit DES encryption.
            // The msb of each byte is a parity bit, so the key length is actually 168 bits.

            des.KeySize = 192;
            des.Key = key;
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            des.BlockSize = 64;
            des.FeedbackSize = 64;

            ICryptoTransform ic = des.CreateEncryptor();

            byte[] enc = ic.TransformFinalBlock(plainText, 0, plainText.Length);

            return enc;
        }

        private static  byte[] TripleDesDecryptBlock(byte[] plainText, byte[] key)
        {
            // Create a new 3DES key.
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

            // Set the KeySize = 192 for 168-bit DES encryption.
            // The msb of each byte is a parity bit, so the key length is actually 168 bits.
            des.KeySize = 192;
            des.Key = key;
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.None;
            des.BlockSize = 64;
            des.FeedbackSize = 64;

            ICryptoTransform ic = des.CreateDecryptor();

            byte[] dec = ic.TransformFinalBlock(plainText, 0, plainText.Length);

            return dec;
        }

        private static string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        private static string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return StrValue;
        }

        #endregion


    }
}
