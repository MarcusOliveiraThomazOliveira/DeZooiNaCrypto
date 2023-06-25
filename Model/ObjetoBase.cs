using SQLite;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace DeZooiNaCrypto.Model
{
    public class ObjetoBase : ObservableObject, INotifyPropertyChanged
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public ObjetoBase()
        {
            Id = Guid.NewGuid();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }
    }
}
