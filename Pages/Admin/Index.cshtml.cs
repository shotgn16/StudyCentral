using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudyCentralV2.Data;
using StudyCentralV2.Pages.App;

namespace StudyCentralV2.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public static string[] LogEntries { get; private set; }

        public void OnGet()
        {
            LogEntries = System.IO.File.ReadAllLines("logs\\log-AspNetCore-all.log");
        }

        public static async Task<int> UserCount(int Users = 0)
        {
            var options = new DbContextOptions<ApplicationDbContext>();

            using (ApplicationDbContext Context = new ApplicationDbContext(options))
                Users = Context.Users.Count();

            return Users;
        }


    }
}
