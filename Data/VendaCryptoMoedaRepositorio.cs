using DeZooiNaCrypto.Model.Entidade;

namespace DeZooiNaCrypto.Data
{
    public class VendaCryptoMoedaRepositorio : RepositorioBase<VendaCryptoMoeda>
    {
        public List<VendaCryptoMoeda> Listar(Guid idCryptoMoeda)
        {
            return _connection.QueryAsync<VendaCryptoMoeda>("select * from vendacryptomoeda where idCryptoMoeda = @idCryptoMoeda").Result;
        }
    }
}
