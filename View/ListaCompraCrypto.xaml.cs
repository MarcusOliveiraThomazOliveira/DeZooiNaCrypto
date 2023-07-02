using DeZooiNaCrypto.Model;

namespace DeZooiNaCrypto.View;

public partial class ListaCompraCrypto : ContentPage
{
    private Usuario _usuario;
    public ListaCompraCrypto()
	{
		InitializeComponent();
	}

    public ListaCompraCrypto(Usuario usuario)
    {
        _usuario = usuario;

        InitializeComponent();
    }
}                                                     