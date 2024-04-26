using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Token
{
    public static class Encrypt
    {
        private static ushort C1 = 52845;
        private static ushort C2 = 22719;
        private static ushort key = 11548;

        /// <summary>
        /// Criptografa valores em md5.
        /// </summary>
        /// <param name="chave"></param>
        /// <returns></returns>
        public static string ToMD5(string chave)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(chave));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }

        /// <summary>
        /// Criptografia legada de token.
        /// </summary>
        /// <param name="chave"></param>
        /// <returns></returns>
        public static string ToLegacyEncrypt(string chave)
        {
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(chave);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes($"sisandVisionHash{DateTime.Now.ToString("dd/MM/yyyy")}"));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Criptografa valores em SHA384.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSHA384(string value)
        {
            using (SHA384 sha384Hash = SHA384.Create())
            {
                byte[] bytes = sha384Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));

                return builder.ToString();
            }
        }

        /// <summary>
        /// Descriptografa senha.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string DecryptPassword(string s) => InternalDecrypt(PreProcess(s));

        /// <summary>
        /// Criptografa senha.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EncriptyPassword(string s) => PostProcess(InternalEncrypt(s));

        /// <summary>
        /// Interno.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * 5];
            for (int i = 0; i < str.Length; i++)
            {
                bytes[i] = (byte)str[i];
            }
            return bytes;
        }
        /// <summary>
        /// Interno.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string Encode(string s)
        {
            string sMap = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            char[] map = sMap.ToCharArray();
            byte[] b = GetBytes(s);
            int i = BitConverter.ToInt32(b, 0);
            string ret = "";
            switch (s.Length)
            {
                case 1: ret = map[i % 64].ToString() + map[((i >> 6) % 64)].ToString(); break;
                case 2: ret = map[i % 64].ToString() + map[((i >> 6) % 64)].ToString() + map[((i >> 12) % 64)].ToString(); break;
                case 3: ret = map[i % 64].ToString() + map[((i >> 6) % 64)].ToString() + map[((i >> 12) % 64)].ToString() + map[((i >> 18) % 64)].ToString(); break;
            }


            return ret;
        }
        /// <summary>
        /// Interno.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string PostProcess(string s)
        {
            string ss = s;
            StringBuilder ret = new StringBuilder();
            while (ss.Length > 0)
            {
                ret.Append(Encode(ss.Substring(0, (ss.Length >= 3 ? 3 : ss.Length))));
                ss = ss.Remove(0, (ss.Length >= 3 ? 3 : ss.Length));
            }
            return ret.ToString();
        }
        /// <summary>
        /// Interno.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string InternalEncrypt(string s)
        {
            int i;
            ushort seed;
            char[] ret;
            ret = s.ToCharArray();
            seed = key;
            for (i = 0; i < s.Length; i++)
            {
                ret[i] = (char)((ret[i]) ^ (seed >> 8));
                seed = (ushort)((ret[i] + seed) * C1 + C2);

            }
            return new string(ret);
        }
        /// <summary>
        /// Interno.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string Decode(string s)
        {
            byte[] map = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,62,0,0,0,63,52,53,54,55,56,57,58,59,
60,61,0,0,0,0,0,0,0,0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,
18,19,20,21,22,23,24,25,0,0,0,0,0,0,26,27,28,29,30,31,32,33,34,35,36,
37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
0,0};
            long i;
            string ret = "";
            byte[] temp;
            switch (s.Length)
            {
                case 2:
                    {
                        i = map[(byte)s[0]] + (map[s[1]] << 6);
                        ret = ((char)i).ToString();
                        break;
                    }
                case 3:
                    {
                        i = map[(byte)s[0]] + (map[s[1]] << 6) + (map[s[2]] << 12);
                        temp = BitConverter.GetBytes(i);
                        ret = ((char)temp[0]).ToString() + ((char)temp[1]).ToString();
                        break;
                    }
                case 4:
                    {
                        i = map[(byte)s[0]] + (map[s[1]] << 6) + (map[s[2]] << 12) + (map[s[3]] << 18);
                        temp = BitConverter.GetBytes(i);
                        ret = ((char)temp[0]).ToString() + ((char)temp[1]).ToString() + ((char)temp[2]).ToString();
                        break;
                    }

            }
            return ret;
        }
        /// <summary>
        /// Interno.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string PreProcess(string s)
        {
            string ss = s;
            StringBuilder ret = new StringBuilder();
            while (ss.Length > 0)
            {
                ret.Append(Decode(ss.Substring(0, (ss.Length >= 4 ? 4 : ss.Length))));
                ss = ss.Remove(0, (ss.Length >= 4 ? 4 : ss.Length));
            }
            return ret.ToString();
        }
        /// <summary>
        /// Interno.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string InternalDecrypt(string s)
        {
            int i;
            ushort seed;
            char[] ret;
            ret = s.ToCharArray();
            seed = key;
            for (i = 0; i < s.Length; i++)
            {
                ret[i] = (char)((ret[i]) ^ (seed >> 8));
                seed = (ushort)((s[i] + seed) * C1 + C2);

            }
            return new string(ret);
        }
    }
}
