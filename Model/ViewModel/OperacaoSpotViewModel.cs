using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class OperacaoSpotViewModel : ModelViewBase
    {
        OperacaoSpotRepositorio _operacaoSpotRepositorio = new OperacaoSpotRepositorio();
        OperacaoSpotCryptoMoeda operacaoSpotCryptoMoeda;

        public ObservableCollection<OperacaoSpotCryptoMoeda> OperacoesSpotCryptoMoeda { get; set; }
        public OperacaoSpotCryptoMoeda OperacaoSpotCryptoMoeda { get { return operacaoSpotCryptoMoeda; } set { operacaoSpotCryptoMoeda = value; RaisePropertyChanged(); } }
        public ICommand Gravar { get; private set; }
        public OperacaoSpotViewModel()
        {
            operacaoSpotCryptoMoeda = new OperacaoSpotCryptoMoeda();
            OperacoesSpotCryptoMoeda = new ObservableCollection<OperacaoSpotCryptoMoeda>(_operacaoSpotRepositorio.Listar().Result.ToList());
            Gravar = new Command(() => { Task<bool> returno = GravarSpot(); });
        }

        public async Task<bool> GravarSpot()
        {
            if (OperacaoSpotCryptoMoeda != null)
            {
                if (OperacaoSpotCryptoMoeda.DataOperacaoSpot == DateTime.MinValue) { return await MessageService.DisplayAlert_OK("Informe uma data válida."); }
                if (OperacaoSpotCryptoMoeda.Quantidade <= 0) { return await MessageService.DisplayAlert_OK("Informe uma quantidade maior que zero."); }
                if (OperacaoSpotCryptoMoeda.ValorUnitario <= 0) { return await MessageService.DisplayAlert_OK("Iforme um valor unitário maior que zero."); }

                _operacaoSpotRepositorio.Salvar(OperacaoSpotCryptoMoeda);

                operacaoSpotCryptoMoeda = new OperacaoSpotCryptoMoeda();
                OperacoesSpotCryptoMoeda.Add(OperacaoSpotCryptoMoeda);

                return true;
            }

            return true;
        }
    }
}
