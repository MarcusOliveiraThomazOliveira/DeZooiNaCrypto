using SQLite;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace DeZooiNaCrypto.Model.Entidade
{
    public class EntidadeBase : ObservableObject, INotifyPropertyChanged
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public EntidadeBase()
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
