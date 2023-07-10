using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class OperacaoFuturoViewModel : INotifyPropertyChanged
    {
        private readonly IMessageService _messageService;
        private OperacaoFuturoRepositorio _operacaoFuturoRepositorio = new OperacaoFuturoRepositorio();

        public event PropertyChangedEventHandler PropertyChanged;
        public OperacaoFuturoCryptoMoeda OperacaoFuturoCryptoMoeda { get; set; }
        public string _valorInvestido { get; set; }
        public string _valorCompra { get; set; }
        public string _valorVenda { get; set; }
        public ICommand Gravar { get; private set; }
        public OperacaoFuturoViewModel()
        {
            _messageService = DependencyService.Get<IMessageService>();
            OperacaoFuturoCryptoMoeda = new OperacaoFuturoCryptoMoeda();

            Gravar = new Command(GravarOperacaoFuturo);
        }
        private void GravarOperacaoFuturo()
        {
            if (OperacaoFuturoCryptoMoeda.DataOperacaoFuturo == DateTime.MinValue) { _messageService.ShowAsync("E preciso infomar a data da venda válida"); return; }
           
            if(OperacaoFuturoCryptoMoeda.Alavancagem < 0) { _messageService.ShowAsync("O valor da alavancagem não pode ser menor que zero"); }

            if (string.IsNullOrEmpty(_valorInvestido) || !Util.Validacao.ehDecimal(_valorInvestido) ||
                (Util.Validacao.ehDecimal(_valorInvestido) && Decimal.Parse(_valorInvestido) <= 0)) { _messageService.ShowAsync("E preciso infomar o valor investido"); return; }
            
            if (string.IsNullOrEmpty(_valorCompra) || !Util.Validacao.ehDecimal(_valorCompra) ||
                (Util.Validacao.ehDecimal(_valorCompra) && Decimal.Parse(_valorCompra) <= 0)) { _messageService.ShowAsync("E preciso infomar o valor da compra"); return; }

            if (string.IsNullOrEmpty(_valorVenda) || !Util.Validacao.ehDecimal(_valorVenda) ||
                (Util.Validacao.ehDecimal(_valorVenda) && Decimal.Parse(_valorVenda) <= 0)) { _messageService.ShowAsync("E preciso infomar o valor da venda válido"); return; }

            OperacaoFuturoCryptoMoeda.ValorInvestido = Decimal.Parse(_valorInvestido);
            OperacaoFuturoCryptoMoeda.ValorCompra = Decimal.Parse(_valorCompra);
            OperacaoFuturoCryptoMoeda.ValorVenda = Decimal.Parse(_valorVenda);
            _operacaoFuturoRepositorio.Salvar(OperacaoFuturoCryptoMoeda);
        }

    }
}
