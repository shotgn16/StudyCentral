using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudyCentralV2.Components.EmailSender;
using StudyCentralV2.Components;

namespace StudyCentralV2.Pages.App
{
    public class AskModel : PageModel
    {
        private readonly ILogger _logger;
        public AskModel() => _logger = configureServices.Logger;

        public void OnGet()
        {
        }
    }
}
