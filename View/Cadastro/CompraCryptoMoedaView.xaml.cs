using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.ViewModel;

namespace DeZooiNaCrypto.View.Cadastro;

public partial class CompraCryptoMoedaView : ContentPage
{
    Usuario _usuario;
    Guid _idCryptoMoeda;
    CompraCryptoMoedaViewModel _compraCryptoMoedaViewModel;

    public CompraCryptoMoedaView()
    {
        InitializeComponent();
    }

    public CompraCryptoMoedaView(Usuario usuario, Guid idCryptoMoeda)
    {
        _usuario = usuario;
        _idCryptoMoeda = idCryptoMoeda;
        
        _compraCryptoMoedaViewModel = new CompraCryptoMoedaViewModel(_usuario, _idCryptoMoeda);

        InitializeComponent();
    }
}