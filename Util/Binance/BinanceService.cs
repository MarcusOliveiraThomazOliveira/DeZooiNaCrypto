using DevExpress.XtraPrinting.Native;
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
        public async Task<bool> Sincronizar()
        {
            try
            {
                ConfiguracaoExchange configuracaoExchange = _configuracaoExchangeRepositorio.Obter(_usuario.Id, TipoExchangeEnum.Binance);

                DateTimeOffset dataInicialSincronizacao =
                    (configuracaoExchange.DataUltimaAtualizacao.HasValue && configuracaoExchange.DataUltimaAtualizacao.Value != DateTime.MinValue ?
                    new DateTimeOffset(configuracaoExchange.DataUltimaAtualizacao.Value)
                    : configuracaoExchange.DataInicioOperacaoExchange != DateTime.MinValue ?
                        configuracaoExchange.DataInicioOperacaoExchange : new DateTimeOffset(new DateTime(2017, 7, 1)));

                DateTimeOffset dataFinalSincronizacao;
                if (configuracaoExchange.DataUltimaAtualizacao.HasValue && configuracaoExchange.DataUltimaAtualizacao.Value != DateTime.MinValue)
                    dataFinalSincronizacao = new DateTimeOffset(new DateTime(configuracaoExchange.DataUltimaAtualizacao.Value.Year, configuracaoExchange.DataUltimaAtualizacao.Value.Month, configuracaoExchange.DataUltimaAtualizacao.Value.Day, 23, 59, 59));
                else
                    dataFinalSincronizacao = dataInicialSincronizacao.AddDays(6);

                while (dataInicialSincronizacao.Date <= DateTime.Now.Date)
                {
                    if (dataFinalSincronizacao.Date > DateTime.Now.Date)
                        dataFinalSincronizacao = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));

                    try
                    {
                        var periodoConsulta = "startTime=" + dataInicialSincronizacao.ToUnixTimeMilliseconds() + "&endTime=" + dataFinalSincronizacao.ToUnixTimeMilliseconds();
                        await RecuperaMovimentacaoFuturo(periodoConsulta + "&");
                    }
                    catch { }

                    dataInicialSincronizacao = dataFinalSincronizacao.AddDays(1);
                    dataFinalSincronizacao = dataInicialSincronizacao.AddDays(6);
                }

                configuracaoExchange.DataUltimaAtualizacao = DateTime.Now.Date;
                _configuracaoExchangeRepositorio.Atualizar(configuracaoExchange);
            }
            catch
            {

                throw;
            }

            return true;
        }
        private async Task<bool> RecuperaMovimentacaoFuturo(string periodoConsulta = "")
        {
            try
            {
                ConfiguracaoExchange configuracaoExchange = _configuracaoExchangeRepositorio.Obter(_usuario.Id, TipoExchangeEnum.Binance);
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-MBX-APIKEY", configuracaoExchange.ChaveDaAPI);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string momentoConsulta = "timestamp=" + DateTimeOffset.Now.ToUnixTimeMilliseconds();

                var assinatura = Util.Criptografia.GerarCriptografiaHMACSHA256(periodoConsulta + momentoConsulta, configuracaoExchange.ChaveSecretaDaAPI);

                var url = new Uri(configuracaoExchange.UrlFuturoBase + "/fapi/v1/userTrades?" +
                     periodoConsulta + momentoConsulta +
                    "&signature=" + assinatura);

                var listaBinanceAccountTradeListDTO = await client.GetFromJsonAsync<List<BinanceAccountTradeListDTO>>(url);
                if (listaBinanceAccountTradeListDTO != null && listaBinanceAccountTradeListDTO.Count > 0)
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
                                ToEnum<TipoOperacaoFuturoEnum>(binanceAccountTradeListDTO.Side.Equals(Constantes.Tipo_Operacao_Futuro_SELL) ? Constantes.Tipo_Operacao_Futuro_SHORT : Constantes.Tipo_Operacao_Futuro_LONG);

                            OperacaoFuturoCryptoMoeda operacaoFuturoCryptoMoeda = new();
                            operacaoFuturoCryptoMoeda.IdCryptoMoeda = cryptoMoeda.Id;
                            operacaoFuturoCryptoMoeda.DataOperacaoFuturo = UnixTimeToDateTime(binanceAccountTradeListDTO.Time);//DateTimeOffset.FromUnixTimeMilliseconds(binanceAccountTradeListDTO.Time).DateTime.AddHours(-3);
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

            return true;

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
        public static DateTime UnixTimeToDateTime(long unixtime)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dtDateTime.AddMilliseconds(unixtime).ToLocalTime();
        }
    }
}
