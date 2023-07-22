using DevExpress.Maui.Controls;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.ViewModel;
using DeZooiNaCrypto.Util;

namespace DeZooiNaCrypto.View.Cadastro;

public partial class CadastroOperacaoSpotView : ContentPage
{
    OperacaoSpotViewModel _operacaoSpotViewModel;
    CryptoMoedaRepositorio _cryptoMoedaRepositorio = new CryptoMoedaRepositorio();
    public CadastroOperacaoSpotView()
    {
        InitializeComponent();
        this.Title = Constantes.Operacao_Spot + " - " + _cryptoMoedaRepositorio.Obter(new Guid(Preferences.Get(Constantes.Id, string.Empty))).NomeLongo;
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