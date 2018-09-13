using System;
using System.Collections.Generic;
using System.Text;

namespace Xgame.WebApi.Client.Providers
{
    public abstract class ProviderBase
    {
        protected readonly string BaseUrl;

        protected ProviderBase(string baseUrl)
        {
            BaseUrl = baseUrl;
        }
    }
}
