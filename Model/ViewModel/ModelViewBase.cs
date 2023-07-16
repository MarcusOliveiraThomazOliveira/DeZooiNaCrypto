
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class ModelViewBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
