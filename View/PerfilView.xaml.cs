using DeZooiNaCrypto.Model.ViewModel;

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

    private async void Gravar(object sender, EventArgs e)
    {
        if (await ((PerfilViewModel)this.BindingContext).Gravar())
        {
            popupConfiguracaoExchange.IsOpen = !popupConfiguracaoExchange.IsOpen;
        }
    }

    private void Cancelar(object sender, EventArgs e)
    {
        popupConfiguracaoExchange.IsOpen = !popupConfiguracaoExchange.IsOpen;
    }
}