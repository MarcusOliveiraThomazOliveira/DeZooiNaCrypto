using DevExpress.Maui.Controls;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.ViewModel;
using DeZooiNaCrypto.Util;
using Newtonsoft.Json;

namespace DeZooiNaCrypto.View;

public partial class CadastroOperacaoFuturoView : ContentPage
{
    readonly Guid _idCryptoMoeda;
    readonly OperacaoFuturoViewModel _operacaoFuturoViewModel = new();
    readonly CryptoMoedaRepositorio _cryptoMoedaRepositorio = new();
    public CadastroOperacaoFuturoView()
    {
        InitializeComponent();

        _idCryptoMoeda = new(Preferences.Get(Constantes.Id, string.Empty));
        this.Title = Constantes.Operacao_Futuro + " - " + _cryptoMoedaRepositorio.Obter(_idCryptoMoeda).NomeLongo;
        BindingContext = _operacaoFuturoViewModel;
    }

    private void ApresentaMenu(object sender, EventArgs e)
    {
        //actionsPopup.IsOpen = !actionsPopup.IsOpen;
    }

    private void Gravar(object sender, EventArgs e)
    {
        _operacaoFuturoViewModel.Gravar();
        LimparTela();
    }

    private void Cancelar(object sender, EventArgs e)
    {
        //LimparTela();
        //actionsPopup.IsOpen = false;
    }

    private void LimparTela()
    {
        //txtValorRetorno.Text = string.Empty;
        //txtValorTaxa.Text = string.Empty;
        //dtDataVenda.Focus();
    }

    private void Apagar(object sender, EventArgs e)
    {
        //_operacaoFuturoViewModel.Apagar((Guid)((SimpleButton)sender).CommandParameter);
    }

    private void Editar(object sender, EventArgs e)
    {
        //_operacaoFuturoViewModel.Editar((Guid)((SimpleButton)sender).CommandParameter);
        //ApresentaMenu(null, null);
    }

    private void Cadastrar(object sender, EventArgs e)
    {
        _operacaoFuturoViewModel.CriarObjetoInsercao();
        ApresentaMenu(null, null);
    }
}
