using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model
{
    public class Crypto : ObjetoBase
    {
        public string Nome { get; set; }
        public Usuario Usuario { get; set; }
    }
}
