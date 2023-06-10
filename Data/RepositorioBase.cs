using DeZooiNaCrypto.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Data
{
    public class RepositorioBase<T> where T : new()
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

        public Task<List<T>> Lista()
        {
            return _connection.Table<T>().ToListAsync();
        }

        public T Obter(Guid guid)
        {
            var nomeTabela = (new T()).GetType().Name;
            return _connection.QueryAsync<T>("select * from " + nomeTabela + " where {guid} = ", guid).Result.FirstOrDefault();
        }

    }
}
