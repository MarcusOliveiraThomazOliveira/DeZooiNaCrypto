using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DeZooiNaCrypto.Model.Entidade
{
    [SQLite.Table("Usuario")]
    public class Usuario : EntidadeBase
    {
        [Required, MaxLength(100)]
        public string Nome { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [Required, MaxLength(100)]
        public string Senha { get; set; }
        [OneToMany]
        public List<CryptoMoeda> CryptoMoedas { get; set; }
        [OneToMany]
        public List<CompraCryptoMoeda> CompraCryptoMoedas { get; set; }
    }
}
