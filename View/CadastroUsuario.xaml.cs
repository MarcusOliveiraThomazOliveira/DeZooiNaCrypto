using DeZooiNaCrypto.Data;

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
        var retorno = _usuarioRepositorio.Salvar(new Model.Usuario() { Nome = txtNome.Text, Email = txtEmail.Text, Senha = txtSenha.Text });
    }
}