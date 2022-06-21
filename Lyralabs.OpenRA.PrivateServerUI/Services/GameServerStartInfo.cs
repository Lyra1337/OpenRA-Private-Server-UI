using System.Diagnostics;
using System.Text;

namespace Lyralabs.OpenRA.PrivateServerUI.Services
{
    public class GameServerStartInfo
    {
        public GameServerOptions Options { get; internal set; }
        public Thread Thread { get; internal set; }
        public DateTime StartedAt { get; internal set; }
        public DateTime StopAt { get; internal set; }
        public Process Process { get; internal set; }
        public StringBuilder ProcessOutput { get; } = new();
    }
}
