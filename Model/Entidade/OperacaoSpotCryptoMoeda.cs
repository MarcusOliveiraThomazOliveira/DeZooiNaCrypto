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
    [Table("OperacaoSpotCryptoMoeda")]
    public class OperacaoSpotCryptoMoeda : EntidadeBase
    {
        DateTime _dataOperacaoSpot;
        decimal? _quantidade;
        decimal? _valorUnitario;

        public OperacaoSpotCryptoMoeda()
        {
            DataOperacaoSpot = DateTime.Now.Date;
        }
        [Required]
        public DateTime DataOperacaoSpot { get { return _dataOperacaoSpot; } set { _dataOperacaoSpot = value; OnPropertyChanged(); } }
        [Ignore]
        public string DataOperacaoSpotStr { get { return DataOperacaoSpot.ToString("dd/MM/yyyy"); } }
        [Required]
        public decimal? Quantidade { get { return _quantidade; } set { _quantidade = value; OnPropertyChanged(); } }
        [Ignore]
        public string QuantidadeStr { get { return "Quantidade : " + Quantidade.ToString(); } }
        [Required]
        public decimal? ValorUnitario { get { return _valorUnitario; } set { _valorUnitario = value; OnPropertyChanged(); } }
        [Ignore]
        public string ValorUnitarioStr { get { return "Val. Unitário : " + ValorUnitario.ToString(); } }

        [Required, Column("IdCryptoMoeda"), ForeignKey(typeof(CryptoMoeda))]
        public Guid IdCryptoMoeda { get; set; }
        [ManyToOne]
        public CryptoMoeda CryptoMoeda { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<OperacaoSpotVendaCryptoMoeda> OperacoesSpotVenda { get; set; }

    }


}
