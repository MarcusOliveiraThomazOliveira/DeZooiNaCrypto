using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.Entidade
{
    public class CompraCryptoMoeda
    {
        public CompraCryptoMoeda()
        {
            DataCompra = DateTime.Now.Date;
        }
        [Required]
        public DateTime DataCompra { get; set; }
        [Required]
        public decimal QuantidadeCompra { get; set; }
        [Required]
        public decimal ValorUnitarioCompra { get; set; }
        [ForeignKey(typeof(CryptoMoeda))]
        public Guid IdCryptoMoeda { get; set; }
        [ManyToOne]
        public CryptoMoeda CryptoMoeda { get; set; }
        [OneToMany]
        public List<VendaCryptoMoeda> VendaCryptoMoedas { get; set; }
    }
}
