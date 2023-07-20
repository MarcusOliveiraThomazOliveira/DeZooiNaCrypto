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
 
    [Table("OperacaoSpotVendaCryptoMoeda")]
    public class OperacaoSpotVendaCryptoMoeda : EntidadeBase
    {
        [Required]
        public DateTime DataOperacaoSpot { get; set; }
        [Required]
        public decimal Quantidade { get; set; }
        [Required]
        public decimal ValorUnitario { get; set; }

        [Required, Column("IdOperacaoSpotCryptoMoeda"), ForeignKey(typeof(OperacaoSpotCryptoMoeda))]
        public Guid IdOperacaoSpotCryptoMoeda { get; set; }
        [ManyToOne]
        public OperacaoSpotCryptoMoeda OperacaoSpotCryptoMoeda { get; set; }
    }
}
