using Lyralabs.OpenRA.PrivateServerUI.Services;

namespace Lyralabs.OpenRA.PrivateServerUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (String.IsNullOrWhiteSpace(builder.Configuration.GetValue<string>("LaunchScriptPath")) == true)
            {
                throw new ApplicationException("can't start with missing LaunchScriptPath");
            }

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddSingleton<GameServerService>();

            var app = builder.Build();

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
    }
}
