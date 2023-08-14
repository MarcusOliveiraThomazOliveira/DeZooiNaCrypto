using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.Enumerador;
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
            return new ObservableCollection<OperacaoFuturoCryptoMoeda>(_connection.QueryAsync<OperacaoFuturoCryptoMoeda>("select * from OperacaoFuturoCryptoMoeda where IdCryptoMoeda = @idCryptoMoeda order by DataInicialOperacaoFuturo desc", idCryptoMoeda).Result);
        }
        public decimal TotalOperacaoFuturo(Guid idCryptoMoeda)
        {
            return _connection.QueryScalarsAsync<decimal>("select sum((valorretorno + valortaxafinanciamento) + (valortaxa - valordescontotaxa)) from OperacaoFuturoCryptoMoeda where IdCryptoMoeda = @idCryptoMoeda", idCryptoMoeda).Result.FirstOrDefault();
        }
        public int QuantidadeOperacoes(Guid idCryptoMoeda)
        {
            return _connection.QueryScalarsAsync<int>("select count(*) from OperacaoFuturoCryptoMoeda where IdCryptoMoeda = @idCryptoMoeda", idCryptoMoeda).Result.FirstOrDefault();
        }
        public List<OperacaoFuturoCryptoMoeda> Listar(DateTime dataInicial, DateTime dataFinal)
        {
            return _connection.QueryAsync<OperacaoFuturoCryptoMoeda>("select * from OperacaoFuturoCryptoMoeda inner join CryptoMoeda on(OperacaoFuturoCryptoMoeda.IdCryptoMoeda = CryptoMoeda.Id) where (DataInicialOperacaoFuturo >= @dataInicial and DataInicialOperacaoFuturo <= @dataFinal) or (DataFinalOperacaoFuturo >= @dataInicial and DataFinalOperacaoFuturo <= @dataFinal) order by DataInicialOperacaoFuturo desc", dataInicial.InitialDayHour(), dataFinal.FinalDayHour()).Result;
        }
        public OperacaoFuturoCryptoMoeda Obter(long idOrdemCorretora, TipoExchangeEnum tipoExchangeEnum)
        {
            var parametroTipoExchangeEnum = (int)tipoExchangeEnum;
            return _connection.QueryAsync<OperacaoFuturoCryptoMoeda>("select operacaoFuturoCryptoMoeda.* from OperacaoFuturoCryptoMoeda operacaoFuturoCryptoMoeda inner join CryptoMoeda cryptoMoeda on (cryptoMoeda.Id = operacaoFuturoCryptoMoeda.IdCryptoMoeda)  where IdOrdemCorretora = @idOperacaoCorretora and cryptoMoeda.TipoCorretora = @parametroTipoExchangeEnum", idOrdemCorretora, parametroTipoExchangeEnum).Result.FirstOrDefault();
        }
        public OperacaoFuturoCryptoMoeda Obter(TipoExchangeEnum tipoExchangeEnum, string nomeCryptoMoeda, TipoMoedaParEnum tipoMoedaParEnum)
        {
            var parametroTipoExchangeEnum = (int)tipoExchangeEnum;
            var parametroTipoMoedaParEnum = (int)tipoMoedaParEnum;
            return _connection.QueryAsync<OperacaoFuturoCryptoMoeda>("select operacaoFuturoCryptoMoeda.* from OperacaoFuturoCryptoMoeda operacaoFuturoCryptoMoeda inner join CryptoMoeda cryptoMoeda on (cryptoMoeda.Id = operacaoFuturoCryptoMoeda.IdCryptoMoeda) where cryptoMoeda.Nome = @nomeCryptoMoeda and cryptoMoeda.TipoMoedaPar = @parametroTipoMoedaParEnum and cryptoMoeda.TipoCorretora = @parametroTipoExchangeEnum and valorretorno = 0", nomeCryptoMoeda, parametroTipoMoedaParEnum, parametroTipoExchangeEnum).Result.FirstOrDefault();
        }
    }
}
