using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using StudyCentralV2.Components;
using StudyCentralV2.Components.VerseQuery.Model;

namespace StudyCentralV2.Pages.App
{
    public class VerseModel : PageModel, IDisposable
    {
        private readonly ILogger<VerseModel> _logger;
        public VerseModel()
        {
            using (var factory = LoggerFactory.Create(options => { options.AddNLog(); options.AddConsole(); }))
            {
                _logger = factory.CreateLogger<VerseModel>();
            }
        }

        public static string SearchQuery;
        public static string BibleID;
        public static int NumberOfVerses;
        public static List<Bibles> ListOfBibles;
        public static List<Verses.Displayed> ListOfVerses;

        public static async Task<List<Bibles>> GetBibles()
        {
            ListOfBibles = new List<Bibles>();

            using (HttpClient Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Add("api-key", configureServices._configuration["ConnectionSettings:BibleKey"]);

                var result = await Client.GetAsync("https://api.scripture.api.bible/v1/bibles");
                Bibles Bibles = JsonConvert.DeserializeObject<Bibles>(await result.Content.ReadAsStringAsync());

                foreach (var Item in Bibles)
                    ListOfBibles.Add(Bibles);
            }

            return ListOfBibles;
        }

        public async Task<IActionResult> OnPost()
        {
            BibleID = Request.Form["Bible"];
            SearchQuery = Request.Form["Keyword"];
            NumberOfVerses = Convert.ToInt32(Request.Form["NumVerses"]);
            //TranslatePhrase = Convert.ToBoolean(Request.Form["TranslateInput"]);

            if (NumberOfVerses == 0)
                NumberOfVerses = int.MaxValue;

            foreach (var item in ListOfBibles)
            {
                if (item.data[Components.Iterator.Logic.Iterator.Value].name == BibleID)
                {
                    BibleID = item.data[Components.Iterator.Logic.Iterator.Value].id;
                    break;
                }
                Components.Iterator.Logic.Iterator.Iterate();
            }

            Components.Iterator.Logic.Iterator.Increment = 0;
            Components.Iterator.Logic.Iterator.Value = 0;

            return Page();
        }

        public static async Task<List<Verses.Displayed>> GetVerses()
        {
            ListOfVerses = new List<Verses.Displayed>();

            using (HttpClient Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Add("api-key", configureServices._configuration["ConnectionSettings:BibleKey"]);
                var result = await Client.GetAsync($"https://api.scripture.api.bible/v1/bibles/{BibleID}/search?query={SearchQuery}&limit={NumberOfVerses}&sort=relevance");

                using (Verses.Root Verse = JsonConvert.DeserializeObject<Verses.Root>(result.Content.ReadAsStringAsync().Result))
                {
                    foreach (var item in Verse) ListOfVerses.Add(new Verses.Displayed { reference = item.reference, text = item.text });
                }
            }

            return ListOfVerses;
        }

        public static async Task SaveQuery(List<Verses.Displayed> Verses)
        {
            string Content = Verses.ToString();
        }

        public void Dispose() => GC.Collect();
    }
}
