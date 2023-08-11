using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.DTO
{
    public class BinanceGetIncomeHistoryDTO
    {
        public string Symbol { get; set; }
        public string IncomeType { get; set; }
        public decimal Income { get; set; }
    }
}
