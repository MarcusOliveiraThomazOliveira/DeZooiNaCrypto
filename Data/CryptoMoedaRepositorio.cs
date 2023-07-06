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
            return new ObservableCollection<CryptoMoeda>(_connection.QueryAsync<CryptoMoeda>("select * from CryptoMoeda where IdUsuario = @Id order by nome", usuario.Id).Result);
        }
        public void ObterValores(ObservableCollection<CryptoMoeda> cryptoMoedas)
        {
            foreach (var cryptoMoeda in cryptoMoedas)
            {
                if (cryptoMoeda.TipoCorretora == TipoCorretoraEnum.Binance)
                {
                    try
                    {
                        string moedaPar = (cryptoMoeda.TipoMoedaPar == TipoMoedaParEnum.USDT ? "USDT" : "USD");
                        var queryString = $"[{string.Join(",", (new List<string> { cryptoMoeda.Nome.ToUpper() + moedaPar }).Select(s => $"\"{s}\""))}]";
                        var urlParametros = @"?symbols=" + queryString;
                        var url = new Uri(@"https://api.binance.com/api/v3/ticker/price" + urlParametros);

                        var client = new HttpClient();
                        List<BinanceCrypto> binanceCryptos = client.GetFromJsonAsync<List<BinanceCrypto>>(url).Result;
                        cryptoMoeda.Valor = binanceCryptos.FirstOrDefault().Price;
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
