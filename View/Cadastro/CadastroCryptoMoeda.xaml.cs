using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.Enumerador;

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
            TipoExchangeEnum TipoExchangeEnum = TipoExchangeEnum.NaoDefinida;
            switch (ccgCorretora.SelectedIndex)
            {
                case (int)TipoExchangeEnum.Binance:
                    TipoExchangeEnum = TipoExchangeEnum.Binance;
                    break;
                case (int)TipoExchangeEnum.BitGet:
                    TipoExchangeEnum = TipoExchangeEnum.BitGet;
                    break;
                case (int)TipoExchangeEnum.CoinMarketCap:
                    TipoExchangeEnum = TipoExchangeEnum.CoinMarketCap;
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
                    TipoCorretora = TipoExchangeEnum,
                    TipoMoedaPar = tipoMoedaParEnum,
                    IdUsuario = _usuario.Id,
                    Usuario = _usuario
                });
            Navigation.PushAsync(new MainPage());
        }
        else
        {
            DisplayAlert("Cadastro Crypto Moeda", "Crypto Moeda já cadastrada.", "Ok");
        }
    }

    private void Cancelar(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
}