using DeZooiNaCrypto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Data
{
    public class CryptoRepositorio : RepositorioBase<Crypto>
    {
        public CryptoRepositorio() : base() { }

        public List<Crypto> Listar(Usuario usuario, String nome)
        {
            return _connection.QueryAsync<Crypto>("select * from Crypto where IdUsuario = @Id and Nome = @nome", usuario.Id, nome).Result;
        }
        public List<Crypto> Listar(Usuario usuario)
        {
            return _connection.QueryAsync<Crypto>("select * from Crypto where IdUsuario = @Id", usuario.Id).Result;
        }
    }
}
