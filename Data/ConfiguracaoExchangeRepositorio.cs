using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.Enumerador;

namespace DeZooiNaCrypto.Data
{
    public class ConfiguracaoExchangeRepositorio : RepositorioBase<ConfiguracaoExchange>
    {
        public ConfiguracaoExchange Obter(Guid idUsuario, TipoExchangeEnum tipoExchangeEnum)
        {
            return _connection.QueryAsync<ConfiguracaoExchange>("select * from ConfiguracaoExchange where IdUsuario = @Id and TipoExchange = @tipoExchangeEnum", idUsuario, (int)tipoExchangeEnum).Result.FirstOrDefault();
        }
    }
}
