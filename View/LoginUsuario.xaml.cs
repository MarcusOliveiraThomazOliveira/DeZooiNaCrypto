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
        var usuarioLogado = _usuarioRepositorio.AutenticarUsuario(txtEmail.Text, txtSenha.Text);
        if (usuarioLogado == null)
        {
            DisplayAlert("Autenticar", "Login ou senha incorreto.", "Cancelar");
        }
        else
        {
            Navigation.PushAsync(new ListarCryptos(usuarioLogado));
        }
    }

    private void CadastrarUsuario(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CadastroUsuario());
    }
}