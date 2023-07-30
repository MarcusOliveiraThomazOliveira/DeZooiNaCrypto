using DeZooiNaCrypto.Model.Enumerador;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.Entidade
{
    [SQLite.Table("ConfiguracaoExchange")]
    public class ConfiguracaoExchange : EntidadeBase
    {
        [Required]
        public TipoExchangeEnum TipoExchange { get;set; }
        [Required] 
        public string UrlFuturoBase { get;set; }
        [Required]
        public string UrlSpotBase { get; set; }
        [Required]
        public string ChaveDaAPI { get;set; }
        [Required]
        public string ChaveSecretaDaAPI { get; set; }
        [Required] 
        public DateTime DataUltimaAtualizacao { get;set; }
        [Required, ManyToOne]
        public Usuario Usuario { get; set; }
    }
}
