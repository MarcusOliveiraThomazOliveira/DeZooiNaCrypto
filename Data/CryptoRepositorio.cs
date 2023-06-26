using Android.Locations;
using DeZooiNaCrypto.Model;
using GoogleGson.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace DeZooiNaCrypto.Data
{
    public class CryptoRepositorio : RepositorioBase<Crypto>
    {
        public CryptoRepositorio() : base() { }

        public List<Crypto> Listar(Usuario usuario, String nome)
        {
            return _connection.QueryAsync<Crypto>("select * from Crypto where IdUsuario = @Id and Nome = @nome", usuario.Id, nome).Result;
        }
        public Crypto Obter(Usuario usuario, String nome, String moedaPar)
        {
            return _connection.QueryAsync<Crypto>("select * from Crypto where IdUsuario = @Id and Nome = @nome and MoedaPar = @moedaPar", usuario.Id, nome, moedaPar).Result.FirstOrDefault();
        }
        public List<Crypto> Listar(Usuario usuario)
        {
            return _connection.QueryAsync<Crypto>("select * from Crypto where IdUsuario = @Id", usuario.Id).Result;
        }
        public IEnumerable<BinanceCrypto> ObterPrecos(IEnumerable<string> binanceCriptos)
        {
            var queryString = $"[{string.Join(",", binanceCriptos.Select(s => $"\"{s}\""))}]";
            var urlParametros = @"?symbols=" + queryString;
            var url = new Uri(@"https://api.binance.com/api/v3/ticker/price" + urlParametros);

            var client = new HttpClient();
            var retorno = client.GetFromJsonAsync<List<BinanceCrypto>>(url).Result;

            return new List<BinanceCrypto>();
        }
    }
}
