using SQLite;
using SQLiteNetExtensions.Attributes;
using Required=System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DeZooiNaCrypto.Model.Entidade
{
    [SQLite.Table("CryptoMoeda")]
    public class CryptoMoeda : EntidadeBase
    {
        [Required, MaxLength(15)]
        public string Nome { get; set; }
        [Ignore]
        public decimal Valor { get; set; }
        [Required]
        public TipoCorretoraEnum TipoCorretora { get; set; }
        [Required]
        public TipoMoedaPar TipoMoedaPar { get; set; }  
        [ForeignKey(typeof(Usuario))]
        public Guid IdUsuario { get; set; }
        [ManyToOne]
        public Usuario Usuario { get; set; }
    }

    public enum TipoCorretoraEnum
    {
        Binance,
        BitGet,
        CoinMarketCap
    }

    public enum TipoMoedaParEnum
    {
        USDT,
        USD
    }
}
