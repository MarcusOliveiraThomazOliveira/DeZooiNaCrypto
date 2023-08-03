using DeZooiNaCrypto.Model.Enumerador;
using SQLite;
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
        public ConfiguracaoExchange()
        {
            DataInicioOperacaoExchange = DateTime.Now.Date;
        }
        [Required]
        public DateTime DataInicioOperacaoExchange { get; set; }
        [Required]
        public TipoExchangeEnum TipoExchange { get;set; }
        [Ignore]
        public string NomeExchange { get { return Enum.GetName(typeof(TipoExchangeEnum), (int)TipoExchange); } }
        [Required] 
        public string UrlFuturoBase { get;set; }
        [Required]
        public string UrlSpotBase { get; set; }
        [Required]
        public string ChaveDaAPI { get;set; }
        [Required]
        public string ChaveSecretaDaAPI { get; set; }
        [Required] 
        public DateTime? DataUltimaAtualizacao { get;set; }
        [Required, Column("IdUsuario"), ForeignKey(typeof(Usuario))]
        public Guid IdUsuario { get; set; }
        [Required, ManyToOne]
        public Usuario Usuario { get; set; }
    }
}
