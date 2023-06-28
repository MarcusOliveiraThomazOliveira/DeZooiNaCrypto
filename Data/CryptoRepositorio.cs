using DeZooiNaCrypto.Model;
using System.Net.Http.Json;

namespace DeZooiNaCrypto.Data
{
    public class CryptoRepositorio : RepositorioBase<Crypto>
    {
        public CryptoRepositorio() : base() { }

        public List<Crypto> Listar(Usuario usuario, String nome)
        {
            return _connection.QueryAsync<Crypto>("select * from Crypto where IdUsuario = @Id and Nome = @nome order by nome", usuario.Id, nome).Result;
        }
        public Crypto Obter(Usuario usuario, String nome, String moedaPar)
        {
            return _connection.QueryAsync<Crypto>("select * from Crypto where IdUsuario = @Id and Nome = @nome and MoedaPar = @moedaPar order by nome", usuario.Id, nome, moedaPar).Result.FirstOrDefault();
        }
        public List<Crypto> Listar(Usuario usuario)
        {
            return _connection.QueryAsync<Crypto>("select * from Crypto where IdUsuario = @Id order by nome", usuario.Id).Result;
        }
        public IEnumerable<BinanceCrypto> ObterPrecos(IEnumerable<string> binanceCriptos)
        {
            var queryString = $"[{string.Join(",", binanceCriptos.Select(s => $"\"{s}\""))}]";
            var urlParametros = @"?symbols=" + queryString;
            var url = new Uri(@"https://api.binance.com/api/v3/ticker/price" + urlParametros);

            var client = new HttpClient();
            return client.GetFromJsonAsync<List<BinanceCrypto>>(url).Result;
        }
    }
}
