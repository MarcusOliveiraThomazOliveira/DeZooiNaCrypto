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
        try
        {
            var usuarioLogado = _usuarioRepositorio.AutenticarUsuario(txtEmail.Text, txtSenha.Text);
            if (usuarioLogado == null)
            {
                DisplayAlert("Autenticar", "Login ou senha incorreto.", "OK");
            }
            else
            {
                txtEmail.Text = txtSenha.Text = string.Empty;
                //Navigation.PushAsync(new ListaCryptoMoeda(usuarioLogado));
                Navigation.PushAsync(new MainPage());
            }
        }
        catch (Exception ex)
        {

            DisplayAlert("Autenticar", ex.Message, "OK");
        }
    }

    private void CadastrarUsuario(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CadastroUsuario());
    }
}