﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model
{
    public class Usuario : ObjetoBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
