using DeZooiNaCrypto.Model.Entidade;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Data
{
    public class OperacaoFuturoRepositorio : RepositorioBase<OperacaoFuturoCryptoMoeda>
    {
        public ObservableCollection<OperacaoFuturoCryptoMoeda> Listar(Guid idCryptoMoeda)
        {
            return new ObservableCollection<OperacaoFuturoCryptoMoeda>(_connection.QueryAsync<OperacaoFuturoCryptoMoeda>("select * from OperacaoFuturoCryptoMoeda where IdCryptoMoeda = @idCryptoMoeda order by DataOperacaoFuturo ", idCryptoMoeda).Result);
        }
        public decimal TotalOperacaoFuturo(Guid idCryptoMoeda)
        {
            return _connection.QueryScalarsAsync<decimal>("select sum(valorretorno - valortaxa) from OperacaoFuturoCryptoMoeda where IdCryptoMoeda = @idCryptoMoeda", idCryptoMoeda).Result.FirstOrDefault();
        }
    }
}
