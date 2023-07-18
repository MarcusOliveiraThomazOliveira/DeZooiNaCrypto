using DevExpress.Maui.Controls;
using DevExpress.Maui.Core.Internal;
using DevExpress.Maui.Editors;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public string valorRetorno { get; set; }
        public string valorTaxa { get; set; }
        public ICommand SetTipoOperacaoFuturo { get; set; }
        public OperacaoFuturoViewModel(Guid IdCryptoMoeda, ChoiceChipGroup ccgMoedaPar, DXPopup actionsPopup)
        {
            _messageService = DependencyService.Get<IMessageService>();
            OperacaoFuturoCryptoMoeda = new OperacaoFuturoCryptoMoeda();
            valorRetorno = string.Empty;
            valorTaxa = string.Empty;

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

            if (string.IsNullOrEmpty(valorRetorno) || !Util.Validacao.ehDecimal(valorRetorno)) { _messageService.ShowAsync("E preciso infomar o valor investido"); return; }

            if (string.IsNullOrEmpty(valorTaxa) || !Util.Validacao.ehDecimal(valorTaxa)) { _messageService.ShowAsync("E preciso infomar o valor da compra"); return; }

            OperacaoFuturoCryptoMoeda.ValorRetorno = Decimal.Parse(valorRetorno);
            OperacaoFuturoCryptoMoeda.ValorTaxa = Decimal.Parse(valorTaxa);
            OperacaoFuturoCryptoMoeda.IdCryptoMoeda = _idCryptoMoeda;

            valorRetorno = string.Empty;
            valorTaxa = string.Empty;

            _operacaoFuturoRepositorio.Salvar(OperacaoFuturoCryptoMoeda);

            _operacaoFuturoCryptoMoedas.Clear();
            _operacaoFuturoCryptoMoedas.InsertRange<OperacaoFuturoCryptoMoeda>(0, _operacaoFuturoRepositorio.Listar(_idCryptoMoeda));

            
            _actionsPopup.IsOpen = false;
        }
        public void Apagar(Guid idOperacaoFuturo)
        {
            OperacaoFuturoCryptoMoeda operacaoFuturoCryptoMoeda = _operacaoFuturoCryptoMoedas.Where(x => x.Id == idOperacaoFuturo).FirstOrDefault();
            if (operacaoFuturoCryptoMoeda != null)
            {
                _operacaoFuturoRepositorio.Deletar(operacaoFuturoCryptoMoeda);
                _operacaoFuturoCryptoMoedas.Remove(operacaoFuturoCryptoMoeda);
            }
        }

    }
}
