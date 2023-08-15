using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.Enumerador
{
    public enum TipoFiltroExtratoEnum
    {
        [Description("Hoje")]
        Hoje = 0,
        [Description("Semana")]
        Semana = 1,
        [Description("Mes")]
        Mes = 2,
        [Description("Ano")]
        Ano = 3,
        [Description("Personalizado")]
        Personalizado = 4,
        [Description("FiltroPersonalizado")]
        FiltroPersonalizado = 5
    }
}
