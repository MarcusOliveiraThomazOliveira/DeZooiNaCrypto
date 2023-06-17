using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model;
using System;

namespace DeZooiNaCrypto.View;

public partial class CadastroCrypto : ContentPage
{
    private Usuario _usuario;
    private CryptoRepositorio _cryptoRepositorio = new CryptoRepositorio();
    public CadastroCrypto(Usuario usuario)
    {
        _usuario = usuario;
        InitializeComponent();
    }

    private void Gravar(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtNome.Text)) { DisplayAlert("Cadastro Crypto", "É preciso informar um nome para a crypto.", "OK"); }
            _cryptoRepositorio.Salvar(new Crypto() { Nome = txtNome.Text, Usuario = _usuario, IdUsuario  = _usuario.Id});
            Navigation.PushAsync(new ListarCryptos(_usuario));
        }
        catch (Exception ex)
        {
            DisplayAlert("Cadastro Crypto", ex.Message, "OK");
        }

    }

    private List<Crypto> Listar(Usuario usuario, String nome)
    {
        return _cryptoRepositorio.Listar(usuario, nome);
    }
}