using DevExpress.Maui.Controls;
using DeZooiNaCrypto.Model.ViewModel;

namespace DeZooiNaCrypto.View.Cadastro;

public partial class CadastroOperacaoSpotView : ContentPage
{
    OperacaoSpotViewModel _operacaoSpotViewModel;
    public CadastroOperacaoSpotView()
    {
        InitializeComponent();

        _operacaoSpotViewModel = new OperacaoSpotViewModel();
        BindingContext = _operacaoSpotViewModel;
    }

    private void ApresentaMenu(object sender, EventArgs e)
    {
        actionsPopup.IsOpen = !actionsPopup.IsOpen;
    }

    private void Apagar(object sender, EventArgs e)
    {
        _operacaoSpotViewModel.Apagar((Guid)((SimpleButton)sender).CommandParameter);
    }
}