using System;
using System.Text;

namespace LPR
{
    public class XTea
    {
        private const uint delta = 2654435769;
        private const uint num_rounds = 32;

        private byte[] ConvertUIntToBytes(uint Input)
        {
            return new byte[4]
            {
    (byte) (Input & (uint) byte.MaxValue),
    (byte) (Input >> 8 & (uint) byte.MaxValue),
    (byte) (Input >> 16 & (uint) byte.MaxValue),
    (byte) (Input >> 24 & (uint) byte.MaxValue)
            };
        }

        private uint ConvertBytesToUInt(byte Input1, byte Input2, byte Input3, byte Input4)
        {
            return (uint)Input1 + ((uint)Input2 << 8) + ((uint)Input3 << 16) + ((uint)Input4 << 24);
        }

        private uint ConvertStringToUInt(string Input)
        {
            return (uint)Input[0] + ((uint)Input[1] << 8) + ((uint)Input[2] << 16) + ((uint)Input[3] << 24);
        }

        private uint ConvertBytesToUInt(byte[] Input)
        {
            return (uint)Input[0] + ((uint)Input[1] << 8) + ((uint)Input[2] << 16) + ((uint)Input[3] << 24);
        }

        private uint[] FormatKey(string Key)
        {
            if (Key.Length == 0)
                throw new ArgumentException("Key must be between 1 and 16 characters in length");
            Key = Key.PadRight(16, ' ').Substring(0, 16);
            uint[] numArray = new uint[4];
            int num = 0;
            int startIndex = 0;
            while (startIndex < Key.Length)
            {
                numArray[num++] = this.ConvertStringToUInt(Key.Substring(startIndex, 4));
                startIndex += 4;
            }
            return numArray;
        }

        private void code(uint[] v, uint[] k)
        {
            uint num1 = v[0];
            uint num2 = v[1];
            uint num3 = 0;
            for (uint index = 0; index < 32U; ++index)
            {
                num1 += (uint)(((int)num2 << 4 ^ (int)(num2 >> 5)) + (int)num2 ^ (int)num3 + (int)k[(int)(num3 & 3U)]);
                num3 += 2654435769U;
                num2 += (uint)(((int)num1 << 4 ^ (int)(num1 >> 5)) + (int)num1 ^ (int)num3 + (int)k[(int)(num3 >> 11 & 3U)]);
            }
            v[0] = num1;
            v[1] = num2;
        }

        private void decode(uint[] v, uint[] k)
        {
            uint num1 = 3337565984;
            uint num2 = v[0];
            uint num3 = v[1];
            for (uint index = 0; index < 32U; ++index)
            {
                num3 -= (uint)(((int)num2 << 4 ^ (int)(num2 >> 5)) + (int)num2 ^ (int)num1 + (int)k[(int)(num1 >> 11 & 3U)]);
                num1 -= 2654435769U;
                num2 -= (uint)(((int)num3 << 4 ^ (int)(num3 >> 5)) + (int)num3 ^ (int)num1 + (int)k[(int)(num1 & 3U)]);
            }
            v[0] = num2;
            v[1] = num3;
        }

        public byte[] Encrypt(string Data, string Key)
        {
            if (Data.Length == 0)
                throw new ArgumentException("Data must be at least 1 character in length.");
            uint[] k = this.FormatKey(Key);
            if (Data.Length % 2 != 0)
                Data += (string)(object)' ';
            byte[] bytes = Encoding.Unicode.GetBytes(Data);
            byte[] numArray = new byte[Data.Length * 4];
            uint[] v = new uint[2];
            int index = 0;
            while (index < bytes.Length)
            {
                v[0] = (uint)bytes[index];
                v[1] = (uint)bytes[index + 1];
                this.code(v, k);
                Buffer.BlockCopy((Array)this.ConvertUIntToBytes(v[0]), 0, (Array)numArray, index * 4, 4);
                Buffer.BlockCopy((Array)this.ConvertUIntToBytes(v[1]), 0, (Array)numArray, (index + 1) * 4, 4);
                index += 2;
            }
            return numArray;
        }

        public string Decrypt(byte[] Data, string Key)
        {
            uint[] k = this.FormatKey(Key);
            int num1 = 0;
            uint[] v = new uint[2];
            byte[] bytes = new byte[Data.Length / 8 * 2];
            int index1 = 0;
            while (index1 < Data.Length)
            {
                v[0] = this.ConvertBytesToUInt(Data[index1], Data[index1 + 1], Data[index1 + 2], Data[index1 + 3]);
                v[1] = this.ConvertBytesToUInt(Data[index1 + 4], Data[index1 + 5], Data[index1 + 6], Data[index1 + 7]);
                this.decode(v, k);
                byte[] numArray1 = bytes;
                int index2 = num1;
                int num2 = 1;
                int num3 = index2 + num2;
                int num4 = (int)(byte)v[0];
                numArray1[index2] = (byte)num4;
                byte[] numArray2 = bytes;
                int index3 = num3;
                int num5 = 1;
                num1 = index3 + num5;
                int num6 = (int)(byte)v[1];
                numArray2[index3] = (byte)num6;
                index1 += 8;
            }
            return Encoding.Unicode.GetString(bytes, 0, bytes.Length);
        }
    }
}
