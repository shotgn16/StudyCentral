using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NLog.Extensions.Logging;
using NLog.Web;
using StudyCentralV2.Components;
using StudyCentralV2.Pages.App;

namespace StudyCentralV2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel()
        {
            using (var factory = LoggerFactory.Create(options => options.AddNLog()))
            {
                _logger = factory.CreateLogger<IndexModel>();
            }
        }

        public void OnGet()
        {

        }
    }
}