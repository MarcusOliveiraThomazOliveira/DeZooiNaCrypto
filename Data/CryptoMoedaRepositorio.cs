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
        public async void ObterValores(ObservableCollection<CryptoMoeda> cryptoMoedas)
        {
            try
            {
                var queryString = $"[{string.Join(",", cryptoMoedas.Where(x => x.TipoCorretora == TipoExchangeEnum.Binance && x.TipoMoedaPar == TipoMoedaParEnum.USDT).Select(x => $"\"{x.Nome + x.NomeMoedaPar}\""))}]";

                try
                {
                    var urlParametros = @"?symbols=" + queryString;
                    var url = new Uri(@"https://api.binance.com/api/v3/ticker/price" + urlParametros);

                    var client = new HttpClient();
                    List<BinanceCrypto> binanceCryptos = await client.GetFromJsonAsync<List<BinanceCrypto>>(url);
                    foreach (var binance in binanceCryptos)
                    {
                        var cryptoMoeda = cryptoMoedas.Where(x => (x.Nome + x.NomeMoedaPar) == binance.Symbol).FirstOrDefault();
                        if (cryptoMoeda != null)
                            cryptoMoeda.Valor = binance.Price;
                    }
                }
                catch
                {
                }
            }
            catch
            {
            }
        }
        public CryptoMoeda Obter(Usuario usuario, string nome, int idCorretora, int idMoedaPar)
        {
            return _connection.QueryAsync<CryptoMoeda>("select * from CryptoMoeda where IdUsuario = @Id and Nome = @nome and TipoCorretora = @idCorretora and TipoMoedaPar = @idMoedaPar order by nome", usuario.Id, nome, idCorretora, idMoedaPar).Result.FirstOrDefault();
        }
    }
}
