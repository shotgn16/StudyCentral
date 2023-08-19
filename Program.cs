using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Fluent;
using NLog.Targets;
using NLog.Web;
using StudyCentralV2.Data;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace StudyCentralV2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                var builder = WebApplication.CreateBuilder(args);
                //builder.Services.AddTransient<IEmailSender, EmailSender>();;

                builder.Host.UseNLog();

                // Add services to the container.
                var connectionString = "server=localhost;user id=SCDBUser;password=VQca75F00!RCBH7T;database=studycentral";
                builder.Services.AddDbContext<ApplicationDbContext>(options => options
                    .UseMySql(connectionString, new MySqlServerVersion(new Version(5, 2, 1))));

                builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();

                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                builder.Services.AddHttpClient();
                builder.Services.AddRazorPages();

                var app = builder.Build();

                app.UseHttpLogging();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthorization();

                app.MapRazorPages();

                app.Run();
            }

            catch (Exception ex)
            {
                throw;
            }

            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}