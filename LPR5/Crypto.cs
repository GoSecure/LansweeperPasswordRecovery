using System;
using System.Text;
using System.Diagnostics;

namespace LPR
{
    public class Crypto
    {
        public static string GetStaticSalt()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            int num1 = 0;
            string PwDstr;
            do
            { 
                stringBuilder1.Append(Char.ConvertFromUtf32(checked(40 - num1 + (num1 * 2 + num1) - 1)));
                stringBuilder1.Append(Char.ConvertFromUtf32(checked(num1 + 15 + num1)));
                checked { ++num1; }
            }
            while (num1 <= 60);
            PwDstr = stringBuilder1.ToString();
            return PwDstr;
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            string str1 = "";
            XTea xtea = new XTea();
            string str2 = "";
            try
            { 
                byte[] numArray = Convert.FromBase64String(encryptedPassword);
                string @string = Convert.ToString(Char.ConvertFromUtf32((int)numArray[0]));
                int index1 = 1;
                do
                {
                    str1 += Convert.ToString(Char.ConvertFromUtf32((int)numArray[index1]));
                    checked { ++index1; }
                }
                while (index1 <= 8);
                byte[] Data = new byte[checked(numArray.Length - 9 - 1 + 1)];
                int num1 = 9;
                int num2 = checked(numArray.Length - 1);
                int index2 = num1;
                while (index2 <= num2)
                {
                    Data[checked(index2 - 9)] = numArray[index2];
                    checked { ++index2; }
                }
                str2 = xtea.Decrypt(Data, str1 + GetStaticSalt());
                if (@string.Contains("1"))
                    str2 = str2.Substring(0, (str2.Length - 1));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return str2;
        }
    }
}
