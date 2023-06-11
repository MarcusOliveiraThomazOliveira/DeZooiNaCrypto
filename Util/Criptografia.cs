﻿
using System.Security.Cryptography;
using System.Text;

namespace DeZooiNaCrypto.Util
{
    public class Criptografia
    {
        public static string GerarCriptografia(string valor)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(valor);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
