
using Android.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public partial class CryptoMoedaViewModel
    {
        CryptoMoedaRepositorio _cryptoMoedaRepositorio = new CryptoMoedaRepositorio();
        ObservableCollection<CryptoMoeda> _cryptoMoedas;
        public ObservableCollection<CryptoMoeda> CryptoMoedas { 
            get { return _cryptoMoedas; }
            set { _cryptoMoedas = value; } }

        public CryptoMoedaViewModel(Usuario usuario)
        {
            _cryptoMoedas = _cryptoMoedaRepositorio.Listar(usuario);
            AtualizarValor();
        }

        public void AtualizarValor()
        {
            _cryptoMoedaRepositorio.ObterValores(_cryptoMoedas);
        }
    }
}
