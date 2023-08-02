using DevExpress.Maui.Core.Internal;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.Enumerador;
using DeZooiNaCrypto.Util;
using System.Collections.ObjectModel;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class OperacaoFuturoViewModel : ModelViewBase
    {
        OperacaoFuturoRepositorio _operacaoFuturoRepositorio = new OperacaoFuturoRepositorio();
        Guid _idCryptoMoeda;
        Guid _idOperacaoFuturoCryptoMoeda;
        ObservableCollection<OperacaoFuturoCryptoMoeda> _operacaoFuturoCryptoMoedas;
        int setTipoOperacaoFuturo;

        public ObservableCollection<OperacaoFuturoCryptoMoeda> OperacaoFuturoCryptoMoedas
        {
            get { return _operacaoFuturoCryptoMoedas; }
            set { _operacaoFuturoCryptoMoedas = value; }
        }
        public OperacaoFuturoCryptoMoeda OperacaoFuturoCryptoMoeda { get; set; }
        public string valorRetorno { get; set; }
        public string valorTaxa { get; set; }
        public int SetTipoOperacaoFuturo { get { return setTipoOperacaoFuturo; } set { setTipoOperacaoFuturo = value; InformaTipoOperacaoFuturo(value); } }
        public OperacaoFuturoViewModel()
        {
            _idOperacaoFuturoCryptoMoeda = Guid.Empty;
            OperacaoFuturoCryptoMoeda = new OperacaoFuturoCryptoMoeda();
            valorRetorno = string.Empty;
            valorTaxa = string.Empty;
            _idCryptoMoeda = new(Preferences.Get(Constantes.Id, string.Empty));

            _operacaoFuturoCryptoMoedas = _operacaoFuturoRepositorio.Listar(_idCryptoMoeda);
        }
        private void InformaTipoOperacaoFuturo(int arg)
        {

            switch (arg)
            {
                case 0:
                    OperacaoFuturoCryptoMoeda.TipoOperacaoFuturo = TipoOperacaoFuturoEnum.Long; 
                    break;
                case 1:
                    OperacaoFuturoCryptoMoeda.TipoOperacaoFuturo = TipoOperacaoFuturoEnum.Short;
                    break;
                default:
                    break;
            }
        }
        public async void Gravar()
        {
            if (OperacaoFuturoCryptoMoeda.DataOperacaoFuturo == DateTime.MinValue) { await MessageService.DisplayAlert_OK("E preciso infomar a data da venda válida"); return; }

            if (string.IsNullOrEmpty(valorRetorno) || !Util.Validacao.ehDecimal(valorRetorno)) { await MessageService.DisplayAlert_OK("E preciso infomar o valor investido"); return; }

            if (string.IsNullOrEmpty(valorTaxa) || !Util.Validacao.ehDecimal(valorTaxa)) { await MessageService.DisplayAlert_OK("E preciso infomar o valor da compra"); return; }

            OperacaoFuturoCryptoMoeda.ValorRetorno = Decimal.Parse(valorRetorno);
            OperacaoFuturoCryptoMoeda.ValorTaxa = Decimal.Parse(valorTaxa);
            OperacaoFuturoCryptoMoeda.IdCryptoMoeda = _idCryptoMoeda;

            valorRetorno = string.Empty;
            valorTaxa = string.Empty;

            if (!_idOperacaoFuturoCryptoMoeda.Equals(OperacaoFuturoCryptoMoeda.Id))
                _operacaoFuturoRepositorio.Salvar(OperacaoFuturoCryptoMoeda);
            else 
                _operacaoFuturoRepositorio.Atualizar(OperacaoFuturoCryptoMoeda);

            OperacaoFuturoCryptoMoeda = new();
            _idOperacaoFuturoCryptoMoeda = Guid.Empty;

            _operacaoFuturoCryptoMoedas.Clear();
            _operacaoFuturoCryptoMoedas.InsertRange<OperacaoFuturoCryptoMoeda>(0, _operacaoFuturoRepositorio.Listar(_idCryptoMoeda));
        }
        public async void Apagar(Guid idOperacaoFuturo)
        {
            if (await MessageService.DisplayAlert_CONFIRMAR_CANCELAR("Deseja realmente apagar esse registro?") == true)
            {
                OperacaoFuturoCryptoMoeda operacaoFuturoCryptoMoeda = _operacaoFuturoCryptoMoedas.Where(x => x.Id == idOperacaoFuturo).FirstOrDefault();
                if (operacaoFuturoCryptoMoeda != null)
                {
                    _operacaoFuturoRepositorio.Deletar(operacaoFuturoCryptoMoeda);
                    _operacaoFuturoCryptoMoedas.Remove(operacaoFuturoCryptoMoeda);
                }
            }
        }
        public void Editar(Guid idOperacaoFuturo)
        {
            var operacaoFuturoCryptoMoeda = _operacaoFuturoCryptoMoedas.Where(x => x.Id == idOperacaoFuturo).FirstOrDefault();
            OperacaoFuturoCryptoMoeda.Id = operacaoFuturoCryptoMoeda.Id;
            _idOperacaoFuturoCryptoMoeda = OperacaoFuturoCryptoMoeda.Id;
            OperacaoFuturoCryptoMoeda.DataOperacaoFuturo = operacaoFuturoCryptoMoeda.DataOperacaoFuturo;
            OperacaoFuturoCryptoMoeda.ValorRetorno = operacaoFuturoCryptoMoeda.ValorRetorno;
            valorRetorno = operacaoFuturoCryptoMoeda.ValorRetorno.ToString();
            RaisePropertyChanged("valorRetorno");
            OperacaoFuturoCryptoMoeda.ValorTaxa = operacaoFuturoCryptoMoeda.ValorTaxa;
            valorTaxa = operacaoFuturoCryptoMoeda.ValorTaxa.ToString();
            RaisePropertyChanged("valorTaxa");
            OperacaoFuturoCryptoMoeda.IdCryptoMoeda = OperacaoFuturoCryptoMoeda.IdCryptoMoeda;
            SetTipoOperacaoFuturo = (int)OperacaoFuturoCryptoMoeda.TipoOperacaoFuturo;
            RaisePropertyChanged("SetTipoOperacaoFuturo");
        }
        public void CriarObjetoInsercao()
        {
            OperacaoFuturoCryptoMoeda = new();
            valorRetorno = 0.ToString();
            RaisePropertyChanged("valorRetorno");
            valorTaxa = 0.ToString();
            RaisePropertyChanged("valorTaxa");
            SetTipoOperacaoFuturo = 0;
            RaisePropertyChanged("SetTipoOperacaoFuturo");
        }

    }
}
