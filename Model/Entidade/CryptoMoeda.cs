using SQLiteNetExtensions.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeZooiNaCrypto.Model.Entidade
{
    [SQLite.Table("CryptoMoeda")]
    public class CryptoMoeda : EntidadeBase
    {
        [Required, MaxLength(15)]
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        [ForeignKey(typeof(Configuracao))]
        public Guid IdConfiguracao { get; set; }
        [ManyToOne]
        public Configuracao Configuracao { get; set; }
    }
}
