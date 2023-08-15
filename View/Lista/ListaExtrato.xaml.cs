using DevExpress.Maui.Controls;
using DeZooiNaCrypto.Model.ViewModel;

namespace DeZooiNaCrypto.View;
public partial class ListaExtrato : ContentPage
{
    public ListaExtrato()
    {
        InitializeComponent();
    }

    private void FiltroPersonalizado(object sender, System.ComponentModel.HandledEventArgs e)
    {
        ((ExtratoViewModel)this.BindingContext).FiltrarPeriodo(-1);
        bsFiltroPersonalizado.State = BottomSheetState.FullExpanded;
    }
    private void Filtrar(object sender, EventArgs e)
    {
    }
    private void Cancelar(object sender, EventArgs e)
    {
        bsFiltroPersonalizado.State = BottomSheetState.Hidden;
    }
}