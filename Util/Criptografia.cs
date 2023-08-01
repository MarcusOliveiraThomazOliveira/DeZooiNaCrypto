
using System.Security.Cryptography;
using System.Text;

namespace DeZooiNaCrypto.Util
{
    public class Criptografia
    {
        public static string GerarCriptografiaMD5(string valor)
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
        public static string GerarCriptografiaHMACSHA256(string valor, string chave)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(chave);
            byte[] queryStringBytes = Encoding.UTF8.GetBytes(valor);
            HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes);

            byte[] bytes = hmacsha256.ComputeHash(queryStringBytes);

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();

            //byte[] valorBytes = Encoding.ASCII.GetBytes(valor);
            //byte[] chaveBytes = Encoding.ASCII.GetBytes(chave);

            //var hmacsha256 = new HMACSHA256(chaveBytes);
            //var hash = hmacsha256.ComputeHash(valorBytes);

            //return Convert.ToBase64String(hash);

            //StringBuilder sb = new StringBuilder();

            //for (int i = 0; i < hash.Length; i++)
            //{
            //    sb.Append(hash[i].ToString("X2"));
            //}
            //return sb.ToString();
        }

        private static string GerarHMACSHA256(string strComputeHash, string secretKey)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] queryStringBytes = Encoding.UTF8.GetBytes(strComputeHash);
            HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes);

            byte[] bytes = hmacsha256.ComputeHash(queryStringBytes);

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
