using DevExpress.Maui.Controls;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.ViewModel;
using DeZooiNaCrypto.View;
using DeZooiNaCrypto.View.Cadastro;
using Newtonsoft.Json;
using DeZooiNaCrypto.Util;
using DeZooiNaCrypto.View.Lista;
using DeZooiNaCrypto.Util.DriveGoogle;
using DeZooiNaCrypto.Testes;

namespace DeZooiNaCrypto;

public partial class MainPage : ContentPage
{
    Usuario _usuario;
    CryptoMoedaViewModel _cryptoMoedaViewModel;
    public MainPage()
    {
        _usuario = JsonConvert.DeserializeObject<Usuario>(Preferences.Get(Constantes.Usuario_Logado, string.Empty));
        _cryptoMoedaViewModel = new CryptoMoedaViewModel();

        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            this.BindingContext = _cryptoMoedaViewModel;
        }
        catch (Exception ex)
        {
            DisplayAlert("Crypto Moeda", ex.Message, "Que triste ;(");
        }

    }

    private void ApresentaMenu(object sender, EventArgs e)
    {
        actionsPopup.IsOpen = !actionsPopup.IsOpen;
    }

    private void Sair(object sender, EventArgs e)
    {
        ApresentaMenu(null, null);
        Preferences.Clear();
        _usuario = null;
        Navigation.PushAsync(new LoginUsuario());
    }

    private void CadastrarCryptoMoeda(object sender, EventArgs e)
    {
        ApresentaMenu(null, null);
        Navigation.PushAsync(new CadastroCryptoMoeda(_usuario));
    }

    private void Apagar(object sender, EventArgs e)
    {
        _cryptoMoedaViewModel.Apagar((Guid)((SimpleButton)sender).CommandParameter);
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

    private void Futuro(object sender, EventArgs e)
    {
        Preferences.Set(Constantes.Id, ((Guid)((SimpleButton)sender).CommandParameter).ToString());
        Navigation.PushAsync(new CadastroOperacaoFuturoView());
    }

    private void Spot(object sender, EventArgs e)
    {
        Preferences.Set(Constantes.Id, ((Guid)((SimpleButton)sender).CommandParameter).ToString());
        Navigation.PushAsync(new CadastroOperacaoSpotView());
    }

    private void Extrato(object sender, EventArgs e)
    {
        ApresentaMenu(null, null);
        Navigation.PushAsync(new ListaExtrato());
    }

    private void Teste(object sender, EventArgs e)
    {
        ApresentaMenu(null, null);
        Navigation.PushAsync(new TesteView());
    }
}

