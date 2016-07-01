/*
 * Lansweeper 4 & 5 Password Recovery tool.
 * by Martin Lemay, GoSecure Canada Inc. 2016
 * 
 * Update: Finally implemented the Xtea stuff. No need for the dependency anymore.
 * 
 */
using System;

namespace LPR
{
    class Program
    {
        static void usage()
        {
            Console.WriteLine("[*] Usage: LPR4and5.exe <encrypted password>");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                usage();
                return;
            }

            String password = args[0].ToString();
            Console.WriteLine(string.Format("[*] Decrypting Lansweeper Password: {0}\n[*] Note that this operation will NOT take a while...", password));
            
            
            String result = Crypto.DecryptPassword(password);
            Console.WriteLine(string.Format("\n[-] Recovered: {0}", result));
            Console.ReadLine();
        }
    }
}
