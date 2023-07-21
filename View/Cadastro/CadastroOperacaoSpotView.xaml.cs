namespace DeZooiNaCrypto.View.Cadastro;

public partial class CadastroOperacaoSpotView : ContentPage
{
    public CadastroOperacaoSpotView()
    {
        InitializeComponent();
    }

    private async void ApresentaMenu(object sender, EventArgs e)
    {
        actionsPopup.IsOpen = !actionsPopup.IsOpen;
    }
}