using DeZooiNaCrypto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Data
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>
    {
        public UsuarioRepositorio(string connectionString) : base(connectionString)
        {

        }
    }
}
