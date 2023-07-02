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
            if (string.IsNullOrEmpty(txtNome.Text)) { DisplayAlert("Cadastro Crypto", "É preciso informar um nome para a crypto.", "OK"); return; }
            if (pckMoedaPar.SelectedIndex < 0) { DisplayAlert("Cadastro Crypto", "É preciso informar uma moeda par para a crypto.", "OK"); return; }
            if (_cryptoRepositorio.Obter(_usuario, txtNome.Text.ToUpper(), pckMoedaPar.Items[pckMoedaPar.SelectedIndex]) != null) { DisplayAlert("Cadastro Crypto", "Essa Crypto já foi cadastrada.", "OK"); return; }
            if (_cryptoRepositorio.ObterPrecos(new List<string>() { txtNome.Text.ToUpper() + pckMoedaPar.Items[pckMoedaPar.SelectedIndex] }) == null) { DisplayAlert("Cadastro Crypto", "Crypto não encontrada.", "OK"); return; }
            _cryptoRepositorio.Salvar(new Crypto()
            {
                Nome = txtNome.Text.ToUpper(),
                NomeLongo = txtNomeLongo.Text,
                MoedaPar = pckMoedaPar.Items[pckMoedaPar.SelectedIndex],
                IdUsuario = _usuario.Id,
                Usuario = _usuario
            }); ; ;
            Navigation.PushAsync(new ListarCryptos(_usuario));
        }
        catch(Exception ex) 
        {
            DisplayAlert("Cadastro Crypto", ex.Message, "OK");
        }

    }

    private List<Crypto> Listar(Usuario usuario, String nome)
    {
        return _cryptoRepositorio.Listar(usuario, nome);
    }
}