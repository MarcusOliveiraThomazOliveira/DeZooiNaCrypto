using DevExpress.Office.Forms;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class PerfilViewModel : ViewModelBase
    {
        readonly ConfiguracaoExchangeRepositorio _configuracaoExchangeRepositorio = new();
        readonly Usuario _usuario;
        string _nome;

        public string Nome { get { return _nome; } private set { _nome = value; OnPropertyChanged("Nome"); } }
        public ConfiguracaoExchange ConfiguracaoExchange { get; private set; }
        public ObservableCollection<ConfiguracaoExchange> ConfiguracoesExchange { get; set; }

        public PerfilViewModel()
        {
            _usuario = JsonConvert.DeserializeObject<Usuario>(Preferences.Get(Constantes.Usuario_Logado, string.Empty));
            _nome = _usuario.Nome;
            ConfiguracaoExchange = new();
        }
        public async Task<bool> Gravar()
        {
            if (string.IsNullOrEmpty(ConfiguracaoExchange.UrlFuturoBase)) { await MessageService.DisplayAlert_OK("Iforme a URL da API Futuro."); return false; }
            if (string.IsNullOrEmpty(ConfiguracaoExchange.UrlSpotBase)) { await MessageService.DisplayAlert_OK("Iforme a URL da API Spot."); return false; }
            if (string.IsNullOrEmpty(ConfiguracaoExchange.ChaveDaAPI)) { await MessageService.DisplayAlert_OK("Iforme a chave da API."); return false; }
            if (string.IsNullOrEmpty(ConfiguracaoExchange.ChaveSecretaDaAPI)) { await MessageService.DisplayAlert_OK("Iformea chave secreta da API."); return false; }

            _configuracaoExchangeRepositorio.Salvar(ConfiguracaoExchange);

            ConfiguracoesExchange.Add(_configuracaoExchangeRepositorio.Obter(ConfiguracaoExchange.Id));

            ConfiguracaoExchange = new();

            return true;
        }
    }
}
