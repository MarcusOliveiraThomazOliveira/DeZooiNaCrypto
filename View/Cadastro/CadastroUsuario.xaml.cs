using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;

namespace DeZooiNaCrypto.View;

public partial class CadastroUsuario : ContentPage
{
    UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
    public CadastroUsuario()
    {
        InitializeComponent();
    }

    private void Gravar(object sender, EventArgs e)
    {
        try
        {
            var senhaCriptografada = Criptografia.GerarCriptografia(txtSenha.Text);
            var retorno = _usuarioRepositorio.Salvar(new Usuario() { Nome = txtNome.Text, Email = txtEmail.Text.ToLower(), Senha = senhaCriptografada });
            Navigation.PushAsync(new View.LoginUsuario());
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", ex.Message, "Fechar");
        }
        
    }
}