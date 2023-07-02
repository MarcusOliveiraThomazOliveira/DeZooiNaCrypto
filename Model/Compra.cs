﻿using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model
{
    [SQLite.Table("Compra")]
    public class Compra : ObjetoBase
    {
        [ManyToOne]
        public Crypto Crypto { get; set; }
        [Required]
        public DateTime DataCompra { get; set; }
        [Required]
        public decimal Quantidade { get; set; }
        [Required]
        public decimal ValorUnitario { get; set; }
        public int DiasComprado
        {
            get
            {
                return (DateTime.Now.Date - DataCompra.Date).Days;
            }
        }
    }
}
