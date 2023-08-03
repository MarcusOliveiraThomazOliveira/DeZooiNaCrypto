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
        readonly OperacaoFuturoRepositorio _operacaoFuturoRepositorio = new();
        readonly BinanceService _binanceService = new();
        readonly Usuario _usuario;
        ObservableCollection<ConfiguracaoExchange> _configuracoesExchange = new();
        string _nome;
        int _setTipoExchange;
        ConfiguracaoExchange _configuracaoExchange;

        public string Nome { get { return _nome; } private set { _nome = value; OnPropertyChanged("Nome"); } }
        public ConfiguracaoExchange ConfiguracaoExchange { get { return _configuracaoExchange; } private set{_configuracaoExchange = value; OnPropertyChanged("ConfiguracaoExchange"); } }
        public ObservableCollection<ConfiguracaoExchange> ConfiguracoesExchange
        {
            get { return _configuracoesExchange; }
            set { _configuracoesExchange = value; OnPropertyChanged("ConfiguracoesExchange"); }
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

            if (ConfiguracaoExchange.DataInicioOperacaoExchange == DateTime.MinValue) { await MessageService.DisplayAlert_OK("Informe a data que começou a operara na exchange."); return false; }
            if (string.IsNullOrEmpty(ConfiguracaoExchange.UrlFuturoBase)) { await MessageService.DisplayAlert_OK("Informe a URL da API Futuro."); return false; }
            if (string.IsNullOrEmpty(ConfiguracaoExchange.UrlSpotBase)) { await MessageService.DisplayAlert_OK("Informe a URL da API Spot."); return false; }
            if (string.IsNullOrEmpty(ConfiguracaoExchange.ChaveDaAPI)) { await MessageService.DisplayAlert_OK("Informe a chave da API."); return false; }
            if (string.IsNullOrEmpty(ConfiguracaoExchange.ChaveSecretaDaAPI)) { await MessageService.DisplayAlert_OK("Informea chave secreta da API."); return false; }

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
            await binanceService.Sincronizar();

            return true;
        }
        public void ApagarOperacoesFuturo()
        {
            _operacaoFuturoRepositorio.DeletarTudo();
        }
        public async Task<bool> Sincronizar()
        {
            return await _binanceService.Sincronizar();
        }
        internal void CarregarConfiguracaoExchange(Guid idConfiguracaoExchange)
        {
            ConfiguracaoExchange = _configuracaoExchangeRepositorio.Obter(idConfiguracaoExchange);
        }
    }
}
