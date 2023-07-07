using DeZooiNaCrypto.Model.Entidade;

namespace DeZooiNaCrypto.View.Cadastro;

public partial class VendaCryptoMoeda : ContentPage
{
    Usuario _usuario;
    Guid _idCryptoMoeda;

    public VendaCryptoMoeda()
	{
		InitializeComponent();
	}

    public VendaCryptoMoeda(Usuario usuario, Guid idCryptoMoeda)
    {
        _usuario = usuario;
        _idCryptoMoeda = idCryptoMoeda;

        InitializeComponent();
    }
}