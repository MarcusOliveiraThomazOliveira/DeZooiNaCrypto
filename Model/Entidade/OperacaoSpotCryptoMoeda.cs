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
        [Required]
        public DateTime DataOperacaoSpot { get; set; }
        [Required] 
        public decimal Quantidade { get; set; }
        [Required] 
        public decimal ValorUnitario { get; set; }

        [Required, Column("IdCryptoMoeda"), ForeignKey(typeof(CryptoMoeda))]
        public Guid IdCryptoMoeda { get; set; }
        [ManyToOne]
        public CryptoMoeda CryptoMoeda { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<OperacaoSpotVendaCryptoMoeda> OperacoesSpotVenda{ get; set; }

    }

    
}
