using DevExpress.Maui.Controls;
using DevExpress.Maui.Editors;
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
        bsFiltroPersonalizado.State = BottomSheetState.HalfExpanded;
        if (bsFiltroPersonalizado.HalfExpandedRatio == 0.2) bsFiltroPersonalizado.Animate("bottomsheet", x => bsFiltroPersonalizado.HalfExpandedRatio = x, 0.2, 0.4);

    }
    private async void Filtrar(object sender, EventArgs e)
    {
        try
        {
            if (sender.GetType() == typeof(ChoiceChipGroup))
            {
                if (((ChoiceChipGroup)sender).SelectedIndex != 4)
                {
                    Util.Load.Loading(bvCarregando, aiCarregando);

                    await ((ExtratoViewModel)this.BindingContext).FiltrarPeriodo(((ChoiceChipGroup)sender).SelectedIndex);
                }
            }
            else
            {
                bsFiltroPersonalizado.State = BottomSheetState.Hidden;

                await ((ExtratoViewModel)this.BindingContext).FiltrarPeriodo((int)TipoFiltroExtratoEnum.FiltroPersonalizado);
            }
        }
        finally
        {
            if ((((ChoiceChipGroup)sender).SelectedIndex != 4) || (sender.GetType() == typeof(SimpleButton)))
                Util.Load.Loading(bvCarregando, aiCarregando);
        }

    }
    private void Cancelar(object sender, EventArgs e)
    {
        bsFiltroPersonalizado.State = BottomSheetState.Hidden;
    }
}