using DevExpress.Maui.Core.Internal;
using DevExpress.Office.Forms;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;
using DeZooiNaCrypto.Util.Binance;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class PerfilViewModel : ViewModelBase
    {
        readonly ConfiguracaoExchangeRepositorio _configuracaoExchangeRepositorio = new();
        readonly Usuario _usuario;
        ObservableCollection<ConfiguracaoExchange> _configuracaoExchange = new();
        string _nome;
        int _setTipoExchange;

        public string Nome { get { return _nome; } private set { _nome = value; OnPropertyChanged("Nome"); } }
        public ConfiguracaoExchange ConfiguracaoExchange { get; private set; }
        public ObservableCollection<ConfiguracaoExchange> ConfiguracoesExchange
        {
            get { return _configuracaoExchange; }
            set { _configuracaoExchange = value; OnPropertyChanged("ConfiguracoesExchange"); }
        }
        public int SetTipoExchange { get { return _setTipoExchange; } private set { _setTipoExchange = value; TipoExchange(value); } }

        public PerfilViewModel()
        {
            _usuario = JsonConvert.DeserializeObject<Usuario>(Preferences.Get(Constantes.Usuario_Logado, string.Empty));
            _nome = _usuario.Nome;
            ConfiguracaoExchange = new();
            ConfiguracoesExchange.InsertRange(0, new ObservableCollection<ConfiguracaoExchange>(_configuracaoExchangeRepositorio.Listar().Result));
        }
        private void TipoExchange(int tipoExchange)
        {

        }
        public async Task<bool> Gravar()
        {
            var configuracaoExchangeJaExiste = _configuracaoExchangeRepositorio.Obter(_usuario.Id, ConfiguracaoExchange.TipoExchange);

            //REMOVER 
            ConfiguracaoExchange = configuracaoExchangeJaExiste;

            if (string.IsNullOrEmpty(ConfiguracaoExchange.UrlFuturoBase)) { await MessageService.DisplayAlert_OK("Iforme a URL da API Futuro."); return false; }
            if (string.IsNullOrEmpty(ConfiguracaoExchange.UrlSpotBase)) { await MessageService.DisplayAlert_OK("Iforme a URL da API Spot."); return false; }
            if (string.IsNullOrEmpty(ConfiguracaoExchange.ChaveDaAPI)) { await MessageService.DisplayAlert_OK("Iforme a chave da API."); return false; }
            if (string.IsNullOrEmpty(ConfiguracaoExchange.ChaveSecretaDaAPI)) { await MessageService.DisplayAlert_OK("Iformea chave secreta da API."); return false; }

            ConfiguracaoExchange.IdUsuario = _usuario.Id;

            if (configuracaoExchangeJaExiste == null)
                _configuracaoExchangeRepositorio.Salvar(ConfiguracaoExchange);
            else
            {
                ConfiguracaoExchange.Id = configuracaoExchangeJaExiste.Id;
                _configuracaoExchangeRepositorio.Atualizar(ConfiguracaoExchange);
            }

            ConfiguracoesExchange.Add(_configuracaoExchangeRepositorio.Obter(ConfiguracaoExchange.Id));

            ConfiguracaoExchange = new();

            BinanceService binanceService = new();
            binanceService.RecuperaMovimentacaoFuturo(_usuario);

            return true;
        }
    }
}
