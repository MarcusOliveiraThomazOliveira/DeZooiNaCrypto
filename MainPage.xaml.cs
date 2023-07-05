using DeZooiNaCrypto.Model;
using DeZooiNaCrypto.Model.ViewModel;

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
}

