using DeZooiNaCrypto.Data;

namespace DeZooiNaCrypto.View;

public partial class LoginUsuario : ContentPage
{
    UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio("");
    public LoginUsuario()
    {
        InitializeComponent();
    }

    private void Logar(object sender, EventArgs e)
    {
        var usuario = _usuarioRepositorio.AutenticarUsuario(txtEmail.Text, txtSenha.Text);
        if (usuario == null)
        {
            DisplayAlert("Autenticar", "Login ou senha incorreto.", "Cancelar");
        }
    }

    private void CadastrarUsuario(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CadastroUsuario());
    }
}