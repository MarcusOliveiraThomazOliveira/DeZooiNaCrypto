using DevExpress.Maui.Controls;
using DevExpress.Maui.Core.Internal;
using DevExpress.Maui.Editors;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class OperacaoFuturoViewModel : INotifyPropertyChanged
    {
        readonly IMessageService _messageService;
        OperacaoFuturoRepositorio _operacaoFuturoRepositorio = new OperacaoFuturoRepositorio();
        Guid _idCryptoMoeda;
        ChoiceChipGroup _choiceChipGroup;
        DXPopup _actionsPopup;
        ObservableCollection<OperacaoFuturoCryptoMoeda> _operacaoFuturoCryptoMoedas;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<OperacaoFuturoCryptoMoeda> OperacaoFuturoCryptoMoedas
        {
            get { return _operacaoFuturoCryptoMoedas; }
            set { _operacaoFuturoCryptoMoedas = value; }
        }
        public OperacaoFuturoCryptoMoeda OperacaoFuturoCryptoMoeda { get; set; }
        public string _valorInvestido { get; set; }
        public string _valorCompra { get; set; }
        public string _valorVenda { get; set; }
        public ICommand SetTipoOperacaoFuturo { get; set; }
        public OperacaoFuturoViewModel(Guid IdCryptoMoeda, ChoiceChipGroup ccgMoedaPar, DXPopup actionsPopup)
        {
            _messageService = DependencyService.Get<IMessageService>();
            OperacaoFuturoCryptoMoeda = new OperacaoFuturoCryptoMoeda();
            _valorInvestido = string.Empty;
            _valorCompra = string.Empty;
            _valorVenda = string.Empty;

            _operacaoFuturoCryptoMoedas = _operacaoFuturoRepositorio.Listar(IdCryptoMoeda);
            _idCryptoMoeda = IdCryptoMoeda;
            _choiceChipGroup = ccgMoedaPar;
            _actionsPopup = actionsPopup;

            

            SetTipoOperacaoFuturo = new Command(InformaTipoOperacaoFuturo);
        }
        private void InformaTipoOperacaoFuturo()
        {
            if (((Chip)_choiceChipGroup.SelectedItem).Text == "Short")
            {
                OperacaoFuturoCryptoMoeda.TipoOperacaoFuturo = TipoOperacaoFuturoEnum.Short;
            }
            else if (((Chip)_choiceChipGroup.SelectedItem).Text == "Long")
            {
                OperacaoFuturoCryptoMoeda.TipoOperacaoFuturo = TipoOperacaoFuturoEnum.Long;
            }
        }
        public void Gravar()
        {
            if (OperacaoFuturoCryptoMoeda.DataOperacaoFuturo == DateTime.MinValue) { _messageService.ShowAsync("E preciso infomar a data da venda válida"); return; }

            if (OperacaoFuturoCryptoMoeda.Alavancagem < 0) { _messageService.ShowAsync("O valor da alavancagem não pode ser menor que zero"); }

            if (string.IsNullOrEmpty(_valorInvestido) || !Util.Validacao.ehDecimal(_valorInvestido) ||
                (Util.Validacao.ehDecimal(_valorInvestido) && Decimal.Parse(_valorInvestido) <= 0)) { _messageService.ShowAsync("E preciso infomar o valor investido"); return; }

            if (string.IsNullOrEmpty(_valorCompra) || !Util.Validacao.ehDecimal(_valorCompra) ||
                (Util.Validacao.ehDecimal(_valorCompra) && Decimal.Parse(_valorCompra) <= 0)) { _messageService.ShowAsync("E preciso infomar o valor da compra"); return; }

            if (string.IsNullOrEmpty(_valorVenda) || !Util.Validacao.ehDecimal(_valorVenda) ||
                (Util.Validacao.ehDecimal(_valorVenda) && Decimal.Parse(_valorVenda) <= 0)) { _messageService.ShowAsync("E preciso infomar o valor da venda válido"); return; }

            OperacaoFuturoCryptoMoeda.ValorInvestido = Decimal.Parse(_valorInvestido);
            OperacaoFuturoCryptoMoeda.ValorCompra = Decimal.Parse(_valorCompra);
            OperacaoFuturoCryptoMoeda.ValorVenda = Decimal.Parse(_valorVenda);
            OperacaoFuturoCryptoMoeda.IdCryptoMoeda = _idCryptoMoeda;
            _operacaoFuturoRepositorio.Salvar(OperacaoFuturoCryptoMoeda);

            _operacaoFuturoCryptoMoedas.Clear();
            _operacaoFuturoCryptoMoedas.InsertRange<OperacaoFuturoCryptoMoeda>(0, _operacaoFuturoRepositorio.Listar(_idCryptoMoeda));
                       
            _actionsPopup.IsOpen = false;
        }

    }
}
