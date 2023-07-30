using DeZooiNaCrypto.Model.Enumerador;
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
        public TipoExchangeEnum TipoCorretora { get; set; }
        [Ignore]
        public string NomeCorretora { get { return Enum.GetName(typeof(TipoExchangeEnum), (int)TipoCorretora); } }
        [Required]
        public TipoMoedaParEnum TipoMoedaPar { get; set; }
        [Ignore]
        public string NomeMoedaPar { get { return Enum.GetName(typeof(TipoMoedaParEnum), (int)TipoMoedaPar); } }
        [Required, Column("IdUsuario"), ForeignKey(typeof(Usuario))]
        public Guid IdUsuario { get; set; }
        [Required, ManyToOne]
        public Usuario Usuario { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<OperacaoFuturoCryptoMoeda> OperacoesFuturo { get; set; }
    }    
}
