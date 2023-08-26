using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeZooiNaCrypto.Util
{
    public static class Load
    {
        public static void Loading(BoxView bvCarregando, ActivityIndicator aiCarregando)
        {
            bvCarregando.IsVisible = !bvCarregando.IsVisible;

            aiCarregando.IsVisible = !aiCarregando.IsVisible;
            aiCarregando.IsRunning = !aiCarregando.IsRunning;
        }
    }
}
