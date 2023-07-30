using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.Enumerador
{
    public enum TipoMoedaParEnum
    {
        [Description("USD")]
        USD = 0,
        [Description("USDT")]
        USDT = 1,
        [Description("Não Definida")]
        NaoDefinida = 999
    }
}
