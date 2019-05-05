using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace StokBarangApp
{
    public class Encryption
    {
        public string password = "";
        public Encryption(string password)
        {
            this.password = password;
        }
        public string encrypt(string data)
        {
            byte[] plainTeks = ASCIIEncoding.ASCII.GetBytes(data);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            var key = new Rfc2898DeriveBytes(ASCIIEncoding.ASCII.GetBytes(password), new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 1000);
            aes.Key = key.GetBytes(32);
            aes.IV = key.GetBytes(16);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            try
            {
                ICryptoTransform ts = aes.CreateEncryptor();
                byte[] result = ts.TransformFinalBlock(plainTeks, 0, plainTeks.Length);
                return Convert.ToBase64String(result, 0, result.Length);
            } catch 
            {
                return null;
            }
        }
        public string decrypt(string data)
        {
            byte[] encryptedTeks = Convert.FromBase64String(data);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            var kunci = new Rfc2898DeriveBytes(password, new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 1000);
            aes.Key = kunci.GetBytes(32);
            aes.IV = kunci.GetBytes(16);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            try
            {
                ICryptoTransform ts = aes.CreateDecryptor();
                byte[] result = ts.TransformFinalBlock(encryptedTeks, 0, encryptedTeks.Length);
                return ASCIIEncoding.ASCII.GetString(result);
            } catch
            {
                return null;
            }
        }
    }
}
