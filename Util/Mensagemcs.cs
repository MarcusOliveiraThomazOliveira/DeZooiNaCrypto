using DevExpress.Maui.Core.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Util
{
    public static class MessageService
    {
        public static async Task<bool> DisplayAlert_OK(string message)
        {
            var retorno = await App.Current.MainPage.DisplayActionSheet(message, null, Constantes.Caption_DisplayAlert_OK);

            return true;
        }
        public static async Task<bool> DisplayAlert_CONFIRMAR_CANCELAR(string message)
        {
            var retorno = await App.Current.MainPage.DisplayActionSheet(message, Constantes.Caption_DisplayAlert_Cancelar, Constantes.Caption_DisplayAlert_Confirmar);

            return retorno == Constantes.Caption_DisplayAlert_Confirmar;
        }
    }
}
