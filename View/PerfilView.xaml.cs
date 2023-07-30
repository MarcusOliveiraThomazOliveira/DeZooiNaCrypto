namespace DeZooiNaCrypto.View;

public partial class PerfilView : ContentPage
{
	public PerfilView()
	{
		InitializeComponent();
	}

    private void CadastrarExchange(object sender, EventArgs e)
    {
        popupConfiguracaoExchange.IsOpen = !popupConfiguracaoExchange.IsOpen;
    }

    private void Gravar(object sender, EventArgs e)
    {

    }

    private void Cancelar(object sender, EventArgs e)
    {
        popupConfiguracaoExchange.IsOpen = !popupConfiguracaoExchange.IsOpen;
    }
}