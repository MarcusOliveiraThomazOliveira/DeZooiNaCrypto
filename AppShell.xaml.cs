using DeZooiNaCrypto.View;

namespace DeZooiNaCrypto;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ListaExtrato), typeof(ListaExtrato));
        Routing.RegisterRoute(nameof(PerfilView), typeof(PerfilView));
    }
}
