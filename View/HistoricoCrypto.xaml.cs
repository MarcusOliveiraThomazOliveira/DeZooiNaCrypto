using DeZooiNaCrypto.Model;

namespace DeZooiNaCrypto.View;

public partial class HistoricoCrypto : ContentPage
{
    private Usuario _usuario;
    public HistoricoCrypto(Usuario usuario)
	{
		_usuario = usuario;
		InitializeComponent();
	}
}