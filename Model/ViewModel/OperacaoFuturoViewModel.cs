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
    public class OperacaoFuturoViewModel : ModelViewBase
    {
        OperacaoFuturoRepositorio _operacaoFuturoRepositorio = new OperacaoFuturoRepositorio();
        Guid _idCryptoMoeda;
        ObservableCollection<OperacaoFuturoCryptoMoeda> _operacaoFuturoCryptoMoedas;

        public ObservableCollection<OperacaoFuturoCryptoMoeda> OperacaoFuturoCryptoMoedas
        {
            get { return _operacaoFuturoCryptoMoedas; }
            set { _operacaoFuturoCryptoMoedas = value; }
        }
        public OperacaoFuturoCryptoMoeda OperacaoFuturoCryptoMoeda { get; set; }
        public string valorRetorno { get; set; }
        public string valorTaxa { get; set; }
        public ICommand SetTipoOperacaoFuturo { get; set; }
        public OperacaoFuturoViewModel()
        {
            OperacaoFuturoCryptoMoeda = new OperacaoFuturoCryptoMoeda();
            valorRetorno = string.Empty;
            valorTaxa = string.Empty;
            _idCryptoMoeda = new(Preferences.Get(Constantes.Id, string.Empty));

            _operacaoFuturoCryptoMoedas = _operacaoFuturoRepositorio.Listar(_idCryptoMoeda);

            SetTipoOperacaoFuturo = new Command<string>((string arg) => { InformaTipoOperacaoFuturo(arg); });
        }
        private void InformaTipoOperacaoFuturo(string arg)
        {
            if (arg == "Short")
            {
                OperacaoFuturoCryptoMoeda.TipoOperacaoFuturo = TipoOperacaoFuturoEnum.Short;
            }
            else if (arg == "Long")
            {
                OperacaoFuturoCryptoMoeda.TipoOperacaoFuturo = TipoOperacaoFuturoEnum.Long;
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

            _operacaoFuturoRepositorio.Salvar(OperacaoFuturoCryptoMoeda);

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
            OperacaoFuturoCryptoMoeda.DataOperacaoFuturo = operacaoFuturoCryptoMoeda.DataOperacaoFuturo;
            OperacaoFuturoCryptoMoeda.ValorRetorno = operacaoFuturoCryptoMoeda.ValorRetorno;
            valorRetorno = operacaoFuturoCryptoMoeda.ValorRetorno.ToString();
            OperacaoFuturoCryptoMoeda.ValorTaxa = operacaoFuturoCryptoMoeda.ValorTaxa;
            valorTaxa = operacaoFuturoCryptoMoeda.ValorTaxa.ToString();
            OperacaoFuturoCryptoMoeda.IdCryptoMoeda = OperacaoFuturoCryptoMoeda.IdCryptoMoeda;
        }

    }
}
