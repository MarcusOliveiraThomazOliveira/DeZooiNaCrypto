using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Util
{
    public class Validacao
    {
        public static bool ehEmail(string email)
        {
            string regex = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            return (System.Text.RegularExpressions.Regex.IsMatch(email, regex));
        }

        public static bool ehDecimal(string valor)
        {
            string regex = @"^-?\d*\.?\d+";
            return (System.Text.RegularExpressions.Regex.IsMatch(valor, regex));
        }
    }
}
