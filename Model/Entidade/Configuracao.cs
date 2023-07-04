using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.Entidade
{
    [SQLite.Table("Configuracao")]
    public class Configuracao : EntidadeBase
    {
        [Required, MaxLength(50)]
        public string Nome { get; set; }
        public bool Binance { get; set; }
        public bool BitGet { get; set; }
        public bool CoinMarketCap { get; set; }
        public bool USDT { get; set; }
        public bool USD { get; set; }
        [OneToMany]
        public List<CryptoMoeda> CryptoMoedas { get; set; }
    }

    public enum TipoCorretora
    {
        Binance,
        BitGet,
        CoinMarketCap
    }

    public enum TipoMoedaPar
    {
        USDT,
        USD
    }
}
