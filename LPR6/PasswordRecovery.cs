using System;
using System.Collections.Generic;
using System.IO;

namespace Lansweeper6_PasswordRecovery
{
    class PasswordRecovery
    {
        private string KeyFilename;
        private string CipherFilename;
        private byte[] Key;
        private Dictionary<string, string> Credentials;
        private CryptoClass Crypto;

        public PasswordRecovery(string keyFilename, string cipherFilename)
        {
            KeyFilename = keyFilename;
            CipherFilename = cipherFilename;
            Key = new byte[1024];
            Credentials = new Dictionary<string, string>();
            Crypto = null;
        }

        public bool LoadFiles()
        {
            bool res = true;
            if (KeyFilename == null || CipherFilename == null)
                return res;

            Console.WriteLine(string.Format("[*] Loading key file {0}.", KeyFilename));
            Key = File.ReadAllBytes(KeyFilename);
            Console.WriteLine(string.Format("[*] Processing {0} bytes for the key file.", Key.Length));
            if (Key != null && Key.Length > 0)
                Crypto = new CryptoClass(Key);
            /* 
             * Cipher File should consist of user:password delimited by comma.
             * Passwords should be base64 encoded.
             * Credentials should be one per line.
             */
            Console.WriteLine(string.Format("[*] Loading cipher file {0}", CipherFilename));
            foreach (string s in File.ReadAllLines(CipherFilename))
            {
                Console.WriteLine(string.Format("[*] Loading cipher line {0}", s));
                Credentials.Add(s.Split(':')[0],
                    s.Split(':')[1]);
            }
            res = false;
            return res;
        }

        public void DecryptAll()
        {
            if (Crypto == null)
            {
                Console.WriteLine("[x] Error processing the key file...quit.");
                return;
            }

            string password;

            foreach(KeyValuePair<string, string> kv in Credentials)
            {
                password = "";
                password = kv.Value;

                Console.WriteLine(string.Format("[-] Recovered password for user {0} as {1}",
                    kv.Key,
                    Crypto.Decrypt(kv.Value)
                    ));
            }
        }

        
    }
}
