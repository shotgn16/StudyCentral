using Microsoft.AspNetCore.Mvc;
using StudyCentralV2.Pages.App;

namespace StudyCentralV2.Components.FileManager.Logic
{
    public class FileBuilder : IDisposable
    {
        private readonly ILogger _logger;
        public FileBuilder() => _logger = configureServices.Logger;

        public async Task<IActionResult> CreateDownloadFile(string fileName, string content, string contentType)
        {
            IActionResult actionResult = null;

            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(content))
            {
                fileName = fileName + ".txt";

                File.WriteAllText(fileName, content);

                actionResult = new FileStreamResult(new FileStream(fileName, FileMode.Open), contentType)
                {
                    FileDownloadName = fileName
                };
            }

            else if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(content))
            {
                //Error
            }

            return actionResult;
        }

        public void Dispose() => GC.Collect();
    }
}
