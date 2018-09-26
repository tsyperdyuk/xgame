using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
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
            var url = new Uri(new Uri(BaseUrl), "api/user");
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            var response = new RestResponse();

            response = await GetResponseContentAsync(client, request) as RestResponse;
            
            var jsonResponse = JsonConvert.DeserializeObject<List<UserModel>>(response.Content);
            return jsonResponse;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }

}

