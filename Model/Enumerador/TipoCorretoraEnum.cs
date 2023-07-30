using System.ComponentModel;

namespace DeZooiNaCrypto.Model.Enumerador
{
    public enum TipoExchangeEnum
    {
        [Description("Binance")]
        Binance = 0,
        [Description("BitGet")]
        BitGet = 1,
        [Description("CoinMarketCap")]
        CoinMarketCap = 2,
        [Description("Não Definida")]
        NaoDefinida = 999
    }
}
