using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel;
using Required = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace DeZooiNaCrypto.Model.Entidade
{
    [Table("VendaCryptoMoeda")]
    public class VendaCryptoMoeda : EntidadeBase
    {
        public VendaCryptoMoeda()
        {
            DataVenda = DateTime.Now.Date;
        }
        [Required]
        public DateTime DataVenda { get; set; }
        [Required]
        public decimal QuantidadeVenda { get; set; }
        [Required]
        public decimal ValorVenda { get; set; }
        [ForeignKey(typeof(CryptoMoeda))]
        public Guid IdCryptoMoeda { get; set; }
        [ManyToOne]
        public CryptoMoeda CryptoMoeda { get; set; }
        public decimal ValorUnitarioCompra { get; set; }
        [ForeignKey(typeof(CompraCryptoMoeda))]
        public Guid IdCompraCryptoMoeda { get; set; }
        [ManyToOne]
        public CompraCryptoMoeda CompraCryptoMoeda { get; set; }

    }
}
