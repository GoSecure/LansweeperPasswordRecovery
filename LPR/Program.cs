/*
 * Lansweeper 5 Password Recovery tool.
 * by Martin Lemay, GoSecure Canada Inc. 2016
 * 
 * Yeah... their Xtea implementation sucks so much that I won't bother translating it to Python.
 * ...and yes... I did import Lansweeper.dll.
 * 
 */
using System;
using LS;

namespace LPR
{
    class Program
    {
        static void usage()
        {
            Console.WriteLine("[*] Usage: lpr.exe <encrypted password>");
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
            
            
            String result = Password.DecryptPassword(password);
            Console.WriteLine(string.Format("\n[-] Recovered: {0}", result));
            Console.ReadLine();
        }
    }
}
