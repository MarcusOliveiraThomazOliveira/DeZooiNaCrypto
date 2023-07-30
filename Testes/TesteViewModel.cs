using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.ViewModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace DeZooiNaCrypto.Testes
{
    public class TesteViewModel : ModelViewBase
    {
        public List<RetornoCabecalhoDTO> RetornoCabecalhoDTOs { get; private set; } = new List<RetornoCabecalhoDTO>();

        public TesteViewModel()
        {
            RetornoCabecalhoDTOs = new();
            CarrgarLista();
        }

        private void CarrgarLista()
        {
            RetornoCabecalhoDTOs
                .Add(
                    new RetornoCabecalhoDTO(new DateTime(2023, 1, 1),
                        new List<RetornoDetallheDTO> {
                            new RetornoDetallheDTO{
                                Data = new DateTime(2023, 1, 1).ToString(),
                                Nome = "Menor que 10",
                                Descricao = "Descrição 1." },
                            new RetornoDetallheDTO{
                                Data = new DateTime(2023, 1, 1).ToString(),
                                Nome = "Menor que 10",
                                Descricao = "Descrição 2." }
                        }
                    ));

            RetornoCabecalhoDTOs
                .Add(
                    new RetornoCabecalhoDTO(new DateTime(2023, 3, 1),
                        new List<RetornoDetallheDTO> {
                            new RetornoDetallheDTO{
                                Data = new DateTime(2023, 3, 1).ToString(),
                                Nome = "Menor que 20",
                                Descricao = "Descrição 3." },
                            new RetornoDetallheDTO{
                                Data = new DateTime(2023, 3, 1).ToString(),
                                Nome = "Menor que 20",
                                Descricao = "Descrição 4." }
                        }
                    ));

            RetornoCabecalhoDTOs
                .Add(
                    new RetornoCabecalhoDTO(new DateTime(2023, 5, 1),
                        new List<RetornoDetallheDTO> {
                            new RetornoDetallheDTO{
                                Data = new DateTime(2023, 5, 1).ToString(),
                                Nome = "Menor que 30",
                                Descricao = "Descrição 5." },
                            new RetornoDetallheDTO{
                                Data = new DateTime(2023, 5, 1).ToString(),
                                Nome = "Menor que 30",
                                Descricao = "Descrição 6." }
                        }
                    ));

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
}
