using DevExpress.Office.Forms;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class PerfilViewModel : ViewModelBase
    {
        UsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        Usuario _usuario;
        string _nome;

        public string Nome { get { return _nome; } private set { _nome = value; OnPropertyChanged("Nome"); } }

        public PerfilViewModel()
        {
            _usuario = JsonConvert.DeserializeObject<Usuario>(Preferences.Get(Constantes.Usuario_Logado, string.Empty));
            _nome = _usuario.Nome;
        }
    }
}
