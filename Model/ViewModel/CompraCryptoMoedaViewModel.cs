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
    public class CompraCryptoMoedaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Gravar { get; private set; }
        public string _quantidadeCompra { get; set; }
        public string _valorUnitarioCompra { get; set; }
        public CompraCryptoMoeda _compraCryptoMoeda { get; set; }

        CompraCryptoMoedaRepositorio _compraCryptoMoedaRepositorio= new CompraCryptoMoedaRepositorio();
        readonly IMessageService _messageService;
        Usuario _usuario;
        Guid _idCryptoMoeda;       

        public CompraCryptoMoedaViewModel(Usuario usuario, Guid idCryptoMoeda)
        {
            _messageService = DependencyService.Get<IMessageService>();
            _usuario = usuario;
            _idCryptoMoeda = idCryptoMoeda;

            Gravar = new Command(GravarCompraCryptoMoeda);
        }

        private void GravarCompraCryptoMoeda()
        {
            if (_compraCryptoMoeda.DataCompra == DateTime.MinValue) { _messageService.ShowAsync("E preciso infomar a data da venda válida"); return; }
            if (string.IsNullOrEmpty(_quantidadeCompra) || !Util.Validacao.ehDecimal(_quantidadeCompra) ||
                (Util.Validacao.ehDecimal(_quantidadeCompra) && Decimal.Parse(_quantidadeCompra) <= 0)) { _messageService.ShowAsync("E preciso infomar a quantidade de venda válida"); return; }
            if (string.IsNullOrEmpty(_valorUnitarioCompra) || !Util.Validacao.ehDecimal(_valorUnitarioCompra) ||
                (Util.Validacao.ehDecimal(_valorUnitarioCompra) && Decimal.Parse(_valorUnitarioCompra) <= 0)) { _messageService.ShowAsync("E preciso infomar o valor da venda válido"); return; }

            _compraCryptoMoeda.QuantidadeCompra= Decimal.Parse(_quantidadeCompra);
            _compraCryptoMoeda.ValorUnitarioCompra = Decimal.Parse(_valorUnitarioCompra);
            _compraCryptoMoedaRepositorio.Salvar(_compraCryptoMoeda);
        }
    }
}
