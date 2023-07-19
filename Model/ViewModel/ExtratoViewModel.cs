using DevExpress.XtraPrinting.Native;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.DTO;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class ExtratoViewModel : ModelViewBase
    {
        readonly IMessageService messageService;
        OperacaoFuturoRepositorio operacaoFuturoRepositorio = new();
        CryptoMoedaRepositorio cryptoMoedaRepositorio = new();
        ObservableCollection<OperacaoDTO> operacoesDTO;
        int filtrarPeriodo;
        decimal valorTotal;
        string valorTotalStr;
        public ObservableCollection<OperacaoDTO> OperacoesDTO { get { return operacoesDTO; } set { operacoesDTO = value; } }
        public OperacaoDTO OperacaoDTO { get; set; }
        public decimal ValorTotal { get { return valorTotal; } set { valorTotal = value; RaisePropertyChanged(); } }
        public string ValorTotalStr { get { return valorTotalStr; } set { valorTotalStr = value; RaisePropertyChanged(); } }
        public int Filtrar
        {
            get { return filtrarPeriodo; }
            set { filtrarPeriodo = value; FiltrarPeriodo(value); }
        }
        public ExtratoViewModel()
        {
            messageService = DependencyService.Get<IMessageService>();
            OperacoesDTO = new();
            FiltrarPeriodo(0);
        }
        public void FiltrarPeriodo(int tipoFiltro)
        {
            operacoesDTO.Clear();
            List<OperacaoFuturoCryptoMoeda> listaRetorno;
            switch (tipoFiltro)
            {
                case 0:
                    listaRetorno = operacaoFuturoRepositorio.Listar().Result;
                    break;
                case 1:
                    listaRetorno = operacaoFuturoRepositorio.Listar(DateTime.Now.Date, DateTime.Now.Date);
                    break;
                case 2:
                    listaRetorno = operacaoFuturoRepositorio.Listar(DateTime.Now.FirstDayOfWeek(), DateTime.Now.LastDayOfWeek());
                    break;
                case 3:
                    listaRetorno = operacaoFuturoRepositorio.Listar(DateTime.Now.FirstDayOfMonth(), DateTime.Now.Date.LastDayOfMonth());
                    break;
                case 4:
                    listaRetorno = operacaoFuturoRepositorio.Listar(DateTime.Now.FirstDayOfYear(), DateTime.Now.LastDayOfYear());
                    break;
                default:
                    listaRetorno = new List<OperacaoFuturoCryptoMoeda>();
                    break;
            }
            foreach (var operacaoFuturoCrypto in listaRetorno)
            {
                OperacaoDTO operacaoDTO = new()
                {
                    DataOperacao = operacaoFuturoCrypto.DataOperacaoFuturo.ToString("dd/MM/yyyy"),
                    NomeCryptoMoeda = cryptoMoedaRepositorio.Obter(operacaoFuturoCrypto.IdCryptoMoeda)?.NomeLongo,
                    ValorOperacao = operacaoFuturoCrypto.ValorTotal
                };

                operacoesDTO.Add(operacaoDTO);
            }
            ValorTotal = operacoesDTO.Sum(x => x.ValorOperacao);
            ValorTotalStr = "Total : " + ValorTotal.ToString();
        }
    }
}
