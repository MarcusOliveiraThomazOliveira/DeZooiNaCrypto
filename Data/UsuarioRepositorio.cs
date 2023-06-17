using DeZooiNaCrypto.Model;
using DeZooiNaCrypto.Util;
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
                if (string.IsNullOrEmpty(login)) { throw new Exception("É preciso informar um login válido"); }
                if (!Validacao.ehEmail(login)) { throw new Exception("É preciso informar um login válido"); }
                if (string.IsNullOrEmpty(password)) { throw new Exception("É preciso informar uma senha válida"); }
                

                var senhaCriptografada = Criptografia.GerarCriptografia(password);
                return _connection.QueryAsync<Usuario>("select * from usuario where email = @login and senha = @password", login, senhaCriptografada).Result.FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

    }

}
