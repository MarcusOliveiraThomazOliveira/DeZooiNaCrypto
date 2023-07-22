using DeZooiNaCrypto.Model.Entidade;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Data
{
    public class OperacaoSpotRepositorio : RepositorioBase<OperacaoSpotCryptoMoeda>
    {
        public ObservableCollection<OperacaoSpotCryptoMoeda> Listar(Guid idCryptoMoeda)
        {
            return new ObservableCollection<OperacaoSpotCryptoMoeda>(_connection.QueryAsync<OperacaoSpotCryptoMoeda>("select * from OperacaoSpotCryptoMoeda where IdCryptoMoeda = @idCryptoMoeda order by DataOperacaoSpot ", idCryptoMoeda).Result);
        }
    }
}
