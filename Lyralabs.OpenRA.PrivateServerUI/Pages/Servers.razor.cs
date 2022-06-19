using Lyralabs.OpenRA.PrivateServerUI.Services;

namespace Lyralabs.OpenRA.PrivateServerUI.Pages
{
    public class ServersViewModel : ComponentBase
    {
        [Inject]
        public GameServerService GameServerService { get; set; }

        public void CreateServer()
        {
            
        }
    }
}
