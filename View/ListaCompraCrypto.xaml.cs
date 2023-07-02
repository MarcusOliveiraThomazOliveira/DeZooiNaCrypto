using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model;

namespace DeZooiNaCrypto.View;

public partial class ListaCompraCrypto : ContentPage
{
    Usuario _usuario;
    CompraRepositorio _compraRepositorio = new CompraRepositorio();
    public ListaCompraCrypto()
    {
        InitializeComponent();
    }

    public ListaCompraCrypto(Usuario usuario)
    {
        _usuario = usuario;

        InitializeComponent();

        CarregaCompras();
    }

    private void CarregaCompras()
    {
        lvCompras.ItemsSource = _compraRepositorio.Listar(_usuario);
    }

    public void Cadastrar(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new CadastroCompraCrypto());
    }
}