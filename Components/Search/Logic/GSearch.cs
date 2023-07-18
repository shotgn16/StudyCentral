using NuGet.Packaging.Licenses;

namespace StudyCentralV2.Components.Search.Logic
{
    public class GSearch
    {
        public IHttpClientFactory HttpClientFactory { get; set; }
        public GSearch() { }

        public GSearch(IHttpClientFactory httpClientFactory) => HttpClientFactory = httpClientFactory;

        public async Task<HttpResponseMessage> GetData(string searchQuery)
        {
            HttpResponseMessage httpResponseMessage;
            using (var httpClient = HttpClientFactory.CreateClient())
            {
                httpResponseMessage = await httpClient.GetAsync("https://www.googleapis.com/customsearch/v1?key=AIzaSyBLG5xkKnERtm_qgSlcfHWFYN2ih_Delxs&cx=b546ccbc83d2e40a4:omuauf_lfve&q=" + searchQuery);
            }

            httpResponseMessage.EnsureSuccessStatusCode();

            return httpResponseMessage;
        }
    }
}
