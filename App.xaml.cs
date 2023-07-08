using DeZooiNaCrypto.View;

namespace DeZooiNaCrypto;

public partial class App : Application
{
    public App()
    {
        DependencyService.Register<DeZooiNaCrypto.Util.IMessageService, DeZooiNaCrypto.Util.MessageService>();
        InitializeComponent();

        MainPage = new NavigationPage(new LoginUsuario());
    }
}
