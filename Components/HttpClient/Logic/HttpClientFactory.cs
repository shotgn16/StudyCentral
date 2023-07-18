using System.Net.Http.Headers;
using System.Net.Http;

namespace StudyCentralV2.Components.HttpClient.Logic
{
    public class HttpClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
