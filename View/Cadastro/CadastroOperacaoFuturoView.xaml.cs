using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.ViewModel;

namespace DeZooiNaCrypto.View.Cadastro;

public partial class CadastroOperacaoFuturoView : ContentPage
{
    Usuario _usuario;
    Guid _idCryptoMoeda;
    public CadastroOperacaoFuturoView()
    {
        InitializeComponent();
    }
    public CadastroOperacaoFuturoView(Usuario usuario, Guid idCryptoMoeda)
    {
        _usuario = usuario;
        _idCryptoMoeda = idCryptoMoeda;
        BindingContext = new OperacaoFuturoViewModel(idCryptoMoeda);

        InitializeComponent();
    }

    private void ApresentaMenu(object sender, EventArgs e)
    {
        actionsPopup.IsOpen = !actionsPopup.IsOpen;
    }
}