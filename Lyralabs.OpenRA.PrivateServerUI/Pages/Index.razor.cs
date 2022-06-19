using Lyralabs.OpenRA.PrivateServerUI.Services;

namespace Lyralabs.OpenRA.PrivateServerUI.Pages
{
    public class IndexViewModel : ComponentBase
    {
        public GameServerUserConfiguration Model { get; set; } = null!;

        [Inject]
        public GameServerService GameServerService { get; set; } = null!;

        public void CreateServer()
        {
            
        }
    }
}
