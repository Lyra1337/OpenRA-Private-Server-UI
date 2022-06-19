using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Lyralabs.OpenRA.PrivateServerUI.Services
{
    public class GameServerService
    {
        private readonly List<GameServerStartInfo> server = new();
        private readonly string launchScriptPath;

        public GameServerService(IConfiguration configuration)
        {
            this.launchScriptPath = configuration.GetValue<string>("LaunchScriptPath");
        }

        public void StartNewInstance(GameServerOptions options)
        {
            var info = new GameServerStartInfo
            {
                Options = options,
                StartedAt = DateTime.Now,
                StopAt = DateTime.Now.Add(options.RunDuration),
                Thread = new Thread(this.ServerKiller)
            };

            this.server.Add(info);
            info.Thread.Start(info);

            var psi = new ProcessStartInfo()
            {
                FileName = this.launchScriptPath
            };

            this.AddArguments(psi.ArgumentList, options);

            info.Process = Process.Start(psi)!;
        }

        private void AddArguments(Collection<string> arguments, GameServerOptions options)
        {
            var defaults = new GameServerOptions();

            options.GetType().GetProperties()
                .Where(x => x.CanRead == true)
                .Select(x => new
                {
                    Name = String.Concat("-", x.Name),
                    Value = this.ConvertArgument(x.GetValue(options)),
                    DefaultValue = this.ConvertArgument(x.GetValue(defaults))
                })
                .Where(x => x.Value != x.DefaultValue)
                .SelectMany(x => new[] { x.Name, x.Value })
                .ToList()
                .ForEach(arguments.Add);
        }

        private string ConvertArgument(object? value)
        {
            if (value is bool b)
            {
                return b == true ? Boolean.TrueString : Boolean.FalseString;
            }
            else
            {
                return value?.ToString() ?? String.Empty;
            }
        }

        private void ServerKiller(object? optionsObject)
        {
            if (optionsObject is GameServerStartInfo info)
            {
                var wait = info.StopAt - DateTime.Now;
                if (wait.TotalSeconds > 1)
                {
                    Thread.Sleep(wait);
                }

                info.Process.Kill(entireProcessTree: true);
            }
        }
    }
}
