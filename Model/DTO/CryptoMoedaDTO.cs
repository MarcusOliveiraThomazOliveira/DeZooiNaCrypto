using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.DTO
{
    public class CryptoMoedaDTO
    {
        public CryptoMoedaDTO()
        {
            Selecionada = false;
        }
        public Guid Id { get; set; }          
        public string Nome { get; set; }              
        public bool Selecionada { get; set; }
    }
}
