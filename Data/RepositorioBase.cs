using DeZooiNaCrypto.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Data
{
    public class RepositorioBase<T> where T : ObjetoBase
    {
        protected readonly SQLiteAsyncConnection _connection;

        public RepositorioBase(string connectionString)
        {
            _connection = new SQLiteAsyncConnection(connectionString);
            BuildTables();
        }

        private void BuildTables()
        {
            _connection.CreateTableAsync<Usuario>().Wait();
        }
    }
}
