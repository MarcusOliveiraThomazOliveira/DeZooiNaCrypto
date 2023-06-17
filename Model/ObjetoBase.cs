using SQLite;

namespace DeZooiNaCrypto.Model
{
    public class ObjetoBase
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public ObjetoBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
