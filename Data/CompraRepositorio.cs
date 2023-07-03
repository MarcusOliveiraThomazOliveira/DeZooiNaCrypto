using DeZooiNaCrypto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Data
{
    public class CompraRepositorio : RepositorioBase<Compra>
    {
        public CompraRepositorio() { }

        public List<Compra> Listar(Usuario usuario, Crypto crypto)
        {
            string query = @"select * from compra compra " +
                "inner join crypto crypto on(compra.IdCrypto = crypto.Id) " +
                "where crypto.IdUsuario = @IdUsuario and crypto.Id = @IdCrypto ";
            return _connection.QueryAsync<Compra>(query, usuario.Id, crypto.Id).Result;
        }
    }
}
