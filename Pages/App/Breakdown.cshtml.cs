using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StudyCentralV2.Components;
using StudyCentralV2.Components.Breakdown.Model;
using System.Text.RegularExpressions;

namespace StudyCentralV2.Pages.App
{
    public class BreakdownModel : PageModel
    {
        private readonly ILogger _logger;
        public BreakdownModel() => _logger = configureServices.Logger;

        public static List<PageCollection> RenderResponse;
        public static string Defination;
        public static string OriginalVerse;

        public async Task<IActionResult> OnPost(IFormCollection Collection)
        {
            List<string> Commentarys = new List<string>();

            RenderResponse = new List<PageCollection>();

            if (!string.IsNullOrEmpty(Collection["BreakdownSubject"]))
            {
                OriginalVerse = Collection["BreakdownSubject"];

                string Verse = ParseResponse(Collection["BreakdownSubject"]);
                string[] VerseIDs = await GetVerseID(Verse);

                foreach (var ID in VerseIDs)
                {
                    RenderResponse.Add(new PageCollection
                    {
                        Commenary = await GetCommentary(ID),
                        Verse = Verse
                    });
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDefine(IFormCollection Collection)
        {
            if (!string.IsNullOrEmpty(Collection["DefinationKeyword"]))
                Defination = await GetDefination(Collection["DefinationKeyword"]);

            return Page();
        }

        private string ParseResponse(string input)
        {
            string step1Output = Regex.Replace(input, @"\b(\w{3})\w*\b", "$1");
            string finalOutput = Regex.Replace(step1Output, @"(\d+:\d+-\d+)", "$1");
            finalOutput = finalOutput.Replace(' ', '.');

            return finalOutput;
        }

        private async Task<string[]> GetVerseID(string Verse)
        {
            VerseID verseID;

            using (HttpClient Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Add("X-RapidAPI-Key", configureServices._configuration["ConnectionSettings:X-RapidAPI-Key"]);
                Client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "iq-bible.p.rapidapi.com");

                using (var response = await Client.GetAsync($"https://iq-bible.p.rapidapi.com/GetParseCitation?citation={Verse}"))
                {
                    response.EnsureSuccessStatusCode();
                    verseID = JsonConvert.DeserializeObject<VerseID>(await response.Content.ReadAsStringAsync());
                }
            }

            return verseID.verseIds;
        }

        private async Task<string> GetCommentary(string VerseID)
        {
            string Commentary = null;

            using (HttpClient Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Add("X-RapidAPI-Key", configureServices._configuration["ConnectionSettings:X-RapidAPI-Key"]);
                Client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "iq-bible.p.rapidapi.com");

                using (var response = await Client.GetAsync($"https://iq-bible.p.rapidapi.com/GetCommentary?commentaryName=gills&verseId={VerseID}"))
                {
                    response.EnsureSuccessStatusCode();
                    Commentary = await response.Content.ReadAsStringAsync();
                }
            }

            return Commentary;
        }

        private async Task<string> GetDefination(string Keyword)
        {
            string Defination = null;

            using (HttpClient Client = new HttpClient())
            {
                Client.DefaultRequestHeaders.Add("X-RapidAPI-Key", configureServices._configuration["ConnectionSettings:X-RapidAPI-Key"]);
                Client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "iq-bible.p.rapidapi.com");

                using (var response = await Client.GetAsync($"https://iq-bible.p.rapidapi.com/GetDefinitionBiblical?query={Keyword}&dictionaryId=smiths"))
                {
                    response.EnsureSuccessStatusCode();
                    Defination = await response.Content.ReadAsStringAsync();
                }
            }

            return Defination;
        }
    }
}
