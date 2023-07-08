using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.ViewModel;

namespace DeZooiNaCrypto.View.Cadastro;

public partial class VendaCryptoMoedaView : ContentPage
{
    Usuario _usuario;
    Guid _idCryptoMoeda;

    public VendaCryptoMoedaView()
    {
        InitializeComponent();
    }

    public VendaCryptoMoedaView(Usuario usuario, Guid idCryptoMoeda)
    {
        _usuario = usuario;
        _idCryptoMoeda = idCryptoMoeda;

        BindingContext = new VendaCryptoMoedaViewModel(_idCryptoMoeda);

        InitializeComponent();
    }

    private void Cancelar(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage(_usuario));
    }
}