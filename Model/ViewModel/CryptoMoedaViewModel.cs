
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class CryptoMoedaViewModel : INotifyPropertyChanged
    {
        private CryptoMoedaRepositorio _cryptoMoedaRepositorio = new CryptoMoedaRepositorio();
        readonly ObservableCollection<CryptoMoeda> _cryptoMoedas;

        public event PropertyChangedEventHandler PropertyChanged;
        public IReadOnlyList<CryptoMoeda> CryptoMoedas { get => _cryptoMoedas; }

        public CryptoMoedaViewModel(Usuario usuario)
        {
            _cryptoMoedas = _cryptoMoedaRepositorio.Listar(usuario);
        }

        protected void RaisePropertyChanged(string Valor)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(Valor));
        }
    }
}
