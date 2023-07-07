using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel;
using Required = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DeZooiNaCrypto.Model.Entidade
{
    [SQLite.Table("CryptoMoeda")]
    public class CryptoMoeda : EntidadeBase
    {
        decimal _valor;

        [Required, MaxLength(15)]
        public string Nome { get; set; }
        [Ignore]
        public string NomeLongo { get { return Nome + " / " + NomeMoedaPar; } }
        [Ignore]
        public decimal Valor { get { return _valor; } set { _valor = value; OnPropertyChanged("Valor"); } }
        [Required]
        public TipoCorretoraEnum TipoCorretora { get; set; }
        [Ignore]
        public string NomeCorretora { get { return Enum.GetName(typeof(TipoCorretoraEnum), (int)TipoCorretora); } }
        [Required]
        public TipoMoedaParEnum TipoMoedaPar { get; set; }
        [Ignore]
        public string NomeMoedaPar { get { return Enum.GetName(typeof(TipoMoedaParEnum), (int)TipoMoedaPar); } }
        [ForeignKey(typeof(Usuario))]
        public Guid IdUsuario { get; set; }
        [ManyToOne]
        public Usuario Usuario { get; set; }
    }

    public enum TipoCorretoraEnum
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

    public enum TipoMoedaParEnum
    {
        [Description("USD")]
        USD = 0,
        [Description("USDT")]
        USDT = 1,
        [Description("Não Definida")]
        NaoDefinida = 999
    }
}
