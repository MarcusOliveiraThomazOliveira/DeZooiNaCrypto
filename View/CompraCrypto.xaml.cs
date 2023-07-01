using DeZooiNaCrypto.Model;

namespace DeZooiNaCrypto.View;

public partial class CompraCrypto : ContentPage
{
    private Usuario _usuario;
    public CompraCrypto()
	{
		InitializeComponent();
	}

    public CompraCrypto(Usuario usuario)
    {
        _usuario = usuario;

        InitializeComponent();
    }
}