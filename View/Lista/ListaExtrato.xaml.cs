using DevExpress.Maui.Controls;
using DeZooiNaCrypto.Model.Enumerador;
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
        bsFiltroPersonalizado.State = BottomSheetState.HalfExpanded;
        if (bsFiltroPersonalizado.HalfExpandedRatio == 0.2) bsFiltroPersonalizado.Animate("bottomsheet", x => bsFiltroPersonalizado.HalfExpandedRatio = x, 0.2, 0.4);
    }
    private async void Filtrar(object sender, EventArgs e)
    {
        if (await ((ExtratoViewModel)this.BindingContext).FiltrarPeriodo((int)TipoFiltroExtratoEnum.FiltroPersonalizado))
        {
            bsFiltroPersonalizado.State = BottomSheetState.Hidden;
        }
    }
    private void Cancelar(object sender, EventArgs e)
    {
        bsFiltroPersonalizado.State = BottomSheetState.Hidden;
    }
}