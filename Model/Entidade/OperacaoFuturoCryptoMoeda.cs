using DeZooiNaCrypto.Model.Enumerador;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DeZooiNaCrypto.Model.Entidade
{
    [Table("OperacaoFuturoCryptoMoeda")]
    public class OperacaoFuturoCryptoMoeda : EntidadeBase
    {
        DateTime dataInicialOperacaoFuturo;
        DateTime? dataFinalOperacaoFuturo;
        decimal valorRetorno;
        decimal valorTaxa;
        decimal valorDescontoTaxa;
        decimal valorTaxaFinanciamento;
        long idOrdemCorretora;
        decimal preco;
        decimal quantidade;
        public OperacaoFuturoCryptoMoeda()
        {
            DataInicialOperacaoFuturo = DateTime.Now.Date;
            DataFinalOperacaoFuturo = null;
        }

        [Required]
        public TipoOperacaoFuturoEnum TipoOperacaoFuturo { get; set; }
        [Ignore]
        public string NomeOperacaoFuturo { get { return Enum.GetName(typeof(TipoOperacaoFuturoEnum), (int)TipoOperacaoFuturo); } }
        [Required]
        public DateTime DataInicialOperacaoFuturo { get { return dataInicialOperacaoFuturo; } set { dataInicialOperacaoFuturo = value; OnPropertyChanged(); } }
        [AllowNull]
        public DateTime? DataFinalOperacaoFuturo { get { return dataFinalOperacaoFuturo; } set { dataFinalOperacaoFuturo = value; OnPropertyChanged(); } }
        [Ignore]
        public string DataInicialOperacaoFuturoStr { get { return DataInicialOperacaoFuturo.ToString("dd/MM/yyyy HH:mm:ss"); } }
        [Ignore]
        public string TipoOperacaoFuturoDataOperacaoStr { get { return NomeOperacaoFuturo + " em " + DataInicialOperacaoFuturoStr; } }
        [Required]
        public decimal ValorRetorno { get { return valorRetorno; } set { valorRetorno = value; OnPropertyChanged(); } }
        [Ignore]
        public string ValorRetornoStr { get { return "Retorno : " + ValorRetorno; } }
        [Required]
        public decimal ValorTaxaFinanciamento { get { return valorTaxaFinanciamento; } set { valorTaxaFinanciamento = value; OnPropertyChanged(); } }
        [Ignore]
        public string ValorTaxaFinanciamentoStr { get { return "Taxa Financiamento : " + valorTaxaFinanciamento; } }
        [Required]
        public decimal ValorTaxa { get { return valorTaxa; } set { valorTaxa = value; OnPropertyChanged(); } }
        [Ignore]
        public string ValorTaxaStr { get { return "Comissão : " + ValorTaxa; } }
        [Required]
        public decimal ValorDescontoTaxa { get { return valorDescontoTaxa; } set { valorDescontoTaxa = value; OnPropertyChanged(); } }
        [Ignore]
        public string ValorDescontoTaxaStr { get { return "Reembolso Comissão : " + valorDescontoTaxa; } }
        [Ignore]
        public decimal ValorTotal { get { return ((ValorRetorno + ValorTaxaFinanciamento) - (ValorTaxa - ValorDescontoTaxa)); } }
        [Ignore]
        public string ValorTotalStr { get { return "Ganho/Perda : " + ((ValorRetorno + ValorTaxaFinanciamento) - (ValorTaxa - ValorDescontoTaxa)); } }
        [Required, Column("IdCryptoMoeda"), ForeignKey(typeof(CryptoMoeda))]
        public Guid IdCryptoMoeda { get; set; }
        [ManyToOne]
        public CryptoMoeda CryptoMoeda { get; set; }
        [Required]
        public long IdOrdemCorretora { get { return idOrdemCorretora; } set { idOrdemCorretora = value; OnPropertyChanged(); } }
        [Required]
        public decimal Preco { get { return preco; } set { preco = value; OnPropertyChanged(); } }
        [Required]
        public decimal Quantidade { get { return quantidade; } set { quantidade = value; OnPropertyChanged(); } }
    }
}
