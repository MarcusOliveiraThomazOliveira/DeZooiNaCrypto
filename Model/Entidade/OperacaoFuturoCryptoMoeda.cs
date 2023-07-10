using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.Entidade
{
    public class OperacaoFuturoCryptoMoeda : EntidadeBase
    {
        public OperacaoFuturoCryptoMoeda()
        {
            DataOperacaoFuturo = DateTime.Now.Date;
        }
        [Required]
        public TipoOperacaoFuturoEnum TipoOperacaoFuturo { get; set; }
        [Ignore]
        public string NomeOperacaoFuturo { get { return Enum.GetName(typeof(TipoOperacaoFuturoEnum), (int)TipoOperacaoFuturo); } }
        [Required]
        public DateTime DataOperacaoFuturo { get; set; }
        [Required]
        public decimal ValorInvestido { get; set; }
        [Required]
        public int Alavancagem { get; set; }
        [Required]
        public decimal ValorCompra{ get; set; }
        [ForeignKey(typeof(CryptoMoeda))]
        [Required]
        public decimal ValorVenda { get; set; }
        [ForeignKey(typeof(CryptoMoeda))]
        public Guid IdCryptoMoeda { get; set; }
        [ManyToOne]
        public CryptoMoeda CryptoMoeda { get; set; }
    }

    public enum TipoOperacaoFuturoEnum
    {
        [Description("Long")]
        Long = 0,
        [Description("Short")]
        Short = 1,
        [Description("Não Definida")]
        NaoDefinida = 999
    }
}
