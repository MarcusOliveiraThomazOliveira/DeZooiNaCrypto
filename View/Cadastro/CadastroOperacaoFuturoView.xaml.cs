using DevExpress.Maui.Controls;
using DevExpress.Maui.Editors;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.ViewModel;

namespace DeZooiNaCrypto.View.Cadastro;

public partial class CadastroOperacaoFuturoView : ContentPage
{
    Usuario _usuario;
    Guid _idCryptoMoeda;
    OperacaoFuturoViewModel _operacaoFuturoViewModel;
    public CadastroOperacaoFuturoView()
    {
        InitializeComponent();
    }
    public CadastroOperacaoFuturoView(Usuario usuario, Guid idCryptoMoeda)
    {
        InitializeComponent();

        _usuario = usuario;
        _idCryptoMoeda = idCryptoMoeda;
        _operacaoFuturoViewModel = new OperacaoFuturoViewModel(idCryptoMoeda, ccgMoedaPar, actionsPopup);
        BindingContext = _operacaoFuturoViewModel;
    }

    private void ApresentaMenu(object sender, EventArgs e)
    {
        actionsPopup.IsOpen = !actionsPopup.IsOpen;
    }

    private void Gravar(object sender, EventArgs e)
    {
        _operacaoFuturoViewModel.Gravar();
    }

    private void Cancelar(object sender, EventArgs e)
    {
        actionsPopup.IsOpen = false;
    }

    private void Apagar(object sender, EventArgs e)
    {
        _operacaoFuturoViewModel.Apagar((Guid)((SimpleButton)sender).CommandParameter);
    }
}