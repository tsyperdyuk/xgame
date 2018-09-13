using System;
using System.Collections.Generic;
using System.Text;
using Xgame.WebApi.Client.Providers;
using Xgame.WebApi.Client.Providers.Interfaces;

namespace Xgame.WebApi.Client
{
    public class ApiClient : IApiClient
    {
        private readonly string _baseUrl;

        public ApiClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public IUserProvider User => new UserProvider(_baseUrl);
    }
}
