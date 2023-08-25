using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.ViewModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace DeZooiNaCrypto.Testes
{
    public class TesteViewModel : ModelViewBase
    {

        public List<RetornoCabecalhoDTO> RetornoCabecalhoDTOs { get; private set; } = new List<RetornoCabecalhoDTO>();
        public List<RetornoListaSimples> RetornoListaSimples { get; private set; } = new List<RetornoListaSimples>();

        public TesteViewModel()
        {
            RetornoCabecalhoDTOs = new();
            CarrgarLista();

            CarregaListaSimples();
        }

        private void CarrgarLista()
        {
            for (int i = 0; i < 360; i++)
            {
                RetornoCabecalhoDTOs.Add(
                    new RetornoCabecalhoDTO(DateTime.Now,
                        new List<RetornoDetallheDTO> {
                            new RetornoDetallheDTO{
                                Data =  DateTime.Now.ToString(),
                                Nome = "Menor que 10",
                                Descricao = "Descrição " + i },
                            new RetornoDetallheDTO{
                                Data = DateTime.Now.ToString(),
                                Nome = "Menor que 10",
                                Descricao = "Descrição " + (i +1) }
                        })
                    );
            }
        }

        private void CarregaListaSimples()
        {
            for (int i = 0; i < 360; i++)
            {
                RetornoListaSimples.Add(
                    new RetornoListaSimples()
                    {
                        Nome = "Nome " + i.ToString(),
                        Descricao = "Descrição " + i.ToString()
                    }
                    );
            }
        }
    }

    public class RetornoCabecalhoDTO : List<RetornoDetallheDTO>
    {
        public RetornoCabecalhoDTO()
        {
        }
        public DateTime Data { get; set; }

        public RetornoCabecalhoDTO(DateTime data, List<RetornoDetallheDTO> retornoDetallheDTO) : base(retornoDetallheDTO)
        {
            Data = data;
        }
    }
    public class RetornoDetallheDTO
    {
        public String Data { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }

    public class RetornoListaSimples
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }

        public RetornoListaSimples()
        {
            DataCriacao = new DateTime();
        }
    }
}
