using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Lyralabs.OpenRA.PrivateServerUI.Services
{
    public class GameServerService
    {
        private readonly List<GameServerStartInfo> servers = new();
        private readonly string launchScriptPath;
        private readonly string linuxUser;
        private readonly string launchScriptDirectory;

        public GameServerService(IConfiguration configuration)
        {
            this.launchScriptPath = configuration.GetValue<string>("LaunchScriptPath");
            this.linuxUser = configuration.GetValue<string>("User");
            this.launchScriptDirectory = new FileInfo(this.launchScriptPath).Directory.FullName;
        }

        public List<GameServerOptions> GetServers()
        {
            return this.servers
                .Where(x => x.StopAt < DateTime.Now)
                .Select(x => x.Options)
                .ToList();
        }

        public int StartNewInstance(GameServerOptions options)
        {
            options.ListenPort = this.GetFreePort();

            var info = new GameServerStartInfo
            {
                Options = options,
                StartedAt = DateTime.Now,
                StopAt = DateTime.Now.Add(options.RunDuration),
                Thread = new Thread(this.ServerKiller)
            };

            this.servers.Add(info);
            info.Thread.Start(info);

            var psi = new ProcessStartInfo()
            {
                FileName = this.launchScriptPath,
                WorkingDirectory = this.launchScriptDirectory,
                UserName = this.linuxUser
            };

            this.SetEnvironmentVariables(psi.Environment, options);

            Debug.WriteLine($"launching: {psi.FileName} with Environment {String.Join(" ", psi.Environment.Select(x => String.Concat(x.Key, "=", x.Value)))}");

            info.Process = Process.Start(psi);

            return options.ListenPort;
        }

        private int GetFreePort()
        {
            var port = 11100 + this.servers.Count;

            while (this.servers.Any(x => x.Options.ListenPort == port) == true)
            {
                port++;
            }

            return port;
        }

        private void SetEnvironmentVariables(IDictionary<string, string> environment, GameServerOptions options)
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
                .ToList()
                .ForEach(x => environment[x.Name] = x.Value);
        }

        private string ConvertArgument(object value)
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

        private void ServerKiller(object optionsObject)
        {
            if (optionsObject is GameServerStartInfo info)
            {
                var wait = info.StopAt - DateTime.Now;
                if (wait.TotalSeconds > 1)
                {
                    Thread.Sleep(wait);
                }

                info.Process.Kill(entireProcessTree: true);
                this.servers.Remove(info);
            }
        }
    }
}
