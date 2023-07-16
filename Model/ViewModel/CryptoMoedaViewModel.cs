
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
        OperacaoFuturoRepositorio _operacaoFuturoRepositorio = new OperacaoFuturoRepositorio();
        ObservableCollection<CryptoMoeda> _cryptoMoedas;
        IDispatcherTimer timerAtualizaDados;
        CryptoMoeda cryptoMoedaSelecionada;
        decimal saldo;
        int quantidadeOperacoes;

        public ObservableCollection<CryptoMoeda> CryptoMoedas
        {
            get { return _cryptoMoedas; }
            set { _cryptoMoedas = value; }
        }
        public CryptoMoeda CryptoMoedaSelecionada { get { return cryptoMoedaSelecionada; } set { cryptoMoedaSelecionada = value; PreencheValores(); } }
        public decimal Saldo { get { return saldo; } set { saldo = value; RaisePropertyChanged(); } }
        public int QuantidadeOperacoes { get { return quantidadeOperacoes; } set { quantidadeOperacoes = value; RaisePropertyChanged(); } }
        public decimal ParticipacaoTotal { get; set; }
        public CryptoMoedaViewModel()
        {
            _cryptoMoedas = _cryptoMoedaRepositorio.Listar(JsonConvert.DeserializeObject<Usuario>(Preferences.Get(Constantes.Usuario_Logado, string.Empty)));
            AtualizarValor();
            ConfiguraAtualizacao();
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
        private void PreencheValores()
        {
            Saldo = _operacaoFuturoRepositorio.TotalOperacaoFuturo(CryptoMoedaSelecionada.Id);
            QuantidadeOperacoes = _operacaoFuturoRepositorio.QuantidadeOperacoes(CryptoMoedaSelecionada.Id);

        }

    }
}
