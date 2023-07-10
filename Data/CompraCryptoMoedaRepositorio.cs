using DeZooiNaCrypto.Model.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Data
{
    public class CompraCryptoMoedaRepositorio : RepositorioBase<CompraCryptoMoeda>
    {
        public List<CompraCryptoMoeda> Listar(Guid idCryptoMoeda)
        {
            return _connection.QueryAsync<CompraCryptoMoeda>("select * from compracryptomoeda where idCryptoMoeda = @idCryptoMoeda").Result;
        }
    }
}
