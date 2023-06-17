using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DeZooiNaCrypto.Model
{
    [SQLite.Table("Crypto")]
    public class Crypto : ObjetoBase
    {
        [Required, MaxLength(100)]
        public string Nome { get; set; }


        [ForeignKey(typeof(Usuario))]
        public int IdUsuario { get; set; }

        [ManyToOne]
        public Usuario Usuario { get; set; }
    }
}
