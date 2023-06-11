using DeZooiNaCrypto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Data
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>
    {
        public UsuarioRepositorio() : base()
        {
        }

        public Usuario AutenticarUsuario(string login, string password)
        {
            try
            {
                return _connection.QueryAsync<Usuario>("select * from usuario where email = @login and senha = @password", login, password).Result.FirstOrDefault();
            }
            catch
            {
                throw;
            }                                                                                                                                                                     
        }

    }

}
