namespace DeZooiNaCrypto.View.Cadastro;

public partial class CadastroOperacaoSpotView : ContentPage
{
    public CadastroOperacaoSpotView()
    {
        InitializeComponent();
    }

    private void ApresentaMenu(object sender, EventArgs e)
    {
        actionsPopup.IsOpen = !actionsPopup.IsOpen;
    }
}