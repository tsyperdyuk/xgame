using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Xgame.Model;
using Xgame.WebApi.Client.Providers.Interfaces;

namespace Xgame.WebApi.Client.Providers
{
    public class UserProvider : ProviderBase, IUserProvider
    {
        public UserProvider(string baseUrl) : base(baseUrl)
        {
        }

        public virtual async Task<List<UserModel>> GetAllAsync()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(new Uri(BaseUrl), "api/user");
                
                var serializer = new DataContractJsonSerializer(typeof(List<UserModel>));
                var resultStream = await client.GetStreamAsync(url).ConfigureAwait(false);
                var result = serializer.ReadObject(resultStream) as List<UserModel>;

                return result;
               // return Json(result);
            }
        }
    }
}
