using DeZooiNaCrypto.Model;
using DeZooiNaCrypto.Model.ViewModel;
using DeZooiNaCrypto.View;
using DeZooiNaCrypto.View.Cadastro;

namespace DeZooiNaCrypto;

public partial class MainPage : ContentPage
{
    Usuario _usuario;
    private IDispatcherTimer timerAtualizaDados;
    public MainPage()
    {
        InitializeComponent();
    }

    public MainPage(Usuario usuario)
    {
        _usuario = usuario;

        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        this.BindingContext = new CryptoMoedaViewModel(_usuario);

        timerAtualizaDados = this.Dispatcher.CreateTimer();
        timerAtualizaDados.Interval = TimeSpan.FromSeconds(2);
        timerAtualizaDados.Tick += (sender, e) => AtualizaDados(sender, e);
        timerAtualizaDados.Start();
    }
    private void AtualizaDados(object sender, EventArgs e)
    {
        this.BindingContext = new CryptoMoedaViewModel(_usuario);
    }

    private void ApresentaMenu(object sender, EventArgs e)
    {
        actionsPopup.IsOpen = !actionsPopup.IsOpen;
    }

    private void Sair(object sender, EventArgs e)
    {
        ApresentaMenu(null, null);
        timerAtualizaDados.Stop();
        _usuario = null;
        Navigation.PushAsync(new LoginUsuario());
    }

    private void CadastrarCryptoMoeda(object sender, EventArgs e)
    {
        ApresentaMenu(null, null);
        timerAtualizaDados.Stop();
        Navigation.PushAsync(new CadastroCryptoMoeda());
    }
}

