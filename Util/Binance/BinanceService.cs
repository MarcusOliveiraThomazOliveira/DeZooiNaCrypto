using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.DTO;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.Enumerador;
using Microsoft.Maui.Animations;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace DeZooiNaCrypto.Util.Binance
{
    public class BinanceService
    {
        readonly ConfiguracaoExchangeRepositorio _configuracaoExchangeRepositorio = new();
        readonly CryptoMoedaRepositorio _cryptoMoedaRepositorio = new();
        readonly OperacaoFuturoRepositorio _operacaoFuturoRepositorio = new();
        readonly Usuario _usuario = JsonConvert.DeserializeObject<Usuario>(Preferences.Get(Constantes.Usuario_Logado, string.Empty));
        public BinanceService()
        {
        }

        public void RecuperaMovimentacaoFuturo(Usuario usuario)
        {
            try
            {
                ConfiguracaoExchange configuracaoExchange = _configuracaoExchangeRepositorio.Obter(usuario.Id, TipoExchangeEnum.Binance);
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-MBX-APIKEY", configuracaoExchange.ChaveDaAPI);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string periodoConsulta = "timestamp=" + DateTimeOffset.Now.ToUnixTimeMilliseconds();

                var assinatura = Util.Criptografia.GerarCriptografiaHMACSHA256(periodoConsulta, configuracaoExchange.ChaveSecretaDaAPI);

                var url = new Uri(configuracaoExchange.UrlFuturoBase + "/fapi/v1/userTrades?" +
                     periodoConsulta +
                    "&signature=" + assinatura);

                var listaBinanceAccountTradeListDTO = client.GetFromJsonAsync<List<BinanceAccountTradeListDTO>>(url).Result;
                if (listaBinanceAccountTradeListDTO != null)
                {
                    string symbolAtual = "";
                    CryptoMoeda cryptoMoeda = null;
                    foreach (var binanceAccountTradeListDTO in listaBinanceAccountTradeListDTO.OrderBy(x => x.Symbol))
                    {
                        if (!symbolAtual.Equals(binanceAccountTradeListDTO.Symbol))
                        {
                            symbolAtual = binanceAccountTradeListDTO.Symbol;
                            binanceAccountTradeListDTO.Symbol = binanceAccountTradeListDTO.Symbol.Replace(binanceAccountTradeListDTO.MarginAsset, "");
                            cryptoMoeda = CriaCryptoMoedaSeNaoExistir(binanceAccountTradeListDTO.Symbol, binanceAccountTradeListDTO.MarginAsset);
                        }

                        if (_operacaoFuturoRepositorio.Obter(binanceAccountTradeListDTO.OrderId, TipoExchangeEnum.Binance) == null)
                        {
                            TipoOperacaoFuturoEnum tipoOperacaoFuturoEnum = 
                                ExtensionMethods.
                                ToEnum<TipoOperacaoFuturoEnum>(binanceAccountTradeListDTO.Side.Equals(Constantes.Tipo_Operacao_Futuro_SELL) ? Constantes.Tipo_Operacao_Futuro_SHORT : Constantes.Tipo_Operacao_Futuro_LONG );

                            OperacaoFuturoCryptoMoeda operacaoFuturoCryptoMoeda = new();
                            operacaoFuturoCryptoMoeda.IdCryptoMoeda = cryptoMoeda.Id;
                            operacaoFuturoCryptoMoeda.DataOperacaoFuturo = DateTimeOffset.FromUnixTimeMilliseconds(binanceAccountTradeListDTO.Time).DateTime;
                            operacaoFuturoCryptoMoeda.ValorRetorno = binanceAccountTradeListDTO.RealizedPnl;
                            operacaoFuturoCryptoMoeda.ValorTaxa = binanceAccountTradeListDTO.Commission;
                            operacaoFuturoCryptoMoeda.Preco = binanceAccountTradeListDTO.Price;
                            operacaoFuturoCryptoMoeda.Quantidade = binanceAccountTradeListDTO.Qty;
                            operacaoFuturoCryptoMoeda.IdOperacaoCorretora = binanceAccountTradeListDTO.OrderId;
                            operacaoFuturoCryptoMoeda.TipoOperacaoFuturo = tipoOperacaoFuturoEnum;
                            _operacaoFuturoRepositorio.Salvar(operacaoFuturoCryptoMoeda);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

        }
        private CryptoMoeda CriaCryptoMoedaSeNaoExistir(string symbol, string marginAsset)
        {
            TipoMoedaParEnum tipoMoedaParEnum = ExtensionMethods.ToEnum<TipoMoedaParEnum>(marginAsset);
            var cryptoMoeda = _cryptoMoedaRepositorio.Obter(_usuario, symbol, (int)TipoExchangeEnum.Binance, (int)tipoMoedaParEnum);
            if (cryptoMoeda == null)
            {
                CryptoMoeda cryptoMoedaNovo = new();
                cryptoMoedaNovo.IdUsuario = _usuario.Id;
                cryptoMoedaNovo.Nome = symbol;
                cryptoMoedaNovo.TipoCorretora = TipoExchangeEnum.Binance;
                cryptoMoedaNovo.TipoMoedaPar = tipoMoedaParEnum;
                _cryptoMoedaRepositorio.Salvar(cryptoMoedaNovo);
                return cryptoMoedaNovo;
            }
            else
                return cryptoMoeda;
        }
    }
}
