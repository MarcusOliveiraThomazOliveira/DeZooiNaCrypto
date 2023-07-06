using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;

namespace DeZooiNaCrypto.View.Cadastro;

public partial class CadastroCryptoMoeda : ContentPage
{
    private Usuario _usuario;
    private CryptoMoedaRepositorio _cryptoMoedaRepositorio = new CryptoMoedaRepositorio();

    public CadastroCryptoMoeda()
    {
        InitializeComponent();
    }

    public CadastroCryptoMoeda(Usuario usuario)
    {
        _usuario = usuario;
        InitializeComponent();
    }

    private void Gravar(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txtNome.Text)) { DisplayAlert("Cadastro Crypto Moedas", "É preciso informar o nome.", "Ok"); return; }
        if (ccgCorretora.SelectedItem == null) { DisplayAlert("Cadastro Crypto Moedas", "É preciso informar uma corretora.", "Ok"); return; }
        if (ccgMoedaPar.SelectedItem == null) { DisplayAlert("Cadastro Crypto Moedas", "É preciso informar uma moeda par.", "Ok"); return; }
        if (_cryptoMoedaRepositorio.Obter(_usuario, txtNome.Text, ccgCorretora.SelectedIndex, ccgMoedaPar.SelectedIndex) == null)
        {
            TipoCorretoraEnum tipoCorretoraEnum = TipoCorretoraEnum.NaoDefinida;
            switch (ccgCorretora.SelectedIndex)
            {
                case (int)TipoCorretoraEnum.Binance:
                    tipoCorretoraEnum = TipoCorretoraEnum.Binance;
                    break;
                case (int)TipoCorretoraEnum.BitGet:
                    tipoCorretoraEnum = TipoCorretoraEnum.BitGet;
                    break;
                case (int)TipoCorretoraEnum.CoinMarketCap:
                    tipoCorretoraEnum = TipoCorretoraEnum.CoinMarketCap;
                    break;
            }

            TipoMoedaParEnum tipoMoedaParEnum = TipoMoedaParEnum.NaoDefinida;
            switch (ccgMoedaPar.SelectedIndex)
            {
                case (int)TipoMoedaParEnum.USD:
                    tipoMoedaParEnum = TipoMoedaParEnum.USD;
                    break;
                case (int)TipoMoedaParEnum.USDT:
                    tipoMoedaParEnum = TipoMoedaParEnum.USDT;
                    break;
            }

            _cryptoMoedaRepositorio
                .Salvar(new CryptoMoeda()
                {
                    Nome = txtNome.Text.Trim().ToUpper(),
                    TipoCorretora = tipoCorretoraEnum,
                    TipoMoedaPar = tipoMoedaParEnum,
                    IdUsuario = _usuario.Id,
                    Usuario = _usuario
                });
            Navigation.PushAsync(new MainPage(_usuario));
        }
        else
        {
            DisplayAlert("Cadastro Crypto Moeda", "Crypto Moeda já cadastrda.", "Ok");
        }



    }

    private void Cancelar(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage(_usuario));
    }
}