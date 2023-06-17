using System.ComponentModel.DataAnnotations;

namespace DeZooiNaCrypto.Model
{
    [SQLite.Table("Usuario")]
    public class Usuario : ObjetoBase
    {
        [Required, MaxLength(100)]
        public string Nome { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [Required, MaxLength(100)]
        public string Senha { get; set; }
    }
}
