using DevExpress.Maui.Controls;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.ViewModel;
using DeZooiNaCrypto.View;
using DeZooiNaCrypto.View.Cadastro;

namespace DeZooiNaCrypto;

public partial class MainPage : ContentPage
{
    Usuario _usuario;
    IDispatcherTimer timerAtualizaDados;
    CryptoMoedaViewModel _cryptoMoedaViewModel;
    public MainPage()
    {
        InitializeComponent();
    }

    public MainPage(Usuario usuario)
    {
        _usuario = usuario;
        _cryptoMoedaViewModel = new CryptoMoedaViewModel(_usuario);

        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            this.BindingContext = _cryptoMoedaViewModel;

            timerAtualizaDados = this.Dispatcher.CreateTimer();
            timerAtualizaDados.Interval = TimeSpan.FromSeconds(2);
            timerAtualizaDados.Tick += (sender, e) => AtualizaDados(sender, e);
            timerAtualizaDados.Start();
        }
        catch (Exception ex)
        {
            DisplayAlert("Crypto Moeda", ex.Message, "Que triste ;(");
        }
        
    }
    private void AtualizaDados(object sender, EventArgs e)
    {
        _cryptoMoedaViewModel.AtualizarValor();
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
        Navigation.PushAsync(new CadastroCryptoMoeda(_usuario));
    }

    private void Apagar(object sender, EventArgs e)
    {
        timerAtualizaDados.Stop();
        _cryptoMoedaViewModel.Apagar((Guid)((SimpleButton)sender).CommandParameter);
        timerAtualizaDados.Start();
    }

    private void Comprar(object sender, EventArgs e)
    {

    }

    private void Vender(object sender, EventArgs e)
    {
        Navigation.PushAsync(new VendaCryptoMoedaView(_usuario, (Guid)((SimpleButton)sender).CommandParameter));
    }

    private void SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        bsDetalheCryptoMoeda.State = BottomSheetState.HalfExpanded;
        if (bsDetalheCryptoMoeda.HalfExpandedRatio == 0.2) bsDetalheCryptoMoeda.Animate("bottomsheet", x => bsDetalheCryptoMoeda.HalfExpandedRatio = x, 0.2, 0.4);
    }

    private void Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        if (bsDetalheCryptoMoeda.HalfExpandedRatio == 0.4) bsDetalheCryptoMoeda.Animate("bottomsheet", x => bsDetalheCryptoMoeda.HalfExpandedRatio = x, 0.4, 0.2);
    }
}

