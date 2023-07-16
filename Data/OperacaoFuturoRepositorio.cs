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
        public decimal TotalOperacaoFuturo(Guid? idCryptoMoeda = null, DateTime? dataInicial = null, DateTime? dataFinal = null)
        {
            StringBuilder query = new StringBuilder();
            query.Append("select sum(valorretorno - valortaxa) from OperacaoFuturoCryptoMoeda ");
            query.Append(" Where 1 = 1 ")
                .Append(idCryptoMoeda.HasValue ? " and IdCryptoMoeda = " + idCryptoMoeda : string.Empty)
                .Append(dataInicial.HasValue ? " and DataOperacaoFuturo >= " + dataInicial.Value.ToString("dd/MM/yyyy") : string.Empty)
                .Append(dataInicial.HasValue ? " and DataOperacaoFuturo <= " + dataFinal.Value.ToString("dd/MM/yyyy") : string.Empty);

            return _connection.QueryScalarsAsync<decimal>(query.ToString()).Result.FirstOrDefault();
        }
    }
}
