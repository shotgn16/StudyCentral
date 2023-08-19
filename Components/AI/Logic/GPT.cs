using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using StudyCentralV2.Components.AI.Model;
using StudyCentralV2.Pages.App;

namespace StudyCentralV2.Components.AI.Logic
{
    public class GPT : IDisposable
    {
        private readonly ILogger _logger;
        public GPT() => _logger = configureServices.Logger;

        public async Task<string> GPTRequest(string prompt)
        {
            GPTModel.dataModel _GPTModel;

            using (var Client = new System.Net.Http.HttpClient())
            {
                HttpContent Content = new StringContent(await buildData(prompt), Encoding.UTF8, "application/json");
                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", configureServices._configuration["ConnectionSettings:OpenAIKey"]);

                var Response = Client.PostAsync("https://api.openai.com/v1/completions", Content);

                _GPTModel = JsonConvert.DeserializeObject<GPTModel.dataModel>(await Response.Result.Content.ReadAsStringAsync());
            }

            return _GPTModel.choices[0].text;
        }

        private static async Task<string> buildData(string prompt)
        {
            var JsonArray = new
            {
                model = "text-davinci-003",
                max_tokens = 4000,
                prompt =
                prompt,
                temperature = 0.85
            };

            return JsonConvert.SerializeObject(JsonArray);
        }

        public void Dispose() => GC.Collect();
    }
}
