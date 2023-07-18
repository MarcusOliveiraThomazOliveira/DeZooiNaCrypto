using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class ExtratoViewModel : ModelViewBase
    {
        OperacaoFuturoRepositorio _operacaoFuturoRepositorio = new OperacaoFuturoRepositorio();
        CryptoMoedaRepositorio _cryptoMoedaRepositorio = new CryptoMoedaRepositorio();
        public List<OperacaoDTO> OperacoesDTO { get; set; }
        public OperacaoDTO OperacaoDTO { get; set; }
        public ExtratoViewModel()
        {
            OperacoesDTO = new List<OperacaoDTO>();
            CarregaExtrato();            
        }

        private void CarregaExtrato()
        {            
            foreach (var operacaoFuturoCrypto in _operacaoFuturoRepositorio.Listar().Result.OrderByDescending(x => x.DataOperacaoFuturo))
            {
                OperacoesDTO.Add( new OperacaoDTO() { 
                    DataOperacao = operacaoFuturoCrypto.DataOperacaoFuturo,
                    NomeCryptoMoeda = _cryptoMoedaRepositorio.Obter(operacaoFuturoCrypto.IdCryptoMoeda).NomeLongo,
                    ValorOperacao = operacaoFuturoCrypto.ValorTotal
                });
            }
        }
    }
}
