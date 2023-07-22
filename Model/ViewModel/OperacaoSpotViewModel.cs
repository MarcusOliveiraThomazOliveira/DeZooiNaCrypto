using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class OperacaoSpotViewModel : ModelViewBase
    {
        OperacaoSpotRepositorio _operacaoSpotRepositorio = new OperacaoSpotRepositorio();
        OperacaoSpotCryptoMoeda operacaoSpotCryptoMoeda;
        Guid _idCryptoMoeda;

        public ObservableCollection<OperacaoSpotCryptoMoeda> OperacoesSpotCryptoMoeda { get; set; }
        public OperacaoSpotCryptoMoeda OperacaoSpotCryptoMoeda { get { return operacaoSpotCryptoMoeda; } set { operacaoSpotCryptoMoeda = value; RaisePropertyChanged(); } }
        public ICommand Gravar { get; private set; }
        public OperacaoSpotViewModel()
        {
            operacaoSpotCryptoMoeda = new OperacaoSpotCryptoMoeda();
            _idCryptoMoeda = new(Preferences.Get(Constantes.Id, string.Empty));
            OperacoesSpotCryptoMoeda = _operacaoSpotRepositorio.Listar(_idCryptoMoeda);
            Gravar = new Command(() => { Task<bool> retorno = GravarSpot(); });
        }

        public async Task<bool> GravarSpot()
        {
            if (OperacaoSpotCryptoMoeda != null)
            {
                if (OperacaoSpotCryptoMoeda.DataOperacaoSpot == DateTime.MinValue) { return await MessageService.DisplayAlert_OK("Informe uma data válida."); }
                if (OperacaoSpotCryptoMoeda.Quantidade <= 0) { return await MessageService.DisplayAlert_OK("Informe uma quantidade maior que zero."); }
                if (OperacaoSpotCryptoMoeda.ValorUnitario <= 0) { return await MessageService.DisplayAlert_OK("Iforme um valor unitário maior que zero."); }

                OperacaoSpotCryptoMoeda.IdCryptoMoeda = _idCryptoMoeda;
                _operacaoSpotRepositorio.Salvar(OperacaoSpotCryptoMoeda);

                OperacoesSpotCryptoMoeda.Add(new OperacaoSpotCryptoMoeda()
                {
                    Id = operacaoSpotCryptoMoeda.Id,
                    IdCryptoMoeda = _idCryptoMoeda,
                    DataOperacaoSpot = operacaoSpotCryptoMoeda.DataOperacaoSpot,
                    Quantidade = operacaoSpotCryptoMoeda.Quantidade,
                    ValorUnitario = operacaoSpotCryptoMoeda.ValorUnitario
                });

                OperacaoSpotCryptoMoeda = new();
                OperacaoSpotCryptoMoeda.Quantidade = null;
                OperacaoSpotCryptoMoeda.ValorUnitario = null;

                return true;
            }

            return true;
        }
        public void Apagar(Guid arg)
        {
            var operacaoSpotCryptoMoeda = OperacoesSpotCryptoMoeda.Where(x => x.Id == arg).FirstOrDefault();
            if (operacaoSpotCryptoMoeda != null)
            {
                _operacaoSpotRepositorio.Deletar(operacaoSpotCryptoMoeda);
                OperacoesSpotCryptoMoeda.Remove(operacaoSpotCryptoMoeda);
            }
        }
    }
}
