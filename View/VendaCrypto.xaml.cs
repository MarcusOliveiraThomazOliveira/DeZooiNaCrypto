using DeZooiNaCrypto.Model;

namespace DeZooiNaCrypto.View;

public partial class VendaCrypto : ContentPage
{
    private Usuario _usuario;
    public VendaCrypto()
	{
		InitializeComponent();
	}

    public VendaCrypto(Usuario usuario)
    {
        _usuario = usuario;

        InitializeComponent();
    }
}