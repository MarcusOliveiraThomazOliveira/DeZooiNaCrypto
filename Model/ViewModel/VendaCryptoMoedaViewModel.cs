using AndroidX.AppCompat.Widget;
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
    public class VendaCryptoMoedaViewModel : INotifyPropertyChanged
    {
        private readonly IMessageService _messageService;

        public event PropertyChangedEventHandler PropertyChanged;

        public string _quantidadeVendida { get; set; }
        public string _valorVenda { get; set; }


        public Guid _idCryptoMoeda;
        public VendaCryptoMoeda _vendaCryptoMoeda { get; set; }
        VendaCryptoMoedaRepositorio _vendaCryptoMoedaRepositorio = new VendaCryptoMoedaRepositorio();
        public ICommand Gravar { get; private set; }

        public VendaCryptoMoedaViewModel(Guid idCryptoMoeda)
        {
            _messageService = DependencyService.Get<IMessageService>();
            _vendaCryptoMoeda = new VendaCryptoMoeda();
            _idCryptoMoeda = idCryptoMoeda;

            Gravar = new Command(GravarVendaCryptoMoeda);
        }

        private void GravarVendaCryptoMoeda()
        {
            if (_vendaCryptoMoeda.DataVenda == DateTime.MinValue) { _messageService.ShowAsync("E preciso infomar a data da venda válida"); return; }
            if (string.IsNullOrEmpty(_quantidadeVendida) || !Util.Validacao.ehDecimal(_quantidadeVendida) || 
                (Util.Validacao.ehDecimal(_quantidadeVendida) && Decimal.Parse(_quantidadeVendida) < 0)) { _messageService.ShowAsync("E preciso infomar a quantidade de venda válida");  return; }
            if (string.IsNullOrEmpty(_valorVenda) || !Util.Validacao.ehDecimal(_valorVenda) ||
                (Util.Validacao.ehDecimal(_valorVenda) && Decimal.Parse(_valorVenda) < 0)) { _messageService.ShowAsync("E preciso infomar o valor da venda válido");  return; }

            _vendaCryptoMoeda.QuantidadeVenda = Decimal.Parse(_quantidadeVendida);
            _vendaCryptoMoeda.ValorVenda = Decimal.Parse(_valorVenda);
            _vendaCryptoMoedaRepositorio.Salvar(_vendaCryptoMoeda);
        }
    }
}
