using System;

namespace Lansweeper6_PasswordRecovery
{
    class Program
    {
        static void usage()
        {
            Console.WriteLine("Usage: Lansweeper6_PasswordRecovery <key file> <encrypted password file>");
        }

        static void header()
        {

        }

        static void Main(string[] args)
        {
            header();
            if (args.Length != 2)
            {
                usage();
                return;
            }
            Console.WriteLine(string.Format("[*] Processing files {0}, {1}.", args[0].ToString(), args[1].ToString()));

            PasswordRecovery pr = new PasswordRecovery(args[0].ToString(), args[1].ToString());
            pr.LoadFiles();
            pr.DecryptAll();

            Console.Write("Press any key...");
            Console.ReadLine();
        }
    }
}
