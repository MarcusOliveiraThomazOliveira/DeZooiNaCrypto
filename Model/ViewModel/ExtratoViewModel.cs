using DevExpress.Maui.Core.Internal;
using DevExpress.Pdf;
using DevExpress.XtraPrinting.Native;
using DeZooiNaCrypto.Data;
using DeZooiNaCrypto.Model.DTO;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Model.Enumerador;
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
        OperacaoFuturoRepositorio operacaoFuturoRepositorio = new();
        CryptoMoedaRepositorio cryptoMoedaRepositorio = new();
        ObservableCollection<OperacaoDTO> operacoesDTO;
        int filtrarPeriodo;
        decimal valorTotal;
        string valorTotalStr;
        string quantidadeOperacoes;
        string quantidadeOperacoesPositivasNegativas;

        DateTime? dataInicialFiltro;
        DateTime? dataFinalFiltro;

        public ObservableCollection<OperacaoDTO> OperacoesDTO { get { return operacoesDTO; } set { operacoesDTO = value; RaisePropertyChanged(); } }
        public OperacaoDTO OperacaoDTO { get; set; }
        public decimal ValorTotal { get { return valorTotal; } set { valorTotal = value; RaisePropertyChanged(); } }
        public string ValorTotalStr { get { return valorTotalStr; } set { valorTotalStr = value; RaisePropertyChanged(); } }
        public string QuantidadeOperacoes { get { return quantidadeOperacoes; } set { quantidadeOperacoes = value; RaisePropertyChanged(); } }
        public string QuantidadeOperacoesPositivasNegativas { get { return quantidadeOperacoesPositivasNegativas; } set { quantidadeOperacoesPositivasNegativas = value; RaisePropertyChanged(); } }

        public DateTime? DataInicialFiltro { get { return dataInicialFiltro; } set { dataInicialFiltro = value; RaisePropertyChanged(); } }
        public DateTime? DataFinalFiltro { get { return dataFinalFiltro; } set { dataFinalFiltro = value; RaisePropertyChanged(); } }

        public int Filtrar
        {
            get { return filtrarPeriodo; }
            set { filtrarPeriodo = value; FiltrarPeriodo(value); }
        }
        public ExtratoViewModel()
        {
            OperacoesDTO = new();
            FiltrarPeriodo(0);
        }
        public async Task<bool> FiltrarPeriodo(int tipoFiltro)
        {
            var retorno = true;

            if (!tipoFiltro.Equals(4))
            {
                for (int i = operacoesDTO.Count - 1; i >= 0; i--)
                {
                    operacoesDTO.Remove(operacoesDTO[i]);
                }

                ObservableCollection<OperacaoFuturoCryptoMoeda> listaRetorno;
                switch (tipoFiltro)
                {
                    case (int)TipoFiltroExtratoEnum.Hoje:
                        listaRetorno = new ObservableCollection<OperacaoFuturoCryptoMoeda>(operacaoFuturoRepositorio.Listar(DateTime.Now.Date, DateTime.Now.Date));
                        break;
                    case (int)TipoFiltroExtratoEnum.Semana:
                        listaRetorno = new ObservableCollection<OperacaoFuturoCryptoMoeda>(operacaoFuturoRepositorio.Listar(DateTime.Now.FirstDayOfWeek(), DateTime.Now.LastDayOfWeek()));
                        break;
                    case (int)TipoFiltroExtratoEnum.Mes:
                        listaRetorno = new ObservableCollection<OperacaoFuturoCryptoMoeda>(operacaoFuturoRepositorio.Listar(DateTime.Now.FirstDayOfMonth(), DateTime.Now.Date.LastDayOfMonth()));
                        break;
                    case (int)TipoFiltroExtratoEnum.Ano:
                        listaRetorno = new ObservableCollection<OperacaoFuturoCryptoMoeda>(operacaoFuturoRepositorio.Listar(DateTime.Now.FirstDayOfYear(), DateTime.Now.LastDayOfYear()));
                        break;
                    case (int)TipoFiltroExtratoEnum.FiltroPersonalizado:
                        if (DataInicialFiltro.IsNullOrMinMaxDate() || DataFinalFiltro.IsNullOrMinMaxDate())
                        {
                            await Util.MessageService.DisplayAlert_OK("Informe a data inicial e final.");
                            return false;
                        }
                        
                        listaRetorno = new ObservableCollection<OperacaoFuturoCryptoMoeda>(operacaoFuturoRepositorio.Listar(DataInicialFiltro.Value, DataFinalFiltro.Value));
                        DataInicialFiltro = null;
                        DataFinalFiltro = null;
                        break;
                    case 4:
                    default:
                        listaRetorno = new ObservableCollection<OperacaoFuturoCryptoMoeda>();
                        break;
                }
                foreach (var operacaoFuturoCrypto in listaRetorno)
                {
                    if (operacaoFuturoCrypto != null)
                    {
                        OperacaoDTO operacaoDTO = new()
                        {
                            DataOperacao = operacaoFuturoCrypto?.DataInicialOperacaoFuturo.ToString("dd/MM/yyyy") + " (" + (operacaoFuturoCrypto.DataFinalOperacaoFuturo.HasValue ? Constantes.Operacao_Fechada : Constantes.Operacao_Em_Andamento) + ")",
                            NomeCryptoMoeda = cryptoMoedaRepositorio.Obter(operacaoFuturoCrypto.IdCryptoMoeda)?.NomeLongo,
                            ValorOperacao = operacaoFuturoCrypto.ValorTotal
                        };
                        operacoesDTO.Add(operacaoDTO);
                    }
                }
                ValorTotal = operacoesDTO.Sum(x => x.ValorOperacao);
                ValorTotalStr = "Total : " + ValorTotal;
                QuantidadeOperacoes = "Operações : " + operacoesDTO.Count();
                QuantidadeOperacoesPositivasNegativas = "Positivas : " + operacoesDTO.Where(x => x.ValorOperacao > 0).Count() +
                    " / Negativas : " + operacoesDTO.Where(x => x.ValorOperacao < 0).Count();

            }
            return retorno;
        }
    }
}
