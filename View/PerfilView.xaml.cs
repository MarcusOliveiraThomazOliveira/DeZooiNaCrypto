using DevExpress.Maui.Controls;
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

    private void ApagarOperacoesFuturo(object sender, EventArgs e)
    {
        ((PerfilViewModel)this.BindingContext).ApagarOperacoesFuturo();
    }

    private async void Sincronizar(object sender, EventArgs e)
    {
       await ((PerfilViewModel)this.BindingContext).Sincronizar();
    }

    private void Editar(object sender, EventArgs e)
    {
        ((PerfilViewModel)this.BindingContext).CarregarConfiguracaoExchange((Guid)((SimpleButton)sender).CommandParameter);
        popupConfiguracaoExchange.IsOpen = !popupConfiguracaoExchange.IsOpen;
    }
}