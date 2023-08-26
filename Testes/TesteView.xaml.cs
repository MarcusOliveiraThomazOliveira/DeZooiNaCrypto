using DevExpress.Maui.Controls;
using DeZooiNaCrypto.Testes;

namespace DeZooiNaCrypto.View;

public partial class TesteView : ContentPage
{
    public TesteView()
    {
        InitializeComponent();
    }
    private async void FiltrarPeriodo(object sender, DevExpress.Maui.Editors.ChipEventArgs e)
    {
        try
        {
            Util.Load.Loading(bvCarregando, aiCarregando);
            await ((TesteViewModel)this.BindingContext).CarregaListaSimples();
        }
        finally
        {
            Util.Load.Loading(bvCarregando, aiCarregando);
        }

    }
}