using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.Enumerador
{
    public enum TipoOperacaoFuturoEnum
    {
        [Description("Long")]
        Long = 0,
        [Description("Short")]
        Short = 1,
        [Description("Não Definida")]
        NaoDefinida = 999
    }
}
