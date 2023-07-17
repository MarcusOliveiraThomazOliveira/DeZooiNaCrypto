using DevExpress.Maui.Controls;
using DevExpress.Maui.Editors;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.ViewModel;
using DeZooiNaCrypto.Util;

namespace DeZooiNaCrypto.View.Cadastro;

public partial class CadastroOperacaoFuturoView : ContentPage
{
    Usuario _usuario;
    Guid _idCryptoMoeda;
    OperacaoFuturoViewModel _operacaoFuturoViewModel;
    CryptoMoedaRepositorio _cryptoMoedaRepositorio = new CryptoMoedaRepositorio();
    public CadastroOperacaoFuturoView()
    {
        InitializeComponent();
    }
    public CadastroOperacaoFuturoView(Usuario usuario, Guid idCryptoMoeda)
    {
        InitializeComponent();

        _usuario = usuario;
        _idCryptoMoeda = idCryptoMoeda;
        _operacaoFuturoViewModel = new OperacaoFuturoViewModel(idCryptoMoeda, ccgMoedaPar, actionsPopup);
        this.Title = Constantes.Operacao_Futuro + " - " + _cryptoMoedaRepositorio.Obter(_idCryptoMoeda).NomeLongo;
        BindingContext = _operacaoFuturoViewModel;
    }

    private void ApresentaMenu(object sender, EventArgs e)
    {
        actionsPopup.IsOpen = !actionsPopup.IsOpen;
    }

    private void Gravar(object sender, EventArgs e)
    {
        _operacaoFuturoViewModel.Gravar();
        LimparTela();
    }

    private void Cancelar(object sender, EventArgs e)
    {
        LimparTela();
        actionsPopup.IsOpen = false;
    }

    private void LimparTela()
    {
        txtValorRetorno.Text = string.Empty;
        txtValorTaxa.Text = string.Empty;
        dtDataVenda.Focus();
    }

    private void Apagar(object sender, EventArgs e)
    {
        _operacaoFuturoViewModel.Apagar((Guid)((SimpleButton)sender).CommandParameter);
    }
}