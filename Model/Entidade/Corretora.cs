using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.Entidade
{
    [SQLite.Table("Corretora")]
    public class Corretora : EntidadeBase
    {
        [Required, MaxLength(50)]
        public string Nome { get; set; }
        [ManyToMany(typeof(CryptoMoeda))]
        public List<CryptoMoeda> CryptoMoedas { get; set; }
    }
}
