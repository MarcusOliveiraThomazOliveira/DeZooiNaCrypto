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
        [ManyToMany(typeof(Corretora))]
        public List<Corretora>  Corretoras { get; set; }
    }
}
