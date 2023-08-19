using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyCentralV2.Components;
using StudyCentralV2.Components.AI.Logic;
using StudyCentralV2.Components.FileManager.Logic;

namespace StudyCentralV2.Pages.App
{
    public class CreatorModel : PageModel
    {
        private readonly ILogger _logger;
        public CreatorModel() => _logger = configureServices.Logger;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            string fileContent;
            IActionResult result = Page();

            if (!string.IsNullOrEmpty(Request.Form["sTopic"]))
            {
                using (var AIRequest = new GPT())
                {
                    fileContent = await AIRequest.GPTRequest($"Create a bible study on the subject of {Request.Form["sTopic"]}.");
                }

                using (FileBuilder builder = new FileBuilder())
                {
                    using (FileBuilder fileBuilder = new FileBuilder())
                    {
                        result = await fileBuilder.CreateDownloadFile("BibleStudy.txt", fileContent, "text/plain");
                    }
                }
            }

            return result;
        }
    }
}
