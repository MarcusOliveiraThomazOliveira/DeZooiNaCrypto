namespace DeZooiNaCrypto.View;

public partial class LoginUsuario : ContentPage
{
	public LoginUsuario()
	{
		InitializeComponent();
	}

    private void Logar(object sender, EventArgs e)
    {

    }

    private void CadastrarUsuario(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CadastroUsuario());
    }
}