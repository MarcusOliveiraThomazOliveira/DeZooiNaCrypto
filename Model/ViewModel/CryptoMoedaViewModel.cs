
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using System.Collections.ObjectModel;
using Microsoft.Maui.Dispatching;
using Newtonsoft.Json;
using DeZooiNaCrypto.Util;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public partial class CryptoMoedaViewModel : ModelViewBase
    {
        CryptoMoedaRepositorio _cryptoMoedaRepositorio = new CryptoMoedaRepositorio();
        ObservableCollection<CryptoMoeda> _cryptoMoedas;
        IDispatcherTimer timerAtualizaDados;

        public ObservableCollection<CryptoMoeda> CryptoMoedas
        {
            get { return _cryptoMoedas; }
            set { _cryptoMoedas = value; }
        }
        public CryptoMoedaViewModel()
        {
            _cryptoMoedas = _cryptoMoedaRepositorio.Listar(JsonConvert.DeserializeObject<Usuario>(Preferences.Get(Constantes.UsuarioLogado, string.Empty)));
            AtualizarValor();
            ConfiguraAtualizacao();
        }
        private void AtualizarValor()
        {
            _cryptoMoedaRepositorio.ObterValores(_cryptoMoedas);
        }
        private void ConfiguraAtualizacao()
        {
            timerAtualizaDados = Dispatcher.GetForCurrentThread().CreateTimer();
            timerAtualizaDados.Interval = TimeSpan.FromSeconds(2);
            timerAtualizaDados.Tick += (sender, e) => AtualizarValor();
            timerAtualizaDados.Start();
        }
        public void Apagar(Guid id)
        {
            var cryptoMoeda = _cryptoMoedas.Where(cm => cm.Id.Equals(id)).FirstOrDefault();
            if (cryptoMoeda != null)
            {
                _cryptoMoedaRepositorio.Deletar(cryptoMoeda);
                _cryptoMoedas.Remove(cryptoMoeda);
            }
        }
    }
}
