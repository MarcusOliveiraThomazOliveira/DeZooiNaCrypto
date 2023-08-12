using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.DTO;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.Enumerador;
using Newtonsoft.Json;
using System.Net.Http.Json;

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
                    dataFinalSincronizacao = (new DateTimeOffset(new DateTime(dataInicialSincronizacao.Year, dataInicialSincronizacao.Month, dataInicialSincronizacao.Day)).AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59));


                while (dataInicialSincronizacao.Date <= DateTime.Now.Date)
                {
                    if (dataFinalSincronizacao.Date > DateTime.Now.Date)
                        dataFinalSincronizacao = new DateTimeOffset(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59));

                    try
                    {
                        var periodoConsulta = "startTime=" + dataInicialSincronizacao.ToUnixTimeMilliseconds() + "&endTime=" + dataFinalSincronizacao.ToUnixTimeMilliseconds();
                        //var periodoConsulta = "startTime=1688612400000&endTime=1688695200000";
                        await AccountTradeList(periodoConsulta + "&");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    dataInicialSincronizacao = new DateTimeOffset(new DateTime(dataFinalSincronizacao.Year, dataFinalSincronizacao.Month, dataFinalSincronizacao.Day, 0, 0, 0)).AddDays(1);
                    dataFinalSincronizacao = new DateTimeOffset(new DateTime(dataFinalSincronizacao.Year, dataFinalSincronizacao.Month, dataFinalSincronizacao.Day, 23, 59, 59)).AddDays(7);
                }

                configuracaoExchange.DataUltimaAtualizacao = DateTime.Now;
                _configuracaoExchangeRepositorio.Atualizar(configuracaoExchange);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return true;
        }
        private async Task<bool> AccountTradeList(string periodoConsulta = "")
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
                    listaBinanceAccountTradeListDTO = listaBinanceAccountTradeListDTO.OrderBy(x => x.Symbol).ThenBy(x => x.Time).ToList();
                    int indice = 0;
                    int quantidadeRegistros = listaBinanceAccountTradeListDTO.Count();
                    foreach (var binanceAccountTradeListDTO in listaBinanceAccountTradeListDTO.OrderBy(x => x.Symbol).ThenBy(x => x.Time).ToList())
                    {
                        var symbol = binanceAccountTradeListDTO.Symbol.Replace(binanceAccountTradeListDTO.MarginAsset, "");
                        TipoMoedaParEnum tipoMoedaParEnum = ExtensionMethods.ToEnum<TipoMoedaParEnum>(binanceAccountTradeListDTO.MarginAsset);

                        if (!binanceAccountTradeListDTO.RealizedPnl.Equals(0))
                        {
                            OperacaoFuturoCryptoMoeda operacaoFuturoCryptoMoedaJaExiste = _operacaoFuturoRepositorio.Obter(TipoExchangeEnum.Binance, symbol, tipoMoedaParEnum);
                            if (operacaoFuturoCryptoMoedaJaExiste == null)
                                operacaoFuturoCryptoMoedaJaExiste = _operacaoFuturoRepositorio.Obter(binanceAccountTradeListDTO.OrderId, TipoExchangeEnum.Binance);

                            if (operacaoFuturoCryptoMoedaJaExiste != null)
                            {
                                operacaoFuturoCryptoMoedaJaExiste.ValorRetorno += binanceAccountTradeListDTO.RealizedPnl;
                                operacaoFuturoCryptoMoedaJaExiste.ValorTaxa += binanceAccountTradeListDTO.Commission;
                                operacaoFuturoCryptoMoedaJaExiste.IdOrdemCorretora = binanceAccountTradeListDTO.OrderId;
                                operacaoFuturoCryptoMoedaJaExiste.DataFinalOperacaoFuturo = UnixTimeToDateTime(binanceAccountTradeListDTO.Time);

                                if (indice.Equals(quantidadeRegistros - 1) ||
                                   (indice < quantidadeRegistros && !binanceAccountTradeListDTO.Symbol.Equals(listaBinanceAccountTradeListDTO[indice + 1].Symbol)))
                                {
                                    var listaGetIncomeHistory =
                                       await GetIncomeHistory("startTime=" + ((new DateTimeOffset(operacaoFuturoCryptoMoedaJaExiste.DataInicialOperacaoFuturo)).ToUnixTimeMilliseconds() - 1000) + "&endTime=" + (binanceAccountTradeListDTO.Time + 100), binanceAccountTradeListDTO.Symbol);

                                    operacaoFuturoCryptoMoedaJaExiste.ValorTaxaFinanciamento =
                                        listaGetIncomeHistory.Where(x => x.IncomeType == Constantes.Tipo_Renda_Taxa_Financiamento).Sum(x => x.Income);

                                    operacaoFuturoCryptoMoedaJaExiste.ValorDescontoTaxa =
                                        listaGetIncomeHistory.Where(x => x.IncomeType == Constantes.Tipo_Renda_Reembolso_Comissao).Sum(x => x.Income);
                                }

                                _operacaoFuturoRepositorio.Atualizar(operacaoFuturoCryptoMoedaJaExiste);
                            }
                        }
                        else
                        {
                            var operacaoFuturoCryptoMoedaJaExiste = _operacaoFuturoRepositorio.Obter(binanceAccountTradeListDTO.OrderId, TipoExchangeEnum.Binance);

                            if (operacaoFuturoCryptoMoedaJaExiste == null)
                            {
                                if (!symbolAtual.Equals(binanceAccountTradeListDTO.Symbol))
                                {
                                    symbolAtual = binanceAccountTradeListDTO.Symbol;
                                    binanceAccountTradeListDTO.Symbol = symbol;
                                    cryptoMoeda = CriaCryptoMoedaSeNaoExistir(binanceAccountTradeListDTO.Symbol, binanceAccountTradeListDTO.MarginAsset);
                                }

                                TipoOperacaoFuturoEnum tipoOperacaoFuturoEnum = ExtensionMethods.
                                    ToEnum<TipoOperacaoFuturoEnum>(binanceAccountTradeListDTO.Side.Equals(Constantes.Tipo_Operacao_Futuro_SELL) ? Constantes.Tipo_Operacao_Futuro_SHORT : Constantes.Tipo_Operacao_Futuro_LONG);

                                OperacaoFuturoCryptoMoeda operacaoFuturoCryptoMoeda = new();
                                operacaoFuturoCryptoMoeda.IdCryptoMoeda = cryptoMoeda.Id;
                                operacaoFuturoCryptoMoeda.DataInicialOperacaoFuturo = UnixTimeToDateTime(binanceAccountTradeListDTO.Time);
                                operacaoFuturoCryptoMoeda.ValorRetorno = binanceAccountTradeListDTO.RealizedPnl;
                                operacaoFuturoCryptoMoeda.ValorTaxa = binanceAccountTradeListDTO.Commission;
                                operacaoFuturoCryptoMoeda.Preco = binanceAccountTradeListDTO.Price;
                                operacaoFuturoCryptoMoeda.Quantidade = binanceAccountTradeListDTO.Qty;
                                operacaoFuturoCryptoMoeda.IdOrdemCorretora = binanceAccountTradeListDTO.OrderId;
                                operacaoFuturoCryptoMoeda.TipoOperacaoFuturo = tipoOperacaoFuturoEnum;
                                _operacaoFuturoRepositorio.Salvar(operacaoFuturoCryptoMoeda);
                            }
                        }

                        indice++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return true;

        }
        private async Task<List<BinanceGetIncomeHistoryDTO>> GetIncomeHistory(string periodoConsulta, string nomeCryptoMoeda)
        {
            ConfiguracaoExchange configuracaoExchange = _configuracaoExchangeRepositorio.Obter(_usuario.Id, TipoExchangeEnum.Binance);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-MBX-APIKEY", configuracaoExchange.ChaveDaAPI);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string queryString = "symbol=" + nomeCryptoMoeda + "&" + periodoConsulta + "&timestamp=" + DateTimeOffset.Now.ToUnixTimeMilliseconds();

            var assinatura = Util.Criptografia.GerarCriptografiaHMACSHA256(queryString, configuracaoExchange.ChaveSecretaDaAPI);

            var url = new Uri(configuracaoExchange.UrlFuturoBase + "/fapi/v1/income?" + queryString + "&signature=" + assinatura);

            var listaBinanceGetIncomeHistoryDTO = await client.GetFromJsonAsync<List<BinanceGetIncomeHistoryDTO>>(url);

            return listaBinanceGetIncomeHistoryDTO;
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
