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
        DateTime dataFinalOperacaoFuturo;
        decimal valorRetorno;
        decimal valorTaxa;
        long idOrdemCorretora;
        decimal preco;
        decimal quantidade;
        public OperacaoFuturoCryptoMoeda()
        {
            DataInicialOperacaoFuturo = DateTime.Now.Date;
            DataFinalOperacaoFuturo = DateTime.Now.Date;
        }

        [Required]
        public TipoOperacaoFuturoEnum TipoOperacaoFuturo { get; set; }
        [Ignore]
        public string NomeOperacaoFuturo { get { return Enum.GetName(typeof(TipoOperacaoFuturoEnum), (int)TipoOperacaoFuturo); } }
        [Required]
        public DateTime DataInicialOperacaoFuturo { get { return dataInicialOperacaoFuturo; } set { dataInicialOperacaoFuturo = value; OnPropertyChanged(); } }
        [AllowNull]
        public DateTime DataFinalOperacaoFuturo { get { return dataFinalOperacaoFuturo; } set { dataFinalOperacaoFuturo = value; OnPropertyChanged(); } }
        [Ignore]
        public string DataInicialOperacaoFuturoStr { get { return DataInicialOperacaoFuturo.ToString("dd/MM/yyyy HH:mm:ss"); } }
        [Ignore]
        public string TipoOperacaoFuturoDataOperacaoStr { get { return NomeOperacaoFuturo + " em " + DataInicialOperacaoFuturoStr; } }
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
        [Required]
        public long IdOrdemCorretora { get { return idOrdemCorretora; } set { idOrdemCorretora = value; OnPropertyChanged(); } }
        [Required] 
        public decimal Preco { get { return preco; } set { preco = value; OnPropertyChanged(); } }
        [Required] 
        public decimal Quantidade { get { return quantidade; } set { quantidade = value; OnPropertyChanged(); } } 
    }  
}
