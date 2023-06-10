using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model
{
    public class ObjetoBase
    {
        public Guid Id { get; set; }
        public ObjetoBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
