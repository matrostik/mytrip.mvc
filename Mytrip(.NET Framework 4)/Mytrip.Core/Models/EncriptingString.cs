using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web;

namespace Mytrip.Core.Models
{
    public class EncryptedString
    {
        public static string EncryptedValue(string value)
        {
            byte[] RawData = GetBytes(value);
            byte[] ClearRawData = ProtectedData.Unprotect(
                        RawData, 
                        null, 
                        DataProtectionScope.LocalMachine);
            return Encoding.UTF8.GetString(ClearRawData);
        }

        public static string DescriptedValue(string value)
        {
            byte[] EncryptedData = ProtectedData.Protect(
                        Encoding.UTF8.GetBytes(value),
                        null, 
                        DataProtectionScope.LocalMachine);
            return GetString(EncryptedData);
        }
        /*PRIVATE*/
        private static string GetString(byte[] data)
        {
            StringBuilder Results = new StringBuilder();
            foreach (byte b in data)
            {
                Results.Append(b.ToString("X2"));
            }

            return Results.ToString();
        }

        private static byte[] GetBytes(string data)
        {
            byte[] Results = new byte[data.Length / 2];
            for (int i = 0; i < data.Length; i += 2)
            {
                Results[i / 2] = Convert.ToByte(data.Substring(i, 2), 16);
            }
            return Results;
        }
    }
}
