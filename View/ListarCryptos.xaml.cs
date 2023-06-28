using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model;
using DeZooiNaCrypto.Util;
using System.Collections.ObjectModel;

namespace DeZooiNaCrypto.View;

public partial class ListarCryptos : ContentPage
{
    private Usuario _usuario;
    private List<Crypto> _lstCryptos;
    private CryptoRepositorio _cryptoRepositorio = new CryptoRepositorio();
    private IDispatcherTimer timerAtualizaDados;
    public ListarCryptos(Usuario usuario)
    {
        _usuario = usuario;

        InitializeComponent();

        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        CarregarCryptos();

        timerAtualizaDados = lvCryptos.Dispatcher.CreateTimer();
        ConfiguraCarregamentoAutomatico();
    }

    private void CarregarCryptos()
    {
        _lstCryptos = _cryptoRepositorio.Listar(_usuario).ToList();
        lvCryptos.ItemsSource = _lstCryptos;
        AtualizaDados(null, null);
    }

    private void ConfiguraCarregamentoAutomatico()
    {
        timerAtualizaDados.Interval = TimeSpan.FromSeconds(3);
        timerAtualizaDados.Tick += (sender, e) => AtualizaDados(sender, e);
        timerAtualizaDados.Start();
    }

    private void AtualizaDados(object sender, EventArgs e)
    {
        if (_lstCryptos.Count > 0)
        {
            var retorno = _cryptoRepositorio.ObterPrecos(_lstCryptos.Select(x => (x.Nome + x.MoedaPar).Replace(" ", "")));
            foreach (var cryptoAtualizada in retorno)
            {
                var crypto = _lstCryptos.Where(c => c.Nome + c.MoedaPar == cryptoAtualizada.Symbol).FirstOrDefault();
                if (crypto != null)
                    crypto.Valor = cryptoAtualizada.Price;
            }
        }
    }

    private void CadastrarCrypto(object sender, EventArgs e)
    {
        timerAtualizaDados.Stop();
        Navigation.PushAsync(new CadastroCrypto(_usuario));
    }

    private void Sair(object sender, EventArgs e)
    {
        timerAtualizaDados.Stop();
        _usuario = null;
        Navigation.PushAsync(new LoginUsuario());
    }

    private void Historico(object sender, EventArgs e)
    {
        timerAtualizaDados.Stop();
        Navigation.PushAsync(new HistoricoCrypto(_usuario));
    }

    private void ApagarCrypto(object sender, EventArgs e)
    {

    }
}