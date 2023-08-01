using DeZooiNaCrypto.Model.Entidade;
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
                if (!Validacao.ehEmail(login)) { throw new Exception("Não é um e-mail válido"); }
                if (string.IsNullOrEmpty(password)) { throw new Exception("É preciso informar uma senha válida"); }
                

                var senhaCriptografada = Criptografia.GerarCriptografiaMD5(password);
                return _connection.QueryAsync<Usuario>("select * from usuario where email = @login and senha = @password", login.ToLower(), senhaCriptografada).Result.FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

    }

}
