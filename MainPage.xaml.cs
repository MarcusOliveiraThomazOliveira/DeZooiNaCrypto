using DevExpress.Maui.Controls;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.ViewModel;
using Newtonsoft.Json;
using DeZooiNaCrypto.Util;
using DeZooiNaCrypto.Testes;

namespace DeZooiNaCrypto.View;

public partial class MainPage : ContentPage
{
    Usuario _usuario;
    CryptoMoedaViewModel _cryptoMoedaViewModel;
    public MainPage()
    {
        _usuario = JsonConvert.DeserializeObject<Usuario>(Preferences.Get(Constantes.Usuario_Logado, string.Empty));        

        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            _cryptoMoedaViewModel = new CryptoMoedaViewModel();
            this.BindingContext = _cryptoMoedaViewModel;
        }
        catch (Exception ex)
        {
            DisplayAlert("Crypto Moeda", ex.Message, "Que triste ;(");
        }

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
        Navigation.PushAsync(new ListaExtrato());
    }

    private void Teste(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TesteView());
    }

    private void Perfil(object sender, EventArgs e)
    {
        _cryptoMoedaViewModel.PararAtualizacaoValorCryptoMoeda();
        Navigation.PushAsync(new PerfilView());
    }
}

