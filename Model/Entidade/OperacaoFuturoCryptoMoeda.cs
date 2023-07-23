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
    [Table("OperacaoFuturoCryptoMoeda")]
    public class OperacaoFuturoCryptoMoeda : EntidadeBase
    {
        DateTime dataOperacaoFuturo;
        decimal valorRetorno;
        decimal valorTaxa;
        public OperacaoFuturoCryptoMoeda()
        {
            DataOperacaoFuturo = DateTime.Now.Date;
        }

        [Required]
        public TipoOperacaoFuturoEnum TipoOperacaoFuturo { get; set; }
        [Ignore]
        public string NomeOperacaoFuturo { get { return Enum.GetName(typeof(TipoOperacaoFuturoEnum), (int)TipoOperacaoFuturo); } }
        [Required]
        public DateTime DataOperacaoFuturo { get { return dataOperacaoFuturo; } set { dataOperacaoFuturo = value; OnPropertyChanged(); } }
        [Ignore]
        public string DataOperacaoFuturoStr { get { return DataOperacaoFuturo.ToString("dd/MM/yyyy"); } }
        [Ignore]
        public string TipoOperacaoFuturoDataOperacaoStr { get { return NomeOperacaoFuturo + " em " + DataOperacaoFuturoStr; } }


        [Required]
        public decimal ValorRetorno { get { return valorRetorno; } set { valorRetorno = value; OnPropertyChanged(); } }
        [Ignore]
        public string ValorRetornoStr { get { return "Retorno : " + ValorRetorno.ToString(); } }
        [Required]
        public decimal ValorTaxa { get { return valorTaxa; } set { valorTaxa = value; OnPropertyChanged(); } }
        [Ignore]
        public string ValorTaxaStr { get { return "Taxa : " + ValorTaxa.ToString(); } }
        [Ignore]
        public decimal ValorTotal { get { return (ValorRetorno - ValorTaxa); } }
        [Ignore]
        public string ValorTotalStr { get { return "Ganho/Perda : " + (ValorRetorno - ValorTaxa).ToString(); } }
        [Required, Column("IdCryptoMoeda"), ForeignKey(typeof(CryptoMoeda))]
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
