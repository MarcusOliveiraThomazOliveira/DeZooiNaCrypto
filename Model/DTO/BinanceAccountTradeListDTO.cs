using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.DTO
{
    public class BinanceAccountTradeListDTO
    {
        public string Symbol { get; set; }
        public long Id { get; set; }
        public long OrderId { get;set; }
        public string Side { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }   
        public decimal RealizedPnl { get; set; }
        public string MarginAsset { get; set; }
        public decimal Commission { get; set;}
        public long Time { get; set; }
        


    }
}
