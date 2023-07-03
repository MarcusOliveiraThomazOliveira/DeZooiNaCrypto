using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model;

namespace DeZooiNaCrypto.View;

public partial class ListaCompraCrypto : ContentPage
{
    Usuario _usuario;
    Crypto _crypto;
    CompraRepositorio _compraRepositorio = new CompraRepositorio();
    public ListaCompraCrypto()
    {
        InitializeComponent();
    }

    public ListaCompraCrypto(Usuario usuario, Crypto crypto)
    {
        _usuario = usuario;
        _crypto = crypto;

        InitializeComponent();

        CarregaCompras();  
    }

    private void CarregaCompras()
    {
        lvCompras.ItemsSource = _compraRepositorio.Listar(_usuario, _crypto);
    }

    public void Cadastrar(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new CadastroCompraCrypto(_usuario, _crypto));
    }
}