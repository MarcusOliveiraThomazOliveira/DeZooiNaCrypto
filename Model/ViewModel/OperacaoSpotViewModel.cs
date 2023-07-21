using DevExpress.Pdf;
using DeZooiNaCrypto.Model.Entidade;
using DeZooiNaCrypto.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DeZooiNaCrypto.Model.ViewModel
{
    public class OperacaoSpotViewModel : ModelViewBase
    {
        readonly IMessageService _messageService;
        OperacaoSpotCryptoMoeda operacaoSpotCryptoMoeda;

        public OperacaoSpotCryptoMoeda OperacaoSpotCryptoMoeda { get { return operacaoSpotCryptoMoeda; } set { operacaoSpotCryptoMoeda = value; RaisePropertyChanged(); } }
        public ICommand Gravar { get; private set; }
        public OperacaoSpotViewModel()
        {
            _messageService = new MessageService();
            operacaoSpotCryptoMoeda = new OperacaoSpotCryptoMoeda();

            Gravar = new Command(() =>
            {
                GravarSpot();
            });
        }

        public void GravarSpot()
        {
            if (OperacaoSpotCryptoMoeda == null)
            {
                return;
            }
        }
    }
}
