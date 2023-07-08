using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Util
{
    public interface IMessageService
    {
        Task ShowAsync(string message);
    }
    public class MessageService : IMessageService
    {
        public async Task ShowAsync(string message)
        {
            await App.Current.MainPage.DisplayAlert("De Zooi na Crypto", message, "Ok");
        }
    }
}
