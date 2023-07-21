using DeZooiNaCrypto.View;

namespace DeZooiNaCrypto;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new LoginUsuario());
    }
}
