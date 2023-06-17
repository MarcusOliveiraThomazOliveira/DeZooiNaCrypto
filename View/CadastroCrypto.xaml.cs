using DeZooiNaCrypto.Model;

namespace DeZooiNaCrypto.View;

public partial class CadastroCrypto : ContentPage
{
    private Usuario _usuario;
    public CadastroCrypto(Usuario usuario)
    {
        _usuario = usuario;
        InitializeComponent();
    }

    private void Gravar(object sender, EventArgs e)
    {
       
    }
}