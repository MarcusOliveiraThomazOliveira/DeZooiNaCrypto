using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model;

namespace DeZooiNaCrypto.View;

public partial class ListarCryptos : ContentPage
{
    private Usuario _usuario;
    private CryptoRepositorio _cryptoRepositorio = new CryptoRepositorio();
    public ListarCryptos(Usuario usuario)
    {
        _usuario = usuario;
        InitializeComponent();
        CarregarCryptos();
    }

    private void CarregarCryptos()
    {
        var lista = _cryptoRepositorio.Listar(_usuario);
    }

    private void CadastrarCrypto(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CadastroCrypto(_usuario));
    }

    private void Sair(object sender, EventArgs e)
    {
        _usuario = null;
        Navigation.PushAsync(new LoginUsuario());
    }
}