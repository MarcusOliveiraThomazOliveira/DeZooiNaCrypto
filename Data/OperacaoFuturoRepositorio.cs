using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;
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
            return new ObservableCollection<OperacaoFuturoCryptoMoeda>(_connection.QueryAsync<OperacaoFuturoCryptoMoeda>("select * from OperacaoFuturoCryptoMoeda where IdCryptoMoeda = @idCryptoMoeda order by DataOperacaoFuturo desc", idCryptoMoeda).Result);
        }
        public decimal TotalOperacaoFuturo(Guid idCryptoMoeda)
        {
            return _connection.QueryScalarsAsync<decimal>("select sum(valorretorno - valortaxa) from OperacaoFuturoCryptoMoeda where IdCryptoMoeda = @idCryptoMoeda", idCryptoMoeda).Result.FirstOrDefault();
        }
        public int QuantidadeOperacoes(Guid idCryptoMoeda)
        {
            return _connection.QueryScalarsAsync<int>("select count(*) from OperacaoFuturoCryptoMoeda where IdCryptoMoeda = @idCryptoMoeda", idCryptoMoeda).Result.FirstOrDefault();
        }
        public List<OperacaoFuturoCryptoMoeda> Listar(DateTime dataInicial, DateTime dataFinal)
        {
            return _connection.QueryAsync<OperacaoFuturoCryptoMoeda>("select * from OperacaoFuturoCryptoMoeda inner join CryptoMoeda on(OperacaoFuturoCryptoMoeda.IdCryptoMoeda = CryptoMoeda.Id) where DataOperacaoFuturo >= @dataInicial and DataOperacaoFuturo <= @dataFinal order by DataOperacaoFuturo desc", dataInicial.InitialDayHour(), dataFinal.FinalDayHour()).Result;
        }
    }
}
