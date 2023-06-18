using DeZooiNaCrypto.Model;
using Microsoft.VisualBasic;
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
        const string _dataBaseName = "DeZooiNaCrypto.db3";
        public const SQLite.SQLiteOpenFlags _flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public RepositorioBase()
        {
            _connection = GetConnection();
            BuildTables();
        }

        private SQLiteAsyncConnection GetConnection()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var fullBasePath = Path.Combine(basePath, _dataBaseName);

            return new SQLiteAsyncConnection(fullBasePath, _flags);
        }

        private void BuildTables()
        {

            //_connection.DropTableAsync<Usuario>().Wait();
            //_connection.DropTableAsync<Crypto>().Wait();

            _connection.CreateTableAsync<Usuario>().Wait();
            _connection.CreateTableAsync<Crypto>().Wait();
        }

        public Task<List<T>> Listar()
        {
            try
            {
                return _connection.Table<T>().ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public T Obter(Guid guid)
        {
            try
            {
                var nomeTabela = (new T()).GetType().Name;
                return _connection.QueryAsync<T>("select * from " + nomeTabela + " where {guid} = ", guid).Result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }

        public int Salvar(T objeto)
        {
            try
            {
                return _connection.InsertAsync(objeto).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Deletar(T objeto)
        {
            try
            {
                return _connection.DeleteAsync(objeto).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

    }
}
