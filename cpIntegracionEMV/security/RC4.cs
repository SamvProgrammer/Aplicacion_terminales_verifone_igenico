using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpIntegracionEMV.security
{
    public static class RC4
    {
        public static String Encrypt(String data, String key)
        {
            return BitConverter.ToString(Encrypt(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(data))).Replace("-", "");
        }
        public static String Decrypt(String data, String key)
        {

            return DecryptRC4(data, key);
        }
        private static byte[] Encrypt(byte[] pwd, byte[] data)
        {
            int a, i, j, k, tmp;
            int[] key, box;
            byte[] cipher;

            key = new int[256];
            box = new int[256];
            cipher = new byte[data.Length];

            for (i = 0; i < 256; i++)
            {
                key[i] = pwd[i % pwd.Length];
                box[i] = i;
            }
            for (j = i = 0; i < 256; i++)
            {
                j = (j + box[i] + key[i]) % 256;
                tmp = box[i];
                box[i] = box[j];
                box[j] = tmp;
            }
            for (a = j = i = 0; i < data.Length; i++)
            {
                a++;
                a %= 256;
                j += box[a];
                j %= 256;
                tmp = box[a];
                box[a] = box[j];
                box[j] = tmp;
                k = box[((box[a] + box[j]) % 256)];
                cipher[i] = (byte)(data[i] ^ k);
            }
            return cipher;
        }

        private static byte[] Decrypt(byte[] pwd, byte[] data)
        {
            return Encrypt(pwd, data);
        }
        
        #region RC4

        private static string EncryptRC4(string data, string llave)
        {

            return StrToHexStr(EnDeCrypt(llave, data));
        }

        private static string DecryptRC4(string data, string llave)
        {
            string text;
            text = HexStrToStr(data);
            text = EnDeCrypt(llave, text);

            return text;
        }

        private static string EnDeCrypt(string password, string text)
        {
            const int N = 256;
            int[] sbox;

            sbox = new int[N];
            int[] key = new int[N];
            int n = password.Length;
            for (int a = 0; a < N; a++)
            {
                key[a] = (int)password[a % n];
                sbox[a] = a;
            }

            int b = 0;
            for (int a = 0; a < N; a++)
            {
                b = (b + sbox[a] + key[a]) % N;
                int tempSwap = sbox[a];
                sbox[a] = sbox[b];
                sbox[b] = tempSwap;
            }

            int i = 0, j = 0, k = 0;
            StringBuilder cipher = new StringBuilder();
            for (int a = 0; a < text.Length; a++)
            {
                i = (i + 1) % N;
                j = (j + sbox[i]) % N;
                int tempSwap = sbox[i];
                sbox[i] = sbox[j];
                sbox[j] = tempSwap;

                k = sbox[(sbox[i] + sbox[j]) % N];
                int cipherBy = ((int)text[a]) ^ k;
                cipher.Append(Convert.ToChar(cipherBy));
            }

            return cipher.ToString();

        }

        private static string StrToHexStr(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                int v = Convert.ToInt32(str[i]);
                sb.Append(string.Format("{0:X2}", v));
            }
            return sb.ToString();
        }

        private static string HexStrToStr(string hexStr)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hexStr.Length; i += 2)
            {
                int n = Convert.ToInt32(hexStr.Substring(i, 2), 16);
                sb.Append(Convert.ToChar(n));
            }
            return sb.ToString();
        }


        #endregion
    }
}
