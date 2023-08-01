using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.Enumerador;
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

                var retorno = client.GetAsync(url).Result;
                if (retorno != null)
                {

                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
