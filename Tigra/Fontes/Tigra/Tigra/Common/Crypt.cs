﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Tigra.Common
{
    public class Crypt
    {
        private const string RgbIV = "ASIDJkwjelsAJDIq";
        private const string Key = "TalksdasjekSD123mkasdaKJDKAJKSDJ";

        public static string EncryptString(string ClearText)
        {
            byte[] clearTextBytes = Encoding.UTF8.GetBytes(ClearText);
            SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();

            MemoryStream ms = new MemoryStream();
            byte[] rgbIV = Encoding.ASCII.GetBytes(RgbIV);
            byte[] key = Encoding.ASCII.GetBytes(Key);

            CryptoStream cs = new CryptoStream(ms, rijn.CreateEncryptor(key, rgbIV), CryptoStreamMode.Write);
            cs.Write(clearTextBytes, 0, clearTextBytes.Length);
            cs.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string DecryptString(string EncryptedText)
        {
            byte[] encryptedTextBytes = Convert.FromBase64String(EncryptedText);
            SymmetricAlgorithm rijn = SymmetricAlgorithm.Create();

            MemoryStream ms = new MemoryStream();
            byte[] rgbIV = Encoding.ASCII.GetBytes(RgbIV);
            byte[] key = Encoding.ASCII.GetBytes(Key);

            CryptoStream cs = new CryptoStream(ms, rijn.CreateDecryptor(key, rgbIV), CryptoStreamMode.Write);
            cs.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);
            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}