using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model;

namespace DeZooiNaCrypto.View;

public partial class CadastroCompraCrypto : ContentPage
{
    Usuario _usuario;
    Crypto _crypto;
    CompraRepositorio _compraRepositorio = new CompraRepositorio();
    public CadastroCompraCrypto()
    {
        InitializeComponent();
    }
    public CadastroCompraCrypto(Usuario usuario, Crypto crypto)
    {
        _usuario = usuario;
        _crypto = crypto;  
        InitializeComponent();
    }

    private void Gravar(object sender, EventArgs e)
    {
        if (pckData.Date == DateTime.MinValue) { DisplayAlert("Cadastro Compra Crypto", "É preciso informar a data da compra.", "Ok"); return; }
        if (String.IsNullOrEmpty(txtQuantidade.Text) ||
            (!String.IsNullOrEmpty(txtQuantidade.Text) && Decimal.Parse(txtQuantidade.Text) <= 0)) { DisplayAlert("Cadastro Compra Crypto", "É preciso informar a quantidade mario que zero.", "Ok"); return; }
        if (String.IsNullOrEmpty(txtValorUnitario.Text) ||
            (!String.IsNullOrEmpty(txtValorUnitario.Text) && Decimal.Parse(txtValorUnitario.Text) <= 0)) { DisplayAlert("Cadastro Compra Crypto", "É preciso informar a quantidade mario que zero.", "Ok"); return; }
        _compraRepositorio.Salvar(new Compra() { Crypto = _crypto, IdCrypto = _crypto.Id, DataCompra = pckData.Date, Quantidade = Decimal.Parse(txtQuantidade.Text) , ValorUnitario = Decimal.Parse(txtValorUnitario.Text) });
        Navigation.PushAsync(new ListaCompraCrypto(_usuario, _crypto));
    }
}