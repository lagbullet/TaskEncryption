using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionApp
{
    public class Encrypter
    {
        private static string _privateKey;
        private static string _publicKey;
        private static UnicodeEncoding _encoder = new UnicodeEncoding();

        public static void GenerateKeys()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                _privateKey = rsa.ToXmlString(true);
                _publicKey = rsa.ToXmlString(false);
            }
        }

        public static string Encrypt(string data)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_publicKey);
                byte[] dataToEncrypt = _encoder.GetBytes(data);
                byte[] encryptedByteArray = rsa.Encrypt(dataToEncrypt, true).ToArray();
                int length = encryptedByteArray.Count();
                int item = 0;
                StringBuilder sb = new StringBuilder();
                foreach (byte x in encryptedByteArray)
                {
                    sb.Append(x);
                    if (++item < length)
                        sb.Append(",");
                }
                return sb.ToString();
            }
        }

        public static string Decrypt(string data)
        {
            string[] dataArray = data.Split(new char[] { ',' });
            byte[] dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++)
                dataByte[i] = Convert.ToByte(dataArray[i]);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_privateKey);
                return _encoder.GetString(rsa.Decrypt(dataByte, true));
            }
        }
    }
}
