using DeZooiNaCrypto.Model;
using DeZooiNaCrypto.Model.Entidade;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Data
{
    public class CryptoMoedaRepositorio : RepositorioBase<CryptoMoeda>
    {
        public ObservableCollection<CryptoMoeda> Listar(Usuario usuario)
        {
            ObservableCollection<CryptoMoeda> retorno = new ObservableCollection<CryptoMoeda>();
            retorno.Add(new CryptoMoeda() { Nome = "Teste 1", Valor = DateTime.Now.Second * 3 });
            retorno.Add(new CryptoMoeda() { Nome = "Teste 2", Valor = DateTime.Now.Second * 4 });
            retorno.Add(new CryptoMoeda() { Nome = "Teste 3", Valor = DateTime.Now.Second * 5 });
            retorno.Add(new CryptoMoeda() { Nome = "Teste 4", Valor = DateTime.Now.Second * 6 });
            retorno.Add(new CryptoMoeda() { Nome = "Teste 5", Valor = DateTime.Now.Millisecond });

            return retorno;

            //return new ObservableCollection<CryptoMoeda>(_connection.QueryAsync<CryptoMoeda>("select * from CryptoMoeda where IdUsuario = @Id order by nome", usuario.Id).Result);
        }
        public IEnumerable<BinanceCrypto> ObterPrecos(IEnumerable<string> binanceCriptos)
        {
            List<BinanceCrypto> binanceCryptos = new List<BinanceCrypto>();
            foreach (var crypto in binanceCriptos)
            {
                try
                {
                    var queryString = $"[{string.Join(",", (new List<string> { crypto }).Select(s => $"\"{s}\""))}]";
                    var urlParametros = @"?symbols=" + queryString;
                    var url = new Uri(@"https://api.binance.com/api/v3/ticker/price" + urlParametros);

                    var client = new HttpClient();
                    binanceCryptos.Add(client.GetFromJsonAsync<BinanceCrypto>(url).Result);
                }
                catch
                {
                }
            }
            return binanceCryptos.AsEnumerable<BinanceCrypto>();
        }
    }
}
