using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.DTO
{
    public class OperacaoDTO
    {
        public DateTime DataOperacao { get; set; }
        public string NomeCryptoMoeda { get; set; }
        public decimal ValorOperacao { get; set; }
    }
}
