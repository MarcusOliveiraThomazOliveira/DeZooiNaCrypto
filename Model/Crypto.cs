using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DeZooiNaCrypto.Model
{
    [SQLite.Table("Crypto")]
    public class Crypto : ObjetoBase
    {
        [Required, MaxLength(10)]
        public string Nome { get; set; }
        [Required, MaxLength(100)]
        public string NomeLongo { get; set; }
        [Required, MaxLength(10)]
        public string MoedaPar { get; set; }
        public string NomeMoedaPar { get { return Nome + " / " + MoedaPar; } }
        [ForeignKey(typeof(Usuario))]
        public Guid IdUsuario { get; set; }
        [ManyToOne]
        public Usuario Usuario { get; set; }
    }
}
