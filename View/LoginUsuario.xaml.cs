using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model;

namespace DeZooiNaCrypto.View;

public partial class LoginUsuario : ContentPage
{
    UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
    public LoginUsuario()
    {
        InitializeComponent();
    }

    private void Logar(object sender, EventArgs e)
    {
        if (_usuarioRepositorio.AutenticarUsuario(txtEmail.Text, txtSenha.Text) == null)
        {
            DisplayAlert("Autenticar", "Login ou senha incorreto.", "Cancelar");
        }
        else
        {
            Navigation.PushAsync(new ListaMinhasCryptos());
        }
    }

    private void CadastrarUsuario(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CadastroUsuario());
    }
}