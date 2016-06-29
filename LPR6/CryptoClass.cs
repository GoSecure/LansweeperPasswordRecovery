using System;
using System.IO;
using System.Security.Cryptography;

namespace Lansweeper6_PasswordRecovery
{
    public class CryptoClass
    {
        private byte[] Key;
        private byte[] blob;
        private byte[] iv;
        private Pbkdf2 pbkdf2;
        private byte[] AESKey;


        public string Decrypt(string cipher)
        {
            string password = "";

            // Initializing PBKDF2 parameters
            pbkdf2 = new Pbkdf2(Key);

            // Deriving AES key from PBKDF2 algorithm.
            Rfc2898DeriveBytes pb = new Rfc2898DeriveBytes(
                pbkdf2.MasterKey,
                pbkdf2.Salt,
                pbkdf2.Iter);
            AESKey = pb.GetBytes(16);

            // Convert base64 input as binary blob
            byte[] tmp = new byte[Convert.FromBase64String(cipher).Length];
            tmp = Convert.FromBase64String(cipher);
            iv = new byte[16];
            blob = new byte[tmp.Length - 16];
            Array.Copy(tmp, 0, iv, 0, 16);
            Array.Copy(tmp, 16, blob, 0, blob.Length);


            // Prepare AES
            using (Aes aes = (Aes)new AesCryptoServiceProvider())
            {
                aes.Key = AESKey;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                ICryptoTransform decryptor = aes.CreateDecryptor();
                using (MemoryStream memoryStream = new MemoryStream(blob))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            password = streamReader.ReadToEnd();
                    }
                }
            }
            return password;
        }

        public CryptoClass(byte[] key)
        {
            Key = key;
        }
    }

    /*
     * Rfc2898Derivedbytes aka PBKDF2 parameters.
     */
    public class Pbkdf2
    {
        public byte[] Salt { get; }
        public int Iter { get; }
        public byte[] MasterKey { get; }
        public Pbkdf2(byte[] masterKey)
        {
            MasterKey = masterKey;
            Salt = new byte[8] {
              (byte) 39,
              (byte) 15,
              (byte) 41,
              (byte) 17,
              (byte) 43,
              (byte) 19,
              (byte) 45,
              (byte) 21 };
            Iter = 10000;
        }
    }
}
