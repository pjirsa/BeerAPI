using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BeerAppMVC.Controllers
{
    public abstract class BaseController : Controller
    {
        protected async Task<string> GetClientResponseAsync(string endpoint, string token, string subscriptionId = "")
        {

            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(Startup.BeerAPIBaseUri) })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionId);
                HttpResponseMessage response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    throw new Exception("Client call failed.", new Exception(response.ReasonPhrase));
                }
            }
        }

        protected async Task<string> PostClientReponseAsync(string endpoint, string token, string content, string subscriptionId = "")
        {
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(Startup.BeerAPIBaseUri) })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionId);
                var postbody = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(endpoint, postbody);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Client call failed.", new Exception(response.ReasonPhrase));
                }
            }
        }

        protected async Task<string> PutClientResponseAsync(string endpoint, string token, string content, string subscriptionId = "")
        {
            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(Startup.BeerAPIBaseUri) })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionId);
                var postbody = new StringContent(content, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(endpoint, postbody);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Client call failed.", new Exception(response.ReasonPhrase));
                }
            }
        }

        protected async Task<string> DeleteClientResponseAsync(string endpoint, string token, string subscriptionId = "")
        {

            using (HttpClient client = new HttpClient() { BaseAddress = new Uri(Startup.BeerAPIBaseUri) })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionId);
                HttpResponseMessage response = await client.DeleteAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    throw new Exception("Client call failed.", new Exception(response.ReasonPhrase));
                }
            }
        }

        private HttpClient GetClient()
        {
            return new HttpClient();
        }
    }
}
