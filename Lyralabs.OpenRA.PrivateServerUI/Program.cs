using CommunityToolkit.Mvvm.Messaging;
using Lyralabs.OpenRA.PrivateServerUI.Services;

namespace Lyralabs.OpenRA.PrivateServerUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddSingleton<IMessenger, WeakReferenceMessenger>();
            builder.Services.AddSingleton<GameServerService>();
            builder.Services.AddSingleton(builder.Configuration.GetSection("Settings").Get<AppSettings>());
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHostedService(di => di.GetRequiredService<GameServerService>());

            var app = builder.Build();

            Program.FailFast(app.Services);

            if (app.Environment.IsDevelopment() == false)
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            await app.RunAsync();
        }

        private static void FailFast(IServiceProvider services)
        {
            var settings = services.GetRequiredService<AppSettings>();

            if (String.IsNullOrWhiteSpace(settings.LaunchScriptPath) == true)
            {
                throw new ApplicationException("can't start with missing LaunchScriptPath");
            }
        }
    }
}
